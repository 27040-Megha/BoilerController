using System;
using BoilerSystemController.ApplicationLayer.Service;
using BoilerSystemController.InfrastructureLayer;
using BoilerSystemController.PresentationLayer.View;

namespace BoilerSystemController
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
            var boilerSystemService = new BoilerSystemService();
            var eventLogRepository = new EventLogRepository();
            var eventLogService = new EventLogService(eventLogRepository);
            boilerSystemService.OnStatusChanged += NotificationOperator.DisplayNotification;
            boilerSystemService.OnStatusChanged += eventLogService.WriteLogToFile;
            var consoleOperator = new ConsoleOperations(boilerSystemService, eventLogService);
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
