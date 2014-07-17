using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public static string IISVersion = GetIISVersion();

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

        private static string GetIISVersion()
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1/");
            HttpWebResponse myHttpWebResponse = null;
            try
            {
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            }
            catch (WebException ex)
            {
                myHttpWebResponse = (HttpWebResponse)ex.Response;
            }
            string WebServer = myHttpWebResponse.Headers["Server"];
            myHttpWebResponse.Close();

            if (WebServer.StartsWith("Microsoft-IIS/"))
            {
                WebServer = WebServer.Substring(14, 3);
            }
            return WebServer;
        }


    }
}

//Project: EkzoPluginSystem  
//Author:  Alexey Misyaign
//Licence: MIT Licence