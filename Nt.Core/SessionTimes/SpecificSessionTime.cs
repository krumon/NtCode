
namespace NtCore
{
    /// <summary>
    /// The type price of the bar.
    /// </summary>
    public enum SpecificSessionTime
    {
        // Custom session
        Custom,                         // Custom Session.

        // Session times
        Electronic_Open,                // Electronic Session - Open 
        Electronic_Close,               // Electronic Session - Close 

        // Regular and overnight sessions times
        Regular_Open,                   // Regular Session - Open 
        Regular_Close,                  // Regular Session - Close 
        OVN_Open,                       // Overnight Session - Open 
        OVN_Close,                      // Overnight Session - Close 

        // Major sessions times
        European_Open,                  // European Session - Open 
        European_Close,                 // European Session - Close 
        AmericanAndEuropean_Open,       // American and European Session - Open 
        AmericanAndEuropean_Close,      // American and European Session - Close 
        American_Open,                  // American Session - Open 
        American_Close,                 // American Session - Close 
        Asian_Open,                     // Asian Session - Open 
        Asian_Close,                    // Asian Session - Close 

        // Residual sessions times
        Residual_AM_Open,               // American Residual Session - Open
        Residual_AM_Close,              // American Residual Session - Close
        Residual_AS_Open,               // Asian Residual Session - Open
        Residual_AS_Close,              // Asian Residual Session - Close

        // Residual Minor sessions
        Residual_AM_AET_Open,           // American Residual Session - American Extra time - Open
        Residual_AM_AET_Close,          // American Residual Session - American Extra time - Close
        Residual_AM_EOD_Open,           // American Residual Session - End of day time - Open
        Residual_AM_EOD_Close,          // American Residual Session - End of day time - End
        Residual_AM_NDIB_Open,          // American Residual Session - New day initial balance - Open
        Residual_AM_NDIB_Close,         // American Residual Session - New day initial balance - Close
        Residual_AM_NDBB_Open,          // American Residual Session - Between balances - Open
        Residual_AM_NDBB_Close,         // American Residual Session - Between balances - Close
        Residual_AM_NDFB_Open,          // American Residual Session - New day final balance - Open
        Residual_AM_NDFB_Close,         // American Residual Session - New day final balance - Close
        Residual_AS_IB_Open,             // American Residual Session - New day initial balance - Open
        Residual_AS_IB_Close,            // American Residual Session - New day initial balance - Close
        Residual_AS_BB_Open,             // American Residual Session - Between balances - Open
        Residual_AS_BB_Close,            // American Residual Session - Between balances - Close
        Residual_AS_FB_Open,             // American Residual Session - New day final balance - Open
        Residual_AS_FB_Close,            // American Residual Session - New day final balance - Close

        // Asian Minor sessions
        Asian_IB_Open,                  // Asian Session Time - Initial Balance - Open
        Asian_IB_Close,                 // Asian Session Time - Initial Balance - Close
        Asian_BB_Open,                  // Asian Session Time - Between balances - Open
        Asian_BB_Close,                 // Asian Session Time - Between balances - Close
        Asian_FB_Open,                  // Asian Session Time - Final Balance - Open
        Asian_FB_Close,                 // Asian Session Time - Final Balance - Close

        // European Minor sessions
        European_PM_Open,               // European Session Time - Premarket - Open
        European_PM_Close,              // European Session Time - Premarket - Close
        European_IB_Open,               // European Session Time - Initial Balance - Open
        European_IB_Close,              // European Session Time - Initial Balance - Close
        European_BB_Open,               // European Session Time - Between balances - Open
        European_BB_Close,              // European Session Time - Between balances - Close
        European_FB_Open,               // European Session Time - Final Balance - Open
        European_FB_Close,              // European Session Time - Final Balance - Close

        // American and European Minor sessions
        AmericanAndEuropean_IB_Open,    // American and European Session Time - Initial Balance - Open
        AmericanAndEuropean_IB_Close,   // American and European Session Time - Initial Balance - Close
        AmericanAndEuropean_BB_Open,    // American and European Session Time - Between balances - Open
        AmericanAndEuropean_BB_Close,   // American and European Session Time - Between balances - Close
        AmericanAndEuropean_FB_Open,    // American and European Session Time - Final Balance - Open
        AmericanAndEuropean_FB_Close,   // American and European Session Time - Final Balance - Close

        // American Minor sessions
        American_IB_Open,               // American Session Time - Initial Balance - Open
        American_IB_Close,              // American Session Time - Initial Balance - Close
        American_BB_Open,               // American Session Time - Between balances - Open
        American_BB_Close,              // American Session Time - Between balances - Close
        American_FB_Open,               // American Session Time - Final Balance - Open
        American_FB_Close,              // American Session Time - Final Balance - Close

    }
}