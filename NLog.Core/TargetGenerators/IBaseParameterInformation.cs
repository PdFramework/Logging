using NLog.Layouts;

namespace PeinearyDevelopment.Framework.Logging.NLog.Core.TargetGenerators
{
    public interface IBaseParameterInformation
    {
        string Name { get; set; }
        Layout Layout { get; set; }
    }
}
