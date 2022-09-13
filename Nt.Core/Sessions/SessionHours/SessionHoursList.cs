using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// Represents the SessionHours of the day trading.
    /// </summary>
    public class SessionHoursList : BaseSession<SessionHoursList, SessionHoursListOptions>
    {

        #region Public properties

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

        /// <summary>
        /// Gets true if any sessionHoursList are stored.
        /// </summary>
        public bool HasSessions => sessionHoursList != null && sessionHoursList.Count > 0;

        /// <summary>
        /// Gets the number of <see cref="SessionHours"/> stored.
        /// </summary>
        public int Count => HasSessions ? sessionHoursList.Count : 0;

        #endregion

        #region Configure properties

        /// <summary>
        /// Max sessions to stored
        /// </summary>
        public int MaxSessionsToStored { get; private set; }

        #endregion

        #region State changed methods

        /// <summary>
        /// Load the <see cref="SessionFilters"/>.
        /// </summary>
        /// <param name="ninjascript">The ninjascript.</param>
        /// <param name="bars">The bars.</param>
        /// <param name="o">Any object necesary to load the script.</param>
        public override void Load(NinjaScriptBase ninjascript, Bars bars)
        {
            // Call parent method to load.
            base.Load(ninjascript, bars);

        }

        /// <summary>
        /// Method used to free memory when the script is terminate.
        /// </summary>
        public override void Terminated()
        {
        }

        #endregion

        #region Market data methods

        /// <summary>
        /// Event driven method which is called for every new session. 
        /// </summary>
        /// <param name="e"></param>
        public override void OnSessionChanged(SessionChangedEventArgs e)
        {
            // Update actual session
            lastSession = actualSession;
            actualSession = new SessionHours();
            actualSession.Load(e);
            // TODO: Revisar esta asignación.
            actualSession.N = Count;

            // Add the actual session to the list
            if (sessionHoursList == null)
                sessionHoursList = new List<SessionHours>();
            if (Count >= MaxSessionsToStored)
                sessionHoursList.Remove(sessionHoursList[0]);
            sessionHoursList.Add(actualSession);
        }

        #endregion

        #region Public methods

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
            if (idx < 0 || idx >= Count)
                return "Index is out of range";

            return sessionHoursList[Count - 1 - idx].ToString();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Mapper <see cref="SessionHoursList"/> from <see cref="SessionHoursListOptions"/>.
        /// </summary>
        /// <param name="options"></param>
        protected override void Mapper(SessionHoursListOptions options)
        {
            MaxSessionsToStored = options.MaxSessionsToStored;
        }

        #endregion

    }


}
