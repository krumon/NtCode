using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Represents the properties and methods to built a ninjascript host.
    /// </summary>
    public interface IHost : IDisposable
    {

        /// <summary>
        /// The programs configured services.
        /// </summary>
        IServiceProvider Services { get; }

        /// <summary>
        /// Start the program.
        /// </summary>
        /// <param name="cancellationToken">Used to abort program start.</param>
        /// <returns>A System.Threading.Tasks.Task that will be completed when the <see cref="IHost"/> starts.</returns>
        Task StartAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Attempts to gracefully stop the program.
        /// </summary>
        /// <param name="cancellationToken">Used to indicate when stop should no longer be graceful.</param>
        /// <returns>A System.Threading.Tasks.Task that will be completed when the <see cref="IHost"/> stops.</returns>
        Task StopAsync(CancellationToken cancellationToken = default);

        //void LogInformation(string message);

        ///// <summary>
        ///// Indicates if the session is updated.
        ///// </summary>
        //bool? IsInNewSession { get; }

        ///// <summary>
        ///// Gets the sessions configured by the user.
        ///// </summary>
        //ISessionsService Sessions { get; }

        ///// <summary>
        ///// Configure the hosted services of the Host.
        ///// </summary>
        ///// <param name="ninjascriptObjects">The ninjascript objects necesary to configure the service.</param>
        //void Configure(params object[] ninjascriptObjects);

        ///// <summary>
        ///// Configure the hosted services of the Host when the ninjascript data is loaded.
        ///// </summary>
        ///// <param name="ninjascriptObjects">The ninjascript objects necesary to configure the service.</param>
        //void DataLoaded(params object[] ninjascriptObjects);

        ///// <summary>
        ///// Execute the <see cref="IOnBarUpdateService"/> services.
        ///// </summary>
        //void OnBarUpdate();

        ///// <summary>
        ///// Execute the <see cref="IOnMarketDataService"/> services.
        ///// </summary>
        //void OnMarketData();

        //void OnBarUpdate(Action<object> print = null);

        ///// <summary>
        ///// Execute the <see cref="IOnSessionUpdateService"/> services.
        ///// </summary>
        //void OnSessionUpdate(Action<object> print = null);

    }
}
