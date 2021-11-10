using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WebApp_AccessDenied
{
    public class PluginManager
    {
        private readonly DirectoryInfo _pluginsDirectory;
        private DirectoryInfo _currentWorkspace;

        public PluginManager(string pluginsDirectory)
        {
            _pluginsDirectory = new DirectoryInfo(pluginsDirectory);
            _pluginsDirectory.Create();
        }

        public Assembly[] LoadCustomAssemblies()
        {
            var assemblyFiles = _currentWorkspace.GetFiles("*.dll");
            var customAssemblyArray = new Assembly[assemblyFiles.Length];
            for (var i = 0; i < assemblyFiles.Length; i++)
            {
                var currentAssemblyPath = assemblyFiles[i];
                try
                {
                    var assembly = Assembly.LoadFrom(currentAssemblyPath.FullName);
                    assembly.GetTypes();
                    customAssemblyArray[i] = assembly;
                }
                catch (Exception e)
                {
                    // log
                    throw;
                }
            }

            return customAssemblyArray;
        }

        public string CreateNewWorkspace()
        {
            _currentWorkspace = _pluginsDirectory.CreateSubdirectory(DateTime.UtcNow.Ticks.ToString());

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "WebApp_AccessDenied.Plugin.dll";
            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            using var fileStream = File.Create(Path.Combine(_currentWorkspace.FullName, "Plugin.dll"));
            stream.CopyTo(fileStream);

            return _currentWorkspace.FullName;
        }

        public void ClearOldWorkspace()
        {
            var directoriesToDelete = _pluginsDirectory.EnumerateDirectories().Where(info => info.Name != _currentWorkspace.Name).ToArray();
            foreach (var info in directoriesToDelete)
            {
                info.Delete(true);
            }
        }
    }
}