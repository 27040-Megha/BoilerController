using System.Collections.Generic;
using BoilerSystemController.ApplicationLayer.Interface;
using BoilerSystemController.Domain.Model;

namespace BoilerSystemController.ApplicationLayer.Service
{
    /// <summary>
    /// Contains business logic to log system status and errors to file
    /// </summary>
    public class EventLogService
    {
        private readonly IEventLogRepository _eventLogRepository;

        /// <summary>
        /// Initializes the EventLogRepository object
        /// </summary>
        /// <param name="eventLogRepository">EventLogRepository object</param>
        public EventLogService(IEventLogRepository eventLogRepository)
        {
            this._eventLogRepository = eventLogRepository;
        }
        
        /// <summary>
        /// SUBSCRIBER: Subscribed to the event OnStatusChanged
        /// Whenever the event is published, the subscriber will be notified and the method logs the event
        /// Calls repository method to write the log to the file
        /// </summary>
        /// <param name="eventLog">EventLog object</param>
        public void WriteLogToFile(EventLog eventLog)
        {
            this._eventLogRepository.AddLogMessage(eventLog);
        }

        /// <summary>
        /// Fetches all logs from the repository
        /// </summary>
        /// <returns>IEnumerable collection of EventLog</returns>
        public IEnumerable<EventLog> GetAllLogs()
        {
            return this._eventLogRepository.FetchAllLogs();
        }
    }
}
