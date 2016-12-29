namespace PeinearyDevelopment.Framework.Logging.Contracts
{
    using System;

    public class BaseLogEvent
    {
        public int Id { get; set; }
        public string MachineName { get; set; }
        public DateTimeOffset LoggedOn { get; set; }
        public string ApplicationName { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
    }
}
