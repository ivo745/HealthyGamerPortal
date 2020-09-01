using System;

namespace HealthyGamerPortal.Models
{
    public class LogEntry
    {
        public Guid Id { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string StackTrace { get; set; }
        public DateTime DateCreated { get; set; }

        public string ExceptionOrMessage
        {
            get
            {
                var message = LogLevel == "Info" || LogLevel == "Warn" ? Message : Exception;

                if (message.Length > 100)
                {
                    return message.Substring(0, 100) + "(...)";
                }

                return message;
            }
        }
    }
}