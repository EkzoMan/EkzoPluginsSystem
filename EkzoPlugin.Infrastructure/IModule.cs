using System;

namespace EkzoPlugin.Infrastructure
{
    public interface IModule
    {
        /// <summary>
        /// Title of the plugin, can be used as a property to display on the user interface
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Name of the plugin, should be an unique name
        /// </summary>
        string Name { get; }
                
        /// <summary>
        /// Entry controller name
        /// </summary>
        string EntryControllerName { get; }
    }
}
