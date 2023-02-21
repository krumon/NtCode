using NinjaTrader.Core;
using Nt.Core.Services;
using System;

namespace Nt.Scripts.Ninjascripts
{
    public class GlobalsDataScript : GlobalsDataService
    {
        Action<object> action;
        NinjaTrader.NinjaScript.NinjaScriptBase ninjascript;

        public override void Configure(object[] ninjascriptObjects)
        {
            MinDate = Globals.MinDate;
            MaxDate = Globals.MaxDate;
            UserConfigureTimeZoneInfo = Globals.GeneralOptions.TimeZoneInfo;
            IsConfigured = true;
            action = ninjascript.Print;

        }

        public override void DataLoaded(object[] ninjascriptObjects) => IsDataLoaded = IsConfigured;

        public override void Dispose()
        {
        }

    }
}
