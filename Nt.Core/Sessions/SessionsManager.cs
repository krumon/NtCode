using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;
using System.Reflection;
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

        /// <summary>
        /// Flags to indicates if the <see cref="NtScript"/> is configured.
        /// </summary>
        public bool ntScriptIsConfigured;

        /// <summary>
        /// Flags to indicates if the <see cref="SessionsManager"/> is configured.
        /// </summary>
        public bool sessionsManagerIsConfigured;

        /// <summary>
        /// Flags to indicates if the <see cref="SessionFilters"/> is configured.
        /// </summary>
        public bool sessionFiltersIsConfigured;

        #endregion

        #region Delegates

        /// <summary>
        /// Delegate to execute in OnSessionHoursChanged method.
        /// </summary>
        public Action SessionHoursChangedAction;

        #endregion

        #region Configure Properties

        /// <summary>
        /// Max sessions to stored
        /// </summary>
        public int MaxSessionsToStored { get; private set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the last session hours.
        /// </summary>
        //public SessionHours LastSession => lastSession;

        /// <summary>
        /// Gets the actual session hours.
        /// </summary>
        //public SessionHours ActualSession => actualSession;

        /// <summary>
        /// Gets true if any sessionHoursList are stored.
        /// </summary>
        public bool HasSessions => sessionHoursList != null && sessionHoursList.Count > 0;

        /// <summary>
        /// Gets true if <see cref="SessionsManager"/> has <see cref="SessionFilters"/>.
        /// </summary>
        public bool HasSessionFilters => sessionFilters != null;

        /// <summary>
        /// Gets the <see cref="SessionFilters"/> configure by the user.
        /// </summary>
        public SessionFilters SessionFilters => sessionFilters;

        /// <summary>
        /// Gets the number of <see cref="SessionHours"/> stored.
        /// </summary>
        public int Count => HasSessions ? sessionHoursList.Count : 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a <see cref="SessionsManager"/> default instance.
        /// </summary>
        public SessionsManager()
        {
        }

        /// <summary>
        /// Create a default instance of the <see cref="SessionsManager"/> class.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="bars"></param>
        private SessionsManager(NinjaScriptBase ninjascript, Bars bars)
        {
            // Set values.
            this.ninjascript = ninjascript;
            this.bars = bars;

            // Init and load the session iterator.
            sessionsIterator = new SessionsIterator();
            sessionsIterator.Load(ninjascript, bars);
        }

        #endregion

        #region StateChanged methods

        /// <summary>
        /// Load the <see cref="SessionsManager"/>.
        /// </summary>
        /// <param name="ninjascript">The ninjascript.</param>
        /// <param name="bars">The bars.</param>
        /// <param name="o">Any object necesary to load the script.</param>
        public override void Load(NinjaScriptBase ninjascript, Bars bars)
        {
            // Make sure session manager can be loaded.
            if (ninjascript == null || bars == null)
                throw new Exception($"{nameof(SessionsManager)} load parameters can not be null"); // return null;

            // Set values.
            this.ninjascript = ninjascript;
            this.bars = bars;

            // Make sure the ninjascript is sessionsManagerIsConfigured
            if (!sessionsManagerIsConfigured)
            {
                ConfigureSessionsManager();
                sessionsManagerIsConfigured = true;
            }

            // If we need the session iterator...create him.
            // TODO: Make Sure we need the session iterator object.
            // Initialize the session iterator
            if (sessionsIterator == null)
                sessionsIterator = new SessionsIterator();

            // Load the elements...
            sessionsIterator.Load(ninjascript, bars);
            if (HasSessionFilters)
                sessionFilters.Load(ninjascript, bars, sessionsIterator);

            // Aggregate the delegates
            sessionsIterator.SessionChanged += OnSessionHoursChanged;

        }

        /// <summary>
        /// Method used to free memory when the script is terminate.
        /// </summary>
        public override void Terminated()
        {
            // Disaggregate the delegates.
            sessionsIterator.SessionChanged -= OnSessionHoursChanged;

            // Terminated the elements
            sessionsIterator.Terminated();
            if (HasSessionFilters)
                sessionFilters.Terminated();
        }

        #endregion

        #region Configure methods

        /// <summary>
        /// Add <see cref="NtScriptConfigureOptions"/> to <see cref="NtScript"/> configure.
        /// </summary>
        /// <param name="options">The ninjascript configure options.</param>
        public override SessionsManager ConfigureNtScripts<SessionsManager>(Action<NtScriptConfigureOptions> options = null)
        {
            // Make sure the trading hours is sessionsManagerIsConfigured by default
            //TradingHours th = NinjaTrader.Data.TradingHours.String2TradingHours("CBOE US Index Futures ETH");
            //th.CopyTo(ninjascript.TradingHours);

            // Create default session manager options.
            var ntScriptConfigureOptions = new NtScriptConfigureOptions();

            // If options is not null...invoke delegate to update the options configure by the user.
            options?.Invoke(ntScriptConfigureOptions);

            // Mapper the sesion manager with the session manager options.
            AutoMapper(ntScriptConfigureOptions);

            // Update the configure flag
            if (!ntScriptIsConfigured)
                ntScriptIsConfigured = true;

            return this as SessionsManager;
        }

        /// <summary>
        /// Add <see cref="SessionManagerOptions"/> to <see cref="SessionsManager"/> configure.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManager ConfigureSessionsManager(Action<SessionManagerOptions> options = null)
        {
            // Create default session manager options.
            var sessionManagerOptions = new SessionManagerOptions();

            // If options is not null...invoke delegate to update the options configure by the user.
            options?.Invoke(sessionManagerOptions);

            // Mapper the sesion manager with the session manager options.
            AutoMapper(sessionManagerOptions);

            // Update the configure flag
            if (!sessionsManagerIsConfigured)
                sessionsManagerIsConfigured = true;

            return this;
        }

        /// <summary>
        /// Add <see cref="SessionManagerOptions"/> to <see cref="SessionsManager"/> configure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManager ConfigureSessionsManager<T>(Action<T> options = null)
            where T : SessionManagerOptions, new()
        {
            // Create default session manager options.
            var sessionManagerOptions = new T();

            // If options is not null...invoke delegate to update the options configure by the user.
            options?.Invoke(sessionManagerOptions);

            // Mapper the sesion manager with the session manager options.
            AutoMapper(sessionManagerOptions);

            // Update the configure flag
            if (!sessionsManagerIsConfigured)
                sessionsManagerIsConfigured = true;

            return this;
        }

        /// <summary>
        /// Add <see cref="SessionManagerOptions"/> to <see cref="SessionsManager"/> configure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManager ConfigureSessionsManager<T>(T options = null)
            where T : SessionManagerOptions, new()
        {
            // If options is null...create a default options...
            if (options == null)
                options = new T();

            // Mapper the sesion filters with the session filters options.
            AutoMapper(options);

            // Update the configure flag
            if (!sessionsManagerIsConfigured)
                sessionsManagerIsConfigured = true;

            return this;
        }

        /// <summary>
        /// Add filters funcionality to session manager.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManager ConfigureSessionFilters(Action<SessionFiltersOptions> options = null)
        {
            // Make sure session filters is not null.
            if (sessionFilters == null)
                sessionFilters = new SessionFilters();

            sessionFilters.Configure(options);

            // Update the configure flag
            if (!sessionFiltersIsConfigured)
                sessionFiltersIsConfigured = true;

            return this;
        }

        /// <summary>
        /// Add filters funcionality to session manager.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManager ConfigureSessionFilters(SessionFiltersOptions options = null)
        {
            // Make sure session filters is not null.
            if (sessionFilters == null)
                sessionFilters = new SessionFilters();

            sessionFilters.Configure(options);

            // Update the configure flag
            if (!sessionFiltersIsConfigured)
                sessionFiltersIsConfigured = true;

            return this;
        }

        /// <summary>
        /// Add filters funcionality to session manager.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManager ConfigureSessionFilters<T>(Action<T> options = null)
            where T : SessionFiltersOptions, new()
        {
            // Make sure session filters is not null.
            if (sessionFilters == null)
                sessionFilters = new SessionFilters();

            sessionFilters.Configure(options);

            // Update the configure flag
            if (!sessionFiltersIsConfigured)
                sessionFiltersIsConfigured = true;

            return this;
        }

        /// <summary>
        /// Add filters funcionality to session manager.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManager ConfigureSessionFilters<T>(T options = null)
            where T : SessionFiltersOptions, new()
        {
            // Make sure session filters is not null.
            if (sessionFilters == null)
                sessionFilters = new SessionFilters();

            sessionFilters.Configure(options);

            // Update the configure flag
            if (!sessionFiltersIsConfigured)
                sessionFiltersIsConfigured = true;

            return this;
        }

        #endregion

        #region Market Data methods

        /// <summary>
        /// Event driven method which is called whenever a bar is updated. 
        /// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
        /// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
        /// </summary>
        public override void OnBarUpdate()
        {
            // Update the filters
            if (sessionFilters != null)
                sessionFilters.OnBarUpdate();

            // TODO: No funcionan los filtros. Arreglarlo!!!!!
            // Entry to the method if the filters are ok.
            //if (HasSessionFilters && !(sessionFilters.CanEntry()))
            //    return;

            // Update the session iterator
            if (sessionsIterator != null)
                sessionsIterator.OnBarUpdate();

            ExecuteInBarUpdateMethod(BarUpdateAction);
        }

        /// <summary>
        /// Event driven method which is called and guaranteed to be in the correct sequence 
        /// for every change in level one market data for the underlying instrument. 
        /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        /// </summary>
        public override void OnMarketData()
        {
            // Update the filters
            if (sessionFilters != null)
                sessionFilters.OnBarUpdate();

            // TODO: No funcionan los filtros. Arreglarlo!!!!!
            // Entry to the method if the filters are ok.
            //if (HasSessionFilters && !(sessionFilters.CanEntry()))
            //    return;

            // Update the session iterator
            if (sessionsIterator != null)
                sessionsIterator.OnBarUpdate();

            ExecuteInMarketDataMethod(MarketDataAction);
        }

        /// <summary>
        /// Changed any object or property when the session changed.
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnSessionHoursChanged(SessionChangedEventArgs e)
        {
            // Update Holiday filters
            if (sessionFilters != null)
                sessionFilters.OnSessionHoursChanged(e);

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

            // Execute delegate method.
            ExecuteInSessionHoursChangedMethod(SessionHoursChangedAction);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Mapper <see cref="SessionsManager"/> with <see cref="SessionManagerOptions"/>.
        /// </summary>
        /// <param name="options"></param>
        public void AutoMapper (SessionManagerOptions options)
        {
            MaxSessionsToStored = options.MaxSessionsToStored;
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

        #region Helper methods

        /// <summary>
        /// Mapper <see cref="SessionsManager"/> with <see cref="SessionManagerOptions"/>.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="options"></param>
        public static void AutoMapper(SessionsManager session, SessionManagerOptions options)
        {
            session.MaxSessionsToStored = options.MaxSessionsToStored;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Execute the delegate in the OnSessionHoursChanged method
        /// </summary>
        /// <param name="action">The delegate to execute.</param>
        protected void ExecuteInSessionHoursChangedMethod(Action action)
        {
            action?.Invoke();
        }

        #endregion

    }
}
