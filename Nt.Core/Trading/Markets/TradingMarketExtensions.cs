using System;

namespace Nt.Core.Trading
{

    /// <summary>
    /// Helper methods of <see cref="TradingMarket"/> enum.
    /// </summary>
    public static class TradingMarketExtensions
    {
        /// <summary>
        /// Method to convert the <see cref="TradingMarket"/> to description.
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument code.</param>
        /// <returns>The market exchange description.</returns>
        public static string ToDescription(this TradingMarket instrumentMarketExchange)
        {

            switch (instrumentMarketExchange)
            {
                case (TradingMarket.CME_Future_Index):
                    return "American Future Index market exchange.";
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Converts <see cref="TradingMarket"/> to <see cref="TimeZoneInfo"/>
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeZoneInfo"/> value.</returns>
        public static TimeZoneInfo ToTimeZoneInfo(this TradingMarket instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (TradingMarket.CME_Future_Index):
                    return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="TradingMarket"/> to initial or final <see cref="TimeSpan"/>
        /// of the electronic or regular session.
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToElectronicInitialTime(this TradingMarket instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (TradingMarket.CME_Future_Index):
                    return new TimeSpan(17,0,0);
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="TradingMarket"/> to initial or final <see cref="TimeSpan"/>
        /// of the electronic or regular session.
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToElectronicFinalTime(this TradingMarket instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (TradingMarket.CME_Future_Index):
                    return new TimeSpan(16,0,0);
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="TradingMarket"/> to initial or final <see cref="TimeSpan"/>
        /// of the electronic or regular session.
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToRegularInitialTime(this TradingMarket instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (TradingMarket.CME_Future_Index):
                    return new TimeSpan(8,30,0);
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="TradingMarket"/> to initial or final <see cref="TimeSpan"/>
        /// of the electronic or regular session.
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToRegularFinalTime(this TradingMarket instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (TradingMarket.CME_Future_Index):
                    return new TimeSpan(15,0,0);
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Converts <see cref="TradingMarket"/> to initial break <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToBreakInitialTime(this TradingMarket instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (TradingMarket.CME_Future_Index):
                    return new TimeSpan(15,15,0);
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Converts <see cref="TradingMarket"/> to final break <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToBreakFinalTime(this TradingMarket instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (TradingMarket.CME_Future_Index):
                    return new TimeSpan(15,30,0);
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Converts <see cref="TradingMarket"/> to initial closed <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToClosedInitialTime(this TradingMarket instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (TradingMarket.CME_Future_Index):
                    return instrumentMarketExchange.ToElectronicFinalTime();
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Converts <see cref="TradingMarket"/> to final closed <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToClosedFinalTime(this TradingMarket instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (TradingMarket.CME_Future_Index):
                    return instrumentMarketExchange.ToElectronicInitialTime();
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

    }
}
