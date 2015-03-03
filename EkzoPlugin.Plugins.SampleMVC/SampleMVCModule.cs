using System;
using System.Reflection;
using EkzoPlugin.Infrastructure;

namespace EkzoPlugin.Plugins.SampleMVC
{
    public class SampleMVCModule : IModule
    {
        public string Title
        {
            get { return "Simple Plugin"; }
        }

        public string Name
        {
            get { return Assembly.GetAssembly(GetType()).GetName().Name; }
        }

        public Version Version
        {
            get { return new Version(1, 0, 0, 0); }
        }

        public string EntryControllerName
        {
            get { return "SampleMVC"; }
        }

        public void Install()
        {
            throw new NotImplementedException();
        }

        public void Uninstall()
        {
            throw new NotImplementedException();
        }
    }
}