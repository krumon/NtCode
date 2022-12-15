using Nt.Core.Hosting;
using Nt.Core.Options;

namespace Nt.Core.Services
{
    /// <summary>
    /// Represents the properties and methods to create a default implementation of <see cref="SessionsFiltersService"/>.
    /// </summary>
    public interface ISessionsFiltersService
    {
        SessionsFiltersOptions Options { get; }
    }
}
