using System;
using System.Linq;
using BoilerSystemController.ApplicationLayer.Service;
using BoilerSystemController.Domain.Enums;
using BoilerSystemController.Domain.Model;
using BoilerSystemController.PresentationLayer.Helper;

namespace BoilerSystemController.PresentationLayer.View
{
    /// <summary>
    /// Interacts with user
    /// </summary>
    public class ConsoleOperations
    {
        private readonly BoilerSystemService _boilerSystemService;

        private readonly EventLogService _eventlogService;

        /// <summary>
        /// Initializes Service objects
        /// </summary>
        /// <param name="boilerSystemService">BoilerSystem service object</param>
        /// <param name="eventLogService">EventLog service object</param>
        public ConsoleOperations(BoilerSystemService boilerSystemService, EventLogService eventLogService)
        {
            this._boilerSystemService = boilerSystemService;
            this._eventlogService = eventLogService;
        }

        /// <summary>
        /// Initial method of application: Starts the application
        /// </summary>
        public void Run()
        {
            TextColor.WriteColoredLine(DisplayResource.WelcomeMessage, ConsoleColor.Yellow);
            this.HandleMenu();
        }

        private void HandleMenu()
        {
            MenuOptions menuChoice;
            do
            {
                this.DisplayMenu();
                var isValidChoice = InputValidator.ValidateInteger(Console.ReadLine(), out int choice);
                if (!isValidChoice)
                {
                    choice = 0;
                }

                menuChoice = (MenuOptions)choice;
                this.HandleSwitchCase(menuChoice);
                // this.WaitAndClearConsole();
            }
            while (menuChoice != MenuOptions.Exit);
        }

        private void HandleSwitchCase(MenuOptions menuChoice)
        {
            switch (menuChoice)
            {
                case MenuOptions.StartBoilerSequence:
                    this.StartBoilerSystem();
                    break;
                case MenuOptions.StopBoilerSequence:
                    this.StopBoilerSystem();
                    break;
                case MenuOptions.SimulateBoilerError:
                    this.SimulateBoilerSystemError();
                    break;
                case MenuOptions.ToggleSwitch:
                    this.ToggleInterlockSwitch();
                    break;
                case MenuOptions.ResetLockout:
                    this.ResetSystemLockout();
                    break;
                case MenuOptions.ViewEventLog:
                    this.DisplayEventLog();
                    break;
                case MenuOptions.Exit:
                    TextColor.WriteColoredLine(DisplayResource.ExitMessage, ConsoleColor.Cyan);
                    break;
                default:
                    TextColor.WriteColoredLine(DisplayResource.InvalidChoice, ConsoleColor.Red);
                    break;
            }
        }

        private void WaitAndClearConsole()
        {
            TextColor.WriteColoredLine(DisplayResource.PressAnyKeyMessage, ConsoleColor.Cyan);
            Console.ReadKey();
            Console.Clear();
        }

        private void StartBoilerSystem()
        {
            var startSystemResut = this._boilerSystemService.StartBoilerSequence();
            this.DisplayResult(startSystemResut);
        }

        private void StopBoilerSystem()
        {
            var stopSystemResut = this._boilerSystemService.StopBoilerSequence();
            this.DisplayResult(stopSystemResut);
        }

        private void SimulateBoilerSystemError()
        {
            var simulateSystemResut = this._boilerSystemService.SimulateBoilerError();
            this.DisplayResult(simulateSystemResut);
        }

        private void ToggleInterlockSwitch()
        {
            this._boilerSystemService.ToggleSwitch();
        }

        private void ResetSystemLockout()
        {
            var lockoutSystemResult = this._boilerSystemService.ResetLockOut();
            this.DisplayResult(lockoutSystemResult);
        }

        private void DisplayEventLog()
        {
            var eventLogs = this._eventlogService.GetAllLogs().ToList();
            TextColor.WriteColoredLine(DisplayResource.EventLogHeading, ConsoleColor.Cyan);
            if (eventLogs.Count == 0)
            {
                TextColor.WriteColoredLine(DisplayResource.Nologs, ConsoleColor.Red);
                return;
            }

            foreach (var log in eventLogs)
            {
                Console.WriteLine(string.Format(DisplayResource.Log, log.Timestamp, log.EventName, log.LogMessage));
            }
        }

        private void DisplayResult(Result resultObject)
        {
            if (!resultObject.IsSuccess)
            {
                TextColor.WriteColoredLine(resultObject.Message, ConsoleColor.Red);
                return;
            }

            TextColor.WriteColoredLine(resultObject.Message, ConsoleColor.Green);
        }

        private void DisplayMenu()
        {
            TextColor.WriteColoredLine(DisplayResource.Menu, ConsoleColor.Cyan);
            Console.WriteLine(DisplayResource.MenuOptions);
            TextColor.WriteColoredLine(DisplayResource.PromptChoice, ConsoleColor.Cyan);
        }
    }
}
