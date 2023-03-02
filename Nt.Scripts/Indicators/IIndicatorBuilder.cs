using Nt.Core.DependencyInjection;

namespace Nt.Scripts.Indicators
{
    public interface IIndicatorBuilder
    {

        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where indicator services are configured.
        /// </summary>
        IServiceCollection Services { get; }

    }
}
