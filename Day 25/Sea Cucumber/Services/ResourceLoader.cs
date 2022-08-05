using System.IO;
using System.Reflection;

namespace Sea_Cucumber.Services
{
    internal static class ResourceLoader
    {
        internal static string GetDataFromResource(string path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceStream = assembly.GetManifestResourceStream(path);

            if (resourceStream == null)
            {
                throw new FileNotFoundException($"No file found at location {path}");
            }

            var textStreamReader = new StreamReader(resourceStream);
            var text = textStreamReader.ReadToEnd();

            return text;
        }
    }
}
