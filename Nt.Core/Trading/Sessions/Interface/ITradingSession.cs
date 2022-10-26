using System.Collections;
using System;
using System.Collections.Generic;

namespace Nt.Core.Trading
{
    public interface ITradingSession :
        IComparable,
        IComparable<ITradingSession>,
        IComparer,
        IComparer<ITradingSession>,
        IList<ITradingSession>,
        ICollection<ITradingSession>,
        IEnumerable<ITradingSession>,
        IEnumerable
    {
        ITradingSessionBuilder CreateTradingSessionBuilder();
        IList<ITradingSession> Sessions { get; }
        //ITradingTime BeginSessionTime { get; set; }
        //ITradingTime EndSessionTime { get; set; }
        TradingSessionCompareResult CompareSessionTo(ITradingSession otherSession);


    }
}