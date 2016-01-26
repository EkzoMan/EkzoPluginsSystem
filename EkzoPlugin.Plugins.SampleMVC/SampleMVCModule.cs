using System;
using System.Reflection;
using EkzoPlugin.Infrastructure;

namespace EkzoPlugin.Plugins.SampleMVC
{
    public class SampleMVCModule : IModule
    {
        public string Title
        {
            get { return "MVC Test"; }
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
    }
}