
namespace NtCore
{
    /// <summary>
    /// Trading session types.
    /// </summary>
    public enum DayTradingSessionType
    {
        // MINORS
        // Overnight
        ElectronicInitialBalance,
        ResidualToAsian,
        AsianInitialBalance,
        AsianToEuropean,
        EuropeanInitialBalance,
        EuropeanToAmerican,
        // Day
        AmericanInitialBalance,
        AmericanAndEuropean,
        AmericanToFinalBalance,
        AmericanFinalBalance,
        // Overnight
        ResidualToBreak,

        // MAJORS
        Asian,
        European,
        American_European,
        American,
        Residual,

        // DAY TRADING
        DAY,
        OVERNIGHT,

    }
}
