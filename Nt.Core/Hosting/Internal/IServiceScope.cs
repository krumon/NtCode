namespace Nt.Core.Hosting.Internal
{
    /// <summary>
    /// The System.IDisposable.Dispose method ends the scope lifetime. Once Dispose is called, any scoped services 
    /// that have been resolved from IServiceScope.ServiceProvider will be disposed.
    /// </summary>
    internal interface IServiceScope
    {

        /// <summary>
        /// The <see cref="INinjascriptServiceProvider"/> used to resolve dependencies from the scope.
        /// </summary>
        INinjascriptServiceProvider ServiceProvider { get; }

    }
}