using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BoilerSystemController.Domain.Model;

namespace BoilerSystemController.InfrastructureLayer
{
    public static class FileHandlingService
    {
        public static List<EventLog> ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<EventLog>();
            }

            var eventLogs = new List<EventLog>();
            var fileContent = File.ReadAllLines(filePath);
            foreach (string line in fileContent)
            {
                eventLogs.Add(DeserializeCSV(line));
            }

            return eventLogs;
        }

        public static void WriteFile(string filePath, EventLog eventLog)
        {
            string logToSave = SerializeCSV(eventLog);
            File.AppendAllLines(filePath, new [] { logToSave });
        }

        private static string SerializeCSV(EventLog eventLog)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(eventLog.Timestamp.ToString() + ','+ eventLog.EventName + ','+ eventLog.LogMessage);
            return stringBuilder.ToString();
        }

        private static EventLog DeserializeCSV(string line)
        {
            string[] data = line.Split(',');
            var isValidTimeStamp = DateTime.TryParse(data[0], out DateTime timestamp);
            return new EventLog(timestamp, data[1], data[2]);
        }
    }
}
