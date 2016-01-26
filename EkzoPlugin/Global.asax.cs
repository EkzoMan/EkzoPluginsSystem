using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EkzoPlugin.PluginManager;

namespace EkzoPlugin.CoreSite.Web
{

    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            //Register embedded vews
            PluginBootstrapper.Initialize();

            //AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}