using Nt.Core.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using IServiceProvider = Nt.Core.DependencyInjection.IServiceProvider;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Represents the properties and methods to built a ninjascript host.
    /// </summary>
    public interface IHost : IDisposable
    {
        #region Original interface

        //
        // Resumen:
        //     The programs configured services.
        IServiceProvider Services { get; }

        //
        // Resumen:
        //     Start the program.
        //
        // Parámetros:
        //   cancellationToken:
        //     Used to abort program start.
        //
        // Devuelve:
        //     A System.Threading.Tasks.Task that will be completed when the Microsoft.Extensions.Hosting.IHost
        //     starts.
        //Task StartAsync(CancellationToken cancellationToken = default(CancellationToken));

        //
        // Resumen:
        //     Attempts to gracefully stop the program.
        //
        // Parámetros:
        //   cancellationToken:
        //     Used to indicate when stop should no longer be graceful.
        //
        // Devuelve:
        //     A System.Threading.Tasks.Task that will be completed when the Microsoft.Extensions.Hosting.IHost
        //     stops.
        //Task StopAsync(CancellationToken cancellationToken = default(CancellationToken));
        ///// <summary>
        ///// Indicates if the session is updated.
        ///// </summary>
        //bool? IsInNewSession { get; }

        #endregion

        #region Added properties and methods

        /// <summary>
        /// Gets the sessions configured by the user.
        /// </summary>
        ISessionsService Sessions { get; }

        /// <summary>
        /// Configure the hosted services of the Host.
        /// </summary>
        /// <param name="ninjascriptObjects">The ninjascript objects necesary to configure the service.</param>
        void Configure(params object[] ninjascriptObjects);

        /// <summary>
        /// Configure the hosted services of the Host when the ninjascript data is loaded.
        /// </summary>
        /// <param name="ninjascriptObjects">The ninjascript objects necesary to configure the service.</param>
        void DataLoaded(params object[] ninjascriptObjects);

        /// <summary>
        /// Execute the <see cref="IOnBarUpdateService"/> services.
        /// </summary>
        void OnBarUpdate();

        /// <summary>
        /// Execute the <see cref="IOnMarketDataService"/> services.
        /// </summary>
        void OnMarketData();

        void OnBarUpdate(Action<object> print = null);

        /// <summary>
        /// Execute the <see cref="IOnSessionUpdateService"/> services.
        /// </summary>
        void OnSessionUpdate(Action<object> print = null);

        #endregion
    }
}
