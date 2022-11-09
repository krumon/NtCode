using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Scripts.Providers
{
    public class InstrumentsProvider
    {
        private readonly List<InstrumentProvider> _instruments = new List<InstrumentProvider>();

        public InstrumentProvider this[int index] => _instruments[index];

        public InstrumentsProvider(NinjaScriptBase ninjascript)
        {

            //ninjascript.AddDataSeries();

            if (ninjascript == null)
                throw new System.ArgumentNullException($"{nameof(ninjascript)} cannot be null");

            foreach (var instrument in ninjascript.Instruments)
                _instruments.Add(new InstrumentProvider(instrument));

            
        }

        public virtual void AddDataSeries(Action<string, NinjaTrader.Data.BarsPeriodType, int> addDataSeriesMethod)
        {
            
        }
    }
}
