using NinjaTrader.Core;
using Nt.Core.Services;

namespace Nt.Scripts.Ninjascripts
{
    public class GlobalsDataScript : GlobalsDataService, IGlobalsDataScript
    {

        public override void Configure(object[] ninjascriptObjects)
        {
            MinDate = Globals.MinDate;
            MaxDate = Globals.MaxDate;
            UserConfigureTimeZoneInfo = Globals.GeneralOptions.TimeZoneInfo;
            IsConfigured = true;
        }

        public override void DataLoaded(object[] ninjascriptObjects) => IsDataLoaded = IsConfigured;

        public override void Dispose()
        {
        }

    }
}
