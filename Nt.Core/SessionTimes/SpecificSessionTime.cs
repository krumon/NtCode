
namespace NtCore
{
    /// <summary>
    /// The type price of the bar.
    /// </summary>
    public enum SpecificSessionTime
    {
        // Custom session
        Custom,                         // Custom Session.

        // MAIN SESSIONS TIMES
        Electronic_Open,                // Electronic Session - Open 
        Electronic_Close,               // Electronic Session - Close 
        //Regular_Open,                   // Regular Session - Open 
        //Regular_Close,                  // Regular Session - Close 
        //OVN_Open,                       // Overnight Session - Open 
        //OVN_Close,                      // Overnight Session - Close 

        // MAJOR SESSIONS TIMES
        Asian_Open,                     // Asian Session - Open 
        Asian_Close,                    // Asian Session - Close 
        Asian_RS_Open,                  // Asian Residual Session - Open
        Asian_RS_Close,                 // Asian Residual Session - Close
        European_Open,                  // European Session - Open 
        European_Close,                 // European Session - Close 
        AmericanAndEuropean_Open,       // American and European Session - Open 
        AmericanAndEuropean_Close,      // American and European Session - Close 
        American_Open,                  // American Session - Open 
        American_Close,                 // American Session - Close 
        American_RS_Open,               // American Residual Session - Open
        American_RS_Close,              // American Residual Session - Close

        // MINOR SESSIONS TIMES
        American_RS_EXT_Open,           // American Residual Session - American Extra time - Open
        American_RS_EXT_Close,          // American Residual Session - American Extra time - Close
        American_RS_EOD_Open,           // American Residual Session - End of day - Open
        American_RS_EOD_Close,          // American Residual Session - End of day - Close
        American_RS_NWD_Open,           // American Residual Session - New day - Open
        American_RS_NWD_Close,          // American Residual Session - New day - Close
        
        // BALANCES SESSIONS TIMES
        // -----------------
        // Asian Balances Sessions
        //Asian_IB_Open,                  // Asian Session - Initial Balance - Open
        //Asian_IB_Close,                 // Asian Session - Initial Balance - Close
        //Asian_BB_Open,                  // Asian Session - Between balances - Open
        //Asian_BB_Close,                 // Asian Session - Between balances - Close
        //Asian_FB_Open,                  // Asian Session - Final Balance - Open
        //Asian_FB_Close,                 // Asian Session - Final Balance - Close

        // Asian Residual Balances Sessions
        //Asian_RS_IB_Open,               // Asian Residual Session - New day initial balance - Open
        //Asian_RS_IB_Close,              // Asian Residual Session - New day initial balance - Close
        //Asian_RS_BB_Open,               // Asian Residual Session - Between balances - Open
        //Asian_RS_BB_Close,              // Asian Residual Session - Between balances - Close
        //Asian_RS_FB_Open,               // Asian Residual Session - New day final balance - Open
        //Asian_RS_FB_Close,              // Asian Residual Session - New day final balance - Close

        // European Balances Sessions
        //European_IB_Open,               // European Session Time - Initial Balance - Open
        //European_IB_Close,              // European Session Time - Initial Balance - Close
        //European_BB_Open,               // European Session Time - Between balances - Open
        //European_BB_Close,              // European Session Time - Between balances - Close
        //European_FB_Open,               // European Session Time - Final Balance - Open
        //European_FB_Close,              // European Session Time - Final Balance - Close

        // American and European Balances Sessions
        //AmericanAndEuropean_IB_Open,    // American and European Session Time - Initial Balance - Open
        //AmericanAndEuropean_IB_Close,   // American and European Session Time - Initial Balance - Close
        //AmericanAndEuropean_BB_Open,    // American and European Session Time - Between balances - Open
        //AmericanAndEuropean_BB_Close,   // American and European Session Time - Between balances - Close
        //AmericanAndEuropean_FB_Open,    // American and European Session Time - Final Balance - Open
        //AmericanAndEuropean_FB_Close,   // American and European Session Time - Final Balance - Close

        // American Balances Sessions
        //American_IB_Open,               // American Session Time - Initial Balance - Open
        //American_IB_Close,              // American Session Time - Initial Balance - Close
        //American_BB_Open,               // American Session Time - Between balances - Open
        //American_BB_Close,              // American Session Time - Between balances - Close
        //American_FB_Open,               // American Session Time - Final Balance - Open
        //American_FB_Close,              // American Session Time - Final Balance - Close

        // American Residual Balances Sessions
        //American_RS_NWD_IB_Open,          // American Residual Session - New day initial balance - Open
        //American_RS_NWD_IB_Close,         // American Residual Session - New day initial balance - Close
        //American_RS_NWD_BB_Open,          // American Residual Session - Between balances - Open
        //American_RS_NWD_BB_Close,         // American Residual Session - Between balances - Close
        //American_RS_NWD_FB_Open,          // American Residual Session - New day final balance - Open
        //American_RS_NWD_FB_Close,         // American Residual Session - New day final balance - Close

    }
}