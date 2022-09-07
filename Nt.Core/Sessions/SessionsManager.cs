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

        #region Private members

        SessionManagerOptions sessionManagerOptions;

        /// <summary>
        /// The ninjascript parent of the class.
        /// </summary>
        private NinjaScriptBase ninjascript;

        /// <summary>
        /// The bars of the chart control.
        /// </summary>
        private Bars bars;

        /// <summary>
        /// Gets the <see cref="SessionsIterator"/> funcionality.
        /// </summary>
        private SessionsIterator sessionsIterator;

        /// <summary>
        /// Represents the <see cref="SessionHours"/> configure by the user.
        /// </summary>
        //private SessionHours sessionHours;

        /// <summary>
        /// Represents the <see cref="GenericSessionHours"/> configure by the user.
        /// </summary>
        private GenericSessionHours genericSessions;

        /// <summary>
        /// Represents the <see cref="CustomSessionHours"/> configure by the user.
        /// </summary>
        private CustomSessionHours customSessions;

        /// <summary>
        /// Represents the <see cref="SessionFilters"/> configure by the user.
        /// </summary>
        private SessionFilters sessionFilters;

        /// <summary>
        /// Represents the <see cref="SessionStats"/> configure by the user.
        /// </summary>
        private SessionStats sessionStats;

        /// <summary>
        /// Represents the last session hours.
        /// </summary>
        private SessionHours lastSession;

        /// <summary>
        /// Represents the actual session hours.
        /// </summary>
        private SessionHours actualSession;

        /// <summary>
        /// Stores all session hours in a sorted list.
        /// </summary>
        private List<SessionHours> sessionHoursList;

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
                    throw new ArgumentNullException(nameof(SessionsIterator),"The SessionsManager has not include SessionIterator funcionality.");

                return sessionsIterator;
            }
        }

        /// <summary>
        /// Gets the <see cref="SessionHours"/> configure by the user.
        /// </summary>
        public SessionHours SessionHours
        {
            get
            {
                if (genericSessions == null)
                    throw new ArgumentNullException(nameof(GenericSessions), "The SessionsManager don't include generic session hours.");

                return genericSessions;
            }
        }

        /// <summary>
        /// Gets the <see cref="GenericSessionHours"/> configure by the user.
        /// </summary>
        public GenericSessionHours GenericSessions
        {
            get
            {
                if (genericSessions == null)
                    throw new ArgumentNullException(nameof(GenericSessions), "The SessionsManager don't include generic sessions.");

                return genericSessions;
            }
        }

        /// <summary>
        /// Gets the <see cref="GenericSessionHours"/> configure by the user.
        /// </summary>
        public CustomSessionHours CustomSessions
        {
            get
            {
                if (customSessions == null)
                    throw new ArgumentNullException(nameof(CustomSessions),"The SessionsManager don't include custom sessions.");

                return customSessions;
            }
        }

        /// <summary>
        /// Gets the <see cref="SessionFilters"/> configure by the user.
        /// </summary>
        public SessionFilters SessionFilters
        {
            get
            {
                if (sessionFilters == null)
                    throw new ArgumentNullException(nameof(SessionFilters),"The SessionsManager don't include session filters.");

                return sessionFilters;
            }
        }

        /// <summary>
        /// Gets the <see cref="SessionStats"/> configure by the user.
        /// </summary>
        public SessionStats SessionStats
        {
            get
            {
                if (sessionStats == null)
                    throw new ArgumentNullException(nameof(SessionStats),"The SessionsManager don't include session stats.");

                return sessionStats;
            }
        }

        /// <summary>
        /// Gets true if any sessionHoursList are stored.
        /// </summary>
        public bool HasSessions => sessionHoursList != null && sessionHoursList.Count > 0;

        /// <summary>
        /// Gets the number of <see cref="SessionHours"/> stored.
        /// </summary>
        public int Count => HasSessions ? sessionHoursList.Count : 0;

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

        #region Instance methods

        /// <summary>
        /// Create a <see cref="SessionsManager"/> default instance.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="bars"></param>
        public void CreateDefaultSessionManager(
            NinjaScriptBase ninjascript,
            Bars bars
            )
        {
            this.ninjascript = ninjascript;
            this.bars = bars;
        }

        public void Configure(Action<SessionManagerOptions> options)
        {
            options?.Invoke(sessionManagerOptions);
        }


        #endregion

        #region functionality methods

        /// <summary>
        /// Add generic sessionHoursList with specific configure passed by parameter.
        /// </summary>
        /// <param name="configure">Specific <see cref="GenericSessionConfigure"/>.</param>
        /// <returns><see cref="SessionsManager"/> with generic sessionHoursList funcionality.</returns>
        public SessionsManager UseGenericSessions(GenericSessionsConfigure configure)
        {
            if (genericSessions == null)
            {
                // Create the user session object
                genericSessions = new GenericSessionHours();

                // Add configuration
                //if (configure != null)
                //    genericSessions.AddConfigure(configure);
                //else
                //    genericSessions.AddConfigure(new CustomSessionsDefaultConfigure());

                // Add the delegate
                sessionsIterator.SessionChanged += OnSessionHoursChanged;
            }
            else
            {
                //genericSessions.AddConfigure(configure);
            }

            // return the object with the user sessionHoursList implementation
            return this;
        }

        /// <summary>
        /// Add custom sessionHoursList with specific configure passed by parameter.
        /// </summary>
        /// <param name="configure">Specific <see cref="CustomSessionConfigure"/>.</param>
        /// <returns><see cref="SessionsManager"/> with generic sessionHoursList funcionality.</returns>
        public SessionsManager UseCustomSessions(CustomSessionsConfigure configure) 
        {
            if (genericSessions == null)
            {
                // Create the user session object
                customSessions = new CustomSessionHours();

                // Add configuration
                //if (configure != null)
                //    genericSessions.AddConfigure(configure);
                //else
                //    genericSessions.AddConfigure(new CustomSessionsDefaultConfigure());

                // Add the delegate
                sessionsIterator.SessionChanged += OnSessionHoursChanged;
            }
            else
            {
                //genericSessions.AddConfigure(configure);
            }

            // return the object with the user sessionHoursList implementation
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
        /// Changed any object or property when the session changed.
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnSessionHoursChanged(SessionChangedEventArgs e)
        {
            //var temp = actualSession;
            //lastSession = temp;
            lastSession = actualSession;
            actualSession = new SessionHours(); // genericSessionsConfigure, customSessionsConfigure);
            actualSession.Load(e);
            actualSession.N = Count;
            if (sessionHoursList == null)
                sessionHoursList = new List<SessionHours>();
            sessionHoursList.Add(actualSession);
        }

        /// <summary>
        /// Event driven method which is called whenever a trading sessionHoursList changed.
        /// </summary>
        /// <param name="e"></param>
        //public virtual void OnSessionHoursChanged(SessionChangedEventArgs e)
        //{
        //    genericSessions.OnSessionHoursChanged(e);
        //}

        /// <summary>
        /// Method used to free memory when the script is terminate.
        /// </summary>
        public override void Dispose()
        {
            sessionsIterator.SessionChanged -= OnSessionHoursChanged;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Add configure to the trading sessionHoursList.
        /// The configure can be added only when the configure doesn't exists.
        /// </summary>
        /// <param name="configure"></param>
        /// <exception cref="Exception"></exception>
        public void AddConfigure(ISessionsConfigure configure)
        {
            //if (configure is GenericSessionsConfigure genericConfigure)
            //    if (genericSessionsConfigure != null)
            //        throw new Exception("The generic sessionHoursList configure exists. The configure can not be rewriter.");
            //    else
            //    {
            //        genericSessionsConfigure = genericConfigure;
            //        IncludeGenericSessions = true;
            //        return;
            //    }

            //if (configure is CustomSessionsConfigure customConfigure)
            //    if (customSessionsConfigure != null)
            //        throw new Exception("The custom sessionHoursList configure exists. The configure can not be rewriter.");
            //    else
            //    {
            //        customSessionsConfigure = customConfigure;
            //        IncludeCustomSessions = true;
            //        return;
            //    }
        }

        /// <summary>
        /// Represent a string with the last session stored.
        /// </summary>
        /// <returns>String of the last session stored.</returns>
        public override string ToString()
        {
            return actualSession != null ? actualSession.ToString() : "The session has not started.";
        }

        /// <summary>
        /// Represent a string with the session stored.
        /// </summary>
        /// <param name="idx">The session index. 0 is the actual, 1 is the last,...</param>
        /// <returns>String of the last session stored.</returns>
        public string ToString(int idx)
        {
            return idx>=0 && idx<Count ? sessionHoursList[Count - 1 - idx].ToString() : "SessionHours list is empty.";
        }

        #endregion

    }
}
