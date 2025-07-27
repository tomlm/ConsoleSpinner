namespace Spinner
{

    public static class ConsoleEx
    {
        /// <summary>
        /// Start a spinner scoped to a task with animation
        /// </summary>
        /// <param name="task"></param>
        /// <param name="animation"></param>
        /// <returns></returns>
        public static ConsoleSpinner StartSpinner(Task task, string[] animation, string success = "✓", string failed = "X")
        {
            return StartSpinner(task, new SpinnerOptions()
            {
                Animation = animation,
                Success = success,
                Failed = failed 
            });
        }

        /// <summary>
        /// Start a spinner scoped to a using block
        /// </summary>
        /// <param name="animation"></param>
        /// <returns></returns>
        public static ConsoleSpinner StartSpinner(string[] animation, string success = "✓", string failed = "X")
        {
            return StartSpinner(new SpinnerOptions()
            {
                Animation = animation,
                Success = success,
                Failed = failed
            });
        }

        /// <summary>
        /// Start a spinner scoped to a task
        /// </summary>
        /// <param name="task">task to tie the spinner status to</param>
        /// <param name="options">options</param>
        /// <returns></returns>
        public static ConsoleSpinner StartSpinner(Task task, SpinnerOptions? options = null)
        {
            return new ConsoleSpinner(task: task, options: options);
        }

        /// <summary>
        /// Start a spinner scoped to the IDisposable pattern
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ConsoleSpinner StartSpinner(SpinnerOptions? options = null)
        {
            return new ConsoleSpinner(options: options);
        }


    }
}
