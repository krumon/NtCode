using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.DrawingTools;
using Nt.Core.Data;
using System;
using System.Collections.Generic;

namespace Nt.Scripts.Providers
{
    /// <summary>
    /// Ninjascript instruments provider.
    /// </summary>
    public class InstrumentsProvider
    {
        #region Private members

        private readonly List<InstrumentProvider> _instruments = new List<InstrumentProvider>();
        private readonly InstrumentDescriptorCollection _series = new InstrumentDescriptorCollection();

        #endregion

        #region Public properties

        /// <summary>
        /// Gets an instrument provider.
        /// </summary>
        /// <param name="index">The index to find the instrument provider.</param>
        /// <returns></returns>
        public InstrumentProvider this[int index] => _instruments[index];

        #endregion

        #region Constructors

        public InstrumentsProvider(NinjaScriptBase ninjascript)
        {

            //ninjascript.AddDataSeries();

            if (ninjascript == null)
                throw new System.ArgumentNullException($"{nameof(ninjascript)} cannot be null");

            foreach (var instrument in ninjascript.Instruments)
                _instruments.Add(new InstrumentProvider(instrument));

            
        }

        #endregion

        #region Public methods

        public virtual void AddDataSeries(Action<string, NinjaTrader.Data.BarsPeriodType, int> addDataSeriesMethod)
        {
            //addDataSeriesMethod?.Invoke();
        }
        
        #endregion
    }
}
