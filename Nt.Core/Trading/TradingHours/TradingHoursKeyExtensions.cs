using System;

namespace Nt.Core.Trading
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

    }
}
