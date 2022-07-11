
namespace NtCore
{
    /// <summary>
    /// The type price of the bar.
    /// </summary>
    public enum SpecificTradingTime
    {
        Custom,                         // Custom trading time.

        Electronic_Open,                // Electronic Trading Time - Open 
        Electronic_Close,               // Electronic Trading Time - Close 
        Electronic_IB_Open,             // Electronic Trading Time - Initial Balance - Open
        Electronic_IB_Close,            // Electronic Trading Time - Initial Balance - Close
        Electronic_FB_Open,             // Electronic Trading Time - Final Balance - Open
        Electronic_FB_Close,            // Electronic Trading Time - Final Balance - Close

        Regular_Open,                   // Regular Trading Time - Open 
        Regular_Close,                  // Regular Trading Time - Close 
        Regular_IB_Open,                // Regular Trading Time - Initial Balance - Open
        Regular_IB_Close,               // Regular Trading Time - Initial Balance - Close
        Regular_FB_Open,                // Regular Trading Time - Final Balance - Open
        Regular_FB_Close,               // Regular Trading Time - Final Balance - Close

        DAY_Open,                       // Daylight Trading Time - Open 
        DAY_Close,                      // Daylight Trading Time - Close 
        DAY_IB_Open,                    // Daylight Trading Time - Initial Balance - Open
        DAY_IB_Close,                   // Daylight Trading Time - Initial Balance - Close
        DAY_FB_Open,                    // Daylight Trading Time - Final Balance - Open
        DAY_FB_Close,                   // Daylight Trading Time - Final Balance - Close

        OVN_Open,                       // Overnight Trading Time - Open 
        OVN_Close,                      // Overnight Trading Time - Close 
        OVN_IB_Open,                    // Overnight Trading Time - Initial Balance - Open
        OVN_IB_Close,                   // Overnight Trading Time - Initial Balance - Close
        OVN_FB_Open,                    // Overnight Trading Time - Final Balance - Open
        OVN_FB_Close,                   // Overnight Trading Time - Final Balance - Close

        Asian_Open,                     // Asian Trading Time - Open 
        Asian_Close,                    // Asian Trading Time - Close 
        Asian_IB_Open,                  // Asian Trading Time - Initial Balance - Open
        Asian_IB_Close,                 // Asian Trading Time - Initial Balance - Close
        Asian_FB_Open,                  // Asian Trading Time - Final Balance - Open
        Asian_FB_Close,                 // Asian Trading Time - Final Balance - Close

        American_Open,                  // American Trading Time - Open 
        American_Close,                 // American Trading Time - Close 
        American_IB_Open,               // American Trading Time - Initial Balance - Open
        American_IB_Close,              // American Trading Time - Initial Balance - Close
        American_FB_Open,               // American Trading Time - Final Balance - Open
        American_FB_Close,              // American Trading Time - Final Balance - Close

        European_Open,                  // European Trading Time - Open 
        European_Close,                 // European Trading Time - Close 
        European_IB_Open,               // European Trading Time - Initial Balance - Open
        European_IB_Close,              // European Trading Time - Initial Balance - Close
        European_FB_Open,               // European Trading Time - Final Balance - Open
        European_FB_Close,              // European Trading Time - Final Balance - Close

        AmericanAndEuropean_Open,       // American and European Trading Time - Open 
        AmericanAndEuropean_Close,      // American and European Trading Time - Close 
        AmericanAndEuropean_IB_Open,    // American and European Trading Time - Initial Balance - Open
        AmericanAndEuropean_IB_Close,   // American and European Trading Time - Initial Balance - Close
        AmericanAndEuropean_FB_Open,    // American and European Trading Time - Final Balance - Open
        AmericanAndEuropean_FB_Close,   // American and European Trading Time - Final Balance - Close

    }
}
