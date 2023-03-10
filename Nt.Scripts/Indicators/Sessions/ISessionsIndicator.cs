using Nt.Scripts.Ninjascripts;

namespace Nt.Scripts.Indicators
{
    /// <summary>
    /// Represents a <see cref="ISessionsIndicator"/> service.
    /// </summary>
    public interface ISessionsIndicator : INinjascript, IRecalculableOnBarUpdate, IRecalculableOnSessionChanged
    {
        ISessionsIterator SessionsIterator { get; }
        //ISessionsFilters Filters { get; }

        /// <summary>
        /// Indicates a new session begin.
        /// </summary>
        bool IsNewSession { get; set; }
        
    }
}
