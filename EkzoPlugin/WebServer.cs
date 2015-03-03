using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.Administration;
using Microsoft.Web.Management.Server;

namespace EkzoPlugin.Web
{
    public static class WebServer
    {
        [ModuleServiceMethod(PassThrough = true)]
        public static void RestartAppPool()
        {
            // Use an ArrayList to transfer objects to the client.
            ArrayList arrayOfApplicationBags = new ArrayList();

            ServerManager serverManager = new ServerManager();
            ApplicationPoolCollection applicationPoolCollection = serverManager.ApplicationPools;
            foreach (ApplicationPool applicationPool in applicationPoolCollection)
            {
                PropertyBag applicationPoolBag = new PropertyBag();
                arrayOfApplicationBags.Add(applicationPoolBag);
                
                if (applicationPool.Name == HttpContext.Current.Request.ServerVariables["APP_POOL_ID"])
                {
                    applicationPool.Recycle();
                    applicationPool.Stop();
                    applicationPool.Start();
                }
            }
            // CommitChanges to persist the changes to the ApplicationHost.config.
            serverManager.CommitChanges();
        }
    }
}