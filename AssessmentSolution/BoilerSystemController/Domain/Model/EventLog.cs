using System;

namespace BoilerSystemController.Domain.Model
{
    public class EventLog
    {
        public EventLog(DateTime timeStamp, string eventName, string logMessage)
        {
            this.Timestamp = timeStamp;
            this.EventName = eventName;
            this.LogMessage = logMessage;
        }

        public DateTime Timestamp { get; set; }

        public string EventName { get; set; }

        public string LogMessage { get; set; }
    }
}
