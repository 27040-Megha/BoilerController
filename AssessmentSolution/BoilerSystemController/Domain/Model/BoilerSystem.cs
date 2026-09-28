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

        /// <summary>
        /// Gets or sets the value of Boiler System ID
        /// </summary>
        public int SystemId { get; set; }

        /// <summary>
        /// Gets or sets the value of Boiler System Status
        /// </summary>
        public BoilerStatus BoilerSystemStatus { get; set; }

        /// <summary>
        /// Gets or sets the value of Switch status
        /// </summary>
        public SwitchStatus InterlockSwitchStatus { get; set; }
    }
}
