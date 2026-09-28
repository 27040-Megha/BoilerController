using System;

namespace BoilerSystemController.Domain.Model
{
    /// <summary>
    /// Model for logging messages 
    /// </summary>
    public class EventLog
    {
        /// <summary>
        /// Constructor that initializes EventLog object
        /// </summary>
        /// <param name="timeStamp">Time Stamp</param>
        /// <param name="eventName">Event Name</param>
        /// <param name="logMessage">Log Message</param>
        public EventLog(DateTime timeStamp, string eventName, string logMessage)
        {
            this.Timestamp = timeStamp;
            this.EventName = eventName;
            this.LogMessage = logMessage;
        }

        /// <summary>
        /// Gets or sets the value of TimeStamp of the log
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the value of Event Name
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or Sets the value of log message
        /// </summary>
        public string LogMessage { get; set; }
    }
}
