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
            if (!CheckFileExists(filePath))
            {
                return new List<EventLog>();
            }

            var eventLogs = new List<EventLog>();
            var fileContent = File.ReadAllLines(filePath);
            for (int i = 1; i < fileContent.Length; i++)
            {
                eventLogs.Add(DeserializeCSV(fileContent[i]));
            }

            return eventLogs;
        }

        public static void WriteFile(string filePath, EventLog eventLog)
        {
            if (!CheckFileExists(filePath))
            {
                File.WriteAllText(filePath, FileResource.FileHeader);
            }

            string logToSave = SerializeCSV(eventLog);
            File.AppendAllLines(filePath, new [] { logToSave });
        }

        private static bool CheckFileExists(string filePath)
        {
            return File.Exists(filePath);
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
