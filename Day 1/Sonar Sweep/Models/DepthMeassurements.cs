using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Sonar_Sweep.Models
{
    internal class DepthMeassurements
    {
        public readonly IReadOnlyList<int> Values;

        public DepthMeassurements(List<int> values)
        {
            Values = values;
        }

        internal static DepthMeassurements FromResource(Assembly assembly, string resourcePath)
        {
            var resourceStream = assembly.GetManifestResourceStream(resourcePath);

            if (resourceStream == null)
            {
                throw new FileNotFoundException($"No file found at location {resourcePath}");
            }

            var textStreamReader = new StreamReader(resourceStream);
            var text = textStreamReader.ReadToEnd();
            var values = text.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            return new DepthMeassurements(values);
        }

        internal static DepthMeassurements FromResource(string resourcePath)
        {
            var assembly = Assembly.GetExecutingAssembly();

            return FromResource(assembly, resourcePath);
        }
    }
}
