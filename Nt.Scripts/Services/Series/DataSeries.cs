using NinjaTrader.NinjaScript;
using Nt.Core.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Nt.Scripts.Services
{
    /// <summary>
    /// Represents any ninjascript data series.
    /// </summary>
    public class DataSeries
    {
        private readonly INinjascript _ninjascript;
        private Action<NinjaTrader.Data.BarsPeriodType, int> _addDataSeries;

        private DataSeriesDescriptor _seriesDescriptor;
        private List<DataSeriesDescriptor> _seriesDescriptors;

        private readonly bool _instanceError;
        private readonly InstrumentCode _instrumentKey;

        public DataSeriesDescriptor Primary 
        { 
            get 
            {
                if (_seriesDescriptors != null && _seriesDescriptors.Count > 0)
                {
                    return _seriesDescriptors[_seriesDescriptors.Count - 1];
                }

                Debug.Assert(_seriesDescriptor != null);
                return _seriesDescriptor;
            } 
        }

        public int Count
        {
            get
            {
                if (_seriesDescriptor == null)
                {
                    Debug.Assert(_seriesDescriptors == null);
                    return 0;
                }

                return 1 + (_seriesDescriptors?.Count ?? 0);
            }
        }

        public DataSeriesDescriptor this[int index]
        {
            get
            {
                if (index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                if (index == 0)
                {
                    return _seriesDescriptor;
                }

                return _seriesDescriptors[index - 1];
            }
        }

        /// <summary>
        /// Gets the unique key of the service.
        /// </summary>
        //public OptionalServiceType Key { get; private set; }

        ///// <summary>
        ///// The instrument unique code.
        ///// </summary>
        //public InstrumentCode InstrumentKey => _instrumentKey;

        ///// <summary>
        ///// The market exchange owner of the instrument.
        ///// </summary>
        //public MarketExchange MarketExchange => _instrumentKey.ToMarketExchange();

        ///// <summary>
        ///// The trading hours key.
        ///// </summary>
        //public TradingHoursCode TradingHoursKey {get;set;} = TradingHoursCode.Default;

        ///// <summary>
        ///// Gets the instument name.
        ///// </summary>
        //public string Name => _instanceError ? string.Empty : _instrumentKey.ToString();

        ///// <summary>
        ///// Gets the instrument description.
        ///// </summary>
        //public string Description => _instanceError ? string.Empty : _instrumentKey.ToDescription();

        ///// <summary>
        ///// Gets the default trading hours name.
        ///// </summary>
        //public string TradingHoursName 
        //{
        //    get
        //    {
        //        if (TradingHoursKey == TradingHoursCode.Default)
        //            TradingHoursKey = _instrumentKey.ToDefaultTradingHoursKey();

        //        return TradingHoursKey.ToName();
        //    }
        //    set
        //    {
        //        TradingHoursKey = _instrumentKey.ToTradingHoursKey(value);
        //    }
        //}

        ///// <summary>
        ///// Gets or sets the instrument point value.
        ///// </summary>
        //public double PointValue => _instrumentKey.ToPointValue();

        ///// <summary>
        ///// Gets or sets the instrument tick size.
        ///// </summary>
        //public double TickSize => _instrumentKey.ToTickSize();

        #region Constructors

        /// <summary>
        /// Create <see cref="InstrumentProvider"/> default instance.
        /// </summary>
        public DataSeries(IChartBarsProperties chartBarsProperties, string stringKey)
        {
            if (chartBarsProperties != null)
                _ = Add(new DataSeriesDescriptor(chartBarsProperties.InstrumentName, chartBarsProperties.BarsPeriod.PeriodType, chartBarsProperties.BarsPeriod.PeriodValue, chartBarsProperties.TradingHoursName)); ;

            //if (string.IsNullOrEmpty(stringKey))
            //    throw new ArgumentException($"the parameter {nameof(stringKey)} cannot be null or empty");

            //if (!stringKey.TryGetInstrumentKey(out _instrumentKey))
            //{
            //    _instanceError = true;
            //    throw new Exception("Unknown string key passed bay parameter.");
            //}
        }

        #endregion

        public int GetSlot(DataSeriesDescriptor descriptor)
        {
            if (descriptor == _seriesDescriptor)
            {
                return Count - 1;
            }

            if (_seriesDescriptors != null)
            {
                int index = _seriesDescriptors.IndexOf(descriptor);
                if (index != -1)
                {
                    return _seriesDescriptors.Count - (index + 1);
                }
            }

            throw new InvalidOperationException("Data Series Descriptor Not Exist");
        }

        public DataSeries Add(DataSeriesDescriptor descriptor)
        {
            var newCacheItem = default(DataSeries);
            if (_seriesDescriptor == null)
            {
                Debug.Assert(_seriesDescriptors == null);
                newCacheItem._seriesDescriptor = descriptor;
            }
            else
            {
                newCacheItem._seriesDescriptor = _seriesDescriptor;
                newCacheItem._seriesDescriptors = _seriesDescriptors ?? new List<DataSeriesDescriptor>();
                newCacheItem._seriesDescriptors.Add(descriptor);
            }
            return newCacheItem;
        }

        //public void AddDataSerie()
        //{
        //    foreach (DataSeriesDescriptor descriptor in _seriesDescriptors)
        //        _ninjascript.Instance.AddDataSeries((NinjaTrader.Data.BarsPeriodType)(int)descriptor.PeriodType, descriptor.PeriodValue);
        //}
    }
}
