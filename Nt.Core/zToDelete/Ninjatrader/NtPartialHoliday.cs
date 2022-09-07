using System;
using System.Collections.ObjectModel;

namespace Nt.Core
{
    public class NtPartialHoliday
    {

        public SessionsIterator Constraint { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool IsEarlyEnd { get; set; }
        public bool IsLateBegin { get; set; }
        public Collection<SessionsIterator> Sessions { get; set; }

    }
}
