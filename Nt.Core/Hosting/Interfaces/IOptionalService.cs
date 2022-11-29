using Nt.Core.Data;

namespace Nt.Core.Hosting
{

    /// <summary>
    /// Defines any optional configure service.
    /// </summary>
    public interface IOptionalService : IHostedService<OptionalServiceType>
    {
    }
}
