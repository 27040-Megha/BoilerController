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

        public Action<BoilerSystem> OnStatusChanged;

        public BoilerSystemService()
        {
            _boilerSystem = new BoilerSystem(1, BoilerStatus.Lockout, SwitchStatus.Open);
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
                    _boilerSystem.BoilerSystemStatus = BoilerStatus.Ignition;
                    this.PublishEvent(this._boilerSystem);
                    break;
                case BoilerStatus.Ignition:
                    _boilerSystem.BoilerSystemStatus = BoilerStatus.Operational;
                    this.PublishEvent(this._boilerSystem);
                    break;
                default:
                    break;
            }
        }

        public void PublishEvent(BoilerSystem boilerSystem)
        {
            OnStatusChanged?.Invoke(boilerSystem);
        }

        public Result StartBoilerSequence()
        {
            if (!this.CheckBoilerReady())
            {
                return new Result(false, $"Boiler is not in ready state");
            }
            else if (!this.CheckSwitchOn())
            {
                return new Result(false, $"Switch is in Off state, Toggle the switch to start boiler");
            }

            this._boilerSystem.BoilerSystemStatus = BoilerStatus.PrePurge;
            this.PublishEvent(this._boilerSystem);
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

        public void StopBoilerSequence()
        {
            if (this._boilerSystem.BoilerSystemStatus == BoilerStatus.Operational)
            {
                this._boilerSystem.BoilerSystemStatus = BoilerStatus.Ready;
                this.PublishEvent(this._boilerSystem);
            }
            else
            {
                this._boilerSystem.BoilerSystemStatus = BoilerStatus.Lockout;
                this.PublishEvent(this._boilerSystem);
            }
        }

        public Result SimulateBoilerError()
        {
            if (this._boilerSystem.BoilerSystemStatus != BoilerStatus.Operational)
            {
                return new Result(false, $"The system is not in operational status to simulate error!");
            }

            this._boilerSystem.BoilerSystemStatus = BoilerStatus.Lockout;
            this.PublishEvent(this._boilerSystem);
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

            this.PublishEvent(this._boilerSystem);
        }

        public Result ResetLockOut()
        {
            if (this._boilerSystem.InterlockSwitchStatus == SwitchStatus.Open)
            {
                return new Result(false, $"The interlock switch is in Off (Open) state, Can't Reset Lockout, Toggle the switch!");
            }

            this._boilerSystem.BoilerSystemStatus = BoilerStatus.Ready;
            this.PublishEvent(this._boilerSystem);
            return new Result(true, $"Lockout sucessfully reset!");
        }
    }
}
