using System;

namespace NtCore
{

    /// <summary>
    /// Helper methods of <see cref="MarketExchange"/> enum.
    /// </summary>
    public static class MarketExchangeHelpers
    {
        /// <summary>
        /// Method to convert the <see cref="MarketExchange"/> to description.
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument code.</param>
        /// <returns>The market exchange description.</returns>
        public static string ToDescription(this MarketExchange instrumentMarketExchange)
        {

            switch (instrumentMarketExchange)
            {
                case (MarketExchange.CME_Future_Index):
                    return "American Future Index market exchange.";
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Converts <see cref="MarketExchange"/> to <see cref="TimeZoneInfo"/>
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeZoneInfo"/> value.</returns>
        public static TimeZoneInfo ToTimeZoneInfo(this MarketExchange instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (MarketExchange.CME_Future_Index):
                    return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="MarketExchange"/> to initial or final <see cref="TimeSpan"/>
        /// of the electronic or regular session.
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToElectronicInitialTime(this MarketExchange instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (MarketExchange.CME_Future_Index):
                    return new TimeSpan(17,0,0);
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="MarketExchange"/> to initial or final <see cref="TimeSpan"/>
        /// of the electronic or regular session.
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToElectronicFinalTime(this MarketExchange instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (MarketExchange.CME_Future_Index):
                    return new TimeSpan(16,0,0);
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="MarketExchange"/> to initial or final <see cref="TimeSpan"/>
        /// of the electronic or regular session.
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToRegularInitialTime(this MarketExchange instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (MarketExchange.CME_Future_Index):
                    return new TimeSpan(8,30,0);
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="MarketExchange"/> to initial or final <see cref="TimeSpan"/>
        /// of the electronic or regular session.
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToRegularFinalTime(this MarketExchange instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (MarketExchange.CME_Future_Index):
                    return new TimeSpan(15,0,0);
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Converts <see cref="MarketExchange"/> to initial break <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToBreakInitialTime(this MarketExchange instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (MarketExchange.CME_Future_Index):
                    return new TimeSpan(15,15,0);
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Converts <see cref="MarketExchange"/> to final break <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToBreakFinalTime(this MarketExchange instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (MarketExchange.CME_Future_Index):
                    return new TimeSpan(15,30,0);
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Converts <see cref="MarketExchange"/> to initial closed <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToClosedInitialTime(this MarketExchange instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (MarketExchange.CME_Future_Index):
                    return instrumentMarketExchange.ToElectronicFinalTime();
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

        /// <summary>
        /// Converts <see cref="MarketExchange"/> to final closed <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="instrumentMarketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToClosedFinalTime(this MarketExchange instrumentMarketExchange)
        {
            switch (instrumentMarketExchange)
            {
                case (MarketExchange.CME_Future_Index):
                    return instrumentMarketExchange.ToElectronicInitialTime();
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

    }
}
