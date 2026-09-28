using System;

namespace BoilerSystemController.PresentationLayer.Helper
{
    public static class TextColor
    {
        public static void WriteColoredLine(string input, ConsoleColor colorChoice)
        {
            Console.ForegroundColor = colorChoice;
            Console.WriteLine(input);
            Console.ResetColor();
        }
    }
}
