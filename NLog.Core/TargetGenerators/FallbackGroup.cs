using NLog.Targets;
using NLog.Targets.Wrappers;

namespace PeinearyDevelopment.Framework.Logging.NLog.Core.TargetGenerators
{
    using Utilities;

    public static class FallbackGroup
    {
        public static Target Generate(Target[] targets)
        {
            FallbackGroupTarget target = null;

            for (var i = targets.Length - 1; i > 0; i--)
            {
                var fallbackGroupName = TargetNameGenerator.Generate<FallbackGroupTarget>();
                target = target == null ? new FallbackGroupTarget(targets[i - 1], targets[i]) { Name = fallbackGroupName } : new FallbackGroupTarget(targets[i - 1], target) { Name = fallbackGroupName };
            }

            return target;
        }
    }
}
