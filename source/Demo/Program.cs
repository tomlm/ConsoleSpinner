using Nito.AsyncEx;
using Spinner;
using System.Reflection;

namespace Demo
{
    internal class Program
    {
        static int Main(string[] args)
        {
            // make it so that we can have async console operations 
            return AsyncContext.Run(AsyncMain);
        }

        static async Task<int> AsyncMain()
        {
            Console.WriteLine("Hit any key to start the demo...");
            Console.ReadKey();

            Console.WriteLine("==== Demo of Simple custom invocation ====");
            Console.Write("This will take 3 seconds... ");
            using (_ = ConsoleEx.StartSpinner(Animations.SpinArrows, "YAY", "OH NO"))
            {
                // simulate doing stuff...
                await Task.Delay(3000);
            }
            Console.WriteLine();
            Console.WriteLine("==== Demo of Spinner Styles with task ====");
            List<ConsoleSpinner> spinners = new List<ConsoleSpinner>();

            Random rnd = new Random();

            // show each animation on our Common Animation class
            var themes = typeof(Themes).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Take(5)
                .Select(field => (ConsoleColor[])field.GetValue(null))
                .ToArray();

            foreach (var field in typeof(Animations).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                var animation = (String[])field.GetValue(null);
                var delay = rnd.Next(8000, 12000);
                var task = Task.Delay(delay);

                // use ConsolEx.WriteXX() to write to output while spinners are active.
                Console.Write($"{field.Name} ");
                spinners.Add(ConsoleEx.StartSpinner(task, animation));
                Console.WriteLine();
            }

            // wait for all spinners to complete.
            await Task.WhenAll(spinners.Select(spinner => spinner.Task));

            // all spinners are complete, we can use regular Console API 
            Console.WriteLine("All animations completed!");
            Console.WriteLine();
            Console.WriteLine("==== Demo of inline Spinner no custom frame====");

            int i = 5;
            var pos = Console.GetCursorPosition();
            Console.WriteLine("This will take 5 seconds...");
            using (var _ = ConsoleEx.StartSpinner(new SpinnerOptions() { Theme = Themes.RedWhiteAndBlue }))
            {
                Console.Write(" ");
                for (; i > 0; i--)
                {
                    Console.Write($"{i} ");
                    // simulate doing stuff...
                    await Task.Delay(1000);
                }
            }

            Console.WriteLine();

            Console.WriteLine("==== Demo of inline Spinner with custom frame====");

            pos = Console.GetCursorPosition();
            i = 10;
            using (var _ = ConsoleEx.StartSpinner(new SpinnerOptions()
            {
                Animation = Animations.Arcs,
                Success = "",
                Failed = "Error",
                // Custom Frame gives you ability to customize the animaton frame with additional information via a delegate
                CustomFrame = (frame, done) => $"{frame} Counter: {i} "
            }))
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
            return 0;
        }
    }
}