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
                case (MarketExchange.American_Future_Indices):
                    return "American Future Index market exchange.";
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
                case (MarketExchange.American_Future_Indices):
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
                case (MarketExchange.American_Future_Indices):
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
                case (MarketExchange.American_Future_Indices):
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
                case (MarketExchange.American_Future_Indices):
                    return new TimeSpan(15,0,0);
                default:
                    throw new Exception("The market exchange doesn't exists.");
            }
        }

    }
}
