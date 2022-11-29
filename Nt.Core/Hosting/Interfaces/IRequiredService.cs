using Nt.Core.Data;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Defines any required service.
    /// </summary>
    public interface IRequiredService : IHostedService<RequiredServiceType>
    {
    }
}
