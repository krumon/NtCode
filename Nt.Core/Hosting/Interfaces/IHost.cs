using Nt.Core.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Represents the properties and methods to built a ninjascript host.
    /// </summary>
    public interface IHost
    {
        /// <summary>
        /// The ninjascript configured services.
        /// </summary>
        IServiceProvider Services { get; }

        /// <summary>
        /// Method to configure the service in the ninjascript.
        /// </summary>
        /// <param name="name">The name of the caller method.</param>
        /// <param name="ninjascriptObjects">The ninjascripts objects necesary to configure the host services.</param>
        void Configure([CallerMemberName] string name = "", params object[] ninjascriptObjects);

        /// <summary>
        /// Method to configure the service in the ninjascript when the data is loaded.
        /// </summary>
        /// <param name="ninjascriptObjects">The ninjascripts objects necesary to configure the host services.</param>
        void DataLoaded(params object[] ninjascriptObjects);

    }
}
