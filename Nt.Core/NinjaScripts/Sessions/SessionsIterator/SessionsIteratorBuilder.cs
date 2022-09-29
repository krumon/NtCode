namespace Nt.Core
{
    /// <summary>
    /// The builder class of <see cref="SessionsIterator"/>.
    /// </summary>
    public class SessionsIteratorBuilder : BaseSessionBuilder<SessionsIterator, SessionsIteratorOptions,SessionsIteratorBuilder>
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionHoursBuilder"/> default instance.
        /// </summary>
        public SessionsIteratorBuilder(SessionsIteratorOptions options) : base(options)
        {
            Options = options;
        }

        #endregion

    }
}
