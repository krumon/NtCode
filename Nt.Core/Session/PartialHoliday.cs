using System;
using System.Collections.ObjectModel;

namespace NtCore
{
    public class PartialHoliday
    {

        public Session Constraint { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool IsEarlyEnd { get; set; }
        public bool IsLateBegin { get; set; }
        public Collection<Session> Sessions { get; set; }

    }
}
