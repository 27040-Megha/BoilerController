using System.IO;

namespace BoilerSystemController.ApplicationLayer.Interface
{
    /// <summary>
    /// Interface - Contains method to write log messages to file and fetch all messages from the file
    /// </summary>
    public interface IEventLogRepository
    {
        void AddLogMessage(string message);

        string FetchAllLogs();
    }
}
