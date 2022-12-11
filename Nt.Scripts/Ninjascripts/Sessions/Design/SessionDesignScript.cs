using System;

namespace Nt.Scripts.Ninjascripts.Design
{
    /// <summary>
    /// Design object to use for testing.
    /// </summary>
    public class SessionDesignScript : SessionScript
    {
        public SessionDesignScript(IGlobalsDataScript globalDataScript) : base(globalDataScript)
        {
        }

        public override void Configure(object[] ninjascriptObjects)
        {
            Console.WriteLine($"{nameof(SessionDesignScript)} is configuring...");

            // Initialize data
            
            Console.WriteLine($"{nameof(SessionDesignScript)} is configured.");

        }

        public override void DataLoaded(object[] ninjascriptObjects)
        {
            BarsTimeZoneInfo = TimeZoneInfo.Utc;
            Console.WriteLine($"{nameof(SessionDesignScript)} is configured by data loaded.");
        }

        public override void Dispose()
        {
            Console.WriteLine($"{nameof(SessionDesignScript)} is disposed.");
        }

        public override void OnBarUpdate()
        {
            Console.WriteLine($"{nameof(SessionDesignScript)} is updated (when bar update).");
        }

        public override void OnMarketData()
        {
            Console.WriteLine($"{nameof(SessionDesignScript)} is updated (when market data changed).");
        }

        public override void OnSessionUpdate()
        {
            Console.WriteLine($"SESSION CHANGED !!!!");
        }
    }
}
