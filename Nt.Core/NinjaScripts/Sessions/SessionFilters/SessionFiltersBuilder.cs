using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// The builder class of <see cref="SessionFilters"/>.
    /// </summary>
    public class SessionFiltersBuilder : BaseSessionBuilder<SessionFilters, SessionFiltersOptions,SessionFiltersBuilder>
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionFiltersBuilder"/> default instance.
        /// </summary>
        public SessionFiltersBuilder(SessionFiltersOptions options) : base(options)
        {
            Options = options;
        }

        #endregion

    }
}
