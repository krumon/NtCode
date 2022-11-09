using System;

namespace Nt.Core.Trading
{

    /// <summary>
    /// Helper methods of <see cref="InstrumentKey"/> enum.
    /// </summary>
    public static class InstrumentKeyExtensions
    {
        /// <summary>
        /// Method to convert the <see cref="InstrumentKey"/> to description.
        /// </summary>
        /// <param name="instrumentKey">The instrument code.</param>
        /// <returns>The instrument description.</returns>
        public static string ToDescription(this InstrumentKey instrumentKey)
        {

            switch (instrumentKey)
            {
                case (InstrumentKey.MES):
                    return "MICRO E-MINI S&P 500 INDEX FUTURES";
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="InstrumentKey"/> to <see cref="MarketExchange"/>.
        /// </summary>
        /// <param name="instrumentKey">The instrument code.</param>
        /// <returns>The <see cref="MarketExchange"/> value.</returns>
        public static MarketExchange ToMarketExchange(this InstrumentKey instrumentKey)
        {
            switch (instrumentKey)
            {
                case (InstrumentKey.MES):
                    return MarketExchange.CME;
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="InstrumentKey"/> to default <see cref="TradingHoursKey"/>.
        /// </summary>
        /// <param name="instrumentKey">The instrument key.</param>
        /// <returns>The <see cref="TradingHoursKey"/> value.</returns>
        public static TradingHoursKey ToDefaultTradingHoursKey(this InstrumentKey instrumentKey)
        {
            switch (instrumentKey)
            {
                case (InstrumentKey.MES):
                    return TradingHoursKey.CME_US_Index_Futures_ETH;
                case (InstrumentKey.M6A):
                case (InstrumentKey.M6B):
                case (InstrumentKey.M6E):
                    return TradingHoursKey.CME_FX_Futures_ETH;
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="InstrumentKey"/> to default <see cref="TradingHoursKey"/>.
        /// </summary>
        /// <param name="instrumentKey">The instrument key.</param>
        /// <returns>The <see cref="TradingHoursKey"/> value.</returns>
        public static TradingHoursKey ToTradingHoursKey(this InstrumentKey instrumentKey, string tradingHoursName)
        {
            if (Enum.TryParse(tradingHoursName, out TradingHoursKey key))
                return key;

            return TradingHoursKey.Default;
        }

        /// <summary>
        /// Converts a string to <see cref="InstrumentKey"/> enum.
        /// </summary>
        /// <param name="key">The string to converts to enum.</param>
        /// <returns>The <see cref="InstrumentKey"/> enum.</returns>
        /// <exception cref="Exception">Returns a exception if the string cannot be convert.</exception>
        public static InstrumentKey ToInstrumentKey(this string key)
        {

            if (Enum.TryParse(key, out InstrumentKey instrumentKey))
                return instrumentKey;

            throw new Exception("The string 'key' cannot be convert to InstrumentKey enum.");
        }

        /// <summary>
        /// Method to convert the <see cref="InstrumentKey"/> to <see cref="MarketExchange"/>.
        /// </summary>
        /// <param name="instrumentKey">The instrument code.</param>
        /// <returns>The <see cref="MarketExchange"/> value.</returns>
        public static TimeZoneInfo ToTimeZoneInfo(this InstrumentKey instrumentKey)
        {
            switch (instrumentKey)
            {
                case (InstrumentKey.MES):
                    return instrumentKey.ToMarketExchange().ToTimeZoneInfo();
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Converts the <see cref="InstrumentKey"/> to instrument tick size.
        /// </summary>
        /// <param name="instrumentKey">The instrument key.</param>
        /// <returns>The instrument tick size value.</returns>
        /// <exception cref="Exception">Throw an error if converter is not implemented yet.</exception>
        public static double ToTickSize(this InstrumentKey instrumentKey)
        {
            switch (instrumentKey)
            {
                case (InstrumentKey.MES):
                    return 0.25;
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Converts the <see cref="InstrumentKey"/> to instrument tick size.
        /// </summary>
        /// <param name="instrumentKey">The instrument key.</param>
        /// <returns>The instrument tick size value.</returns>
        /// <exception cref="Exception">Throw an error if converter is not implemented yet.</exception>
        public static double ToPointValue(this InstrumentKey instrumentKey)
        {
            switch (instrumentKey)
            {
                case (InstrumentKey.MES):
                    return 5.0;
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }


    }
}
