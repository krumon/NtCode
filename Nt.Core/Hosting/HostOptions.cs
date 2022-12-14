using Nt.Core.DependencyInjection;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Represents the host options.
    /// </summary>
    public class HostOptions : IOptions<HostOptions>
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
        /// Indicates if the host use <see cref="Services.ISessionService"/>.
        /// </summary>
        public bool UseSessionsScript{ get; set; } = true;

        /// <inheritdoc/>
        public HostOptions Value => new HostOptions
        {
            IsInDesignMode = false
        };
    }
}
