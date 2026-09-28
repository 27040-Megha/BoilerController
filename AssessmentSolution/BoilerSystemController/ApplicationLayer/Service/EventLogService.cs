using System;
using BoilerSystemController.Domain.Model;

namespace BoilerSystemController.ApplicationLayer.Service
{
    public static class EventLogService
    {

        public static Action<EventLog> OnStatusChanged;

        public static void PublishEvent(EventLog eventLog)
        {
            OnStatusChanged?.Invoke(eventLog);
        }
    }
}
