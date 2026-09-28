using System.Collections.Generic;
using BoilerSystemController.ApplicationLayer.Interface;
using BoilerSystemController.Domain.Model;

namespace BoilerSystemController.InfrastructureLayer
{
    public class EventLogRepository : IEventLogRepository
    {
        private static object _fileLock = new object();

        public void AddLogMessage(EventLog eventLog)
        {
            lock (_fileLock)
            {
                FileHandlingService.WriteFile(FileResource.FilePath, eventLog);
            }
        }

        public List<EventLog> FetchAllLogs()
        {
            lock(_fileLock)
            {
                return FileHandlingService.ReadFile(FileResource.FilePath);
            }
        }
    }
}
