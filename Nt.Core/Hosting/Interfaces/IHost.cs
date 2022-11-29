using Nt.Core.DependencyInjection;

namespace Nt.Core.Hosting
{
    public interface IHost
    {
        /// <summary>
        /// The ninjascript configured services.
        /// </summary>
        IServiceProvider Services { get; }

    }
}
