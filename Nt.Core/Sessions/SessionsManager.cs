using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Nt.Core
{
    /// <summary>
    /// Represents consts, fields and properties of the Ninjatrader user configuration.
    /// </summary>
    public class SessionsManager : NtScript
    {

        #region Private members

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

        #region SessionManager options properties

        /// <summary>
        /// Max sessions to stored
        /// </summary>
        public int MaxSessionsStored { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the last session hours.
        /// </summary>
        public SessionHours LastSession => lastSession;

        /// <summary>
        /// Gets the actual session hours.
        /// </summary>
        public SessionHours ActualSession => actualSession;

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
        /// <param name="bars"></param>
        private SessionsManager(NinjaScriptBase ninjascript, Bars bars)
        {
            this.ninjascript = ninjascript;
            this.bars = bars;
            this.sessionsIterator = new SessionsIterator(ninjascript, bars);
        }

        #endregion

        #region StateChanged methods

        /// <summary>
        /// Create a default instance of the <see cref="SessionsManager"/> class.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="bars"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static SessionsManager Load(NinjaScriptBase ninjascript, Bars bars)
        {
            if (ninjascript == null || bars == null)
                throw new Exception("Parameters can not be null"); // return null;
            
            return new SessionsManager(ninjascript,bars);
        }

        /// <summary>
        /// Add <see cref="SessionManagerOptions"/> to configure <see cref="SessionsManager"/>.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManager Configure(Action<SessionManagerOptions> options)
        {
            var sessionManagerOptions = new SessionManagerOptions();
            options?.Invoke(sessionManagerOptions);
            AutoMapper(sessionManagerOptions);

            return this;
        }

        #endregion

        #region functionality methods

        /// <summary>
        /// Add filters funcionality to session manager.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManager AddSessionFilters(Action<SessionFiltersOptions> options)
        {
            var sessionFiltersOptions = new SessionFiltersOptions();
            options?.Invoke(sessionFiltersOptions);
            sessionFilters = new SessionFilters(ninjascript, bars);
            sessionFilters.Configure(options);
            return this;
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
            if (sessionFilters != null && !(sessionFilters.CanEntry()))
                return;

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
            if (sessionFilters != null && !(sessionFilters.CanEntry()))
                return;

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
            actualSession = new SessionHours();
            actualSession.Load(e);
            // TODO: Revisar esta asignación.
            actualSession.N = Count;
            if (sessionHoursList == null)
                sessionHoursList = new List<SessionHours>();
            if (Count >= MaxSessionsStored)
                sessionHoursList.Remove(sessionHoursList[0]);
            sessionHoursList.Add(actualSession);
        }

        /// <summary>
        /// Method used to free memory when the script is terminate.
        /// </summary>
        public override void Dispose()
        {
            sessionsIterator.SessionChanged -= OnSessionHoursChanged;
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Mapper <see cref="SessionsManager"/> with <see cref="SessionManagerOptions"/>.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="options"></param>
        public static void AutoMapper(SessionsManager session, SessionManagerOptions options)
        {
            session.MaxSessionsStored = options.MaxSessionsStored;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Mapper <see cref="SessionsManager"/> with <see cref="SessionManagerOptions"/>.
        /// </summary>
        /// <param name="options"></param>
        public void AutoMapper (SessionManagerOptions options)
        {
            MaxSessionsStored = options.MaxSessionsStored;
        }

        /// <summary>
        /// Represent a string with the last session stored.
        /// </summary>
        /// <returns>String of the last session stored.</returns>
        public override string ToString()
        {
            return actualSession != null ? actualSession.ToString() : "Actual session is null.";
        }

        /// <summary>
        /// Represent a string with the session stored.
        /// </summary>
        /// <param name="idx">The session index. 0 is the actual, 1 is the last,...</param>
        /// <returns>String of the last session stored.</returns>
        public string ToString(int idx)
        {
            if (!HasSessions)
                return "SessionHours list is empty.";

            if (idx < 0 || idx >=Count)
                return "Index is out of range";

            return sessionHoursList[Count - 1 - idx].ToString();
        }

        #endregion

    }
}
