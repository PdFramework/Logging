using System;

namespace PeinearyDevelopment.Framework.Logging.NLog.Core.Utilities
{
    public static class TargetNameGenerator
    {
        public static string Generate<T>()
        {
            return $"{typeof(T).FullName}:{Guid.NewGuid()}";
        }
    }
}
