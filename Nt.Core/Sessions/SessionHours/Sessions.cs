using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// Represents the Sessions of the day trading.
    /// </summary>
    public class Sessions : NtScript
    {

        #region Private members

        /// <summary>
        /// Represents the generic sessions configure.
        /// </summary>
        private readonly GenericSessionsConfigure genericConfigure;

        /// <summary>
        /// Represents the custom sessions configure.
        /// </summary>
        private readonly CustomSessionsConfigure customConfigure;

        /// <summary>
        /// Store the children sessions.
        /// </summary>
        private List<SessionHours> children;

        #endregion

        #region Public properties

        /// <summary>
        /// Collection of <see cref="SessionHours"/>.
        /// </summary>
        public List<SessionHours> Children 
        {
            get 
            {
                if (children == null) 
                    children = new List<SessionHours>();

                return children;
            }
        }

        /// <summary>
        /// Indicates if the <see cref="Sessions"/> has children.
        /// </summary>
        public bool HasSessions => Children != null && Children.Count >= 1;

        /// <summary>
        /// The <see cref="DateTime"/> object of the actual ninjatrader session begin.
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// The <see cref="DateTime"/> object of the actual ninjatrader session end.
        /// </summary>
        public DateTime EndTime { get;set; }

        /// <summary>
        /// Indicates if the trading hours is a partial holiday.
        /// </summary>
        public bool IsPartialHoliday { get; private set; } // => PartialHoliday != null; // {get; private set;}

        /// <summary>
        /// Indicates if the partial holiday has a late begin time.
        /// </summary>
        public bool IsLateBegin { get; private set; } //=> IsPartialHoliday && PartialHoliday.IsLateBegin;

        /// <summary>
        /// Indicates if the partial holiday has a early end.
        /// </summary>
        public bool IsEarlyEnd { get; private set; } //=> IsPartialHoliday && PartialHoliday.IsEarlyEnd;

        /// <summary>
        /// The number of main children stored.
        /// </summary>
        public int N { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Create a default instance of <see cref="Sessions"/>.
        /// </summary>
        public Sessions()
        {
        }

        /// <summary>
        /// Create a default instance of <see cref="Sessions"/> with configure parameters.
        /// </summary>
        /// <param name="genericConfigure">The generic sessions configure object.</param>
        /// <param name="customConfigure">The custom sessions configure object.</param>
        public Sessions(GenericSessionsConfigure genericConfigure, CustomSessionsConfigure customConfigure)
        {
            this.genericConfigure = genericConfigure;
            this.customConfigure = customConfigure;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// If the trading hours is in partial holiday, gets the Partial Holiday object, otherwise, partial holiday is null.
        /// </summary>
        /// <param name="e"></param>
        public void Init(SessionChangedEventArgs e)
        {
            Idx = e.Idx;
            BeginTime = e.NewSessionBeginTime;
            EndTime = e.NewSessionEndTime;
            IsPartialHoliday = e.IsPartialHoliday;
            IsEarlyEnd = e.IsEarlyEnd;
            IsLateBegin = e.IsLateBegin;

            // TODO: Add Generic Sessions existing in the configuration object. (American, Assian, Custom,...)
            //       Add Custom Sessions existing in the configuration object. 
        }

        public void AddSession( 
            TradingSession sessionType,
             InstrumentCode instrumentCode = InstrumentCode.Default,
             int includeInitialBalance = 0,
             int includeFinalBalance = 0)
        {

            if (Children == null)
                children = new List<SessionHours>();

            Add(sessionType.ToSessionHours(instrumentCode));
        }

        public void Remove(SessionHours session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            if (Children == null)
                throw new ArgumentNullException(nameof(Sessions));

            Children.Remove(session);

            if (Children.Count == 0)
                children = null;

        }

        public void Clear()
        {
            Children.Clear();

            children = null;
        }

        /// <summary>
        /// Converts the <see cref="SessionHours"/> to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string holidayText = !IsPartialHoliday ? 
                "Regular Day." : IsLateBegin ? 
                "Partial Holiday - Late Begin." : "Partial Holiday - Early End.";
            return 
                $"N: {N} " +
                $"| Begin: {BeginTime.ToShortDateString()} {BeginTime.ToLongTimeString()} " +
                $"| End: {EndTime.ToShortDateString()} {EndTime.ToLongTimeString()} " +
                $"| {holidayText}";
            
        }

        //public void Iterator(Action<SessionHours> action = null)
        //{
        //    action(this);
        //    if (HasSessions)
        //        for (int i = 0; i < Sessions.Count; i++)
        //            Sessions[i].Iterator(action);
        //}

        #endregion

        #region Market Data methods

        #endregion

        #region Private methods

        
        // TODO: Codificar el método "Add" para añadir sesiones conforme al enum TradingSession
        //       y organizando según queramos que se vean las sesiones.
        private void Add(SessionHours session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            //if (HasSessions)
            //{
            //    DateTime[] nextDateTimes = session.GetNextDateTimes(DateTime.Now);
            //    foreach (var s in Sessions)
            //    {
            //        session.IsInnerSession(s);
            //    }
            //}
            //else
            //    Sessions.Add(session);

            Children.Add(session);
        }

        #endregion

    }


}
