using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Chameleon.Services.Resources
{
    public partial class RadioStations
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("stations")]
        public List<RadioStation> RadioStationsList { get; set; }
    }

    public partial class RadioStation
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("uri", NullValueHandling = NullValueHandling.Ignore)]
        public string Uri { get; set; }
    }

    public partial class RadioStations
    {
        public static Stream GetEmbeddedResourceStream(Assembly assembly, string resourceFileName)
        {
            var resourceNames = assembly.GetManifestResourceNames();

            var resourcePaths = resourceNames
                .Where(x => x.EndsWith(resourceFileName, StringComparison.CurrentCultureIgnoreCase))
                .ToArray();

            if (!resourcePaths.Any())
            {
                throw new Exception($"Resource ending with {resourceFileName} not found.");
            }

            if (resourcePaths.Count() > 1)
            {
                throw new Exception(string.Format("Multiple resources ending with {0} found: {1}{2}", resourceFileName, Environment.NewLine, string.Join(Environment.NewLine, resourcePaths)));
            }

            return assembly.GetManifestResourceStream(resourcePaths.Single());
        }

        public static string GetEmbeddedResourceString(Assembly assembly, string resourceFileName)
        {
            var stream = GetEmbeddedResourceStream(assembly, resourceFileName);

            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }

        public static string GetEmbeddedResourceString(string resourceFileName)
        {
            return GetEmbeddedResourceString(Assembly.GetCallingAssembly(), resourceFileName);
        }

        public static List<RadioStations> FromJson(string json) => JsonConvert.DeserializeObject<List<RadioStations>>(json);
    }

}

