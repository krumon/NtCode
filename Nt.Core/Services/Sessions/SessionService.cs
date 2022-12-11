using Nt.Core.Data;
using Nt.Core.Events;
using Nt.Core.Hosting;
using System;
using System.Collections.Generic;

namespace Nt.Core.Services
{

    /// <summary>
    /// Controller the user configure session inside the ninjascrips.
    /// </summary>
    public abstract class SessionService : IHostedService, ISessionService, IOnBarUpdateService
    {

        #region Private members

        protected readonly IGlobalDataService _globalDataService;
        protected DateTime _currentSessionEnd;
        protected DateTime _sessionDateTmp;
        private readonly List<int> newSessionBarIdx = new List<int>();

        #endregion

        #region Public properties

        public DateTime ActualSessionBegin { get; protected set; }
        public DateTime ActualSessionEnd { get; protected set; }
        public TimeZoneInfo UserTimeZoneInfo { get; protected set; }
        public int Count => newSessionBarIdx.Count;
        public TimeZoneInfo BarsTimeZoneInfo { get; protected set; }
        public bool NewSessionBegin { get; protected set; }
        public bool IsConfigured { get; protected set; }
        public bool IsDataLoaded { get; protected set; }
        public abstract bool? BarsTypeIsIntraday { get; }
        public abstract int CurrentBar { get; }
        public abstract DateTime CurrentTime { get; }
        public abstract bool? IsPartialHoliday { get; }
        public abstract bool? IsLateBegin { get; }
        public abstract bool? IsEarlyEnd {get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionsIterator"/> default instance.
        /// </summary>
        /// <param name="globalDataService">The global data necesary to create the service.</param>
        public SessionService(IGlobalDataService  globalDataService)
        {
            _globalDataService = globalDataService;
        }

        #endregion

        #region Public methods

        public abstract void Dispose();
        public abstract void DataLoaded(object[] ninjascriptObjects);
        public abstract void GetAndUpdateNextSessionValues(DateTime time, bool includeEndTimeStamp = true);
        public abstract bool? TryGetPartialHoliday();
        protected virtual void OnSessionChanged() { }
        public virtual void Configure(object[] ninjascriptObjects) => IsConfigured = true;
        public virtual void OnBarUpdate() => LastBarUpdate();
        public DateTime GetLastBarSessionDate(DateTime time)
        {
            // Make sure the session is terminated
            if (time <= ActualSessionEnd)
                return _sessionDateTmp;

            // Make sure the bars type are not intraday
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

        #endregion

        #region Public events

        /// <summary>
        /// Event thats is raised when the sessoin changed.
        /// </summary>
        public event Action<SessionChangedEventArgs> SessionChanged = (e) => { };

        #endregion

        #region Private methods

        /// <summary>
        /// Method to update the last bar time.
        /// </summary>
        private void LastBarUpdate()
        {
            DateTime lastBarTimeStamp = GetLastBarSessionDate(CurrentTime);

            if (lastBarTimeStamp != _currentSessionEnd)
            {
                // Update the current value
                _currentSessionEnd = lastBarTimeStamp;
                // The new session begin
                NewSessionBegin = true;
                // Check if it's a partial holiday
                TryGetPartialHoliday();
                // Create the session changed arguments
                var args = new SessionChangedEventArgs(CurrentBar, Count, ActualSessionBegin, ActualSessionEnd, UserTimeZoneInfo, IsPartialHoliday, IsLateBegin, IsEarlyEnd);
                // Call the parent
                OnSessionChanged();
                // Call to listeners
                SessionChanged?.Invoke(args);
            }
            else
                NewSessionBegin = false;
            
        }

        public override string ToString()
        {
            string holidayText = string.Empty;
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

            return $"Session {Count}: Begin: {ActualSessionBegin.ToShortDateString()} {ActualSessionEnd.ToShortDateString()} | {holidayText}";
        }

        #endregion

    }
}
