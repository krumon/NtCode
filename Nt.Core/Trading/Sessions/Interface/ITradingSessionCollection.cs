using System.Collections;
using System.Collections.Generic;

namespace Nt.Core.Trading
{
    public interface ITradingSessionCollection :
        IList<ITradingSession>,
        ICollection<ITradingSession>,
        IEnumerable<ITradingSession>,
        IEnumerable
    {
    }
}