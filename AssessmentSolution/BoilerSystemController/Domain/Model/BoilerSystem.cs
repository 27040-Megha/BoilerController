using BoilerSystemController.Domain.Enums;

namespace BoilerSystemController.Domain.Model
{
    /// <summary>
    /// Model Definition - Boiler System
    /// </summary>
    public class BoilerSystem
    {
        /// <summary>
        /// Constructor that initializes BoilerSystem object
        /// </summary>
        /// <param name="systemId">System ID</param>
        /// <param name="boilerStatus">Boiler System Status</param>
        /// <param name="switchStatus">Interlock Switch Status</param>
        public BoilerSystem(int systemId, BoilerStatus boilerStatus, SwitchStatus switchStatus)
        {
            this.SystemId = systemId;
            this.BoilerSystemStatus = boilerStatus;
            this.InterlockSwitchStatus = switchStatus;
        }

        public int SystemId { get; set; }

        public BoilerStatus BoilerSystemStatus { get; set; }

        public SwitchStatus InterlockSwitchStatus { get; set; }
    }
}
