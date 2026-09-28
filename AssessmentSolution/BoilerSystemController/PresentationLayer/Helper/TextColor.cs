using System;

namespace BoilerSystemController.PresentationLayer.Helper
{
    /// <summary>
    /// Helper class that contains method to write text in specific colors
    /// </summary>
    public static class TextColor
    {
        /// <summary>
        /// Helper method to write text in specific color
        /// </summary>
        /// <param name="input">Input string to be written to console</param>
        /// <param name="colorChoice">Color of the string</param>
        public static void WriteColoredLine(string input, ConsoleColor colorChoice)
        {
            Console.ForegroundColor = colorChoice;
            Console.WriteLine(input);
            Console.ResetColor();
        }
    }
}
