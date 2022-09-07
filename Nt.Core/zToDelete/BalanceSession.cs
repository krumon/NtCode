
namespace Nt.Core
{
    /// <summary>
    /// Instrument session balances types.
    /// </summary>
    public enum BalanceSession
    {

        // BALANCES
        // --------
        // Regular Balances.
        Regular_IB,                 // Asian TradingSessionInfo - Initial Balance
        Regular_BB,                 // Asian TradingSessionInfo - Between Balances
        Regular_FB,                 // Asian TradingSessionInfo - Final Balance

        // Overnight SessionHours
        OVN_IB,                     // Overnight TradingSessionInfo - Initial Balance
        OVN_BB,                     // Overnight TradingSessionInfo - Between Balances
        OVN_FB,                     // Overnight TradingSessionInfo - Final Balance

        // Asian Balances
        Asian_IB,                   // Asian TradingSessionInfo - Initial Balance
        Asian_BB,                   // Asian TradingSessionInfo - Between Balances
        Asian_FB,                   // Asian TradingSessionInfo - Final Balance

        // Asian Residual Balances
        Asian_RS_IB,                // American Residual TradingSessionInfo - New Day Initial Balance
        Asian_RS_BB,                // American Residual TradingSessionInfo - New Day Between Balances
        Asian_RS_FB,                // American Residual TradingSessionInfo - New Day Final Balance

        // European Balances
        European_IB,                // European TradingSessionInfo - Initial Balance
        European_BB,                // European TradingSessionInfo - Between Balances
        European_FB,                // European TradingSessionInfo - Final Balance

        // American and European Balances
        AmericanAndEuropean_IB,     // American and European TradingSessionInfo - Initial Balance
        AmericanAndEuropean_BB,     // American and European TradingSessionInfo - Between Balances
        AmericanAndEuropean_FB,     // American and European TradingSessionInfo - Final Balance

        // American Balances
        American_IB,                // American TradingSessionInfo - Initial Balance
        American_BB,                // American TradingSessionInfo - Between Balances
        American_FB,                // American TradingSessionInfo - Final Balance

        // American Residual Balances
        American_RS_IB,             // American Residual TradingSessionInfo - New Day Initial Balance
        American_RS_BB,             // American Residual TradingSessionInfo - New Day Between Balances
        American_RS_FB,             // American Residual TradingSessionInfo - New Day Final Balance

        // American Residual New Day Balances
        American_RS_NWD_IB,         // American Residual TradingSessionInfo - New Day Initial Balance
        American_RS_NWD_BB,         // American Residual TradingSessionInfo - New Day Between Balances
        American_RS_NWD_FB,         // American Residual TradingSessionInfo - New Day Final Balance

    }

}
