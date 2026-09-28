using System;
using BoilerSystemController.ApplicationLayer.Service;
using BoilerSystemController.InfrastructureLayer;
using BoilerSystemController.PresentationLayer.View;

namespace BoilerSystemController
{
    /// <summary>
    /// Main class 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point of application - Create objects and inject dependencies, Subscribe to the events, and starts the application using method Run().
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
            var eventLogRepository = new EventLogRepository();
            var eventLogService = new EventLogService(eventLogRepository);
            var boilerSystemService = new BoilerSystemService();
            boilerSystemService.OnStatusChanged += NotificationOperator.DisplayNotification;
            boilerSystemService.OnStatusChanged += eventLogService.WriteLogToFile;
            var consoleOperator = new ConsoleOperations(boilerSystemService, eventLogService);
            consoleOperator.Run();
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                Console.WriteLine(string.Format(DisplayResource.ExceptionMessage, ex.Message));
            }
        }
    }
}
