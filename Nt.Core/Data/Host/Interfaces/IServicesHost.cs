namespace Nt.Core.Data
{
    public interface IServicesHost //: IDisposable
    {
        /// <summary>
        /// The ninjascript configured service provider.
        /// </summary>
        IServiceProvider[] Services { get; }

        /// <summary>
        /// Gets the service provider object with the specific key.
        /// </summary>
        /// <param name="key">The key of the service provider.</param>
        /// <returns>The object of the ninjascript service provider or null if there aren't object.</returns>
        IServiceProvider GetServiceProvider(string key);

        ///// <summary>
        ///// Start the ninjascript.
        ///// </summary>
        ///// <param name="cancellationToken">Used to abort ninjascript start.</param>
        ///// <returns> A <see cref="Task"/> that will be completed when the ninjascript starts.</returns>
        //Task StartAsync(CancellationToken cancellationToken = default);

        ///// <summary>
        ///// Attempts to gracefully stop the ninjascript.
        ///// </summary>
        ///// <param name="cancellationToken">Used to indicate when stop should no longer be graceful.</param>
        ///// <returns>A <see cref="Task"/> that will be completed when the ninjascript stops.</returns>
        //Task StopAsync(CancellationToken cancellationToken = default(CancellationToken));

    }
}
