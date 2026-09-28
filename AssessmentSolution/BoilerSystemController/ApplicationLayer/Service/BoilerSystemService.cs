using System;
using System.Threading.Tasks;
using System.Timers;
using BoilerSystemController.Domain.Enums;
using BoilerSystemController.Domain.Model;

namespace BoilerSystemController.ApplicationLayer.Service
{
    /// <summary>
    /// Contains Business Logic that controls the Boiler System
    /// </summary>
    public class BoilerSystemService
    {
        private BoilerSystem _boilerSystem;

        public BoilerSystemService()
        {
            _boilerSystem = new BoilerSystem(1, BoilerStatus.Lockout, SwitchStatus.Open);
            EventLogService.PublishEvent(new EventLog(DateTime.Now, "Log:", "Boiler Initialized"));
        }

        public async Task HandleBoilerCycle()
        {
            var timer = new System.Timers.Timer();
            timer.Interval = 10000;
            timer.Start();
            timer.Elapsed += ((object sender, ElapsedEventArgs e) =>
            {
                this.ChangeBoilerStatus();
            });

            if (this.IsBoilerSystemFunctional())
            {
                timer.Stop();
            }
        }

        private bool IsBoilerSystemFunctional()
        {
            return this._boilerSystem.BoilerSystemStatus == BoilerStatus.Operational || this._boilerSystem.BoilerSystemStatus == BoilerStatus.Lockout || this._boilerSystem.BoilerSystemStatus == BoilerStatus.Ready;
        }

        public void ChangeBoilerStatus()
        {
            switch (_boilerSystem.BoilerSystemStatus)
            {
                case BoilerStatus.PrePurge:
                    EventLogService.PublishEvent(new EventLog(DateTime.Now, "Log:", $"{this._boilerSystem.BoilerSystemStatus} completed"));
                    _boilerSystem.BoilerSystemStatus = BoilerStatus.Ignition;
                    break;
                case BoilerStatus.Ignition:
                    EventLogService.PublishEvent(new EventLog(DateTime.Now, "Log:", $"{this._boilerSystem.BoilerSystemStatus} completed"));
                    _boilerSystem.BoilerSystemStatus = BoilerStatus.Operational;
                    EventLogService.PublishEvent(new EventLog(DateTime.Now, "Log:", $"Boiler {this._boilerSystem.BoilerSystemStatus}"));
                    break;
                default:
                    break;
            }
        }

        public Result StartBoilerSequence()
        {
            if (!this.CheckBoilerReady())
            {
                return new Result(false, $"Boiler is not in ready state, Toggle the switch if opened or Reset lockout");
            }
            else if (!this.CheckSwitchOn())
            {
                return new Result(false, $"Switch is in Off state, Toggle the switch to start boiler");
            }

            this._boilerSystem.BoilerSystemStatus = BoilerStatus.PrePurge;
            try
            {
                _ = HandleBoilerCycle();
            }
            catch (Exception ex)
            {
                return new Result(false, $"Exception caught: {ex.Message}");
            }

            return new Result(true, $"Boiler Sequence started successfully!");
        }

        private bool CheckBoilerReady()
        {
            return this._boilerSystem.BoilerSystemStatus == BoilerStatus.Ready;
        }

        private bool CheckSwitchOn()
        {
            return this._boilerSystem.InterlockSwitchStatus == SwitchStatus.Closed;
        }

        public Result StopBoilerSequence()
        {
            if (this._boilerSystem.BoilerSystemStatus == BoilerStatus.Lockout)
            {
                return new Result(false, $"The system is already in lockout state!");
            }

            if (this._boilerSystem.BoilerSystemStatus == BoilerStatus.Operational)
            {
                this._boilerSystem.BoilerSystemStatus = BoilerStatus.Ready;
            }
            else
            {
                this._boilerSystem.BoilerSystemStatus = BoilerStatus.Lockout;
            }

            return new Result(true, $"The boiler system has been successfully stopped!");
        }

        public Result SimulateBoilerError()
        {
            if (this._boilerSystem.BoilerSystemStatus != BoilerStatus.Operational)
            {
                return new Result(false, $"The system is not in operational status to simulate error!");
            }

            this._boilerSystem.BoilerSystemStatus = BoilerStatus.Lockout;
            EventLogService.PublishEvent(new EventLog(DateTime.Now, "Error:", $"System is in {this._boilerSystem.BoilerSystemStatus}"));
            return new Result(true, $"Successfully simulated boiler error!");
        }

        public void ToggleSwitch()
        {
            if (this._boilerSystem.InterlockSwitchStatus == SwitchStatus.Closed)
            {
                this._boilerSystem.InterlockSwitchStatus = SwitchStatus.Open;
            }
            else
            {
                this._boilerSystem.InterlockSwitchStatus = SwitchStatus.Closed;
            }

            EventLogService.PublishEvent(new EventLog(DateTime.Now, "Log:", $"Interlock switch toggled to {this._boilerSystem.InterlockSwitchStatus}"));
        }

        public Result ResetLockOut()
        {
            if (this._boilerSystem.InterlockSwitchStatus == SwitchStatus.Open)
            {
                return new Result(false, $"The interlock switch is in Off (Open) state, Can't Reset Lockout, Toggle the switch!");
            }

            this._boilerSystem.BoilerSystemStatus = BoilerStatus.Ready;
            EventLogService.PublishEvent(new EventLog(DateTime.Now, "Log:", $"Boiler Status changed to {this._boilerSystem.BoilerSystemStatus}"));
            return new Result(true, $"Lockout sucessfully reset!");
        }
    }
}
