namespace BoilerSystemController.Domain.Enums
{
   /// <summary>
   /// Enum representing Boiler System status
   /// </summary>
    public enum BoilerStatus
    {
        Lockout,

        Ready,

        PrePurge,

        Ignition,

        Operational,
    }
}
