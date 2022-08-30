
namespace Nt.Core
{
    /// <summary>
    /// The type price of the bar.
    /// </summary>
    public enum TradingTime
    {
        // Custom session
        Custom,                         // Custom SessionHours.

        // MARKET EXCHANGES TIMES
        //CME_FutureIdx_EL_Open,          // CME Future Index Market Exchange - Open 
        //CME_FutureIdx_EL_Close,         // CME Future Index Market Exchange - Close 
        //CME_FutureIdx_RG_Close,         // CME Future Index Market Exchange - Close 
        //CME_FutureIdx_RG_Open,          // CME Future Index Market Exchange - Open 

        // Main SessionHours Times
        Electronic_Open,
        Electronic_Close,
        Regular_Open,
        Regular_Close,
        OVN_Open,
        OVN_Close,

        // MAJOR SESSIONS TIMES
        Asian_Open,                     // Asian SessionHours - Open 
        Asian_Close,                    // Asian SessionHours - Close 
        Asian_RS_Open,                  // Asian Residual SessionHours - Open
        Asian_RS_Close,                 // Asian Residual SessionHours - Close
        European_Open,                  // European SessionHours - Open 
        European_Close,                 // European SessionHours - Close 
        AmericanAndEuropean_Open,       // American and European SessionHours - Open 
        AmericanAndEuropean_Close,      // American and European SessionHours - Close 
        American_Open,                  // American SessionHours - Open 
        American_Close,                 // American SessionHours - Close 
        American_RS_Open,               // American Residual SessionHours - Open
        American_RS_Close,              // American Residual SessionHours - Close

        // MINOR SESSIONS TIMES
        American_RS_EXT_Open,           // American Residual SessionHours - American Extra time - Open
        American_RS_EXT_Close,          // American Residual SessionHours - American Extra time - Close
        American_RS_EOD_Open,           // American Residual SessionHours - End of day - Open
        American_RS_EOD_Close,          // American Residual SessionHours - End of day - Close
        American_RS_NWD_Open,           // American Residual SessionHours - New day - Open
        American_RS_NWD_Close,          // American Residual SessionHours - New day - Close
        
        // BALANCES SESSIONS TIMES
        // -----------------
        // Asian Balances Sessions
        //Asian_IB_Open,                  // Asian SessionHours - Initial Balance - Open
        //Asian_IB_Close,                 // Asian SessionHours - Initial Balance - Close
        //Asian_BB_Open,                  // Asian SessionHours - Between balances - Open
        //Asian_BB_Close,                 // Asian SessionHours - Between balances - Close
        //Asian_FB_Open,                  // Asian SessionHours - Final Balance - Open
        //Asian_FB_Close,                 // Asian SessionHours - Final Balance - Close

        // Asian Residual Balances Sessions
        //Asian_RS_IB_Open,               // Asian Residual SessionHours - New day initial balance - Open
        //Asian_RS_IB_Close,              // Asian Residual SessionHours - New day initial balance - Close
        //Asian_RS_BB_Open,               // Asian Residual SessionHours - Between balances - Open
        //Asian_RS_BB_Close,              // Asian Residual SessionHours - Between balances - Close
        //Asian_RS_FB_Open,               // Asian Residual SessionHours - New day final balance - Open
        //Asian_RS_FB_Close,              // Asian Residual SessionHours - New day final balance - Close

        // European Balances Sessions
        //European_IB_Open,               // European SessionHours Time - Initial Balance - Open
        //European_IB_Close,              // European SessionHours Time - Initial Balance - Close
        //European_BB_Open,               // European SessionHours Time - Between balances - Open
        //European_BB_Close,              // European SessionHours Time - Between balances - Close
        //European_FB_Open,               // European SessionHours Time - Final Balance - Open
        //European_FB_Close,              // European SessionHours Time - Final Balance - Close

        // American and European Balances Sessions
        //AmericanAndEuropean_IB_Open,    // American and European SessionHours Time - Initial Balance - Open
        //AmericanAndEuropean_IB_Close,   // American and European SessionHours Time - Initial Balance - Close
        //AmericanAndEuropean_BB_Open,    // American and European SessionHours Time - Between balances - Open
        //AmericanAndEuropean_BB_Close,   // American and European SessionHours Time - Between balances - Close
        //AmericanAndEuropean_FB_Open,    // American and European SessionHours Time - Final Balance - Open
        //AmericanAndEuropean_FB_Close,   // American and European SessionHours Time - Final Balance - Close

        // American Balances Sessions
        //American_IB_Open,               // American SessionHours Time - Initial Balance - Open
        //American_IB_Close,              // American SessionHours Time - Initial Balance - Close
        //American_BB_Open,               // American SessionHours Time - Between balances - Open
        //American_BB_Close,              // American SessionHours Time - Between balances - Close
        //American_FB_Open,               // American SessionHours Time - Final Balance - Open
        //American_FB_Close,              // American SessionHours Time - Final Balance - Close

        // American Residual Balances Sessions
        //American_RS_NWD_IB_Open,          // American Residual SessionHours - New day initial balance - Open
        //American_RS_NWD_IB_Close,         // American Residual SessionHours - New day initial balance - Close
        //American_RS_NWD_BB_Open,          // American Residual SessionHours - Between balances - Open
        //American_RS_NWD_BB_Close,         // American Residual SessionHours - Between balances - Close
        //American_RS_NWD_FB_Open,          // American Residual SessionHours - New day final balance - Open
        //American_RS_NWD_FB_Close,         // American Residual SessionHours - New day final balance - Close

    }
}