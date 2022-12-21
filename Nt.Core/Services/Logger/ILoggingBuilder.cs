using Nt.Core.DependencyInjection;

namespace Nt.Core.Services
{
    public interface ILoggingBuilder
    {
        /// <summary>
        /// Gets the Microsoft.Extensions.DependencyInjection.IServiceCollection where 
        /// Logging services are configured.
        /// </summary>
        IServiceCollection Services { get; }

    }
}
