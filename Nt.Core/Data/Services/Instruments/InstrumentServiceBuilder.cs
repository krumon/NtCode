using Nt.Core.Providers;
using System;
using System.Collections.Generic;

namespace Nt.Core.Data
{
    /// <summary>
    /// Builds instruments provider objects.
    /// </summary>
    public class InstrumentServiceBuilder
    {

        #region Private members

        private readonly InstrumentServiceCollection _descriptors = new InstrumentServiceCollection();
        private readonly InstrumentProvider _instrumentProvider;

        //private List<Func<DataSeriesConfiguration, InstrumentBuilder>> _dataSeriesConfigureActions;
        private List<Func<InstrumentProviderOptions, InstrumentServiceBuilder>> _instrumentProviderConfigureActions;
        
        private bool _isBuild;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="SessionBuilder"/> default instance.
        /// </summary>
        public InstrumentServiceBuilder()
        {
        }

        #endregion

        #region Implementation methods

        public InstrumentProvider Build() //Returns ISessionProvider
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
        public InstrumentServiceBuilder ConfigureInstrument()
        {
            throw new NotImplementedException();
        }

        public InstrumentServiceBuilder AddDataSeries(InstrumentKey key) =>
            AddDataSeries(key,PeriodType.Minute,1,key.ToDefaultTradingHoursKey());

        public InstrumentServiceBuilder AddDataSeries(InstrumentKey key, PeriodType period, int value) =>
            AddDataSeries(key,period,value,key.ToDefaultTradingHoursKey());

        public InstrumentServiceBuilder AddDataSeries(InstrumentKey key, PeriodType period, int value, TradingHoursKey tradingHoursKey)
        {
            InstrumentServiceDescriptor descriptor = new InstrumentServiceDescriptor(key,period,value,tradingHoursKey);
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
