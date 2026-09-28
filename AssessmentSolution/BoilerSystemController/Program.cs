using System;
using BoilerSystemController.ApplicationLayer.Service;
using BoilerSystemController.PresentationLayer.View;

namespace BoilerSystemController
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
            var boilerSystemService = new BoilerSystemService();
            var consoleOperator = new ConsoleOperations(boilerSystemService);
            EventLogService.OnStatusChanged += NotificationOperator.DisplayNotification;
            consoleOperator.Run();
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }
        }
    }
}
