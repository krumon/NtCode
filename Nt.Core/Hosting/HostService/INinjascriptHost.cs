using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nt.Core.Hosting
{
    public interface INinjascriptHost : IDisposable
    {
        /// <summary>
        /// The ninjascript configured services.
        /// </summary>
        INinjascriptServiceProvider Services { get; }

        /// <summary>
        /// Start the ninjascript.
        /// </summary>
        /// <param name="cancellationToken">Used to abort ninjascript start.</param>
        /// <returns> A <see cref="Task"/> that will be completed when the ninjascript starts.</returns>
        Task StartAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Attempts to gracefully stop the ninjascript.
        /// </summary>
        /// <param name="cancellationToken">Used to indicate when stop should no longer be graceful.</param>
        /// <returns>A <see cref="Task"/> that will be completed when the ninjascript stops.</returns>
        Task StopAsync(CancellationToken cancellationToken = default(CancellationToken));

    }
}
