using Nt.Core.DependencyInjection;

namespace Nt.Core.Ninjascripts
{
    public interface INinjascriptsBuilder
    {
        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where ninjascript services are configured.
        /// </summary>
        IServiceCollection Services { get; }

    }
}
