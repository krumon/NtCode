using Nt.Core.Hosting;
using Nt.Scripts.Services;

namespace Nt.Scripts.Indicators
{
    /// <summary>
    /// Represents a <see cref="ISessionsIndicator"/> service.
    /// </summary>
    public interface ISessionsIndicator : IConfigurable, IRecalculableOnBarUpdate, IRecalculableOnSessionChanged
    {
        ISessionsIterator SessionsIterator { get; }
        //ISessionsFilters Filters { get; }

        /// <summary>
        /// Indicates a new session begin.
        /// </summary>
        bool IsNewSession { get; set; }
        
    }
}
