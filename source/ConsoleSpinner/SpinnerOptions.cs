using System.Drawing;

namespace Spinner
{
    public class SpinnerOptions
    {
        /// <summary>
        /// Delay between each spin in milliseconds (default 100)
        /// </summary>
        public int Delay { get; set; } = 100;

        /// <summary>
        /// Animation to use. (Default Lines)
        /// </summary>
        public string[] Animation { get; set; } = Spinner.Animations.Lines;

        /// <summary>
        /// Color to use for each frame of the animation.
        /// </summary>
        public ConsoleColor[] Theme { get; set; } 

        /// <summary>
        /// Custom Frame callback
        /// </summary>
        public Func<string, bool, string> CustomFrame { get; set; }

        /// <summary>
        /// Text to use for success (Default: ✓)
        /// </summary>
        public string Success { get; set; } = "✓";

        /// <summary>
        /// Color to use for success (Default: Green)
        /// </summary>
        public ConsoleColor SuccessColor { get; set; } = ConsoleColor.Green;

        /// <summary>
        /// Text to use for failure (Default: X)
        /// </summary>
        public string Failed { get; set; } = "X";

        /// <summary>
        /// Color to use for failure (Default red)
        /// </summary>
        public ConsoleColor FailedColor { get; set; } = ConsoleColor.Red;
    }
}
