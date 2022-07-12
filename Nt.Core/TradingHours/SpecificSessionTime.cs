
namespace NtCore
{
    /// <summary>
    /// The type price of the bar.
    /// </summary>
    public enum SpecificSessionTime
    {
        // Custom session
        Custom,                         // Custom trading time.

        // Session times
        Electronic_Open,                // Electronic Trading Time - Open 
        Electronic_Close,               // Electronic Trading Time - Close 

        // Regular and overnight sessions times
        Regular_Open,                   // Regular Trading Time - Open 
        Regular_Close,                  // Regular Trading Time - Close 
        OVN_Open,                       // Overnight Trading Time - Open 
        OVN_Close,                      // Overnight Trading Time - Close 

        // Major sessions times
        European_Open,                  // European Trading Time - Open 
        European_Close,                 // European Trading Time - Close 

        AmericanAndEuropean_Open,       // American and European Trading Time - Open 
        AmericanAndEuropean_Close,      // American and European Trading Time - Close 

        American_Open,                  // American Trading Time - Open 
        American_Close,                 // American Trading Time - Close 

        Asian_Open,                     // Asian Trading Time - Open 
        Asian_Close,                    // Asian Trading Time - Close 

        Residual_Open,                  // Residual Trading Time - American Extra time - Open
        Residual_Close,                 // Residual Trading Time - American Extra time - Close

        // Minor sessions
        Residual_AET_Open,              // Residual Trading Time - American Extra time - Open
        Residual_AET_Close,             // Residual Trading Time - American Extra time - Close
        Residual_EOD_Open,              // Residual Trading Time - End of day time - Open
        Residual_EOD_Close,             // Residual Trading Time - End of day time - End
        Residual_NDIB_Open,             // Residual Trading Time - New day initial balance - Open
        Residual_NDIB_Close,            // Residual Trading Time - New day initial balance - Close
        Residual_NDBB_Open,             // Residual Trading Time - Between balances - Open
        Residual_NDBB_Close,            // Residual Trading Time - Between balances - Close
        Residual_NDFB_Open,             // Residual Trading Time - New day final balance - Open
        Residual_NDFB_Close,            // Residual Trading Time - New day final balance - Close

        Asian_IB_Open,                  // Asian Trading Time - Initial Balance - Open
        Asian_IB_Close,                 // Asian Trading Time - Initial Balance - Close
        Asian_BB_Open,                  // Asian Trading Time - Between balances - Open
        Asian_BB_Close,                 // Asian Trading Time - Between balances - Close
        Asian_FB_Open,                  // Asian Trading Time - Final Balance - Open
        Asian_FB_Close,                 // Asian Trading Time - Final Balance - Close

        European_PM_Open,               // European Trading Time - Premarket - Open
        European_PM_Close,              // European Trading Time - Premarket - Close
        European_IB_Open,               // European Trading Time - Initial Balance - Open
        European_IB_Close,              // European Trading Time - Initial Balance - Close
        European_BB_Open,               // European Trading Time - Between balances - Open
        European_BB_Close,              // European Trading Time - Between balances - Close
        European_FB_Open,               // European Trading Time - Final Balance - Open
        European_FB_Close,              // European Trading Time - Final Balance - Close

        AmericanAndEuropean_IB_Open,    // American and European Trading Time - Initial Balance - Open
        AmericanAndEuropean_IB_Close,   // American and European Trading Time - Initial Balance - Close
        AmericanAndEuropean_BB_Open,    // American and European Trading Time - Between balances - Open
        AmericanAndEuropean_BB_Close,   // American and European Trading Time - Between balances - Close
        AmericanAndEuropean_FB_Open,    // American and European Trading Time - Final Balance - Open
        AmericanAndEuropean_FB_Close,   // American and European Trading Time - Final Balance - Close

        American_IB_Open,               // American Trading Time - Initial Balance - Open
        American_IB_Close,              // American Trading Time - Initial Balance - Close
        American_BB_Open,               // American Trading Time - Between balances - Open
        American_BB_Close,              // American Trading Time - Between balances - Close
        American_FB_Open,               // American Trading Time - Final Balance - Open
        American_FB_Close,              // American Trading Time - Final Balance - Close

    }
}

        //Electronic_IB_Open,             // Electronic Trading Time - Initial Balance - Open
        //Electronic_IB_Close,            // Electronic Trading Time - Initial Balance - Close
        //Electronic_BB_Open,             // Electronic Trading Time - Between Balances - Open
        //Electronic_BB_Close,            // Electronic Trading Time - Between Balances - Close
        //Electronic_FB_Open,             // Electronic Trading Time - Final Balance - Open
        //Electronic_FB_Close,            // Electronic Trading Time - Final Balance - Close

        //Regular_IB_Open,                // Regular Trading Time - Initial Balance - Open
        //Regular_IB_Close,               // Regular Trading Time - Initial Balance - Close
        //Regular_BB_Open,                // Regular Trading Time - Between balances - Open
        //Regular_BB_Close,               // Regular Trading Time - Between balances - Close
        //Regular_FB_Open,                // Regular Trading Time - Final Balance - Open
        //Regular_FB_Close,               // Regular Trading Time - Final Balance - Close

        //OVN_IB_Open,                    // Overnight Trading Time - Initial Balance - Open
        //OVN_IB_Close,                   // Overnight Trading Time - Initial Balance - Close
        //OVN_BB_Open,                    // Overnight Trading Time - Between balances - Open
        //OVN_BB_Close,                   // Overnight Trading Time - Between balances - Close
        //OVN_FB_Open,                    // Overnight Trading Time - Final Balance - Open
        //OVN_FB_Close,                   // Overnight Trading Time - Final Balance - Close