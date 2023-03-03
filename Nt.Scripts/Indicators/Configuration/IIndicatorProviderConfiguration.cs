using Nt.Core.Configuration;

namespace Nt.Scripts.Indicators.Configuration
{
    /// <summary>
    /// Allows access to configuration section associated with indicator provider
    /// </summary>
    /// <typeparam name="T">Type of indicator provider to get configuration for.</typeparam>
    public interface IIndicatorProviderConfiguration<T>
    {
        /// <summary>
        /// Configuration section for requested indicator provider
        /// </summary>
        IConfiguration Configuration { get; }
    }
}
