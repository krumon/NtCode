using System.Collections;
using System;
using System.Collections.Generic;
using Nt.Core.Trading.Internal;

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
        //ISessionBuilder CreateDefaultTradingSessionBuilder();
        IList<ITradingSession> Sessions { get; }
        SessionCompareResult CompareSessionTo(ITradingSession otherSession);
    }
}