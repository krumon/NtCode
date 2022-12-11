using System;

namespace Nt.Core.Services
{
    public abstract class GlobalsDataService : IGlobalsDataService
    {

        public DateTime MaxDate { get; protected set; }
        public DateTime MinDate { get; protected set; }
        public TimeZoneInfo UserConfigureTimeZoneInfo { get; protected set; }
        public bool IsConfigured { get; protected set; }
        public bool IsDataLoaded {get; protected set; }

        public abstract void Configure(object[] ninjascriptObjects);
        public abstract void DataLoaded(object[] ninjascriptObjects);
        public abstract void Dispose();

    }
}
