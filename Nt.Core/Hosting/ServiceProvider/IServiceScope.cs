using System;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// he System.IDisposable.Dispose method ends the scope lifetime. Once Dispose iscalled, any scoped services 
    /// that have been resolved from Microsoft.Extensions.DependencyInjection.IServiceScope.ServiceProviderwill be disposed.
    /// </summary>
    internal interface IServiceScope
    {

        /// <summary>
        /// The <see cref="INinjascriptServiceProvider"/> used to resolve dependencies from the scope.
        /// </summary>
        IServiceProvider ServiceProvider { get; }

    }
}