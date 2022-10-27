using Nt.Core.Trading.Internal;
using System;
using System.Collections;
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
        //ISessionBuilder CreateDefaultTradingSessionBuilder();
        ITradingSessionCollection Sessions { get; }
        SessionCompareResult CompareSessionTo(ITradingSession otherSession);
    }
}