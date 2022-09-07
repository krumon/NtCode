using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// Represents the core of the ninjascripts  session hours.
    /// </summary>
    //public class NtSessionHours : NtIndicator
    //{

    //    #region Private members

    //    /// <summary>
    //    /// The ninjascript parent of the class.
    //    /// </summary>
    //    private NinjaScriptBase ninjascript;

    //    /// <summary>
    //    /// The bars of the chart control.
    //    /// </summary>
    //    private Bars bars;

    //    /// <summary>
    //    /// The session iterator to store the actual and next session data.
    //    /// </summary>
    //    private SessionIterator sessionIterator;

    //    /// <summary>
    //    /// The session hours structure core.
    //    /// </summary>
    //    private UserSession tradingHours;

    //    /// <summary>
    //    /// <see cref="UserSession"/> sorted collection.
    //    /// </summary>
    //    private List<UserSession> tradingHoursList = new List<UserSession>();

    //    /// <summary>
    //    /// Represents the ninjatrader session configure on the chart bars.
    //    /// </summary>
    //    public SessionsIterator ntSession;

    //    #endregion

    //    #region Public properties

    //    /// <summary>
    //    /// Represents the ninjatrader session configure on the chart bars.
    //    /// </summary>
    //    public SessionsIterator NtSession
    //    {
    //        get
    //        {
    //            if (ntSession == null)
    //                ntSession = new SessionsIterator(ninjascript, bars, SessionIterator);

    //            return ntSession;
    //        }
    //    }

    //    /// <summary>
    //    /// Represents the trading hours structure.
    //    /// </summary>
    //    public UserSession TradingHours
    //    {
    //        get
    //        {
    //            if (tradingHours == null)
    //                tradingHours = new UserSession();

    //            return tradingHours;
    //        }
    //    }


    //    /// <summary>
    //    /// The session iterator to store the actual and next session data.
    //    /// </summary>
    //    public SessionIterator SessionIterator
    //    {
    //        get
    //        {
    //            if (sessionIterator == null)
    //                sessionIterator = new SessionIterator(bars);

    //            return sessionIterator;
    //        }
    //    }

    //    /// <summary>
    //    /// The begin time of the main session.
    //    /// </summary>
    //    public DateTime BeginTime => NtSession.ActualSessionBegin;

    //    /// <summary>
    //    /// The end time of the main session.
    //    /// </summary>
    //    public DateTime EndTime => NtSession.ActualSessionEnd;

    //    /// <summary>
    //    /// The number of main sessionHoursList stored.
    //    /// </summary>
    //    public int Count => tradingHoursList.Count;

    //    #endregion

    //    #region Constructor

    //    /// <summary>
    //    /// Create a default instance of <see cref="SessionStructure"/>.
    //    /// </summary>
    //    /// <param name="ninjascript"></param>
    //    /// <param name="sessionIterator"></param>
    //    public NtSessionHours(NinjaScriptBase ninjascript, SessionIterator sessionIterator, Bars bars)
    //    {
    //        this.ninjascript = ninjascript;
    //        this.bars = bars;

    //        this.sessionIterator = new SessionIterator(bars);
    //        this.tradingHours = new UserSession();

    //        NtSession.SessionChanged += OnSessionChanged;
    //        //NtSession.SessionChanged += TradingHours.OnSessionChanged;
    //    }

    //    #endregion

    //    #region StateChanged Methods

    //    /// <summary>
    //    /// Method to execute when de ninjascript terminated.
    //    /// </summary>
    //    public void OnTerminated()
    //    {
    //        Dispose();
    //    }

    //    #endregion

    //    #region Public methods

    //    /// <summary>
    //    /// Free the memory of the handler methods.
    //    /// </summary>
    //    public override void Dispose()
    //    {
    //        ntSession.SessionChanged -= OnSessionChanged;
    //        //NtSession.SessionChanged -= TradingHours.OnSessionChanged;
    //        tradingHours.Dispose();
    //    }

    //    #endregion

    //    #region Market Data methods

    //    /// <summary>
    //    /// Event driven method which is called whenever a bar is updated. 
    //    /// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
    //    /// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
    //    /// </summary>
    //    public override void OnBarUpdate()
    //    {
    //        NtSession.OnBarUpdate();
    //    }

    //    /// <summary>
    //    /// Event driven method which is called and guaranteed to be in the correct sequence 
    //    /// for every change in level one market data for the underlying instrument. 
    //    /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
    //    /// </summary>
    //    public override void OnMarketData()
    //    {
    //        NtSession.OnMarketData();
    //    }

    //    #endregion

    //    #region Private methods

    //    private void OnSessionChanged(SessionChangedEventArgs e)
    //    {
    //        tradingHours.Dispose();
    //        tradingHours = new UserSession()
    //        {
    //            Idx = Count,
    //            BeginTime = e.NewSessionBeginTime,
    //            EndTime = e.NewSessionEndTime
    //        };

    //        tradingHoursList.Add(tradingHours);

    //    }

    //    #endregion

    //}


}
