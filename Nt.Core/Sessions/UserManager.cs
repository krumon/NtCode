using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// Represents consts, fields and properties of the Ninjatrader user configuration.
    /// </summary>
    public class UserManager : NtScript
    {

        #region Private members

        protected UserSessionIterator userTradingHours;
        protected UserAccount userAccount;
        protected UserSettings userSettings;
        protected UserSessions typicalSessions;
        protected UserSessions customSessions;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a default instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="sessionIterator"></param>
        /// <param name="bars"></param>
        /// <param name="addAccounts"></param>
        /// <param name="addSettings"></param>
        /// <param name="addUserSessionChangedEvent"></param>
        public UserManager(
            NinjaScriptBase ninjascript, 
            SessionIterator sessionIterator, 
            Bars bars, 
            bool addAccounts = false,
            bool addSettings = false,
            bool addUserSessionChangedEvent = true
            )
        {

            this.userAccount = addAccounts ? new UserAccount() : null;
            this.userSettings = addSettings ? new UserSettings() : null;
            this.userTradingHours = addUserSessionChangedEvent ? new UserSessionIterator(ninjascript, bars, sessionIterator) : null;
        }

        #endregion

        #region functionality methods

        public UserManager UseTypicalSessions(TypicalSessionsConfigure configure = null) // Add the options by parameters (includeAmericanSession, includeAssianSession...)
        {
            if (typicalSessions == null)
            {
                // Create the user session object
                typicalSessions = new UserSessions(configure);

                // Add the delegate
                userTradingHours.SessionChanged += OnUserSessionChanged;
            }

            // return the object with the user sessions implementation
            return this;
        }

        #endregion

        #region handler methods

        public override void OnBarUpdate()
        {
            if (userTradingHours != null)
                userTradingHours.OnBarUpdate();
        }

        public override void OnMarketData()
        {
            if (userTradingHours != null)
                userTradingHours.OnMarketData();
        }

        /// <summary>
        /// Changed any object or property when the session changed.
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnUserSessionChanged(UserSessionChangedEventArgs e)
        {
            typicalSessions.OnUserSessionChanged(e);

            //currentSession = new UserSession();
            //currentSession.UpdateUserSessions(e,SessionsCount);
            //if (sessions == null)
            //    sessions = new List<UserSession>();
            //sessions.Add(currentSession);
        }

        public override void Dispose()
        {
            userTradingHours.SessionChanged -= OnUserSessionChanged;
        }

        #endregion

    }
}
