namespace Loupedeck.IftttPlugin
{
    using System;
    using System.IO;
    using System.Reflection;
    public class IftttPluginInstaller
    {
        public static Boolean Install()
        {
            // Here we ensure the plugin data directory is there.
            var helperFunction = new IftttPlugin();
            var pluginDataDirectory = helperFunction.GetPluginDataDirectory();
            if (!IoHelpers.EnsureDirectoryExists(pluginDataDirectory))
            {
                return false;
            }

            // Now we put a template configuration file
            var filePath = System.IO.Path.Combine(pluginDataDirectory, System.IO.Path.Combine(pluginDataDirectory, "events.json"));
            if (File.Exists(filePath))
            {
                return true;
            }
            ResourceReader.CreateFileFromResource("Loupedeck.IftttPlugin.events.json", filePath);
            return true;
        }
    }
    public class ResourceReader
    {
        // to read the file as a Stream
        public static Stream GetResourceStream(String resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream resourceStream = assembly.GetManifestResourceStream(resourceName);
            return resourceStream;
        }

        // to save the resource to a file
        public static void CreateFileFromResource(String resourceName, String path)
        {
            Stream resourceStream = GetResourceStream(resourceName);
            if (resourceStream != null)
            {
                using (Stream input = resourceStream)
                {
                    using (Stream output = File.Create(path))
                    {
                        input.CopyTo(output);
                    }
                }
            }
        }
    }
}
