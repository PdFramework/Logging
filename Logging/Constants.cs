namespace PeinearyDevelopment.Framework.Logging.NLog.Core
{
    using System.Collections.Generic;

    public static class Constants
    {
        public const string DefaultLogsPath = @"C:\Logging";

        // Key = PropertyName, Value = RenderLayoutValue
        // https://github.com/nlog/nlog/wiki/Layout-Renderers
        public static readonly IDictionary<string, string> PropertyToLayoutRendererDefaultMapper = new Dictionary<string, string>
        {
            { "MachineName", "${machinename}" },
            { "LoggedOn", "${date:universalTime=true}" },
            { "Level", "${level}" },
            { "Message", "${message:withException=false}" },
            { "Logger", "${logger}" },
            { "Properties", "${all-event-properties:separator=|}" },
            { "StackTrace", "${message:withException=true}" }
        };

        public static class AppSettingsKeys
        {
            private const string BaseLoggingKey = "Logging";
            private const string BaseMessagingKey = "Messaging";

            public const string ApplicationName = BaseLoggingKey + ".ApplicationName";
            public const string ConnectionStringName = BaseLoggingKey + ".ConnectionStringName";
            public const string LogLevel = BaseLoggingKey + ".LogLevel";
            public const string LogsPath = BaseLoggingKey + ".LogsPath";
            public const string MessagingEndpoint = BaseMessagingKey + ".Endpoint";
            public const string MessagingPassword = BaseMessagingKey + ".Password";
            public const string MessagingUsername = BaseMessagingKey + ".Username";
            public const string WebServiceEndpoint = BaseLoggingKey + ".Endpoint";
        }
    }
}
