using RazorGenerator.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;

namespace EkzoPlugin.PluginManager
{
    public static class PluginBootstrapper
    {
        static PluginBootstrapper() { }

        /// <summary>
        /// Initialize plugin manager and register all plugins
        /// </summary>
        public static void Initialize()
        {
            IList<PrecompiledViewAssembly> assemblies = new List<PrecompiledViewAssembly>();
            foreach (var asmbl in PluginManager.Current.Modules.Values)
            {
                assemblies.Add(new PrecompiledViewAssembly(asmbl));
            }
            var engine = new CompositePrecompiledMvcEngine(assemblies.ToArray());
            ViewEngines.Engines.Insert(0, engine);
            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
        }

    }
}
