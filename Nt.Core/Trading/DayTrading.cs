namespace Nt.Core.Trading
{
    /// <summary>
    /// Create a day trading object instances.
    /// </summary>
    public static class DayTrading
    {

        public static TradingSessionBuilder CreateTradingSessionBuilder => new TradingSessionBuilder();

    }
}
