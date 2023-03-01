using Nt.Core.Data;

namespace Nt.Scripts.Services
{
    /// <summary>
    /// Represents a ninjascript instrument.
    /// </summary>
    public class DataSeriesDescriptor
    {
        /// <summary>
        /// The <see cref="DataSeries"/> instrument name.
        /// </summary>
        public string InstrumentName { get; set; }

        /// <summary>
        /// The <see cref="DataSeries"/> <see cref="PeriodType"/>.
        /// </summary>
        public PeriodType PeriodType { get; set; }

        /// <summary>
        /// The <see cref="DataSeries"/> period value.
        /// </summary>
        public int PeriodValue { get; set; }

        /// <summary>
        /// The <see cref="DataSeries"/> trading hours template name.
        /// </summary>
        public string TradingHoursName { get; set; }

        /// <summary>
        /// The unique key of the data serie.
        /// </summary>
        public string Key => $"{InstrumentName}-{PeriodType}{PeriodValue}-{TradingHoursName}";

        public override string ToString()
        {
            return $"DataSerie[{InstrumentName}] : {PeriodValue}({PeriodType}) - {TradingHoursName}.";
        }
        //#region Constructors

        ///// <summary>
        ///// Create <see cref="DataSeriesDescriptor"/> default instance.
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="periodType"></param>
        ///// <param name="periodValue"></param>
        ///// <param name="tradingHoursKey"></param>
        //public DataSeriesDescriptor(InstrumentCode key, PeriodType periodType, int periodValue, TradingHoursCode tradingHoursKey)
        //{
        //    InstrumentKey = key;
        //    PeriodType = periodType;
        //    PeriodValue = periodValue;
        //    TradingHoursKey = tradingHoursKey;
        //}

        ///// <summary>
        ///// Create <see cref="DataSeriesDescriptor"/> default instance.
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="periodType"></param>
        ///// <param name="periodValue"></param>
        ///// <param name="tradingHoursKey"></param>
        //public DataSeriesDescriptor(string instrumentName, PeriodType periodType, int periodValue, string tradingHoursName)
        //{
        //    InstrumentName = instrumentName;
        //    PeriodType = periodType;
        //    PeriodValue = periodValue;
        //    TradingHoursName = tradingHoursName;
        //}

        //#endregion
    }
}
