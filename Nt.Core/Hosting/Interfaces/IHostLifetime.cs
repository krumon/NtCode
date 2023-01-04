﻿using System.Threading;
using System.Threading.Tasks;

namespace Nt.Core.Hosting
{
    public interface IHostLifetime
    {
        //
        // Resumen:
        //     Called at the start of Microsoft.Extensions.Hosting.IHost.StartAsync(System.Threading.CancellationToken)
        //     which will wait until it's complete before continuing. This can be used to delay
        //     startup until signaled by an external event.
        //
        // Parámetros:
        //   cancellationToken:
        //     Used to abort program start.
        //
        // Devuelve:
        //     A System.Threading.Tasks.Task.
        Task WaitForStartAsync(CancellationToken cancellationToken);

        //
        // Resumen:
        //     Called from Microsoft.Extensions.Hosting.IHost.StopAsync(System.Threading.CancellationToken)
        //     to indicate that the host is stopping and it's time to shut down.
        //
        // Parámetros:
        //   cancellationToken:
        //     Used to indicate when stop should no longer be graceful.
        //
        // Devuelve:
        //     A System.Threading.Tasks.Task.
        Task StopAsync(CancellationToken cancellationToken);
    }
}
