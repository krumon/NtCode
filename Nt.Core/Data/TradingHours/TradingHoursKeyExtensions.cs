using System;

namespace Nt.Core.Data
{

    /// <summary>
    /// Helper methods of <see cref="TradingHoursKey"/> enum.
    /// </summary>
    public static class TradingHoursKeyExtensions
    {
        /// <summary>
        /// Converts a <see cref="TradingHoursKey"/> to name.
        /// </summary>
        /// <param name="tradingHoursKey">The trading hours key.</param>
        /// <returns>The trading hours name.</returns>
        public static string ToName(this TradingHoursKey tradingHoursKey)
        {
            string name = tradingHoursKey.ToString().Replace('_',' ');

            return name;
        }

        /// <summary>
        /// Method to convert the <see cref="TradingHoursKey"/> to description.
        /// </summary>
        /// <param name="tradingHoursKey">The instrument code.</param>
        /// <returns>The market exchange description.</returns>
        public static string ToDescription(this TradingHoursKey tradingHoursKey)
        {
            // TODO: Rectificar.
            switch (tradingHoursKey)
            {
                case (TradingHoursKey.CME_US_Index_Futures_ETH):
                    return "American Future Index market exchange.";
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Converts <see cref="TradingHoursKey"/> to <see cref="TimeZoneInfo"/>
        /// </summary>
        /// <param name="tradingHoursKey">The instrument market exchange.</param>
        /// <returns><see cref="TimeZoneInfo"/> value.</returns>
        public static TimeZoneInfo ToTimeZoneInfo(this TradingHoursKey tradingHoursKey)
        {
            switch (tradingHoursKey)
            {
                case (TradingHoursKey.CME_US_Index_Futures_ETH):
                    return TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="TradingHoursKey"/> to initial or final <see cref="TimeSpan"/>
        /// of the electronic or regular session.
        /// </summary>
        /// <param name="tradingHoursKey">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToElectronicInitialTime(this TradingHoursKey tradingHoursKey)
        {
            switch (tradingHoursKey)
            {
                case (TradingHoursKey.CME_US_Index_Futures_ETH):
                    return new TimeSpan(17, 0, 0);
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="TradingHoursKey"/> to initial or final <see cref="TimeSpan"/>
        /// of the electronic or regular session.
        /// </summary>
        /// <param name="tradingHoursKey">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToElectronicFinalTime(this TradingHoursKey tradingHoursKey)
        {
            switch (tradingHoursKey)
            {
                case (TradingHoursKey.CME_US_Index_Futures_ETH):
                    return new TimeSpan(16, 0, 0);
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="TradingHoursKey"/> to initial or final <see cref="TimeSpan"/>
        /// of the electronic or regular session.
        /// </summary>
        /// <param name="tradingHoursKey">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToRegularInitialTime(this TradingHoursKey tradingHoursKey)
        {
            switch (tradingHoursKey)
            {
                case (TradingHoursKey.CME_US_Index_Futures_ETH):
                    return new TimeSpan(8, 30, 0);
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Method to convert the <see cref="TradingHoursKey"/> to initial or final <see cref="TimeSpan"/>
        /// of the electronic or regular session.
        /// </summary>
        /// <param name="tradingHoursKey">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToRegularFinalTime(this TradingHoursKey tradingHoursKey)
        {
            switch (tradingHoursKey)
            {
                case (TradingHoursKey.CME_US_Index_Futures_ETH):
                    return new TimeSpan(15, 0, 0);
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Converts <see cref="TradingHoursKey"/> to initial break <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="marketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToBreakInitialTime(this TradingHoursKey marketExchange)
        {
            switch (marketExchange)
            {
                case (TradingHoursKey.CME_US_Index_Futures_ETH):
                    return new TimeSpan(15, 15, 0);
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Converts <see cref="TradingHoursKey"/> to final break <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="marketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToBreakFinalTime(this TradingHoursKey marketExchange)
        {
            switch (marketExchange)
            {
                case (TradingHoursKey.CME_US_Index_Futures_ETH):
                    return new TimeSpan(15, 30, 0);
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Converts <see cref="TradingHoursKey"/> to initial closed <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="marketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToClosedInitialTime(this TradingHoursKey marketExchange)
        {
            switch (marketExchange)
            {
                case (TradingHoursKey.CME_US_Index_Futures_ETH):
                    return marketExchange.ToElectronicFinalTime();
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }

        /// <summary>
        /// Converts <see cref="TradingHoursKey"/> to final closed <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="marketExchange">The instrument market exchange.</param>
        /// <returns><see cref="TimeSpan"/> value.</returns>
        public static TimeSpan ToClosedFinalTime(this TradingHoursKey marketExchange)
        {
            switch (marketExchange)
            {
                case (TradingHoursKey.CME_US_Index_Futures_ETH):
                    return marketExchange.ToElectronicInitialTime();
                default:
                    throw new Exception("The converter is not implemented.");
            }
        }


    }
}
