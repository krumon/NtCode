using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// Represents the SessionHours of the day trading.
    /// </summary>
    public class SessionHoursList : BaseSession
    {
        #region Private members

        /// <summary>
        /// Flags to indicates if the <see cref="SessionFilters"/> is sessionHoursListIsConfigured.
        /// </summary>
        public bool configured;

        #endregion

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

        #region Constructors

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
            // Make sure the parameters are not null.
            if (ninjascript == null || bars == null)
                throw new Exception($"{nameof(SessionFilters)} load parameters can not be null"); // return null;

            // Set values.
            this.ninjascript = ninjascript;
            this.bars = bars;
            //this.sessionsIterator = sessionsIterator;

            // Make sure the ninjascript is sessionHoursListIsConfigured
            if (!configured)
                Configure();

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

        #region Configure methods

        /// <summary>
        /// Add <see cref="SessionFiltersOptions"/> to configure <see cref="SessionFilters"/>.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionHoursList Configure(Action<SessionHoursListOptions> options = null)
        {
            // Create default session filters options.
            var sessionOptions = new SessionHoursListOptions();

            // If options is not null...invoke delegate to update the options configure by the user.
            if (options != null)
                options.Invoke(sessionOptions);

            // Mapper the sesion filters with the session filters options.
            Mapper(sessionOptions);

            // Update the configure flag
            if (!configured)
                configured = true;

            return this;
        }

        /// <summary>
        /// Add <see cref="SessionFiltersOptions"/> to configure <see cref="SessionFilters"/>.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionHoursList Configure<T>(Action<T> options)
            where T : SessionHoursListOptions, new()
        {
            // Create default session filters options.
            var sessionFiltersOptions = new T();

            // Invoke delegate to update the options configure by the user.
            if (options != null)
                options.Invoke(sessionFiltersOptions);

            // Mapper the sesion filters with the session filters options.
            Mapper(sessionFiltersOptions);

            // Update the configure flag
            if (!configured)
                configured = true;

            return this;
        }

        /// <summary>
        /// Add <see cref="SessionFiltersOptions"/> to configure <see cref="SessionFilters"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionHoursList Configure<T>(T options = null)
            where T : SessionHoursListOptions, new()
        {
            // If options is null...create a default options...
            if (options == null)
                options = new T();

            // Mapper the sesion filters with the session filters options.
            Mapper(options);

            // Update the configure flag
            if (!configured)
                configured = true;

            return this;
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
        /// Mapper <see cref="SessionFilters"/> with <see cref="SessionFiltersOptions"/>.
        /// </summary>
        /// <param name="options"></param>
        protected override void Mapper<T>(T options)
        {
            if (options is SessionHoursListOptions op)
            {
                MaxSessionsToStored = op.MaxSessionsToStored;
            }

        }

        /// <summary>
        /// Mapper <see cref="SessionHoursList"/> from <see cref="SessionHoursListOptions"/>.
        /// </summary>
        /// <param name="options"></param>
        private void Mapper(SessionHoursListOptions options)
        {
            MaxSessionsToStored = options.MaxSessionsToStored;
        }

        #endregion

    }


}
