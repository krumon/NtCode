using NinjaTrader.Core;
using NinjaTrader.Data;
using Nt.Core.Hosting;
using Nt.Core.Services;

namespace Nt.Scripts.Ninjascripts
{
    public class GlobalDataScript : GlobalDataService, IGlobalDataScript
    {

        public override void Configure(object[] ninjascriptObjects)
        {
            MinDate = Globals.MinDate;
            MaxDate = Globals.MaxDate;
            UserConfigureTimeZoneInfo = Globals.GeneralOptions.TimeZoneInfo;
        }

        public override void DataLoaded(object[] ninjascriptObjects)
        {
            if (this.TryGet<Bars>(ninjascriptObjects, out Bars bars))
                BarsConfigureTimeZoneInfo = bars.TradingHours.TimeZoneInfo;
        }

        public override void Dispose()
        {
        }

    }
}
