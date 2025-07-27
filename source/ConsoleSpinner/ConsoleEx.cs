using System.Runtime.Versioning;
using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;

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

        // Console.Write wrappers
        public static void Write(string value)
        {
            lock (Console.Out)
            {
                Console.Write(value);
            }
        }
        public static void Write(string format, params object[] args)
        {
            lock (Console.Out)
            {
                Console.Write(format, args);
            }
        }
        public static void Write(char value)
        {
            lock (Console.Out)
            {
                Console.Write(value);
            }
        }
        public static void Write(char[] buffer)
        {
            lock (Console.Out)
            {
                Console.Write(buffer);
            }
        }
        public static void Write(char[] buffer, int index, int count)
        {
            lock (Console.Out)
            {
                Console.Write(buffer, index, count);
            }
        }
        public static void Write(bool value)
        {
            lock (Console.Out)
            {
                Console.Write(value);
            }
        }
        public static void Write(int value)
        {
            lock (Console.Out)
            {
                Console.Write(value);
            }
        }
        public static void Write(uint value)
        {
            lock (Console.Out)
            {
                Console.Write(value);
            }
        }
        public static void Write(long value)
        {
            lock (Console.Out)
            {
                Console.Write(value);
            }
        }
        public static void Write(ulong value)
        {
            lock (Console.Out)
            {
                Console.Write(value);
            }
        }
        public static void Write(float value)
        {
            lock (Console.Out)
            {
                Console.Write(value);
            }
        }
        public static void Write(double value)
        {
            lock (Console.Out)
            {
                Console.Write(value);
            }
        }
        public static void Write(decimal value)
        {
            lock (Console.Out)
            {
                Console.Write(value);
            }
        }
        public static void Write(object value)
        {
            lock (Console.Out)
            {
                Console.Write(value);
            }
        }

        // Console.WriteLine wrappers
        public static void WriteLine()
        {
            lock (Console.Out)
            {
                Console.WriteLine();
            }
        }
        public static void WriteLine(string value)
        {
            lock (Console.Out)
            {
                Console.WriteLine(value);
            }
        }
        public static void WriteLine(string format, params object[] args)
        {
            lock (Console.Out)
            {
                Console.WriteLine(format, args);
            }
        }
        public static void WriteLine(char value)
        {
            lock (Console.Out)
            {
                Console.WriteLine(value);
            }
        }
        public static void WriteLine(char[] buffer)
        {
            lock (Console.Out)
            {
                Console.WriteLine(buffer);
            }
        }
        public static void WriteLine(char[] buffer, int index, int count)
        {
            lock (Console.Out)
            {
                Console.WriteLine(buffer, index, count);
            }
        }
        public static void WriteLine(bool value)
        {
            lock (Console.Out)
            {
                Console.WriteLine(value);
            }
        }
        public static void WriteLine(int value)
        {
            lock (Console.Out)
            {
                Console.WriteLine(value);
            }
        }
        public static void WriteLine(uint value)
        {
            lock (Console.Out)
            {
                Console.WriteLine(value);
            }
        }
        public static void WriteLine(long value)
        {
            lock (Console.Out)
            {
                Console.WriteLine(value);
            }
        }
        public static void WriteLine(ulong value)
        {
            lock (Console.Out)
            {
                Console.WriteLine(value);
            }
        }
        public static void WriteLine(float value)
        {
            lock (Console.Out)
            {
                Console.WriteLine(value);
            }
        }
        public static void WriteLine(double value)
        {
            lock (Console.Out)
            {
                Console.WriteLine(value);
            }
        }
        public static void WriteLine(decimal value)
        {
            lock (Console.Out)
            {
                Console.WriteLine(value);
            }
        }
        public static void WriteLine(object value)
        {
            lock (Console.Out)
            {
                Console.WriteLine(value);
            }
        }

    }
}
