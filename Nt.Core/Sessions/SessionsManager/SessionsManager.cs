using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Runtime.CompilerServices;

namespace Nt.Core
{
    /// <summary>
    /// Represents consts, fields and properties of the Ninjatrader user configuration.
    /// </summary>
    public class SessionsManager : BaseSession
    {

        #region Private members

        /// <summary>
        /// Gets the <see cref="SessionsIterator"/> funcionality.
        /// </summary>
        private SessionsIterator sessionsIterator;

        /// <summary>
        /// Represents the <see cref="SessionFilters"/> of the sessions.
        /// </summary>
        private SessionFilters sessionFilters;

        /// <summary>
        /// Represents the <see cref="SessionStats"/> of the sessions.
        /// </summary>
        private SessionStats sessionStats;

        /// <summary>
        /// Represents the <see cref="SessionHoursList"/> of the main session.
        /// </summary>
        private SessionHoursList sessionHoursList;


        // TODO: Estas tres propiedades deben ir cada una en su clase.

        /// <summary>
        /// Flags to indicates if the <see cref="NtScript"/> is configured.
        /// </summary>
        public bool ntScriptIsConfigured;

        /// <summary>
        /// Flags to indicates if the <see cref="SessionsManager"/> is configured.
        /// </summary>
        public bool sessionHoursListIsConfigured;

        /// <summary>
        /// Flags to indicates if the <see cref="SessionFilters"/> is configured.
        /// </summary>
        public bool sessionFiltersIsConfigured;

        #endregion

        #region Configure Properties


        #endregion

        #region Public Properties

        /// <summary>
        /// Gets true if any sessionHoursList are stored.
        /// </summary>
        public bool HasSessionHours => sessionHoursList != null;

        /// <summary>
        /// Gets true if <see cref="SessionsManager"/> has <see cref="SessionFilters"/>.
        /// </summary>
        public bool HasSessionFilters => sessionFilters != null;

        /// <summary>
        /// Gets true if <see cref="SessionsManager"/> has <see cref="SessionStats"/>.
        /// </summary>
        public bool HasSessionStats => sessionStats != null;

        /// <summary>
        /// Gets the <see cref="SessionHoursList"/> of the main session.
        /// </summary>
        public SessionHoursList SessionHours => sessionHoursList;

        /// <summary>
        /// Gets the <see cref="SessionFilters"/> cof the main session.
        /// </summary>
        public SessionFilters SessionFilters => sessionFilters;

        /// <summary>
        /// Gets the <see cref="SessionStats"/> of the main session.
        /// </summary>
        public SessionStats SessionStats => sessionStats;

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

            // If we need the session iterator...create him.
            // TODO: Make Sure we need the session iterator object.
            // Initialize the session iterator
            if (sessionsIterator == null)
                sessionsIterator = new SessionsIterator();

            // Load the elements...
            sessionsIterator.Load(ninjascript, bars);
            if (HasSessionFilters)
                sessionFilters.Load(ninjascript, bars);
            if (HasSessionHours)
                sessionHoursList.Load(ninjascript, bars);
            if (HasSessionStats)
                sessionStats.Load(ninjascript, bars);

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
            if (HasSessionHours)
                sessionHoursList.Terminated();
            if (HasSessionStats)
                sessionStats.Terminated();

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
            // First...update the session iterator
            sessionsIterator.OnBarUpdate();

            // Update the filters
            if (HasSessionFilters)
                sessionFilters.OnBarUpdate();

            // Opcion 1
            if (HasSessionFilters && sessionFilters.OnBarUpdateAndCheckFilters())
            {
                // Enter the code
            }

            // Opcion 2            
            if (sessionFilters.CanEntry())
            {
                // Enter the code
            }
            
            // TODO: No funcionan los filtros. Arreglarlo!!!!!
            // Entry to the method if the filters are ok.
            //if (HasSessionFilters && !(sessionFilters.CanEntry()))
            //    return;

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
            if (HasSessionFilters)
                sessionFilters.OnSessionChanged(e);

            // Update the session hours list.
            if (HasSessionHours)
                sessionHoursList.OnSessionChanged(e);

            // Execute delegate method.
            ExecuteInSessionHoursChangedMethod(SessionHoursChangedAction);
        }

        #endregion

        #region Configure methods

        /// <summary>
        /// Add <see cref="SessionsManagerOptions"/> to <see cref="SessionsManager"/> configure.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManager ConfigureSession<TSession,TOptions>(Action<TOptions> options = null)
            where TSession : BaseSession, new()
            where TOptions : BaseSessionOptions, new()
        {
            if (options is Action<SessionHoursListOptions> shOptions)
            {
                // Make sure session filters is not null.
                if (!HasSessionHours)
                    sessionHoursList = new SessionHoursList();

                // Configure....
                sessionHoursList.Configure<SessionHoursList, SessionHoursListOptions>(shOptions);

                // Update the configure flag
                if (!sessionHoursListIsConfigured)
                    sessionHoursListIsConfigured = true;

            }
            else if (options is Action<SessionFiltersOptions> fOptions)
            {
                // Make sure session filters is not null.
                if (!HasSessionFilters)
                    sessionFilters = new SessionFilters();

                // Configure....
                sessionFilters.Configure<SessionFilters, SessionFiltersOptions>(fOptions);

                // Update the configure flag
                if (!sessionFiltersIsConfigured)
                    sessionFiltersIsConfigured = true;
            }



            return this;
        }

        /// <summary>
        /// Add filters funcionality to session manager.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManager ConfigureSession<TSession, TOptions>(TOptions options = null)
            where TSession : BaseSession, new()
            where TOptions : BaseSessionOptions, new()

        {
            if (options is SessionHoursListOptions shOptions)
            {
                // Make sure session filters is not null.
                if (!HasSessionHours)
                    sessionHoursList = new SessionHoursList();

                // Configure....
                sessionHoursList.Configure<SessionHoursList, SessionHoursListOptions>(shOptions);

                // Update the configure flag
                if (!sessionHoursListIsConfigured)
                    sessionHoursListIsConfigured = true;

            }
            else if (options is SessionFiltersOptions fOptions)
            {
                // Make sure session filters is not null.
                if (!HasSessionFilters)
                    sessionFilters = new SessionFilters();

                // Configure....
                sessionFilters.Configure<SessionFilters, SessionFiltersOptions>(fOptions);

                // Update the configure flag
                if (!sessionFiltersIsConfigured)
                    sessionFiltersIsConfigured = true;
            }



            return this;
        }

        /// <summary>
        /// Add <see cref="NtScriptConfigureOptions"/> to <see cref="NtScript"/> configure.
        /// </summary>
        /// <param name="options">The ninjascript configure options.</param>
        public override SessionsManager ConfigureNtScripts<SessionsManager>(Action<NtScriptConfigureOptions> options = null)
        {
            // Make sure the trading hours is sessionHoursListIsConfigured by default
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

        #region SessionHoursList

        ///// <summary>
        ///// Add <see cref="SessionsManagerOptions"/> to <see cref="SessionsManager"/> configure.
        ///// </summary>
        ///// <param name="options"></param>
        ///// <returns></returns>
        //public SessionsManager ConfigureSessionHoursList(Action<SessionHoursListOptions> options = null)
        //{
        //    // Make sure session filters is not null.
        //    if (!HasSessionHours)
        //        sessionHoursList = new SessionHoursList();

        //    // Configure....
        //    sessionHoursList.Configure(options);

        //    // Update the configure flag
        //    if (!sessionHoursListIsConfigured)
        //        sessionHoursListIsConfigured = true;

        //    return this;

        //}

        ///// <summary>
        ///// Add filters funcionality to session manager.
        ///// </summary>
        ///// <param name="options"></param>
        ///// <returns></returns>
        //public SessionsManager ConfigureSessionHoursList(SessionHoursListOptions options = null)
        //{
        //    // Make sure session filters is not null.
        //    if (!HasSessionHours)
        //        sessionHoursList = new SessionHoursList();

        //    // Configure....
        //    sessionHoursList.Configure(options);

        //    // Update the configure flag
        //    if (!sessionHoursListIsConfigured)
        //        sessionHoursListIsConfigured = true;

        //    return this;
        //}

        ///// <summary>
        ///// Add <see cref="SessionsManagerOptions"/> to <see cref="SessionsManager"/> configure.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="options"></param>
        ///// <returns></returns>
        //public SessionsManager ConfigureSessionHoursList<T>(Action<T> options = null)
        //    where T : SessionHoursListOptions, new()
        //{
        //    // Make sure session filters is not null.
        //    if (!HasSessionHours)
        //        sessionHoursList = new SessionHoursList();

        //    // Configure....
        //    sessionHoursList.Configure(options);

        //    // Update the configure flag
        //    if (!sessionHoursListIsConfigured)
        //        sessionHoursListIsConfigured = true;

        //    return this;
        //}

        ///// <summary>
        ///// Add <see cref="SessionsManagerOptions"/> to <see cref="SessionsManager"/> configure.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="options"></param>
        ///// <returns></returns>
        //public SessionsManager ConfigureSessionHoursList<T>(T options = null)
        //    where T : SessionHoursListOptions, new()
        //{
        //    // Make sure session filters is not null.
        //    if (!HasSessionHours)
        //        sessionHoursList = new SessionHoursList();

        //    // Configure....
        //    sessionHoursList.Configure(options);

        //    // Update the configure flag
        //    if (!sessionHoursListIsConfigured)
        //        sessionHoursListIsConfigured = true;

        //    return this;
        //}

        #endregion

        #region SessionFilters

        ///// <summary>
        ///// Add filters funcionality to session manager.
        ///// </summary>
        ///// <param name="options"></param>
        ///// <returns></returns>
        //public SessionsManager ConfigureSessionFilters(Action<SessionFiltersOptions> options = null)
        //{
        //    // Make sure session filters is not null.
        //    if (sessionFilters == null)
        //        sessionFilters = new SessionFilters();

        //    // Configure....
        //    sessionFilters.Configure<SessionFilters,SessionFiltersOptions>(options);

        //    // Update the configure flag
        //    if (!sessionFiltersIsConfigured)
        //        sessionFiltersIsConfigured = true;

        //    return this;
        //}

        ///// <summary>
        ///// Add filters funcionality to session manager.
        ///// </summary>
        ///// <param name="options"></param>
        ///// <returns></returns>
        //public SessionsManager ConfigureSessionFilters(SessionFiltersOptions options = null)
        //{
        //    // Make sure session filters is not null.
        //    if (sessionFilters == null)
        //        sessionFilters = new SessionFilters();

        //    sessionFilters.Configure<SessionFilters, SessionFiltersOptions>(options);

        //    // Update the configure flag
        //    if (!sessionFiltersIsConfigured)
        //        sessionFiltersIsConfigured = true;

        //    return this;
        //}

        ///// <summary>
        ///// Add filters funcionality to session manager.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="options"></param>
        ///// <returns></returns>
        //public SessionsManager ConfigureSessionFilters<T>(Action<T> options = null)
        //    where T : SessionFiltersOptions, new()
        //{
        //    // Make sure session filters is not null.
        //    if (sessionFilters == null)
        //        sessionFilters = new SessionFilters();

        //    sessionFilters.Configure<SessionFilters, SessionFiltersOptions>((Action<SessionFiltersOptions>)options);

        //    // Update the configure flag
        //    if (!sessionFiltersIsConfigured)
        //        sessionFiltersIsConfigured = true;

        //    return this;
        //}

        ///// <summary>
        ///// Add filters funcionality to session manager.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="options"></param>
        ///// <returns></returns>
        //public SessionsManager ConfigureSessionFilters<T>(T options = null)
        //    where T : SessionFiltersOptions, new()
        //{
        //    // Make sure session filters is not null.
        //    if (sessionFilters == null)
        //        sessionFilters = new SessionFilters();

        //    sessionFilters.Configure<SessionFilters, SessionFiltersOptions>(options);

        //    // Update the configure flag
        //    if (!sessionFiltersIsConfigured)
        //        sessionFiltersIsConfigured = true;

        //    return this;
        //}

        #endregion

        #endregion

        #region Public methods


        #endregion

        #region Extension methods
        
        /// <summary>
        /// Create <see cref="SessionsManagerBuilder"/> to build <see cref="SessionsManager"/> object.
        /// </summary>
        /// <returns></returns>
        public static SessionsManagerBuilder CreateSessionManagerBuilder()
        {
            return new SessionsManagerBuilder();
        }

        #endregion

    }
}
