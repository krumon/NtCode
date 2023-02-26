using Nt.Core.Hosting;

namespace Nt.Scripts.Services
{
    /// <summary>
    /// Represents a <see cref="ISessionsManager"/> service.
    /// </summary>
    public interface ISessionsManager : IConfigurable, IRecalculableOnBarUpdate, IRecalculableOnSessionUpdate
    {
        ISessionsIterator SessionsIterator { get; }
        //ISessionsFilters Filters { get; }

        /// <summary>
        /// Indicates a new session begin.
        /// </summary>
        bool IsNewSession { get; set; }
        
    }
}
