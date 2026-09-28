using System.Collections.Generic;
using BoilerSystemController.ApplicationLayer.Interface;
using BoilerSystemController.Domain.Model;

namespace BoilerSystemController.ApplicationLayer.Service
{
    public class EventLogService
    {
        private readonly IEventLogRepository _eventLogRepository;

        public EventLogService(IEventLogRepository eventLogRepository)
        {
            this._eventLogRepository = eventLogRepository;
        }
        
        public void WriteLogToFile(EventLog eventLog)
        {
            this._eventLogRepository.AddLogMessage(eventLog);
        }

        public IEnumerable<EventLog> GetAllLogs()
        {
            return this._eventLogRepository.FetchAllLogs();
        }
    }
}
