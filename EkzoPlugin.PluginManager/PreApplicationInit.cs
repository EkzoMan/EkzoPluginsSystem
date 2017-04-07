using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;
using EkzoPlugin.Infrastructure;
using System.Collections.Generic;

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



            string cachePath = initShadowCopyDirectory(PluginFolder.FullName);
            AppDomain.CurrentDomain.SetCachePath(cachePath);
            if (!System.IO.Directory.Exists(cachePath)) System.IO.Directory.CreateDirectory(cachePath, new System.Security.AccessControl.DirectorySecurity());

            //Set shadowcopy to prevent locking plugins
            AppDomain.CurrentDomain.SetShadowCopyPath(AppDomain.CurrentDomain.BaseDirectory);
            AppDomain.CurrentDomain.SetShadowCopyFiles();


            var libs = PluginFolder.GetFiles("*.dll", SearchOption.AllDirectories)
                      .Select(x => AssemblyName.GetAssemblyName(x.FullName));

            IList<System.Reflection.AssemblyName> assemblies = new List<AssemblyName>();

            foreach (var dll in libs)
            {
                try
                {
                    if (Assembly.Load(dll).GetTypes().Any(o => o.GetInterface(typeof(IModule).Name) != null))
                    {
                        //Add new assembly to list
                        if (!assemblies.Any(o => o.Name == dll.Name))
                            assemblies.Add(dll);
                        //Replace assembly with higher version copy
                        else if (assemblies.Any(o => o.Name == dll.Name && o.Version < dll.Version))
                        {
                            assemblies.Remove(assemblies.FirstOrDefault(o => o.Name == dll.Name));
                            assemblies.Add(dll);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    foreach (var loaderException in (ex as System.Reflection.ReflectionTypeLoadException).LoaderExceptions)
                        System.Diagnostics.Debug.WriteLine(loaderException.Message);
                }
            }


            foreach (var assembly in assemblies)
            {
                try
                {
                    //Load assabmly from bytes array to prevent file lock
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
                    foreach (var loaderException in (ex as System.Reflection.ReflectionTypeLoadException).LoaderExceptions)
                        System.Diagnostics.Debug.WriteLine(loaderException.Message);
                }
            }
        }

        /// <summary>
        /// Get bytes from specified file
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns>Byte array</returns>
        private static byte[] getFileBytes(string path)
        {
            return System.IO.File.ReadAllBytes(path);
        }

        /// <summary>
        /// Returns assably file path
        /// </summary>
        /// <param name="assembly">Assembly</param>
        /// <returns>Path to assembly</returns>
        private static string getAssemblyPath(AssemblyName assembly)
        {
            string path = (new System.Uri(assembly.CodeBase)).AbsolutePath;
            return path;
        }

        /// <summary>
        /// Initialize shadow copy directory
        /// </summary>
        /// <param name="pluginsPath">Path to plugins directory</param>
        /// <returns>Path to shadow copy directory</returns>
        private static string initShadowCopyDirectory(string pluginsPath)
        {
            string result = System.IO.Path.Combine(pluginsPath, "shadowCopy");
            if (!System.IO.Directory.Exists(result)) System.IO.Directory.CreateDirectory(result);
            return result;
        }

    }
}
