using Nt.Core.DependencyInjection;

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

    }
}
