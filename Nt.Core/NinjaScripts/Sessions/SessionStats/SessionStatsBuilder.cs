using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// The builder class of <see cref="SessionStats"/>.
    /// </summary>
    public class SessionStatsBuilder : BaseSessionBuilder<SessionStats, SessionStatsOptions,SessionStatsBuilder>
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionStatsBuilder"/> default instance.
        /// </summary>
        public SessionStatsBuilder(SessionStatsOptions options) : base(options)
        {
            Options = options;
        }

        #endregion

    }
}
