using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    ///// <summary>
    ///// Represents the Trading Hours structure.
    ///// </summary>
    //public class UserSession : NtScript
    //{

    //    #region Private members

    //    private readonly GenericSessionsConfigure configure;

    //    /// <summary>
    //    /// The ninjascript parent of the class.
    //    /// </summary>
    //    //private NinjaScriptBase ninjascript;

    //    /// <summary>
    //    /// The bars of the chart control.
    //    /// </summary>
    //    //private Bars bars;

    //    /// <summary>
    //    /// SessionHours collection.
    //    /// </summary>
    //    private List<SessionHours> children;

    //    /// <summary>
    //    /// Store the patial holiday object when the trading hours is in a partial holiday.
    //    /// </summary>
    //    //private PartialHoliday partialHoliday;

    //    /// <summary>
    //    /// Represents the ninjatrader session configure on the chart bars.
    //    /// </summary>
    //    //public UserSession ntSession;

    //    #endregion

    //    #region Public properties

    //    /// <summary>
    //    /// Collection of <see cref="SessionHours"/>.
    //    /// </summary>
    //    public List<SessionHours> Sessions 
    //    {
    //        get 
    //        {
    //            if (children == null) 
    //                children = new List<SessionHours>();

    //            return children;
    //        }
    //    }

    //    /// <summary>
    //    /// Indicates if the <see cref="SessionHours"/> has children.
    //    /// </summary>
    //    public bool HasSessions => Sessions != null && Sessions.Count >= 1;

    //    /// <summary>
    //    /// The <see cref="DateTime"/> object of the actual ninjatrader session begin.
    //    /// </summary>
    //    public DateTime BeginTime { get; set; }

    //    /// <summary>
    //    /// The <see cref="DateTime"/> object of the actual ninjatrader session end.
    //    /// </summary>
    //    public DateTime EndTime { get;set; }

    //    /// <summary>
    //    /// Store the patial holiday object when the trading hours is in a partial holiday.
    //    /// </summary>
    //    //public PartialHoliday PartialHoliday => partialHoliday;

    //    /// <summary>
    //    /// Indicates if the trading hours is a partial holiday.
    //    /// </summary>
    //    public bool IsPartialHoliday { get; private set; } // => PartialHoliday != null; // {get; private set;}

    //    /// <summary>
    //    /// Indicates if the partial holiday has a late begin time.
    //    /// </summary>
    //    public bool IsLateBegin { get; private set; } //=> IsPartialHoliday && PartialHoliday.IsLateBegin;

    //    /// <summary>
    //    /// Indicates if the partial holiday has a early end.
    //    /// </summary>
    //    public bool IsEarlyEnd { get; private set; } //=> IsPartialHoliday && PartialHoliday.IsEarlyEnd;

    //    /// <summary>
    //    /// The number of main children stored.
    //    /// </summary>
    //    public int N { get; set; }

    //    #endregion

    //    #region Constructor

    //    /// <summary>
    //    /// Create a default instance of <see cref="UserSession"/>.
    //    /// </summary>
    //    public UserSession()
    //    {
    //        this.configure = new GenericSessionsDefaultConfigure();

    //        //this.ntSession = ntSession;
    //        //this.bars = bars;

    //        //this.ntSession.UserSessionChanged += OnTradingSessionsChanged;
    //    }

    //    /// <summary>
    //    /// Create a default instance of <see cref="UserSession"/>.
    //    /// </summary>
    //    public UserSession(GenericSessionsConfigure configure)
    //    {
    //        if (configure == null)
    //            this.configure = new GenericSessionsDefaultConfigure();
    //        else
    //            this.configure = configure;

    //        //this.ntSession = ntSession;
    //        //this.bars = bars;

    //        //this.ntSession.UserSessionChanged += OnTradingSessionsChanged;
    //    }

    //    #endregion

    //    #region Public methods

    //    /// <summary>
    //    /// If the trading hours is in partial holiday, gets the Partial Holiday object, otherwise, partial holiday is null.
    //    /// </summary>
    //    /// <param name="e"></param>
    //    public void Init(SessionChangedEventArgs e)
    //    {
    //        Idx = e.Idx;
    //        BeginTime = e.NewSessionBeginTime;
    //        EndTime = e.NewSessionEndTime;
    //        IsPartialHoliday = e.IsPartialHoliday;
    //        IsEarlyEnd = e.IsEarlyEnd;
    //        IsLateBegin = e.IsLateBegin;

    //        // TODO: Add Sessions selected by the user in the ninjascript. (American, Assian, Custom,...)

    //    }

    //    /// <summary>
    //    /// Free the memory.
    //    /// </summary>
    //    //public override void Dispose()
    //    //{
    //    //    //ntSession.UserSessionChanged -= OnSessionChanged;
    //    //}


    //    public void AddSession( 
    //        TradingSession sessionType,
    //         InstrumentCode instrumentCode = InstrumentCode.Default,
    //         int includeInitialBalance = 0,
    //         int includeFinalBalance = 0)
    //    {

    //        if (Sessions == null)
    //            children = new List<SessionHours>();

    //        Add(sessionType.ToSessionHours(instrumentCode));
    //    }

    //    public void Remove(SessionHours session)
    //    {
    //        if (session == null)
    //            throw new ArgumentNullException(nameof(session));

    //        if (Sessions == null)
    //            throw new ArgumentNullException(nameof(Sessions));

    //        Sessions.Remove(session);

    //        if (Sessions.Count == 0)
    //            children = null;

    //    }

    //    public void Clear()
    //    {
    //        Sessions.Clear();

    //        children = null;
    //    }

    //    /// <summary>
    //    /// Converts the <see cref="SessionHours"/> to string.
    //    /// </summary>
    //    /// <returns></returns>
    //    public override string ToString()
    //    {
    //        string holidayText = !IsPartialHoliday ? 
    //            "Regular Day." : IsLateBegin ? 
    //            "Partial Holiday - Late Begin." : "Partial Holiday - Early End.";
    //        return 
    //            $"N: {N} " +
    //            $"| Begin: {BeginTime.ToShortDateString()} {BeginTime.ToLongTimeString()} " +
    //            $"| End: {EndTime.ToShortDateString()} {EndTime.ToLongTimeString()} " +
    //            $"| {holidayText}";
            
    //    }

    //    //public void Iterator(Action<SessionHours> action = null)
    //    //{
    //    //    action(this);
    //    //    if (HasSessions)
    //    //        for (int i = 0; i < Sessions.Count; i++)
    //    //            Sessions[i].Iterator(action);
    //    //}

    //    /// <summary>
    //    /// Returns the string with the number of the session, the begin time and the end time.
    //    /// </summary>
    //    /// <returns></returns>
    //    //public override string ToString()
    //    //{
    //    //    return $"SessionHours {N}: Begin: {currentSessions.SessionBegin.ToString()} | End: {currentSessions.SessionEnd.ToString()}";
    //    //}

    //    // TODO:    - Método para añadir las sesiones.
    //    //          - Método para saber si una sesión es menor o mayor que otra.

    //    #endregion

    //    #region Market Data methods

    //    #endregion

    //    #region Private methods

        
    //    // TODO: Codificar el método "Add" para añadir sesiones conforme al enum TradingSession
    //    //       y organizando según queramos que se vean las sesiones.
    //    private void Add(SessionHours session)
    //    {
    //        if (session == null)
    //            throw new ArgumentNullException(nameof(session));

    //        //if (HasSessions)
    //        //{
    //        //    DateTime[] nextDateTimes = session.GetNextDateTimes(DateTime.Now);
    //        //    foreach (var s in Sessions)
    //        //    {
    //        //        session.IsInnerSession(s);
    //        //    }
    //        //}
    //        //else
    //        //    Sessions.Add(session);

    //        Sessions.Add(session);
    //    }

    //    #endregion

    //    #region Para revisar

    //    //public Sessions AddDefaultSessions(InstrumentCode instrumentCode = InstrumentCode.Default)
    //    //{
    //    //    // TODO: Borrar. Es KrSessionHours la que tiene que tener el método AddSession.
    //    //    //       AddSession(TradingSession.Regular, instrumentCode, 0, 0);

    //    //    // Main Sessions
    //    //    Sessions[0].AddSession(TradingSession.Regular, instrumentCode, 0, 0);
    //    //    Sessions[0].AddSession(TradingSession.OVN, instrumentCode, 0, 0);
    //    //    // Regular Sessions
    //    //    Sessions[0].Sessions[0].AddSession(TradingSession.AmericanAndEuropean, instrumentCode, 0, 0);
    //    //    Sessions[0].Sessions[0].AddSession(TradingSession.American, instrumentCode, 0, 0);
    //    //    // Overnight Sessions
    //    //    Sessions[0].Sessions[1].AddSession(TradingSession.American_RS, instrumentCode, 0, 0);
    //    //    Sessions[0].Sessions[1].AddSession(TradingSession.Asian, instrumentCode, 0, 0);
    //    //    Sessions[0].Sessions[1].AddSession(TradingSession.Asian_RS, instrumentCode, 0, 0);
    //    //    Sessions[0].Sessions[1].AddSession(TradingSession.European, instrumentCode, 0, 0);
    //    //    // Minor Sessions
    //    //    Sessions[0].Sessions[1].Sessions[0].AddSession(TradingSession.American_RS_EXT, instrumentCode, 0, 0);
    //    //    Sessions[0].Sessions[1].Sessions[0].AddSession(TradingSession.American_RS_EOD, instrumentCode, 0, 0);
    //    //    Sessions[0].Sessions[1].Sessions[0].AddSession(TradingSession.American_RS_NWD, instrumentCode, 0, 0);

    //    //    return this;
    //    //}

    //    //protected bool IsInSession(DateTime time)
    //    //{
    //    //    CurrentTime = time;

    //    //    if (CurrentTime > NextBeginDateTime)
    //    //    {
    //    //        if (CurrentTime > NextEndDateTime)
    //    //        {
    //    //            //NextBeginDateTime = GetNextBeginDateTime(CurrentTime);
    //    //            //NextEndDateTime = GetNextEndDateTime(CurrentTime);
    //    //            return true;
    //    //        }
    //    //    }
    //    //    return false;
    //    //}

    //    /// <summary>
    //    /// Create a new instance of <see cref="SessionHours"/> class with <see cref="TradingSession"/>.
    //    /// </summary>
    //    /// <param name="genericSession">the <see cref="TradingSession"/> to create the <see cref="SessionHours"/> class.</param>
    //    /// <param name="instrumentCode">The unique code of the instrument.</param>
    //    /// <param name="balanceMinutes">The minutes of the balance session.</param>
    //    /// <returns>A new instance of <see cref="SessionHours"/> class.</returns>
    //    //public static KrSessionHours CreateSessionHours(TradingSession genericSession, InstrumentCode instrumentCode = InstrumentCode.Default, int balanceMinutes = 0)
    //    //{
    //    //return new KrSessionHours
    //    //{
    //    //BeginSessionTime = genericSession.ToBeginSessionTime(instrumentCode, balanceMinutes),
    //    //EndSessionTime = genericSession.ToEndSessionTime(instrumentCode, balanceMinutes),
    //    //};
    //    //}

    //    //public DateTime CurrentTime { get; set; } = DateTime.MinValue;

    //    //public DateTime NextBeginDateTime { get; set; } = DateTime.MinValue;

    //    //public DateTime NextEndDateTime { get; set; } = DateTime.MinValue;

    //    #endregion

    //}


}
