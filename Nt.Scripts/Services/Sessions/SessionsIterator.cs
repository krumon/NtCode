using NinjaTrader.Data;
using Nt.Core.Events;
using Nt.Core.Logging;
using System;
using System.Collections.Generic;

namespace Nt.Scripts.Services
{

    /// <summary>
    /// Determinates all properties of the sessions configure by ninjatrader and user.
    /// indicates when the sessions configured changes.
    /// </summary>
    public class SessionsIterator : ISessionsIterator
    {

        #region Private members

        private readonly INinjascript _ninjascript;
        private readonly Bars _bars;
        private readonly IGlobalsData _globalsData;
        private readonly ILogger<SessionsIterator> _logger;

        protected DateTime _currentSessionEnd;
        protected DateTime _sessionDateTmp;
        private readonly List<int> newSessionBarIdx = new List<int>();
        private SessionIterator _sessionIterator;
        private PartialHoliday _partialHoliday;


        #endregion

        #region Public properties

        public DateTime ActualSessionBegin { get; set; }
        public DateTime ActualSessionEnd { get; protected set; }
        public TimeZoneInfo UserTimeZoneInfo { get; protected set; }
        public int Count => newSessionBarIdx.Count;
        public TimeZoneInfo BarsTimeZoneInfo { get; protected set; }
        public bool IsSessionUpdated { get; protected set; }
        public bool IsConfigured { get; protected set; }
        public bool IsDataLoaded { get; protected set; }
        public bool? BarsTypeIsIntraday => _bars?.BarsType.IsIntraday;
        public int CurrentBar { get; protected set; }
        public DateTime CurrentTime { get; protected set; }
        public bool? IsPartialHoliday => _partialHoliday != null;
        public bool? IsLateBegin => _partialHoliday?.IsLateBegin;
        public bool? IsEarlyEnd => _partialHoliday?.IsEarlyEnd;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionsIteratorService"/> default instance.
        /// </summary>
        /// <param name="globalsData">The global data necesary to create the service.</param>
        public SessionsIterator(INinjascript ninjascript, Bars bars, IGlobalsData  globalsData, ILogger<SessionsIterator> logger)
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException(nameof(ninjascript));
            _bars = bars ?? throw new ArgumentNullException(nameof(bars));
            _globalsData = globalsData ?? throw new ArgumentNullException(nameof(globalsData));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Public methods

        public void Configure() => IsConfigured = true;
        public void DataLoaded()
        {
            // Make sure the script is configured
            if (!IsConfigured)
                return;
            // Initialize variables
            _currentSessionEnd = _globalsData.MinDate;
            _sessionDateTmp = _globalsData.MinDate;
            UserTimeZoneInfo = _globalsData.UserConfigureTimeZoneInfo;
            ActualSessionBegin = _globalsData.MinDate;
            ActualSessionEnd = _globalsData.MaxDate;
            try
            {
                BarsTimeZoneInfo = _bars.TradingHours.TimeZoneInfo;
                _sessionIterator = new SessionIterator(_bars);
                IsDataLoaded = true;
                _logger.LogInformation("Sessions Iterator has been configured.");
            }
            catch
            {
                _logger.LogError("Data loaded error. The object cannot be configured.");
            }
        }
        public void Dispose() { }
        public virtual void OnBarUpdate()
        {
            CurrentBar = _ninjascript.Instance.CurrentBar;
            CurrentTime = _ninjascript.Instance.Time[0];

            LastBarUpdate();
        }
        public virtual void OnMarketData() => LastBarUpdate();
        public virtual void OnSessionUpdate()  { }
        
        public string ToLongString()
        {
            string holidayText;
            if (IsPartialHoliday == true)
            {
                holidayText = "Partial Holiday. ";
                if (IsLateBegin == true)
                    holidayText += "Late Begin.";
                if (IsEarlyEnd == true)
                    holidayText += "Early End";
            }
            else
                holidayText = "Regular Session";

            return $"Session {Count}: Begin: {ActualSessionBegin.ToShortDateString()} End: {ActualSessionEnd.ToShortDateString()} | {holidayText}";
        }

        #endregion

        #region Public events

        /// <summary>
        /// Event thats is raised when the sessoin changed.
        /// </summary>
        public event Action<SessionUpdateArgs> SessionChanged = (e) => { };

        #endregion

        #region Private methods

        private void LastBarUpdate()
        {
            DateTime lastBarTimeStamp = GetLastBarSessionDate(CurrentTime);

            if (lastBarTimeStamp != _currentSessionEnd)
            {
                // Update the current value
                _currentSessionEnd = lastBarTimeStamp;
                // The new session begin
                IsSessionUpdated = true;
                // Check if it's a partial holiday
                TryGetPartialHoliday();
                // Create the session changed arguments
                var args = new SessionUpdateArgs(CurrentBar, Count, ActualSessionBegin, ActualSessionEnd, UserTimeZoneInfo, IsPartialHoliday, IsLateBegin, IsEarlyEnd);
                // Call the parent
                OnSessionUpdate();
                // Call to listeners
                SessionChanged?.Invoke(args);
            }
            else
                IsSessionUpdated = false;
            
        }
        private DateTime GetLastBarSessionDate(DateTime time)
        {
            var currentTime = CurrentTime;
            // Make sure the session is terminated
            if (time <= ActualSessionEnd)
                return _sessionDateTmp;

            // Make sure the bars type are intraday
            if (BarsTypeIsIntraday == false)
                return _sessionDateTmp;

            // Get and update the next session values
            GetAndUpdateNextSessionValues(time);

            // Converts the start and end time to bar's configured time zone.
            _sessionDateTmp = TimeZoneInfo.ConvertTime(ActualSessionEnd.AddSeconds(-1), UserTimeZoneInfo, BarsTimeZoneInfo);

            // Store the bar index of the session.
            if (newSessionBarIdx.Count == 0 ||
                newSessionBarIdx.Count > 0 && CurrentBar > newSessionBarIdx[newSessionBarIdx.Count - 1])
                newSessionBarIdx.Add(CurrentBar);

            return _sessionDateTmp;
        }
        private void GetAndUpdateNextSessionValues(DateTime time, bool includeEndTimeStamp = true)
        {
            // Get the next session value.
            _sessionIterator.GetNextSession(time, includeEndTimeStamp);
            // Update the next session value.
            ActualSessionBegin = _sessionIterator.ActualSessionBegin;
            ActualSessionEnd = _sessionIterator.ActualSessionEnd;
        }
        private bool? TryGetPartialHoliday() => _bars.TradingHours.PartialHolidays.TryGetValue(ActualSessionEnd.Date, out _partialHoliday);

        #endregion

    }
}
