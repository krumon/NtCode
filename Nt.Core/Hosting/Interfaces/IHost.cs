using Nt.Core.Services;
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
        /// Configure the hosted services of the Host.
        /// </summary>
        /// <param name="ninjascriptObjects">The ninjascript objects necesary to configure the service.</param>
        void Configure(object[] ninjascriptObjects);

        /// <summary>
        /// Configure the hosted services of the Host when the ninjascript data is loaded.
        /// </summary>
        /// <param name="ninjascriptObjects">The ninjascript objects necesary to configure the service.</param>
        void DataLoaded(object[] ninjascriptObjects);

        /// <summary>
        /// Execute the <see cref="IOnBarUpdateService"/> services of the host.
        /// </summary>
        void OnBarUpdate();

        /// <summary>
        /// Execute the <see cref="IMarketDataService"/> services of the host.
        /// </summary>
        void MarketData();

    }
}
