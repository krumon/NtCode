using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// Represents consts, fields and properties of the Ninjatrader user configuration.
    /// </summary>
    public class SessionsManager : NtScript
    {

        #region Consts


        #endregion

        #region Private members

        ///// <summary>
        ///// The ninjascript parent of the class.
        ///// </summary>
        //private NinjaScriptBase ninjascript;

        ///// <summary>
        ///// The bars of the chart control.
        ///// </summary>
        //private Bars bars;

        ///// <summary>
        ///// The session iterator to store the actual and next session data.
        ///// </summary>
        //private SessionIterator sessionIterator;

        //private NtTradingHours tradingHours = null;

        /// <summary>
        /// The session hours structure core.
        /// </summary>
        private NtTradingHours tradingHours;

        /// <summary>
        /// <see cref="NtTradingHours"/> sorted collection.
        /// </summary>
        private List<NtTradingHours> tradingHoursList = new List<NtTradingHours>();

        protected UserAccount userAccount;
        protected UserSession userSession;
        protected UserSettings userSettings;

        #endregion

        #region Fields

        #endregion

        #region Properties

        //public NtTradingHours TradingHours { get;private set; }
        //public UserAccount UserAccount { get;private set; }
        //public UserSession UserSession { get;private set; }
        //public UserSettings UserSettings { get;private set; }

        /// <summary>
        /// The number of main sessions stored.
        /// </summary>
        public int UserSessionsCount => tradingHoursList.Count;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a default instance of the <see cref="SessionsManager"/> class.
        /// </summary>
        public SessionsManager(
            NinjaScriptBase ninjascript, 
            SessionIterator sessionIterator, 
            Bars bars, 
            bool addUserAccounts = false,
            bool addUserSettings = false,
            bool addUserSessions = true
            )
        {

            this.userAccount = addUserAccounts ? new UserAccount() : null;
            this.userSettings = addUserSettings ? new UserSettings() : null;
            this.userSession = addUserSessions ? new UserSession(ninjascript, bars, sessionIterator) : null;
        }

        #endregion

        #region methods to add functionality

        public SessionsManager UseTradingHours()
        {
            userSession.UserSessionChanged += OnUserSessionChanged;
            return this;
        }

        #endregion

        #region handler methods

        public override void OnBarUpdate()
        {
            userSession.OnBarUpdate();
        }

        public override void OnMarketData()
        {
            userSession.OnMarketData();
        }

        /// <summary>
        /// If the trading hours is in partial holiday, gets the Partial Holiday object, otherwise, partial holiday is null.
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnUserSessionChanged(UserSessionChangedEventArgs e)
        {
            tradingHours = new NtTradingHours();
            tradingHours.UpdateUserSession(e,UserSessionsCount);
            tradingHoursList.Add(tradingHours);
        }

        public override void Dispose()
        {
            userSession.UserSessionChanged -= OnUserSessionChanged;
        }

        #endregion

    }
}
