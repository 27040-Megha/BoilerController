using System;
using BoilerSystemController.ApplicationLayer.Service;
using BoilerSystemController.Domain.Enums;
using BoilerSystemController.Domain.Model;
using BoilerSystemController.PresentationLayer.Helper;

namespace BoilerSystemController.PresentationLayer.View
{
    public class ConsoleOperations
    {
        private readonly BoilerSystemService _boilerSystemService;

        public ConsoleOperations(BoilerSystemService boilerSystemService)
        {
            this._boilerSystemService = boilerSystemService;
        }

        public void Run()
        {
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
                switch(menuChoice)
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
            while (menuChoice != MenuOptions.Exit);
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
            throw new NotImplementedException();
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
