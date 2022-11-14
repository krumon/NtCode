using System;

namespace Nt.Core.Data
{
    /// <summary>
    /// Builds data series service objects.
    /// </summary>
    public class DataSeriesBuilder : IServiceProviderBuilder<DataSeriesProvider,DataSeriesBuilder, DataSeriesCollection, DataSeriesDescriptor>
    {

        #region Private members

        private readonly DataSeriesCollection _descriptors = new DataSeriesCollection();
        private readonly IServiceProvider _dataSeriesProvider;

        //private List<Func<DataSeriesConfiguration, InstrumentBuilder>> _dataSeriesConfigureActions;
        //private List<Func<InstrumentProviderOptions, DataSeriesBuilder>> _instrumentProviderConfigureActions;
        
        private bool _isBuild;

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="DataSeriesBuilder"/> default instance.
        /// </summary>
        public DataSeriesBuilder()
        {
        }

        #endregion

        #region Implementation methods

        public DataSeriesProvider Build()
        {
            // The trading session can be only once time created.
            if (_isBuild)
                return (DataSeriesProvider)_dataSeriesProvider;

            if (_descriptors != null && _descriptors.Count > 0)
            {
                //_sessionFactory = new SessionFactory(_descriptors);
                //AddTypesToTradingSessionCollection();
                //SortTradingSessionCollection();
                //CreateTradingSession();
            }

            // Sets the flag to indicate the instrument provider is created.
            _isBuild = true;

            return (DataSeriesProvider)_dataSeriesProvider;
        }

        public DataSeriesBuilder ConfigureService(Action<DataSeriesCollection> configureDelegate)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Public Methods


        #endregion

        #region Private methods

        private DataSeriesBuilder AddDataSeries(InstrumentKey key) =>
            AddDataSeries(key, PeriodType.Minute, 1, key.ToDefaultTradingHoursKey());

        private DataSeriesBuilder AddDataSeries(InstrumentKey key, PeriodType period, int value) =>
            AddDataSeries(key, period, value, key.ToDefaultTradingHoursKey());

        private DataSeriesBuilder AddDataSeries(InstrumentKey key, PeriodType period, int value, TradingHoursKey tradingHoursKey)
        {
            DataSeriesDescriptor descriptor = new DataSeriesDescriptor(key, period, value, tradingHoursKey);
            _descriptors.Add(descriptor);
            return this;
        }


        #endregion

    }
}
