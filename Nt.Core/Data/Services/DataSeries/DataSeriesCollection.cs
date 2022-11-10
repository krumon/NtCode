﻿namespace Nt.Core.Data
{
    /// <summary>
    /// Represents a default implementation of data series descriptor collection.
    /// </summary>
    public class DataSeriesCollection : BaseServiceCollection<IDataSeriesDescriptor>, IDataSeriesCollection
    {

        #region Implementation methods

        /// <inheritdoc/>
        public override void Add(IDataSeriesDescriptor item)
        {
            if (item == null)
                return;
            if (!Exist(item))
                base.Add(item);
        }

        /// <inheritdoc/>
        public IDataSeriesCollection AddDataSerie(InstrumentKey instrumentKey, PeriodType periodType, int periodValue, TradingHoursKey tradingHoursKey)
        {
            DataSeriesDescriptor descriptor = new DataSeriesDescriptor(instrumentKey, periodType, periodValue, tradingHoursKey);
            Add(descriptor);
            return this;
        }

        /// <inheritdoc/>
        public IDataSeriesCollection AddDataSerie(PeriodType periodType, int periodValue) =>
            AddDataSerie(InstrumentKey.Default,periodType,periodValue,TradingHoursKey.Default);

        /// <inheritdoc/>
        public IDataSeriesCollection AddDataSerie(InstrumentKey instrumentKey, PeriodType periodType, int periodValue) =>
            AddDataSerie(instrumentKey,periodType,periodValue,TradingHoursKey.Default);

        #endregion

    }
}
