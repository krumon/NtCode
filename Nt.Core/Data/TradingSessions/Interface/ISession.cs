using System;
using System.Collections;
using System.Collections.Generic;

namespace Nt.Core.Data
{
    public interface ISession :
        IComparable,
        IComparable<ISession>,
        IComparer,
        IComparer<ISession>,
        IList<ISession>,
        ICollection<ISession>,
        IEnumerable<ISession>,
        IEnumerable

    {
        //ISessionBuilder CreateDefaultTradingSessionBuilder();
        ITradingSessionCollection Sessions { get; }
        SessionCompareResult CompareSessionTo(ISession otherSession);
    }
}