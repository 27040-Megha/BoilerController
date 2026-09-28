using System.Collections.Generic;
using BoilerSystemController.Domain.Model;

namespace BoilerSystemController.ApplicationLayer.Interface
{
    /// <summary>
    /// Interface - Contains method to write log messages to file and fetch all messages from the file
    /// </summary>
    public interface IEventLogRepository
    {
        /// <summary>
        /// Adds log messages to the .txt file in csv format
        /// </summary>
        /// <param name="eventLog">Eventlog objec to be written</param>
        void AddLogMessage(EventLog eventLog);

        /// <summary>
        /// Returns all logs from the log file
        /// </summary>
        /// <returns>IEnumerable collection of EventLog</returns>
        IEnumerable<EventLog> FetchAllLogs();
    }
}
