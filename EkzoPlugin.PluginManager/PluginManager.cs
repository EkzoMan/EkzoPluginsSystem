using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using EkzoPlugin.Infrastructure;

namespace EkzoPlugin.PluginManager
{
    public class PluginManager
    {
        /// <summary>
        /// Class builder
        /// </summary>
        public PluginManager()
        {
            Modules = new Dictionary<IModule, Assembly>();
        }


        private static PluginManager _current;
        
        /// <summary>
        /// Returns current initialized plugin manager object
        /// </summary>
        public static PluginManager Current 
        { 
	        get { return _current ?? (_current = new PluginManager()); }
        }

        /// <summary>
        /// List of loaded modules
        /// </summary>
    	internal Dictionary<IModule, Assembly> Modules { get; set; }

        /// <summary>
        /// Load modules
        /// </summary>
        /// <returns>List of loaded by manager modules</returns>
        public IEnumerable<IModule> GetModules()
        {
            return Modules.Select(m => m.Key).ToList();
        }

        /// <summary>
        /// Load modules
        /// </summary>
        /// <returns>List of loaded by manager modules</returns>
        public IModule GetModule(string name)
        {
            return GetModules().Where(m => m.Name == name).FirstOrDefault();
        }
    }
}
