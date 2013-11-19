using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BoC.Web.Mvc.PrecompiledViews;
using EkzoPlugin.PluginManager;

namespace EkzoPlugin.Web
{

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Register embedded vews
            PluginBootstrapper.Initialize();

            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}