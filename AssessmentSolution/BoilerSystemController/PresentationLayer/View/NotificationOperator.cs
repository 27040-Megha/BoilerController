using System;
using BoilerSystemController.Domain.Model;
using BoilerSystemController.PresentationLayer.Helper;

namespace BoilerSystemController.PresentationLayer.View
{
    public static class NotificationOperator
    {
        private static readonly object _consoleLock = new object();

        public static void DisplayNotification(BoilerSystem _boilerSystem)
        {
            lock (_consoleLock)
            {
                int currentleft = Console.CursorLeft;
                int currentTop = Console.CursorTop;
                int left = Console.WindowWidth - 60;
                Console.SetCursorPosition(left, 0);
                var notificationMessage = $"Boiler System status: {_boilerSystem.BoilerSystemStatus}, Switch status: {_boilerSystem.InterlockSwitchStatus}";
                TextColor.WriteColoredLine(notificationMessage.PadRight(50), ConsoleColor.Yellow);
                Console.SetCursorPosition(currentleft, currentTop);
            }
        }
    }
}
