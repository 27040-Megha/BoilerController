using System.Collections.Generic;
using BoilerSystemController.Domain.Model;

namespace BoilerSystemController.ApplicationLayer.Interface
{
    /// <summary>
    /// Interface - Contains method to write log messages to file and fetch all messages from the file
    /// </summary>
    public interface IEventLogRepository
    {
        void AddLogMessage(EventLog eventLog);

        List<EventLog> FetchAllLogs();
    }
}
