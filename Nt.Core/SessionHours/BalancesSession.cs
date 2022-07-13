
namespace NtCore
{
    /// <summary>
    /// Instrument session hours types.
    /// </summary>
    public enum BalancesSession
    {

        // Asian Balances
        Asian_IB,                   // Asian Session - Initial Balance
        Asian_BB,                   // Asian Session - Between Balances
        Asian_FB,                   // Asian Session - Final Balance

        // European Balances
        European_IB,                // European Session - Initial Balance
        European_BB,                // European Session - Between Balances
        European_FB,                // European Session - Final Balance

        // American and European Balances
        AmericanAndEuropean_IB,     // American and European Session - Initial Balance
        AmericanAndEuropean_BB,     // American and European Session - Between Balances
        AmericanAndEuropean_FB,     // American and European Session - Final Balance

        // American Balances
        American_IB,                // American Session - Initial Balance
        American_BB,                // American Session - Between Balances
        American_FB,                // American Session - Final Balance

        // New Day Balances
        American_RS_NWDIB,           // American Residual Session - New Day Initial Balance
        American_RS_NWDBB,           // American Residual Session - New Day Between Balances
        American_RS_NWDFB,           // American Residual Session - New Day Final Balance

        // Asian Residual Balances
        Asian_RS_IB,             // American Residual Session - New Day Initial Balance
        Asian_RS_BB,             // American Residual Session - New Day Between Balances
        Asian_RS_FB,             // American Residual Session - New Day Final Balance

    }
}
