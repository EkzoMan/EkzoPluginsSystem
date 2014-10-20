using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;
using EkzoPlugin.Infrastructure;

[assembly: PreApplicationStartMethod(typeof(EkzoPlugin.PluginManager.PreApplicationInit), "InitializePlugins")]
namespace EkzoPlugin.PluginManager
{
    public class PreApplicationInit
    {
        /// <summary>
        /// The source plugin folder from which to copy from
        /// </summary>
        /// <remarks>
        /// This folder can contain sub folders to organize plugin types
        /// </remarks>
        private static DirectoryInfo PluginFolder;

        /// <summary>
        /// Initialize method that registers all plugins
        /// </summary>
        public static void InitializePlugins()
        {
            string pluginsDir = System.Configuration.ConfigurationSettings.AppSettings["pluginsDirectory"] == null ? "~/plugins" : System.Configuration.ConfigurationSettings.AppSettings["pluginsDirectory"];
            if (!pluginsDir.StartsWith("~/")) pluginsDir = "~/" + pluginsDir;
            pluginsDir = pluginsDir.Replace("//", "/");

            string pluginsPath = HostingEnvironment.MapPath(pluginsDir);
            if (pluginsPath == null)
                throw new DirectoryNotFoundException("plugins");

            PluginFolder = new DirectoryInfo(pluginsPath);

           var assemblies = PluginFolder.GetFiles("*.dll", SearchOption.AllDirectories)
                    .Select(x => AssemblyName.GetAssemblyName(x.FullName));

            foreach (var assembly in assemblies)
            {
                try
                {
                    var currentAssambly = AppDomain.CurrentDomain.Load(assembly);
                    Type type = currentAssambly.GetTypes().Where(t => t.GetInterface(typeof(IModule).Name) != null).FirstOrDefault();
                    if (type != null)
                    {
                        //Add the plugin as a reference to the application
                        BuildManager.AddReferencedAssembly(currentAssambly);

                        //Add the modules to the PluginManager to manage them later
                        var module = (IModule)Activator.CreateInstance(type);
                        PluginManager.Current.Modules.Add(module, currentAssambly);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}
