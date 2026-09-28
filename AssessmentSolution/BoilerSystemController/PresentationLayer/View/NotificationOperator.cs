using System;
using BoilerSystemController.Domain.Model;
using BoilerSystemController.PresentationLayer.Helper;

namespace BoilerSystemController.PresentationLayer.View
{
    /// <summary>
    /// Class contains method that displays notifications to the user
    /// </summary>
    public static class NotificationOperator
    {
        private static readonly object _consoleLock = new object();

        /// <summary>
        /// SUBSCRIBER: Subscribed to the event OnStatusChanged
        /// Whenever the event is published, the subscriber will be notified and the method displays notification
        /// Locks the console and display
        /// </summary>
        /// <param name="eventLog">EventLog object</param>
        public static void DisplayNotification(EventLog eventLog)
        {
            lock (_consoleLock)
            {
                int currentleft = Console.CursorLeft;
                int currentTop = Console.CursorTop;
                int left = Console.WindowWidth - 55;
                Console.SetCursorPosition(left, 0);                
                TextColor.WriteColoredLine(eventLog.LogMessage.PadRight(50), ConsoleColor.Yellow);
                Console.SetCursorPosition(currentleft, currentTop);
            }
        }
    }
}
