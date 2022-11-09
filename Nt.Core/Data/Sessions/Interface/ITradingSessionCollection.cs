using System.Collections;
using System.Collections.Generic;

namespace Nt.Core.Data
{
    public interface ITradingSessionCollection :
        IList<ISession>,
        ICollection<ISession>,
        IEnumerable<ISession>,
        IEnumerable
    {
    }
}