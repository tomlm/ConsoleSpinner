using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace Spinner
{
    public class ConsoleSpinner : IDisposable
    {
        private CancellationTokenSource _cancelationTokenSource;
        private int _frame;
        private int _left;
        private int _top;
        private Task _spinnerTask;
        private Task _task;

        public ConsoleSpinner(Task task = null, SpinnerOptions? options = null)
        {
            _left = Console.CursorLeft;
            _top = Console.CursorTop;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            _cancelationTokenSource = new CancellationTokenSource();
            Options = options ?? new SpinnerOptions();
            if (Options.Theme == null)
            {
                Options.Theme = new ConsoleColor[] { Console.ForegroundColor };
            }

            if (!Options.Animation.All(s => s.Length == Options.Animation[0].Length))
                throw new Exception("All animation frames must be the same length");

            _frame = 0;

            task = task ?? Task.Delay(int.MaxValue);
            this._task = task.ContinueWith(async t =>
            {
                // we wait for the spinner task to finish 
                await _spinnerTask;
            });

            this._spinnerTask = Spinner();
        }

        public SpinnerOptions Options { get; set; }

        /// <summary>
        /// This is the async task which is running the animation, you can await this to know that it is no 
        /// longer manipulating the console output.
        /// </summary>
        public Task Task => _spinnerTask;

        /// <summary>
        /// Stop the spinner
        /// </summary>
        public void Dispose()
        {
            this._cancelationTokenSource.Cancel();
            Finished(Task.CompletedTask);
        }

        public void Stop()
        {
            this._cancelationTokenSource.Cancel();
            Finished(Task.CompletedTask);
        }

        private async Task Spinner()
        {
            while (_cancelationTokenSource.IsCancellationRequested == false && !_task.IsCompleted && !_task.IsFaulted && !_task.IsCanceled)
            {
                _frame++;
                // capture position and color 
                var (origLeft, origTop) = Console.GetCursorPosition();
                var origColor = Console.ForegroundColor;

                Console.ForegroundColor = Options.Theme[_frame % Options.Theme.Length];
                Console.SetCursorPosition(_left, _top);
                var frame = Options.Animation[_frame % Options.Animation.Length];
                var text = (Options.CustomFrame != null) ? Options.CustomFrame(frame, false) : frame;
                Console.Write(text);

                // restore position and color 
                Console.ForegroundColor = origColor;
                Console.SetCursorPosition(origLeft, origTop);
                Console.Out.Flush();

                if (Options.Delay <= 0)
                    // we need to yield to allow the console to update
                    await Task.Delay(5);
                else
                    await Task.Delay(Options.Delay, _cancelationTokenSource.Token);
            }

            Finished(_task);
        }

        private void Finished(Task task)
        {
            var (origLeft, origTop) = Console.GetCursorPosition();
            var origColor = Console.ForegroundColor;

            Console.SetCursorPosition(_left, _top);

            string frame = null;
            if (task.IsCompletedSuccessfully)
            {
                frame = $"{Options.Success}".PadRight(Options.Animation[0].Length, ' ');
                Console.ForegroundColor = Options.SuccessColor;
            }
            else
            {
                frame = $"{Options.Failed}".PadRight(Options.Animation[0].Length, ' ');
                Console.ForegroundColor = Options.FailedColor;
            }

            var text = (Options.CustomFrame != null) ? Options.CustomFrame(frame, false) : frame;

            Console.Write(text);

            Console.ForegroundColor = origColor;
            Console.SetCursorPosition(origLeft, origTop);
        }
    }
}
