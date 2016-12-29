namespace PeinearyDevelopment.Framework.Logging.Contracts
{
    public class LogEvent : BaseLogEvent
    {
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string Properties { get; set; }
        public string StackTrace { get; set; }
    }
}
