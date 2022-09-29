namespace Nt.Core
{
    /// <summary>
    /// The builder class of <see cref="SessionHoursList"/>.
    /// </summary>
    public class SessionHoursListBuilder : BaseSessionBuilder<SessionHoursList, SessionHoursListOptions,SessionHoursListBuilder>
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionHoursListBuilder"/> default instance.
        /// </summary>
        public SessionHoursListBuilder(SessionHoursListOptions options) : base(options)
        {
            Options = options;
        }

        #endregion

    }
}
