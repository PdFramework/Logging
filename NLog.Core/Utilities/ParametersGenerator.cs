using NLog.Layouts;

namespace PeinearyDevelopment.Framework.Logging.NLog.Core.Utilities
{
    using TargetGenerators;
    using System.Collections.Generic;
    using System.Linq;

    public static class ParametersGenerator
    {
        public static string GenerateLayoutString<TEvent>(IDictionary<string, string> layoutOverrides = null)
        {
            var eventProperties = typeof(TEvent).GetProperties();
            return string.Join(" ", eventProperties.Select(eventProperty => GetLayoutForPropertyName(eventProperty.Name, layoutOverrides)));
        }

        public static IEnumerable<TParameterInfo> Generate<TParameterInfo, TEvent>(IDictionary<string, string> layoutOverrides = null) where TParameterInfo : IBaseParameterInformation, new()
        {
            var eventProperties = typeof(TEvent).GetProperties();
            return eventProperties.Select(eventProperty => new TParameterInfo
            {
                Layout = GetLayoutForPropertyName(eventProperty.Name, layoutOverrides),
                Name = eventProperty.Name
            });
        }

        private static Layout GetLayoutForPropertyName(string propertyName, IDictionary<string, string> layoutOverrides = null)
        {
            if (layoutOverrides != null && layoutOverrides.ContainsKey(propertyName)) return layoutOverrides[propertyName];
            if (Constants.PropertyToLayoutRendererDefaultMapper.ContainsKey(propertyName)) return Constants.PropertyToLayoutRendererDefaultMapper[propertyName];
            return $"${{event-properties:item={propertyName}:format=String}}";
        }
    }
}
