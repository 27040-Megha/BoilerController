using System;
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

        /// <summary>
        /// Event that will be raised when switch or boiler status change 
        /// </summary>
        public static Action<EventLog> OnStatusChanged;

        /// <summary>
        /// Event invoked when Boiler started and till the Pre-Purge status
        /// </summary>
        public static Action<double> OnPrePurgeStatus;

        /// <summary>
        /// Initializes Boiler System with default status and publishes the event
        /// </summary>
        public BoilerSystemService()
        {
            _boilerSystem = new BoilerSystem(1, BoilerStatus.Lockout, SwitchStatus.Open);
            PublishEvent(new EventLog(DateTime.Now, "Log:", "Boiler Initialized"));
        }

        /// <summary>
        /// Uses a Timer to handle the Boiler System's cycle
        /// </summary>
        public void HandleBoilerCycle()
        {
            var timer1 = new System.Timers.Timer();
            timer1.Interval = 10000;
            timer1.Start();
            HandleCountDown();
            timer1.Elapsed += ((object sender, ElapsedEventArgs e) =>
            {
                if (this.CanBoilerCycleStop())
                {
                    timer1.Stop();
                }

                this.ChangeBoilerStatus();
            });
        }

        private void HandleCountDown()
        {
            var timer2 = new System.Timers.Timer();
            var countDown = 10000;
            timer2.Interval = 200;
            timer2.Start();
            timer2.Elapsed += ((object sender, ElapsedEventArgs e) =>
            {
                if (this._boilerSystem.BoilerSystemStatus == BoilerStatus.PrePurge)
                {
                    OnPrePurgeStatus?.Invoke(countDown);
                    countDown = countDown - 200;
                }
                else
                {
                    timer2.Stop();
                }
            });
        }

        /// <summary>
        /// Subscribed to the Timer's Elapsed event
        /// Will be notified whenever timer is elapsed (Every 10 secs) 
        /// Changes the status and calls the PublishEvent to publish the event
        /// </summary>
        public void ChangeBoilerStatus()
        {
            switch (_boilerSystem.BoilerSystemStatus)
            {
                case BoilerStatus.PrePurge:
                    PublishEvent(new EventLog(DateTime.Now, "Log:", $"{this._boilerSystem.BoilerSystemStatus} completed"));
                    _boilerSystem.BoilerSystemStatus = BoilerStatus.Ignition;
                    break;
                case BoilerStatus.Ignition:
                    PublishEvent(new EventLog(DateTime.Now, "Log:", $"{this._boilerSystem.BoilerSystemStatus} completed"));
                    _boilerSystem.BoilerSystemStatus = BoilerStatus.Operational;
                    PublishEvent(new EventLog(DateTime.Now, "Log:", $"Boiler {this._boilerSystem.BoilerSystemStatus}"));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Publishes the event when status changes, which the subscribers will be notified
        /// </summary>
        /// <param name="eventLog">EventLog object</param>
        public void PublishEvent(EventLog eventLog)
        {
            OnStatusChanged?.Invoke(eventLog);
        }

        /// <summary>
        /// Starts the Boiler Sequence only when both switch is On (Closed state) and Boiler is in Ready State
        /// </summary>
        /// <returns>Result Object</returns>
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
            HandleBoilerCycle();
            return new Result(true, $"Boiler Sequence started successfully!");
        }

        /// <summary>
        /// Stops the boiler system
        /// If System is already stopped (Lockout state), Return false
        /// If system is in operational state, the system goes to Ready state when stopped
        /// When system is in other states (Pre-Purge or Ignition), the system goes to Lockout state when stopped
        /// </summary>
        /// <returns>Result Object</returns>
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

        /// <summary>
        /// Can simulate error only when the Boiler System is in Operational System.
        /// Simulate Boiler error - Change the BoilerStatus to Lockout and publishes the event
        /// </summary>
        /// <returns></returns>
        public Result SimulateBoilerError()
        {
            if (this._boilerSystem.BoilerSystemStatus != BoilerStatus.Operational)
            {
                return new Result(false, $"The system is not in operational status to simulate error!");
            }

            this._boilerSystem.BoilerSystemStatus = BoilerStatus.Lockout;
            PublishEvent(new EventLog(DateTime.Now, "Error:", $"System is in {this._boilerSystem.BoilerSystemStatus}"));
            return new Result(true, $"Successfully simulated boiler error!");
        }

        /// <summary>
        /// Toggles switch and pusblishes event
        /// </summary>
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

            PublishEvent(new EventLog(DateTime.Now, "Log:", $"Interlock switch toggled to {this._boilerSystem.InterlockSwitchStatus}"));
        }

        /// <summary>
        /// Changes the Boiler System's status to Ready
        /// Reset can only be done when the switch is On (Closed) state
        /// </summary>
        /// <returns></returns>
        public Result ResetLockOut()
        {
            if (this._boilerSystem.InterlockSwitchStatus == SwitchStatus.Open)
            {
                return new Result(false, $"The interlock switch is in Off (Open) state, Can't Reset Lockout, Toggle the switch!");
            }

            this._boilerSystem.BoilerSystemStatus = BoilerStatus.Ready;
            PublishEvent(new EventLog(DateTime.Now, "Log:", $"Boiler Status changed to {this._boilerSystem.BoilerSystemStatus}"));
            return new Result(true, $"Lockout sucessfully reset!");
        }

        private bool CanBoilerCycleStop()
        {
            return this._boilerSystem.BoilerSystemStatus == BoilerStatus.Operational || this._boilerSystem.BoilerSystemStatus == BoilerStatus.Lockout || this._boilerSystem.BoilerSystemStatus == BoilerStatus.Ready;
        }

        private bool CheckBoilerReady()
        {
            return this._boilerSystem.BoilerSystemStatus == BoilerStatus.Ready;
        }

        private bool CheckSwitchOn()
        {
            return this._boilerSystem.InterlockSwitchStatus == SwitchStatus.Closed;
        }
    }
}
