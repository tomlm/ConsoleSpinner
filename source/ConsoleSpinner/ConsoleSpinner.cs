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
        private string[] _animation;
        private string _success;
        private string _failed;
        private Task _spinnerTask;
        private Task _task;
        private Func<string, bool, string> _customFrame;


        public ConsoleSpinner(string[] animation = null, Task task = null, Func<string, bool, string> customFrame = null, string success = null, string failed = null)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            _cancelationTokenSource = new CancellationTokenSource();
            _customFrame = customFrame;
            _animation = animation ?? Animation.Lines;
            if (!_animation.All(s => s.Length == _animation[0].Length))
                throw new Exception("All animation frames must be the same length");

            this._success = success ?? "✓";
            this._failed = failed ?? "X";
            _frame = 0;

            this._task = (task == null) ? Task.Delay(int.MaxValue) : task;

            this._spinnerTask = Spinner();

            _ = this._task.ContinueWith(async t =>
            {
                // we wait for the spinner task to finish before writing the final character
                await _spinnerTask;
            });
        }

        /// <summary>
        /// Delay between each spin in milliseconds (default 100)
        /// </summary>
        public int Delay { get; set; } = 100;

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
                var frame = _animation[_frame % _animation.Length];
                var text = (_customFrame != null) ? _customFrame(frame, false) : frame;
                Console.Write(text);
            }

            while (_cancelationTokenSource.IsCancellationRequested == false && !_task.IsCompleted && !_task.IsFaulted && !_task.IsCanceled)
            {
                lock (Console.Out)
                {
                    _frame++;
                    // capture position
                    var (origLeft, origTop) = Console.GetCursorPosition();

                    Console.SetCursorPosition(_left, _top);
                    var frame = _animation[_frame % _animation.Length];
                    var text = (_customFrame != null) ? _customFrame(frame, false) : frame;
                    Console.Write(text);

                    // restore position
                    Console.SetCursorPosition(origLeft, origTop);
                }

                await Task.Delay(Delay, _cancelationTokenSource.Token);
            }

            Finished(_task);
        }

        private void Finished(Task task)
        {
            lock (Console.Out)
            {
                var (origLeft, origTop) = Console.GetCursorPosition();

                Console.SetCursorPosition(_left, _top);

                string frame = null;
                if (task.IsCompletedSuccessfully)
                    frame = $"{_success}".PadRight(_animation[0].Length, ' ');
                else
                    frame = $"{_failed}".PadRight(_animation[0].Length, ' ');

                var text = (_customFrame != null) ? _customFrame(frame, false) : frame;

                Console.Write(text);
                Console.SetCursorPosition(origLeft, origTop);
            }
        }
    }
}
