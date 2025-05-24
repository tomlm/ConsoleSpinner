using System.Drawing;
using System.IO;
using System.Threading.Tasks;

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
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            _cancelationTokenSource = new CancellationTokenSource();
            Options = options ?? new SpinnerOptions();
            if (options.Theme == null)
            {
                Options.Theme = new ConsoleColor[] { Console.ForegroundColor };
            }

            if (!Options.Animation.All(s => s.Length == Options.Animation[0].Length))
                throw new Exception("All animation frames must be the same length");

            _frame = 0;

            this._task = (task == null) ? Task.Delay(int.MaxValue) : task;

            this._spinnerTask = Spinner();

            _ = this._task.ContinueWith(async t =>
            {
                // we wait for the spinner task to finish before writing the final character
                await _spinnerTask;
            });
        }

        public SpinnerOptions Options { get; set; }
        /// <summary>
        /// Stop the spinner
        /// </summary>
        public void Dispose()
        {
            this._cancelationTokenSource.Cancel();
            Finished(Task.CompletedTask);
        }

        private async Task Spinner()
        {
            lock (Console.Out)
            {
                _left = Console.CursorLeft;
                _top = Console.CursorTop;
                var frame = Options.Animation[_frame % Options.Animation.Length];
                var text = (Options.CustomFrame != null) ? Options.CustomFrame(frame, false) : frame;
                Console.Write(text);
            }

            while (_cancelationTokenSource.IsCancellationRequested == false && !_task.IsCompleted && !_task.IsFaulted && !_task.IsCanceled)
            {
                lock (Console.Out)
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
                }

                if (Options.Delay <= 0)
                    // we need to yield to allow the console to update
                    await Task.Delay(1);
                else 
                    await Task.Delay(Options.Delay, _cancelationTokenSource.Token);
            }

            Finished(_task);
        }

        private void Finished(Task task)
        {
            lock (Console.Out)
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
}
