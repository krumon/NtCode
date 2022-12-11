using System;

namespace Nt.Core.Services
{
    public abstract class GlobalDataService : IGlobalDataService
    {

        public DateTime MaxDate { get; protected set; }
        public DateTime MinDate { get; protected set; }
        public TimeZoneInfo UserConfigureTimeZoneInfo { get; protected set; }
        public TimeZoneInfo BarsConfigureTimeZoneInfo { get; protected set; }

        public abstract void Configure(object[] ninjascriptObjects);
        public abstract void DataLoaded(object[] ninjascriptObjects);
        public abstract void Dispose();

    }
}
