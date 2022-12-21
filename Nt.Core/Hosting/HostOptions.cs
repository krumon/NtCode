using Nt.Core.DependencyInjection;
using Nt.Core.Options;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Represents the host options.
    /// </summary>
    public class HostOptions
    {
        /// <inheritdoc/>
        public static HostOptions Default => new HostOptions
        {
            IsInDesignMode = false
        };
        
        /// <summary>
        /// Indicates if the host is in design mode.
        /// </summary>
        public bool IsInDesignMode { get; set; }

        /// <summary>
        /// Indicates if the host use <see cref="Services.ISessionsIteratorService"/>.
        /// </summary>
        public bool IncludeSessions{ get; set; } = true;

    }
}
