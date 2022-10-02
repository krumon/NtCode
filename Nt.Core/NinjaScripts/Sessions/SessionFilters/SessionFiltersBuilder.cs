using System;

namespace Nt.Core
{
    /// <summary>
    /// The builder class of <see cref="SessionFilters"/>.
    /// </summary>
    public class SessionFiltersBuilder : BaseSessionBuilder<SessionFilters, SessionFiltersOptions,SessionFiltersBuilder>, ISessionFiltersBuilder
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionFiltersBuilder"/> default instance.
        /// </summary>
        /// <param name="script">The script to build.</param>
        public SessionFiltersBuilder(INinjascript script) : base(script)
        {
        }

        #endregion

        #region Configuration methods

        ///// <summary>
        ///// Configure the ninjascript with the options passed by <see cref="Action{SessionFiltersOptions}"/> delegate.
        ///// </summary>
        ///// <param name="options">The options to configure the ninjascript.</param>
        ///// <returns>The builder to continue construction the ninjascript.</returns>
        //public SessionFiltersBuilder Configure(Action<SessionFiltersOptions> options) =>
        //    (SessionFiltersBuilder)Configure<SessionFilters, SessionFiltersOptions>(options);

        ///// <summary>
        ///// Configure the ninjascript with the options passed by <see cref="SessionFiltersOptions"/> object.
        ///// </summary>
        ///// <param name="options">The options to configure the ninjascript.</param>
        ///// <returns>The builder to continue construction the ninjascript.</returns>
        //public SessionFiltersBuilder Configure(SessionFiltersOptions options) =>
        //    (SessionFiltersBuilder)Configure<SessionFilters,SessionFiltersOptions>(options);

        #endregion

    }
}
