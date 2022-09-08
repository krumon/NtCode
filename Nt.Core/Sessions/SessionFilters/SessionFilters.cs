using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    public class SessionFilters : NtScript
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

        #endregion

        #region Public properties

        /// <summary>
        /// Indicates if the trading hours is a partial holiday.
        /// </summary>
        public bool IsPartialHoliday => partialHoliday != null;

        /// <summary>
        /// Indicates if the partial holiday has a late begin time.
        /// </summary>
        public bool IsLateBegin => IsPartialHoliday && partialHoliday.IsLateBegin;

        /// <summary>
        /// Indicates if the partial holiday has a early end.
        /// </summary>
        public bool IsEarlyEnd => IsPartialHoliday && partialHoliday.IsEarlyEnd;

        #endregion

        #region Filter options properties

        public bool IncludeHistoricalData { get; set; } = true;

        public bool IncludePartialHolidays { get; set; } = true;
        public bool IncludeLateBegin { get; set; } = true;
        public bool IncludeEarlyEnd { get; set; } = true;

        public int MinYear { get; set; } = int.MinValue;
        public int MaxYear { get; set; } = int.MaxValue;
        public int MinMonth { get; set; } = int.MinValue;
        public int MaxMonth { get; set; } = int.MaxValue;
        public int MinDay { get; set; } = int.MinValue;
        public int MaxDay { get; set; } = int.MaxValue;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="SessionFilters"/> default instance.
        /// </summary>
        /// <param name="bars"></param>
        /// <param name="ninjascript"></param>
        public SessionFilters(NinjaScriptBase ninjascript, Bars bars)
        {
            this.ninjascript = ninjascript;
            this.bars = bars;
            dateTimeNow = DateTime.Now;
        }

        #endregion

        #region Market data methods

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

        /// <summary>
        /// Event driven method which is called whenever a bar is updated. 
        /// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
        /// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
        /// </summary>
        public override void OnBarUpdate()
        {
            // Store the current date time
            currentDateTime = ninjascript.Time[0];

            // if date is a partial hoilday...store the partial holiday
            if (!(bars.TradingHours.PartialHolidays.TryGetValue(currentDateTime, out partialHoliday)))
                partialHoliday = null;
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

            // if date is a partial hoilday...store the partial holiday
            if (!(bars.TradingHours.PartialHolidays.TryGetValue(ninjascript.Time[0], out partialHoliday)))
                partialHoliday = null;
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

            session.MaxYear = options.MaxYear;
            session.MinYear = options.MinYear;
            session.MaxMonth = options.MaxMonth;
            session.MinMonth = options.MinMonth;
            session.MaxDay = options.MaxDay;
            session.MinDay = options.MinDay;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Add <see cref="SessionFiltersOptions"/> to configure <see cref="SessionFilters"/>.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public SessionFilters Configure(Action<SessionFiltersOptions> options)
        {
            var sessionFiltersOptions = new SessionFiltersOptions();
            options?.Invoke(sessionFiltersOptions);
            AutoMapper(sessionFiltersOptions);
            return this;
        }

        /// <summary>
        /// Mapper <see cref="SessionFilters"/> with <see cref="SessionFiltersOptions"/>.
        /// </summary>
        /// <param name="options"></param>
        public void AutoMapper(SessionFiltersOptions options)
        {
            IncludeHistoricalData = options.IncludeHistoricalData;

            IncludePartialHolidays = options.IncludePartialHolidays;
            IncludeLateBegin = options.IncludeLateBegin;
            IncludeEarlyEnd = options.IncludeEarlyEnd;

            MaxYear = options.MaxYear;
            MinYear = options.MinYear;
            MaxMonth = options.MaxMonth;
            MinMonth = options.MinMonth;
            MaxDay = options.MaxDay;
            MinDay = options.MinDay;
        }

        #endregion

        #region Private methods

        private bool CheckFilters()
        {
            bool historicalDataFilter   =   IncludeHistoricalData || currentDateTime > dateTimeNow;
            bool dateFilter             =   MinYear >= currentDateTime.Year && MaxYear <= currentDateTime.Year && 
                                            MinMonth >= currentDateTime.Month && MaxMonth <= currentDateTime.Month && 
                                            MinDay >= currentDateTime.Day && MaxDay <= currentDateTime.Day;
            bool holidaysFilter         =   IncludePartialHolidays == IsPartialHoliday && IncludeEarlyEnd == IsEarlyEnd && IncludeLateBegin == IsLateBegin;

            bool dayOfWeekFilters       =   true;

            return
                historicalDataFilter &&
                dateFilter &&
                holidaysFilter &&
                dayOfWeekFilters
                ;
        }


        #endregion
    }
}
