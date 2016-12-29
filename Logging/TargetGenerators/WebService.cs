using NLog.Internal;
using NLog.Targets;

namespace PeinearyDevelopment.Framework.Logging.NLog.Core.TargetGenerators
{
    using Utilities;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;

    public static class WebService
    {
        public static WebServiceTarget Generate<TEvent>(IConfigurationManager configurationManager, IDictionary<string, string> layoutOverrides = null)
        {
            var webServiceUri = new Uri(Path.Combine(configurationManager.AppSettings[Constants.AppSettingsKeys.WebServiceEndpoint], typeof(TEvent).Name));

            var target = new WebServiceTarget
            {
                Protocol = WebServiceProtocol.HttpPost,
                Url = webServiceUri,
                Encoding = Encoding.UTF8,
                Name = TargetNameGenerator.Generate<WebServiceTarget>()
            };

            foreach (var parameter in ParametersGenerator.Generate<WebServiceParameterInformation, TEvent>(layoutOverrides))
            {
                target.Parameters.Add(parameter);
            }

            return target;
        }
    }
}
