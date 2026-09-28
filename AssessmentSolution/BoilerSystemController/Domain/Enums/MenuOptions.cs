namespace BoilerSystemController.Domain.Enums
{
    /// <summary>
    /// Menu options to control the Boiler System
    /// </summary>
    public enum MenuOptions
    {
        StartBoilerSequence = 1,

        StopBoilerSequence,

        SimulateBoilerError,

        ToggleSwitch,

        ResetLockout,

        ViewEventLog,

        Exit,

        Invalid = 0,
    }
}
