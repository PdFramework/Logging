using NLog.Config;
using NLog.Layouts;

namespace PeinearyDevelopment.Framework.Logging.NLog.Core.TargetGenerators
{
    [NLogConfigurationItem]
    public class MessageQueueParameterInformation : IBaseParameterInformation
    {
        [RequiredParameter]
        public Layout Layout { get; set; }
        [RequiredParameter]
        public string Name { get; set; }
    }
}
