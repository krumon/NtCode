using Nt.Core.Services;
using System;

namespace Nt.Scripts.Ninjascripts.Design
{
    /// <summary>
    /// Design object to use for testing.
    /// </summary>
    public class SessionsIteratorDesignScript : SessionsIteratorScript
    {
        public SessionsIteratorDesignScript(IGlobalsDataService globalsData) : base(globalsData)
        {
        }

        public override void Configure(object[] ninjascriptObjects)
        {
            Console.WriteLine($"{nameof(SessionsIteratorDesignScript)} is configuring...");

            // Initialize data
            
            Console.WriteLine($"{nameof(SessionsIteratorDesignScript)} is configured.");

        }

        public override void DataLoaded(object[] ninjascriptObjects)
        {
            BarsTimeZoneInfo = TimeZoneInfo.Utc;
            Console.WriteLine($"{nameof(SessionsIteratorDesignScript)} is configured by data loaded.");
        }

        public override void Dispose()
        {
            Console.WriteLine($"{nameof(SessionsIteratorDesignScript)} is disposed.");
        }

        public override void OnBarUpdate()
        {
            Console.WriteLine($"{nameof(SessionsIteratorDesignScript)} is updated (when bar update).");
        }

        public override void OnMarketData()
        {
            Console.WriteLine($"{nameof(SessionsIteratorDesignScript)} is updated (when market data changed).");
        }

        public override void OnSessionUpdate()
        {
            Console.WriteLine($"SESSION CHANGED !!!!");
        }
    }
}
