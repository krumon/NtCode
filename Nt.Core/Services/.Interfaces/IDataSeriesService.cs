using Nt.Core.Data;

namespace Nt.Core.Services
{
    /// <summary>
    /// Represents default implementation of any data serie.
    /// </summary>
    public interface IDataSeriesService
    {
        IInstrument Instrument { get; }
        ITradingHours TradingHours { get; }
        PeriodType PeriodType { get; }
        int PeriodValue { get; }
    }
}
