namespace BoilerSystemController.Domain.Enums
{
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
