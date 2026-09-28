using System.Collections.Generic;
using BoilerSystemController.ApplicationLayer.Interface;
using BoilerSystemController.Domain.Model;

namespace BoilerSystemController.InfrastructureLayer
{
    /// <summary>
    /// Class implements interface IEventLogRepository and provides definition for methods
    /// </summary>
    public class EventLogRepository : IEventLogRepository
    {
        private static object _fileLock = new object();

        /// <summary>
        /// Writes log messages to the file using helper method
        /// Uses a file lock, to ensure only single thread accesses the file at a time
        /// </summary>
        /// <param name="eventLog">Eventlog objec to be written</param>
        public void AddLogMessage(EventLog eventLog)
        {
            lock (_fileLock)
            {
                FileHandlingService.WriteFile(FileResource.FilePath, eventLog);
            }
        }

        /// <summary>
        /// Returns all event log data from csv file 
        /// Uses a file lock, to ensure only single thread accesses the file at a time
        /// </summary>
        /// <returns>IEnumerable collection of EventLog</returns>
        public IEnumerable<EventLog> FetchAllLogs()
        {
            lock(_fileLock)
            {
                return FileHandlingService.ReadFile(FileResource.FilePath);
            }
        }
    }
}
