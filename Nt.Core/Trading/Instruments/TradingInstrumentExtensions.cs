using System;

namespace Nt.Core.Trading
{

    /// <summary>
    /// Helper methods of <see cref="TradingInstrumentCode"/> enum.
    /// </summary>
    public static class TradingInstrumentExtensions
    {
        /// <summary>
        /// Method to convert the <see cref="TradingInstrumentCode"/> to description.
        /// </summary>
        /// <param name="instrumentCode">The instrument code.</param>
        /// <returns>The instrument description.</returns>
        public static string ToDescription(this TradingInstrumentCode instrumentCode)
        {

            switch (instrumentCode)
            {
                case(TradingInstrumentCode.Default):
                case (TradingInstrumentCode.MES):
                    return "MICRO E-MINI S&P 500 INDEX FUTURES";
                default:
                    throw new Exception("The instrument code doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="TradingInstrumentCode"/> to <see cref="TradingMarket"/>.
        /// </summary>
        /// <param name="instrumentCode">The instrument code.</param>
        /// <returns>The <see cref="TradingMarket"/> value.</returns>
        public static TradingMarket ToMarketExchange(this TradingInstrumentCode instrumentCode)
        {
            switch (instrumentCode)
            {
                case (TradingInstrumentCode.Default):
                case (TradingInstrumentCode.MES):
                    return TradingMarket.CME_Future_Index;
                default:
                    throw new Exception("The instrument code doesn't exists.");
            }
        }

        /// <summary>
        /// Converts a string to <see cref="TradingInstrumentCode"/> enum.
        /// </summary>
        /// <param name="code">The string to converts to enum.</param>
        /// <returns>The <see cref="TradingInstrumentCode"/> enum.</returns>
        /// <exception cref="Exception">Returns a exception if the string cannot be convert.</exception>
        public static TradingInstrumentCode ToInstrumentCode(this string code)
        {

            if (TradingInstrumentCode.TryParse(code, out TradingInstrumentCode instrumentCode))
                return instrumentCode;

            throw new Exception("The string 'code' cannot be convert to InstrumentCode enum.");
        }

        /// <summary>
        /// Method to convert the <see cref="TradingInstrumentCode"/> to <see cref="TradingMarket"/>.
        /// </summary>
        /// <param name="instrumentCode">The instrument code.</param>
        /// <returns>The <see cref="TradingMarket"/> value.</returns>
        public static TimeZoneInfo ToTimeZoneInfo(this TradingInstrumentCode instrumentCode)
        {
            switch (instrumentCode)
            {
                case (TradingInstrumentCode.Default):
                case (TradingInstrumentCode.MES):
                    return instrumentCode.ToMarketExchange().ToTimeZoneInfo();
                default:
                    throw new Exception("The instrument code doesn't exists.");
            }
        }


    }
}
