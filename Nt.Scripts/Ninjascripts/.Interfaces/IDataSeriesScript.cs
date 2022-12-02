using Nt.Core.Data;

namespace Nt.Scripts.Ninjascripts
{
    /// <summary>
    /// Represents default implementation of any data serie.
    /// </summary>
    public interface IDataSeriesScript
    {
        IInstrument Instrument { get; }
        ITradingHours TradingHours { get; }
        PeriodType PeriodType { get; }
        int PeriodValue { get; }
    }
}
