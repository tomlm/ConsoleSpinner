using System.Runtime.Versioning;

namespace Spinner
{

    public static class ConsoleEx
    {
        /// <summary>
        /// Write out a spinner to the console. 
        /// </summary>
        /// <param name="animation">collection of animation frames all of the same width</param>
        /// <param name="task">task to tie the spinner status to</param>
        /// <param name="customFrame">custom function for the frame.  Example: (frame, done) => $"{frame} progress"</param>
        /// <returns></returns>
        public static ConsoleSpinner WriteSpinner(Task task, SpinnerOptions? options = null)
        {
            return new ConsoleSpinner(task: task, options: options);
        }

        public static ConsoleSpinner WriteSpinner(SpinnerOptions? options = null)
        {
            return new ConsoleSpinner(options: options);
        }
    }
}
