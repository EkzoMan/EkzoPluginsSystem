namespace EkzoPlugin.PluginManager
{
    public static class PluginBootstrapper
    {
        static PluginBootstrapper()
        {
	        
        }

        /// <summary>
        /// Initialize plugin manager and register all plugins
        /// </summary>
    	public static void Initialize()
        {
            foreach (var asmbl in PluginManager.Current.Modules.Values)
	        {
                BoC.Web.Mvc.PrecompiledViews.ApplicationPartRegistry.Register(asmbl);
	        }
        }
    }
}
