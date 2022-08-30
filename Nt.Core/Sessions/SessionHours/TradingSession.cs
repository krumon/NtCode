
namespace Nt.Core
{
    /// <summary>
    /// Trading session types.
    /// </summary>
    public enum TradingSession
    {
        // Main session
        Electronic,                 // Electronic SessionHours  

        // Regular and overnight children
        Regular,                    // Regular SessionHours.
        OVN,                        // Overnight SessionHours 

        // Major children
        Asian,                      // Asian SessionHours 
        Asian_RS,                   // Asian Residual SessionHours 
        European,                   // European SessionHours 
        AmericanAndEuropean,        // American and European SessionHours 
        American,                   // American SessionHours 
        American_RS,                // American Residual SessionHours 

        // American Minor children
        American_RS_EXT,            // American Residual SessionHours - American Extra Time
        American_RS_EOD,            // American Residual SessionHours - End Of Day
        American_RS_NWD,            // American Residual SessionHours - New Day

        Custom,                     // Custom session
        
        //// BALANCES
        //// --------
        //// Regular Balances.
        //Regular_IB,                 // Asian SessionHours - Initial Balance
        //Regular_BB,                 // Asian SessionHours - Between Balances
        //Regular_FB,                 // Asian SessionHours - Final Balance

        //// Overnight Sessions
        //OVN_IB,                     // Overnight SessionHours - Initial Balance
        //OVN_BB,                     // Overnight SessionHours - Between Balances
        //OVN_FB,                     // Overnight SessionHours - Final Balance

        //// Asian Balances
        //Asian_IB,                   // Asian SessionHours - Initial Balance
        //Asian_BB,                   // Asian SessionHours - Between Balances
        //Asian_FB,                   // Asian SessionHours - Final Balance

        //// Asian Residual Balances
        //Asian_RS_IB,                // American Residual SessionHours - New Day Initial Balance
        //Asian_RS_BB,                // American Residual SessionHours - New Day Between Balances
        //Asian_RS_FB,                // American Residual SessionHours - New Day Final Balance

        //// European Balances
        //European_IB,                // European SessionHours - Initial Balance
        //European_BB,                // European SessionHours - Between Balances
        //European_FB,                // European SessionHours - Final Balance

        //// American and European Balances
        //AmericanAndEuropean_IB,     // American and European SessionHours - Initial Balance
        //AmericanAndEuropean_BB,     // American and European SessionHours - Between Balances
        //AmericanAndEuropean_FB,     // American and European SessionHours - Final Balance

        //// American Balances
        //American_IB,                // American SessionHours - Initial Balance
        //American_BB,                // American SessionHours - Between Balances
        //American_FB,                // American SessionHours - Final Balance

        //// American Residual Balances
        //American_RS_IB,             // American Residual SessionHours - New Day Initial Balance
        //American_RS_BB,             // American Residual SessionHours - New Day Between Balances
        //American_RS_FB,             // American Residual SessionHours - New Day Final Balance

        //// American Residual New Day Balances
        //American_RS_NWD_IB,         // American Residual SessionHours - New Day Initial Balance
        //American_RS_NWD_BB,         // American Residual SessionHours - New Day Between Balances
        //American_RS_NWD_FB,         // American Residual SessionHours - New Day Final Balance

    }
}
