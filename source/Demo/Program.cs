using Spinner;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Demo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hit any key to start the demo...");
            Console.ReadKey();

            Console.WriteLine("==== Demo of Spinner Styles with task ====");
            List<Task> tasks = new List<Task>();

            Random rnd = new Random();

            // show each animation on our Common Animation class
            int c = 0;
            foreach (var field in typeof(Animation).GetFields(BindingFlags.Static|BindingFlags.Public))
            {
                var animation = (String[])field.GetValue(null);
                var delay = rnd.Next(15000, 20000);
                var task = Task.Delay(delay);
                ConsoleEx.WriteSpinner(animation, task, (frame, done) => $" {field.Name} {frame}");
                lock (Console.Out) Console.WriteLine();
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            Console.WriteLine();
            Console.WriteLine("==== Demo of inline Spinner no custom frame====");

            var pos = Console.GetCursorPosition();
            int i = 5;
            Console.WriteLine("This will take 5 seconds...");
            using (var _ = ConsoleEx.WriteSpinner())
            {
                for (; i > 0; i--)
                {
                    lock(Console.Out)
                    {
                        Console.Write($"{i} ");
                    }
                    // simulate doing stuff...
                    await Task.Delay(1000);
                }
            }
            Console.WriteLine();

            Console.WriteLine("==== Demo of inline Spinner with custom frame====");

            pos = Console.GetCursorPosition();
            i = 10;
            using (var _ = ConsoleEx.WriteSpinner(Animation.Arcs, customFrame: (frame, done) => $"{frame} Counter: {i} "))
            {
                for (; i > 0; i--)
                {
                    // simulate doing stuff...
                    await Task.Delay(1000);
                }
            }
            Console.SetCursorPosition(0, pos.Top);
            Console.WriteLine();
            Console.WriteLine("==== Done =====");
        }
    }
}