namespace Nt.Core
{
    /// <summary>
    /// The builder class of <see cref="SessionHours"/>.
    /// </summary>
    public class SessionHoursBuilder : BaseSessionBuilder<SessionHours, SessionHoursOptions,SessionHoursBuilder>
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionHoursBuilder"/> default instance.
        /// </summary>
        public SessionHoursBuilder(SessionHoursOptions options) : base(options)
        {
            Options = options;
        }

        #endregion

    }
}
