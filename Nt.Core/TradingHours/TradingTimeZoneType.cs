
namespace NtCore
{
    /// <summary>
    /// The type price of the bar.
    /// </summary>
    public enum TradingTimeZoneType
    {

        ETH,                // Electronic Trading Hours  
        ETH_IB,             // Electronic Trading Hours - Initial Balance
        ETH_FB,             // Electronic Trading Hours - Final Balance

        OVN,                // Overnight Trading Hours 

        Asian,              // Asian Trading Hours 
        Asian_IB,           // Asian Trading Hours - Initial Balance
        Asian_FB,           // Asian Trading Hours - Final Balance

        American,           // American Trading Hours 
        American_IB,        // American Trading Hours - Initial Balance
        American_FB,        // American Trading Hours - Final Balance

        European,           // European Trading Hours 
        European_IB,        // European Trading Hours - Initial Balance
        European_FB,        // European Trading Hours - Final Balance

    }
}
