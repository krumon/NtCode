using NinjaTrader.Data;
using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// Represents consts, fields and properties of the Ninjatrader user configuration.
    /// </summary>
    public class SessionsManager : NtScript
    {

        #region Fields

        public SessionsIterator sessionsIterator;
        public UserSessions generalSessions;
        public UserSessions customSessions;

        protected UserAccount userAccount;
        protected UserSettings userSettings;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a default instance of the <see cref="SessionsManager"/> class.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="sessionIterator"></param>
        /// <param name="bars"></param>
        /// <param name="addAccounts"></param>
        /// <param name="addSettings"></param>
        /// <param name="addUserSessionChangedEvent"></param>
        public SessionsManager(
            NinjaScriptBase ninjascript, 
            Bars bars, 
            SessionIterator sessionIterator, 
            bool addAccounts = false,
            bool addSettings = false,
            bool addUserSessionChangedEvent = true
            )
        {

            this.userAccount = addAccounts ? new UserAccount() : null;
            this.userSettings = addSettings ? new UserSettings() : null;
            this.sessionsIterator = addUserSessionChangedEvent ? new SessionsIterator(ninjascript, bars, sessionIterator) : null;
        }

        #endregion

        #region functionality methods

        public SessionsManager UseGeneralSessions(GeneralSessionsConfigure configure = null) // Add the options by parameters (includeAmericanSession, includeAssianSession...)
        {
            if (generalSessions == null)
            {
                // Create the user session object
                generalSessions = new UserSessions(configure);

                // Add the delegate
                sessionsIterator.SessionChanged += OnUserSessionChanged;
            }

            // return the object with the user sessions implementation
            return this;
        }

        #endregion

        #region handler methods

        public override void OnBarUpdate()
        {
            if (sessionsIterator != null)
                sessionsIterator.OnBarUpdate();
        }

        public override void OnMarketData()
        {
            if (sessionsIterator != null)
                sessionsIterator.OnMarketData();
        }

        /// <summary>
        /// Changed any object or property when the session changed.
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnUserSessionChanged(SessionChangedEventArgs e)
        {
            generalSessions.OnUserSessionChanged(e);

            //currentSession = new UserSession();
            //currentSession.UpdateUserSessions(e,SessionsCount);
            //if (sessions == null)
            //    sessions = new List<UserSession>();
            //sessions.Add(currentSession);
        }

        public override void Dispose()
        {
            sessionsIterator.SessionChanged -= OnUserSessionChanged;
        }

        #endregion

    }
}
