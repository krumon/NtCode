using NinjaTrader.NinjaScript;
using Nt.Core.Data;
using Nt.Core.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Nt.Scripts.Services
{
    /// <summary>
    /// Represents any ninjascript data series.
    /// </summary>
    public class DataSeries : IDataSeries
    {
        private readonly INinjascript _ninjascript;
        private DataSeriesOptions _currentOptions;
        private readonly IDisposable _optionsReloadToken;

        //private readonly INinjascript _ninjascript;
        //private Action<NinjaTrader.Data.BarsPeriodType, int> _addDataSeries;

        private DataSeriesDescriptor _seriesDescriptor;
        private List<DataSeriesDescriptor> _seriesDescriptors;

        //private readonly bool _instanceError;
        //private readonly InstrumentCode _instrumentKey;

        public DataSeriesDescriptor PrimaryDataSerie 
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
                    return _seriesDescriptors?.Count ?? 0;
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

                if (index == 0 && _seriesDescriptor != null)
                {
                    return _seriesDescriptor;
                }
                else if(index == 0 && _seriesDescriptor == null)
                    return _seriesDescriptors[index];

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
        public DataSeries(INinjascript ninjascript, IChartBarsProperties chartBarsProperties, IOptionsMonitor<DataSeriesOptions> options, string stringKey="")
        {
            _ninjascript = ninjascript;
            _currentOptions = options.CurrentValue;
            ReloadDataSeriesOptions(_currentOptions);
            _optionsReloadToken = options.OnChange(ReloadDataSeriesOptions);
            
            if (chartBarsProperties != null)
                Add(new DataSeriesDescriptor()
                {
                    InstrumentName = chartBarsProperties.InstrumentName, 
                    PeriodType = chartBarsProperties.BarsPeriod.PeriodType, 
                    PeriodValue = chartBarsProperties.BarsPeriod.PeriodValue, 
                    TradingHoursName = chartBarsProperties.TradingHoursName
                },isPrimaryDataSerie: true); 

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

        public void Add(DataSeriesDescriptor descriptor, bool isPrimaryDataSerie = false)
        {
            if (isPrimaryDataSerie)
            {
                _seriesDescriptor = descriptor;
            }
            else
            {
                if (_seriesDescriptors == null)
                    _seriesDescriptors = new List<DataSeriesDescriptor>();
                _seriesDescriptors.Add(descriptor);
            }
        }

        public void Dispose()
        {
            _optionsReloadToken.Dispose();
        }

        private void ReloadDataSeriesOptions(DataSeriesOptions options)
        {
            _ninjascript.Instance.SetState(State.Configure);
            _currentOptions = options;
        }
    }
}
