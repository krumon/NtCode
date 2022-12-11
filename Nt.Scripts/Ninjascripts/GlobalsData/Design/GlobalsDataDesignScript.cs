using System;

namespace Nt.Scripts.Ninjascripts.Design
{
    /// <summary>
    /// Design object to use for testing.
    /// </summary>
    public class GlobalsDataDesignScript : GlobalsDataScript
    {

        public override void Configure(object[] ninjascriptObjects)
        {
            Console.WriteLine($"{nameof(GlobalsDataDesignScript)} is configuring...");

            // Initialize data
            MinDate = new DateTime(1800,1,1);
            MaxDate = new DateTime(2023,9,20);
            UserConfigureTimeZoneInfo = TimeZoneInfo.Local;
            IsConfigured = true;
            Console.WriteLine($"{nameof(GlobalsDataDesignScript)} is configured.");

        }

        public override void DataLoaded(object[] ninjascriptObjects)
        {
            IsDataLoaded = true;
            Console.WriteLine($"{nameof(GlobalsDataDesignScript)} is configured by data loaded.");
        }

        public override void Dispose()
        {
            Console.WriteLine($"{nameof(GlobalsDataDesignScript)} is disposed.");
        }

    }
}
