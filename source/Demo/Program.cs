using Spinner;
using System.Reflection;

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
            var themes = typeof(Themes).GetFields(BindingFlags.Public | BindingFlags.Static).Take(5).Select(field => (ConsoleColor[])field.GetValue(null)).ToArray();

            int counter = 0;
            foreach (var field in typeof(Animations).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                var animation = (String[])field.GetValue(null);
                var delay = rnd.Next(8000, 12000);
                var task = Task.Delay(delay);

                lock (Console.Out)
                    Console.Write($"{field.Name} ");
                ConsoleEx.WriteSpinner(task, new SpinnerOptions() { Animation = animation });
                lock (Console.Out)
                    Console.WriteLine();
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);


            Console.WriteLine("All animations completed!");
            Console.WriteLine();
            Console.WriteLine("==== Demo of inline Spinner no custom frame====");

            int i = 5;
            var pos = Console.GetCursorPosition();
            Console.WriteLine("This will take 5 seconds...");
            using (var _ = ConsoleEx.WriteSpinner(new SpinnerOptions() { Theme = Themes.RedWhiteAndBlue }))
            {
                lock (Console.Out)
                    Console.Write(" ");
                for (; i > 0; i--)
                {
                    lock (Console.Out)
                        Console.Write($"{i} ");
                    // simulate doing stuff...
                    await Task.Delay(1000);
                }
            }
            Console.WriteLine();

            Console.WriteLine("==== Demo of inline Spinner with custom frame====");

            pos = Console.GetCursorPosition();
            i = 10;
            using (var _ = ConsoleEx.WriteSpinner(new SpinnerOptions() { Animation = Animations.Arcs, CustomFrame = (frame, done) => $"{frame} Counter: {i} " }))
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