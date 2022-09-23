using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// Represents consts, fields and properties of the Ninjatrader user configuration.
    /// </summary>
    public class SessionsManager : BaseManager<SessionsManager, SessionsManagerOptions,SessionsManagerBuilder>
    {

        #region Private members

        /// <summary>
        /// Gets the <see cref="SessionsIterator"/> funcionality.
        /// </summary>
        private SessionsIterator sessionsIterator;

        /// <summary>
        /// Represents the <see cref="SessionFilters"/> of the main sessions.
        /// </summary>
        private SessionFilters sessionFilters;

        /// <summary>
        /// Represents the <see cref="SessionStats"/> of the main sessions.
        /// </summary>
        private SessionStats sessionStats;

        /// <summary>
        /// Represents the <see cref="SessionHoursList"/> of the main session.
        /// </summary>
        private SessionHoursList sessionHoursList;

        #endregion

        #region Protected members

        /// <summary>
        /// Nijascripts collection
        /// </summary>
        protected new List<ISession> scripts;

        protected Dictionary<Type, ISession> sessions = new Dictionary<Type, ISession>();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the configured sessions.
        /// </summary>
        public List<ISession> Sessions => scripts;

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
        /// Gets the <see cref="SessionHoursList"/> of the manager.
        /// </summary>
        public SessionHoursList SessionHours => sessionHoursList;

        /// <summary>
        /// Gets the <see cref="SessionFilters"/> cof the manager.
        /// </summary>
        public SessionFilters SessionFilters => sessionFilters;

        /// <summary>
        /// Gets the <see cref="SessionStats"/> of the manager.
        /// </summary>
        public SessionStats SessionStats => sessionStats;

        /// <summary>
        /// Gets the <see cref="SessionsIterator"/> of the manager.
        /// </summary>
        public SessionsIterator SessionsIterator => sessionsIterator;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a <see cref="SessionsManager"/> default instance.
        /// </summary>
        public SessionsManager()
        {
        }

        #endregion

        #region StateChanged methods

        /// <summary>
        /// Loaded <see cref="SessionsManager"/> in "OnStateChanged" method.
        /// </summary>
        /// <param name="ninjascript">The ninjascript.</param>
        /// <param name="bars">The bars.</param>
        public override void Load(NinjaScriptBase ninjascript, Bars bars)
        {
            // Call the parent method to load.
            base.Load(ninjascript, bars);

            // If we need the session iterator...create him.
            // TODO: Make Sure we need the session iterator object.
            // Initialize the session iterator
            if (sessionsIterator == null)
                sessionsIterator = new SessionsIterator();

            // AddValues the elements...
            sessionsIterator.Load(ninjascript, bars);
            if (HasSessionFilters)
                sessionFilters.Load(ninjascript, bars);
            if (HasSessionHours)
                sessionHoursList.Load(ninjascript, bars);
            if (HasSessionStats)
                sessionStats.Load(ninjascript, bars);

            // Aggregate the delegates
            sessionsIterator.SessionChanged += OnSessionChanged;

        }

        /// <summary>
        /// Method used to free memory when the script is terminate.
        /// </summary>
        public override void Terminated()
        {
            // Disaggregate the delegates.
            sessionsIterator.SessionChanged -= OnSessionChanged;

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
        public void OnSessionChanged(SessionChangedEventArgs e)
        {
            // Update Holiday filters
            if (HasSessionFilters)
                sessionFilters.OnSessionChanged(e);

            // Update the session hours list.
            if (HasSessionHours)
                sessionHoursList.OnSessionChanged(e);

            // Execute delegate method.
            //ExecuteInSessionHoursChangedMethod(SessionHoursChangedAction);
        }

        #endregion

        #region Configure methods

        /// <summary>
        /// Add <see cref="SessionsManagerOptions"/> to <see cref="SessionsManager"/> configure.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManager ConfigureSession<TScript,TOptions,TBuilder>(Action<TOptions> options = null)
            where TScript : BaseSession<TScript,TOptions,TBuilder>, new()
            where TOptions : BaseSessionOptions<TOptions>, new()
            where TBuilder : BaseSessionBuilder<TScript,TOptions,TBuilder>, new()
        {
            TScript script;

            if (!sessions.ContainsKey(typeof(TScript)))
                sessions.Add(typeof(TScript), new TScript());
            
            script = (TScript)sessions[typeof(TScript)];

            script.Configure(options);

            return this;
        }

        public SessionsManager ConfigureSession<TOptions>(Action<TOptions> options = null)
            where TOptions : SessionsManagerOptions, new()
        {
            if (options is Action<SessionHoursListOptions> shOptions)
            {
                // Make sure session filters is not null.
                if (!HasSessionHours)
                    sessionHoursList = new SessionHoursList();

                // Configure....
                sessionHoursList.Configure(shOptions);

            }
            //else if (options is Action<SessionFiltersOptions> fOptions)
            //{
            //    // Make sure session filters is not null.
            //    if (!HasSessionFilters)
            //        sessionFilters = new SessionFilters();

            //    // Configure....
            //    sessionFilters.Configure(fOptions);

            //}

            return this;
        }

        /// <summary>
        /// Add filters funcionality to session manager.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionsManager ConfigureSession<TOptions>(TOptions options = null)
            where TOptions : SessionsManagerOptions, new()

        {
            if (options is SessionHoursListOptions shOptions)
            {
                // Make sure session filters is not null.
                if (!HasSessionHours)
                    sessionHoursList = new SessionHoursList();

                // Configure....
                sessionHoursList.Configure(shOptions);

            }
            else if (options is SessionFiltersOptions fOptions)
            {
                // Make sure session filters is not null.
                if (!HasSessionFilters)
                    sessionFilters = new SessionFilters();

                // Configure....
                sessionFilters.Configure(fOptions);

            }

            return this;
        }

        #endregion

        #region Extension methods
        
        

        /// <summary>
        /// Create <see cref="SessionsManagerBuilder"/> to build <see cref="SessionsManager"/> object.
        /// </summary>
        /// <returns></returns>
        //public static SessionsManagerBuilder CreateSessionManagerBuilder()
        //{
        //    return new SessionsManagerBuilder();
        //}

        #endregion

        #region ToString methods

        public override string ToString()
        {
            if (HasSessionHours)
                return sessionHoursList.ToString();
            else 
                return $"{nameof(SessionsManager)} is empty";
        }

        #endregion

    }
}
