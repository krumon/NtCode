using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// Represents consts, fields and properties of the Ninjatrader user configuration.
    /// </summary>
    public class SessionsManager : NtScript
    {

        #region Fields

        /// <summary>
        /// Gets the <see cref="SessionsIterator"/> funcionality.
        /// </summary>
        private SessionsIterator sessionsIterator;

        /// <summary>
        /// Gets the <see cref="Core.TradingSessions"/> configure by the user.
        /// </summary>
        private TradingSessions tradingSessions;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the <see cref="SessionsIterator"/> funcionality.
        /// </summary>
        public SessionsIterator SessionsIterator
        {
            get
            {
                if (sessionsIterator == null)
                    throw new ArgumentNullException(nameof(SessionsIterator),"SessionsManager has not initialize with SessionIterator funcionality.");

                return sessionsIterator;
            }
        }

        /// <summary>
        /// Gets the <see cref="Core.TradingSessions"/> configure by the user.
        /// </summary>
        public TradingSessions TradingSessions
        {
            get
            {
                if (tradingSessions == null)
                    throw new ArgumentNullException(nameof(TradingSessions),"SessionsManager has not configure generic sessions.");

                return tradingSessions;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a default instance of the <see cref="SessionsManager"/> class.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="sessionIterator"></param>
        /// <param name="bars"></param>
        /// <param name="useSessionIterator"></param>
        public SessionsManager(
            NinjaScriptBase ninjascript, 
            Bars bars, 
            SessionIterator sessionIterator, 
            bool useSessionIterator = true
            )
        {
            this.sessionsIterator = useSessionIterator ? new SessionsIterator(ninjascript, bars, sessionIterator) : null;
        }

        #endregion

        #region functionality methods

        /// <summary>
        /// Add generic sessions with specific configure passed by parameter.
        /// </summary>
        /// <param name="configure">Specific <see cref="GenericSessionConfigure"/>.</param>
        /// <returns><see cref="SessionsManager"/> with generic sessions funcionality.</returns>
        public SessionsManager UseGenericSessions(GenericSessionsConfigure configure)
        {
            if (tradingSessions == null)
            {
                // Create the user session object
                tradingSessions = new TradingSessions();

                // Add configuration
                if (configure != null)
                    tradingSessions.AddConfigure(configure);
                else
                    tradingSessions.AddConfigure(new CustomSessionsDefaultConfigure());

                // Add the delegate
                sessionsIterator.SessionChanged += OnTradingSessionsChanged;
            }
            else
            {
                tradingSessions.AddConfigure(configure);
            }

            // return the object with the user children implementation
            return this;
        }

        /// <summary>
        /// Add custom sessions with specific configure passed by parameter.
        /// </summary>
        /// <param name="configure">Specific <see cref="CustomSessionConfigure"/>.</param>
        /// <returns><see cref="SessionsManager"/> with generic sessions funcionality.</returns>
        public SessionsManager UseCustomSessions(CustomSessionsConfigure configure) 
        {
            if (tradingSessions == null)
            {
                // Create the user session object
                tradingSessions = new TradingSessions();

                // Add configuration
                if (configure != null)
                    tradingSessions.AddConfigure(configure);
                else
                    tradingSessions.AddConfigure(new CustomSessionsDefaultConfigure());

                // Add the delegate
                sessionsIterator.SessionChanged += OnTradingSessionsChanged;
            }
            else
            {
                tradingSessions.AddConfigure(configure);
            }

            // return the object with the user children implementation
            return this;
        }

        // TODO: Implemtated the follow methods

        public SessionsManager UseSessionFilters()
        {
            throw new NotImplementedException("Method not implemented.");
        }

        #endregion

        #region handler methods

        /// <summary>
        /// Event driven method which is called whenever a bar is updated. 
        /// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
        /// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
        /// </summary>
        public override void OnBarUpdate()
        {
            if (sessionsIterator != null)
                sessionsIterator.OnBarUpdate();
        }

        /// <summary>
        /// Event driven method which is called and guaranteed to be in the correct sequence 
        /// for every change in level one market data for the underlying instrument. 
        /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        /// </summary>
        public override void OnMarketData()
        {
            if (sessionsIterator != null)
                sessionsIterator.OnMarketData();
        }

        /// <summary>
        /// Event driven method which is called whenever a trading sessions changed.
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnTradingSessionsChanged(SessionChangedEventArgs e)
        {
            tradingSessions.OnTradingSessionsChanged(e);
        }

        /// <summary>
        /// Method used to free memory when the script is terminate.
        /// </summary>
        public override void Dispose()
        {
            sessionsIterator.SessionChanged -= OnTradingSessionsChanged;
        }

        #endregion

        #region Public methods

        #endregion

    }
}
