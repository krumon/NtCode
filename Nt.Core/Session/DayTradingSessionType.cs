
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
        EuropeanAndAsian,
        AsianFinalBalance,
        EuropeanToAmerican,
        // Day
        AmericanInitialBalance,
        AmericanAndEuropean,
        American,
        AmericanFinalBalance,
        // Overnight
        ResidualToBreak,

        // MAJORS
        //Asian,
        //European,
        //American_European,
        //American,
        //Residual,

        // DAY TRADING
        DAY,
        OVERNIGHT,

    }
}
