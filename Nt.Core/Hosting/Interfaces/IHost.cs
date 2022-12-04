using Nt.Core.DependencyInjection;
using System;
using IServiceProvider = Nt.Core.DependencyInjection.IServiceProvider;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Represents the properties and methods to built a ninjascript host.
    /// </summary>
    public interface IHost : IDisposable
    {
        /// <summary>
        /// The ninjascript configured services.
        /// </summary>
        IServiceProvider Services { get; }

        /// <summary>
        /// Start the host.
        /// </summary>
        void Start(object[] ninjascriptObjects);

        /// <summary>
        /// Attempts to gracefully stop the host.
        /// </summary>
        void Stop();

        /// <summary>
        /// Execute all services of the same type.
        /// </summary>
        /// <typeparam name="T">The type of the service to execute.</typeparam>
        void ExecuteServices<T>();


    }
}
