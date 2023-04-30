using System.Runtime.Versioning;

namespace Spinner
{

    public static class ConsoleEx
    {
        /// <summary>
        /// Write out a spinner to the console. 
        /// </summary>
        /// <param name="style">style of the spinner</param>
        /// <param name="task">task to tie the spinner status to</param>
        /// <param name="customFrame">custom function for the frame.  Example: (frame, done) => $"{frame} progress"</param>
        /// <returns></returns>
        public static ConsoleSpinner WriteSpinner(SpinnerStyle style=SpinnerStyle.Lines, Task task = null, Func<string, bool, string> customFrame = null, string success = null, string failed = null)
        {
            return new ConsoleSpinner(style, task, customFrame, success, failed);
        }
    }
}
