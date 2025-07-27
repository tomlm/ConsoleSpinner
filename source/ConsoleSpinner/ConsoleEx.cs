using System.Runtime.Versioning;
using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace Spinner
{

    public static class ConsoleEx
    {
        // Correct initialization: 1 means one thread can enter, max 1.
        internal static readonly SemaphoreSlim _consoleSemaphore = new SemaphoreSlim(1, 1);

        public static void Lock()
        {
            _consoleSemaphore.Wait();
        }

        public static void Release()
        {
            _consoleSemaphore.Release();
        }

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

        // Console.Write wrappers
        public static void Write(string value)
        {
            _consoleSemaphore.Wait();
            try { Console.Write(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void Write(string format, params object[] args)
        {
            _consoleSemaphore.Wait();
            try { Console.Write(format, args); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void Write(char value)
        {
            _consoleSemaphore.Wait();
            try { Console.Write(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void Write(char[] buffer)
        {
            _consoleSemaphore.Wait();
            try { Console.Write(buffer); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void Write(char[] buffer, int index, int count)
        {
            _consoleSemaphore.Wait();
            try { Console.Write(buffer, index, count); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void Write(bool value)
        {
            _consoleSemaphore.Wait();
            try { Console.Write(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void Write(int value)
        {
            _consoleSemaphore.Wait();
            try { Console.Write(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void Write(uint value)
        {
            _consoleSemaphore.Wait();
            try { Console.Write(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void Write(long value)
        {
            _consoleSemaphore.Wait();
            try { Console.Write(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void Write(ulong value)
        {
            _consoleSemaphore.Wait();
            try { Console.Write(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void Write(float value)
        {
            _consoleSemaphore.Wait();
            try { Console.Write(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void Write(double value)
        {
            _consoleSemaphore.Wait();
            try { Console.Write(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void Write(decimal value)
        {
            _consoleSemaphore.Wait();
            try { Console.Write(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void Write(object value)
        {
            _consoleSemaphore.Wait();
            try { Console.Write(value); }
            finally { _consoleSemaphore.Release(); }
        }

        // Console.WriteLine wrappers
        public static void WriteLine()
        {
            _consoleSemaphore.Wait();
            try { Console.WriteLine(); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void WriteLine(string value)
        {
            _consoleSemaphore.Wait();
            try { Console.WriteLine(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void WriteLine(string format, params object[] args)
        {
            _consoleSemaphore.Wait();
            try { Console.WriteLine(format, args); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void WriteLine(char value)
        {
            _consoleSemaphore.Wait();
            try { Console.WriteLine(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void WriteLine(char[] buffer)
        {
            _consoleSemaphore.Wait();
            try { Console.WriteLine(buffer); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void WriteLine(char[] buffer, int index, int count)
        {
            _consoleSemaphore.Wait();
            try { Console.WriteLine(buffer, index, count); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void WriteLine(bool value)
        {
            _consoleSemaphore.Wait();
            try { Console.WriteLine(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void WriteLine(int value)
        {
            _consoleSemaphore.Wait();
            try { Console.WriteLine(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void WriteLine(uint value)
        {
            _consoleSemaphore.Wait();
            try { Console.WriteLine(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void WriteLine(long value)
        {
            _consoleSemaphore.Wait();
            try { Console.WriteLine(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void WriteLine(ulong value)
        {
            _consoleSemaphore.Wait();
            try { Console.WriteLine(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void WriteLine(float value)
        {
            _consoleSemaphore.Wait();
            try { Console.WriteLine(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void WriteLine(double value)
        {
            _consoleSemaphore.Wait();
            try { Console.WriteLine(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void WriteLine(decimal value)
        {
            _consoleSemaphore.Wait();
            try { Console.WriteLine(value); }
            finally { _consoleSemaphore.Release(); }
        }
        public static void WriteLine(object value)
        {
            _consoleSemaphore.Wait();
            try { Console.WriteLine(value); }
            finally { _consoleSemaphore.Release(); }
        }

    }
}
