using Nt.Core.Events;
using Nt.Core.Hosting;
using System;
using System.Collections.Generic;

namespace Nt.Core.Services
{

    /// <summary>
    /// Controller the user configure session inside the ninjascrips.
    /// </summary>
    public abstract class SessionIteratorService : IHostedService, IOnBarUpdateService
    {

        #region Private members

        private readonly IGlobalDataService _globalDataService;

        /// <summary>
        /// The current session end in Bars TimeZoneInfo.
        /// </summary>
        private DateTime _currentSessionEnd;

        /// <summary>
        /// The session end time in Bars TimeZoneInfo.
        /// </summary>
        private DateTime _sessionDateTmp;

        /// <summary>
        /// TradingSessionInfo bar indexs collection.
        /// </summary>
        private List<int> newSessionBarIdx = new List<int>();

        /// <summary>
        /// Flag to indicate whe the session changed to new session.
        /// </summary>
        private bool isNewSession;

        /// <summary>
        /// Represents a partial partialHoliday
        /// </summary>
        private PartialHoliday partialHoliday;

        #endregion

        #region Public events

        /// <summary>
        /// Event thats is raised when the sessoin changed.
        /// </summary>
        public event Action<SessionChangedEventArgs> SessionChanged = (e) => { };

        #endregion

        #region Public properties

        public DateTime ActualSessionBegin { get; protected set; }
        public DateTime ActualSessionEnd { get; protected set; }
        public TimeZoneInfo UserTimeZoneInfo { get; protected set; }
        public int Count { get; protected set; }
        public TimeZoneInfo BarsTimeZoneInfo { get; protected set; }
        public abstract bool IsNewSession { get; protected set; }
        public bool IsConfigured { get; protected set; }  
        public bool IsDataLoaded { get; protected set; }  
        public abstract bool? IsBarsIntraday { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionsIterator"/> default instance.
        /// </summary>
        /// <param name="globalDataService">The global data necesary to create the service.</param>
        public SessionIteratorService(IGlobalDataService  globalDataService)
        {
            _globalDataService = globalDataService;
        }

        #endregion

        #region Public methods

        /// <inheritdoc/>
        public abstract void Dispose();

        /// <inheritdoc/>
        public virtual void Configure(object[] ninjascriptObjects)
        {
            _currentSessionEnd = _globalDataService.MinDate;
            _sessionDateTmp = _globalDataService.MinDate;
            UserTimeZoneInfo = _globalDataService.UserConfigureTimeZoneInfo;
        }

        /// <inheritdoc/>
        public abstract void DataLoaded(object[] ninjascriptObjects);

        /// <summary>
        /// Event driven method which is called whenever a bar is updated. 
        /// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
        /// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
        /// </summary>
        public virtual void OnBarUpdate()
        {
            LastBarUpdate();
        }

        /// <summary>
        /// Returns the session last bar date.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="bars"></param>
        /// <param name="platformTimeZoneInfo"></param>
        /// <returns></returns>
        public DateTime GetLastBarSessionDate(DateTime time)
        {
            // Make sure the session is terminated
            if (time <= ActualSessionEnd)
                return _sessionDateTmp;

            // Make sure the bars type are not intraday
            if (IsBarsIntraday == false)
                return _sessionDateTmp;

            // Get the next session values
            sessionIterator.GetNextSession(time, true);

            // Store the start and final time in user's configured time zone.
            ActualSessionBegin = sessionIterator.ActualSessionBegin;
            ActualSessionEnd = sessionIterator.ActualSessionEnd;

            // Converts the start and end time to bar's configured time zone.
            _sessionDateTmp = TimeZoneInfo.ConvertTime(ActualSessionEnd.AddSeconds(-1), UserTimeZoneInfo, BarsTimeZoneInfo);

            // Store the bar index of the session.
            if (newSessionBarIdx.Count == 0 ||
                newSessionBarIdx.Count > 0 && ninjascript.CurrentBar > newSessionBarIdx[newSessionBarIdx.Count - 1])
                newSessionBarIdx.Add(ninjascript.CurrentBar);

            return _sessionDateTmp;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Mthod to raise the event and driven methods.
        /// </summary>
        /// <param name="e"></param>
        private void OnNtSessionChanged(SessionChangedEventArgs e)
        {

            // Raise the handler method
            OnSessionChanged(e);

            // Raise the event
            SessionChanged?.Invoke(e);

        }

        /// <summary>
        /// Method to update the last bar time.
        /// </summary>
        private void LastBarUpdate()
        {
            DateTime lastBarTimeStamp = GetLastBarSessionDate(ninjascript.Time[0]);
            bool isNewSession = false;

            if (lastBarTimeStamp != _currentSessionEnd)
                isNewSession = true;
            
            _currentSessionEnd = lastBarTimeStamp;
            IsNewSession = isNewSession;
        }

        #endregion

    }
}
