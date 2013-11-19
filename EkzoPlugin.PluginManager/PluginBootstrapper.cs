namespace EkzoPlugin.PluginManager
{
    public static class PluginBootstrapper
    {
        static PluginBootstrapper()
        {
	        
        }

    	public static void Initialize()
        {
            foreach (var asmbl in PluginManager.Current.Modules.Values)
	        {
                BoC.Web.Mvc.PrecompiledViews.ApplicationPartRegistry.Register(asmbl);
	        }
        }
    }
}
