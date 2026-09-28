using System;
using BoilerSystemController.Domain.Model;
using BoilerSystemController.PresentationLayer.Helper;

namespace BoilerSystemController.PresentationLayer.View
{
    public static class NotificationOperator
    {
        private static readonly object _consoleLock = new object();

        public static void DisplayNotification(EventLog eventLog)
        {
            lock (_consoleLock)
            {
                int currentleft = Console.CursorLeft;
                int currentTop = Console.CursorTop;
                int left = Console.WindowWidth - 60;
                Console.SetCursorPosition(left, 0);                
                TextColor.WriteColoredLine(eventLog.LogMessage.PadRight(50), ConsoleColor.Yellow);
                Console.SetCursorPosition(currentleft, currentTop);
            }
        }
    }
}
