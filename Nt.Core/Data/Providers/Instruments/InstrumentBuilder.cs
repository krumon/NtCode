using Kr.Core.Helpers;
using Nt.Core.Data.Internal;
using Nt.Core.Providers;
using System.Collections.Generic;
using System;

namespace Nt.Core.Data
{
    /// <summary>
    /// Builds instruments provider objects.
    /// </summary>
    public class InstrumentBuilder
    {

        #region Private members

        private readonly InstrumentDescriptorCollection _descriptors = new InstrumentDescriptorCollection();
        private readonly BaseInstrumentProvider _instrumentProvider;

        //private List<Func<DataSeriesConfiguration, InstrumentBuilder>> _dataSeriesConfigureActions;
        private List<Func<InstrumentProviderConfiguration, InstrumentBuilder>> _instrumentProviderConfigureActions;
        
        private bool _isBuild;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="SessionBuilder"/> default instance.
        /// </summary>
        public InstrumentBuilder()
        {
        }

        #endregion

        #region Implementation methods

        public BaseInstrumentProvider Build() //Returns ISessionProvider
        {
            // The trading session can be only once time created.
            if (_isBuild)
                return _instrumentProvider;

            if (_descriptors == null && _descriptors.Count > 0)
            {
                //_sessionFactory = new SessionFactory(_descriptors);
                //AddTypesToTradingSessionCollection();
                //SortTradingSessionCollection();
                //CreateTradingSession();
            }

            // Sets the flag to indicate the instrument provider is created.
            _isBuild = true;

            return _instrumentProvider;
        }

        #endregion

        #region Public Methods

        // Añadir sucesivamente todas las sesiones y sus configuraciones individuales
        public InstrumentBuilder ConfigureInstrument()
        {
            throw new NotImplementedException();
        }

        public InstrumentBuilder AddDataSeries(InstrumentKey key) =>
            AddDataSeries(key,PeriodType.Minute,1,key.ToDefaultTradingHoursKey());

        public InstrumentBuilder AddDataSeries(InstrumentKey key, PeriodType period, int value) =>
            AddDataSeries(key,period,value,key.ToDefaultTradingHoursKey());

        public InstrumentBuilder AddDataSeries(InstrumentKey key, PeriodType period, int value, TradingHoursKey tradingHoursKey)
        {
            InstrumentDescriptor descriptor = new InstrumentDescriptor(key,period,value,tradingHoursKey);
            _descriptors.Add(descriptor);
            return this;
        }

        #endregion

        #region Private methods

        private void AddTypesToTradingSessionCollection()
        {
            //throw new NotImplementedException();
        }

        private void SortTradingSessionCollection()
        {
            //throw new NotImplementedException();
        }

        private void CreateTradingSession()
        {
            //throw new NotImplementedException();
        }

        private void CreateDefaultSessionProvider()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
