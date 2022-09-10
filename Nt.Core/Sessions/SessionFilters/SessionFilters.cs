using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    public class SessionFilters : NtScript
    {

        #region static consts

        /// <summary>
        /// The minimum date for the filters.
        /// </summary>
        public readonly static DateTime MIN_DATE = new DateTime(1970, 1, 1);

        /// <summary>
        /// The maximum date for the filters.
        /// </summary>
        public readonly static DateTime MAX_DATE = new DateTime(2050, 12, 31);

        #endregion

        #region Private members

        /// <summary>
        /// Gets the <see cref="SessionsIterator"/> funcionality.
        /// </summary>
        private SessionsIterator sessionsIterator;

        /// <summary>
        /// The partial holiday object.
        /// </summary>
        private PartialHoliday partialHoliday;

        /// <summary>
        /// The current date and time.
        /// </summary>
        private DateTime currentDateTime;

        /// <summary>
        /// Represents the datetime when the object is initialized.
        /// </summary>
        private DateTime dateTimeNow;

        /// <summary>
        /// Flags to indicates if the <see cref="SessionFilters"/> is sessionsManagerIsConfigured.
        /// </summary>
        public bool configured;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets if the session is a partial holiday.
        /// </summary>
        public bool IsPartialHoliday => partialHoliday != null;

        /// <summary>
        /// Indicates if the partial holiday is late begin.
        /// </summary>
        public bool IsLateBegin => partialHoliday == null ? false : partialHoliday.IsLateBegin; // IsPartialHoliday && partialHoliday.IsLateBegin;

        /// <summary>
        /// Indicates if the partial holiday is early end.
        /// </summary>
        public bool IsEarlyEnd => partialHoliday == null ? false : partialHoliday.IsEarlyEnd; // IsPartialHoliday && partialHoliday.IsEarlyEnd;

        /// <summary>
        /// Gets if the ninjascript enter in historial data bars.
        /// </summary>
        public bool IncludeHistoricalData {get;private set;}

        /// <summary>
        /// Gets if the ninjascript enter in partial holidays.
        /// </summary>
        public bool IncludePartialHolidays { get; private set; }

        /// <summary>
        /// Gets if the ninjascript enter in partial holidays with late begin.
        /// </summary>
        public bool IncludeLateBegin { get; private set; }

        /// <summary>
        /// Gets if the ninjascript enter in partial holidays with early end.
        /// </summary>
        public bool IncludeEarlyEnd { get; private set; }

        /// <summary>
        /// Gets the minimum date to enter with the ninjascript.
        /// </summary>
        public DateTime InitialDate { get; private set; }

        /// <summary>
        /// Gets the maximum date to enter with the ninjascript.
        /// </summary>
        public DateTime FinalDate { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="SessionFilters"/> default instance.
        /// </summary>
        public SessionFilters()
        {
        }

        /// <summary>
        /// Create <see cref="SessionFilters"/> instance with parameters.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="bars"></param>
        private SessionFilters(NinjaScriptBase ninjascript, Bars bars)
        {
            this.ninjascript = ninjascript;
            this.bars = bars;
            dateTimeNow = DateTime.Now;
        }

        #endregion

        #region State changed methods

        /// <summary>
        /// Load the <see cref="SessionFilters"/>.
        /// </summary>
        /// <param name="ninjascript">The ninjascript.</param>
        /// <param name="bars">The bars.</param>
        /// <param name="o">Any object necesary to load the script.</param>
        public override void Load(NinjaScriptBase ninjascript, Bars bars, object o = null)
        {
            // Make sure the object is a session iterator and session manager can be loaded.
            if (ninjascript == null || bars == null || !(o is SessionsIterator sessionsIterator))
                throw new Exception("Parameters can not be null"); // return null;

            // Set values.
            this.ninjascript = ninjascript;
            this.bars = bars;
            this.sessionsIterator = sessionsIterator;

            // Make sure the ninjascript is sessionsManagerIsConfigured
            if (!configured)
                Configure();

            // Save now time for the historical data
            dateTimeNow = DateTime.Now;

            // Agregatte delegates
            sessionsIterator.SessionChanged += OnSessionHoursChanged;

        }

        /// <summary>
        /// Method used to free memory when the script is terminate.
        /// </summary>
        public override void Terminated()
        {
            // Disagregatte delegates
            sessionsIterator.SessionChanged -= OnSessionHoursChanged;
        }

        #endregion

        #region Configure methods

        /// <summary>
        /// Add <see cref="SessionFiltersOptions"/> to configure <see cref="SessionFilters"/>.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionFilters Configure(Action<SessionFiltersOptions> options = null)
        {
            // Create default session filters options.
            var sessionFiltersOptions = new SessionFiltersOptions();

            // If options is not null...invoke delegate to update the options configure by the user.
            if (options != null)
                options.Invoke(sessionFiltersOptions);

            // Mapper the sesion filters with the session filters options.
            AutoMapper(sessionFiltersOptions);

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
        public SessionFilters Configure<T>(Action<T> options)
            where T : SessionFiltersOptions, new()
        {
            // Create default session filters options.
            var sessionFiltersOptions = new T();

            // Invoke delegate to update the options configure by the user.
            if (options != null)
                options.Invoke(sessionFiltersOptions);

            // Mapper the sesion filters with the session filters options.
            AutoMapper(sessionFiltersOptions);

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
        public SessionFilters Configure<T>(T options = null)
            where T : SessionFiltersOptions, new()
        {
            // If options is null...create a default options...
            if (options == null)
                options = new T();

            // Mapper the sesion filters with the session filters options.
            AutoMapper(options);

            // Update the configure flag
            if (!configured)
                configured = true;

            return this;
        }

        #endregion

        #region Market data methods

        /// <summary>
        /// Event driven method which is called whenever a bar is updated. 
        /// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
        /// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
        /// </summary>
        public override void OnBarUpdate()
        {
            // Store the current date time
            currentDateTime = ninjascript.Time[0];
        }

        /// <summary>
        /// Event driven method which is called and guaranteed to be in the correct sequence 
        /// for every change in level one market data for the underlying instrument. 
        /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        /// </summary>
        public override void OnMarketData()
        {
            // Store the current date time
            currentDateTime = ninjascript.Time[0];
        }

        /// <summary>
        /// Changed any object or property when the session changed.
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnSessionHoursChanged(SessionChangedEventArgs e)
        {
            // if date is a partial hoilday...store the partial holiday
            if (!(bars.TradingHours.PartialHolidays.TryGetValue(ninjascript.Time[0], out partialHoliday)))
                partialHoliday = null;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Event driven method which is called whenever a bar is updated.
        /// Evaluated if the ninjascript pass the filter conditions.
        /// </summary>
        /// <returns>True if the ninjascript pass the filter conditions.</returns>
        public bool CanEntry()
        {
            OnBarUpdate();

            return CheckFilters();
        }


        #endregion

        #region Private methods

        /// <summary>
        /// Mapper <see cref="SessionFilters"/> with <see cref="SessionFiltersOptions"/>.
        /// </summary>
        /// <param name="options"></param>
        private void AutoMapper(SessionFiltersOptions options)
        {
            IncludeHistoricalData = options.IncludeHistoricalData;
            IncludePartialHolidays = options.IncludePartialHolidays;
            IncludeLateBegin = options.IncludeLateBegin;
            IncludeEarlyEnd = options.IncludeEarlyEnd;
            FinalDate = options.FinalDate;
            InitialDate = options.InitialDate;
        }

        /// <summary>
        /// Check the filters.
        /// </summary>
        /// <returns></returns>
        private bool CheckFilters()
        {
            bool historicalDataFilter   =   IncludeHistoricalData || currentDateTime > dateTimeNow;
            bool dateFilter             =   InitialDate >= currentDateTime && FinalDate <= currentDateTime;
            bool holidaysFilter         =   IncludePartialHolidays == IsPartialHoliday && IncludeEarlyEnd == IsEarlyEnd && IncludeLateBegin == IsLateBegin;
            bool dayOfWeekFilters       =   true;
            bool monthOfYearFilters     =   true;

            return
                historicalDataFilter &&
                dateFilter &&
                holidaysFilter &&
                monthOfYearFilters &&
                dayOfWeekFilters
                ;
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Mapper <see cref="SessionFilters"/> with <see cref="SessionFiltersOptions"/>.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="options"></param>
        public static void AutoMapper(SessionFilters session, SessionFiltersOptions options)
        {
            session.IncludeHistoricalData = options.IncludeHistoricalData;
            session.IncludePartialHolidays = options.IncludePartialHolidays;
            session.IncludeLateBegin = options.IncludeLateBegin;
            session.IncludeEarlyEnd = options.IncludeEarlyEnd;
            session.FinalDate = options.FinalDate;
            session.InitialDate = options.InitialDate;
        }

        #endregion

    }
}
