using System;

namespace NtCore
{

    /// <summary>
    /// Helper methods of <see cref="InstrumentCode"/> enum.
    /// </summary>
    public static class InstrumentCodeHelpers
    {
        /// <summary>
        /// Method to convert the <see cref="InstrumentCode"/> to description.
        /// </summary>
        /// <param name="instrumentCode">The instrument code.</param>
        /// <returns>The instrument description.</returns>
        public static string ToDescription(this InstrumentCode instrumentCode)
        {

            switch (instrumentCode)
            {
                case(InstrumentCode.Default):
                case (InstrumentCode.MES):
                    return "MICRO E-MINI S&P 500 INDEX FUTURES";
                default:
                    throw new Exception("The instrument code doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="InstrumentCode"/> to <see cref="MarketExchange"/>.
        /// </summary>
        /// <param name="instrumentCode">The instrument code.</param>
        /// <returns>The <see cref="MarketExchange"/> value.</returns>
        public static MarketExchange ToMarketExchange(this InstrumentCode instrumentCode)
        {
            switch (instrumentCode)
            {
                case (InstrumentCode.Default):
                case (InstrumentCode.MES):
                    return MarketExchange.CME_Future_Index;
                default:
                    throw new Exception("The instrument code doesn't exists.");
            }
        }

        /// <summary>
        /// Converts a string to <see cref="InstrumentCode"/> enum.
        /// </summary>
        /// <param name="code">The string to converts to enum.</param>
        /// <returns>The <see cref="InstrumentCode"/> enum.</returns>
        /// <exception cref="Exception">Returns a exception if the string cannot be convert.</exception>
        public static InstrumentCode ToInstrumentCode(this string code)
        {

            if (InstrumentCode.TryParse(code, out InstrumentCode instrumentCode))
                return instrumentCode;

            throw new Exception("The string 'code' cannot be convert to InstrumentCode enum.");
        }
    }
}
