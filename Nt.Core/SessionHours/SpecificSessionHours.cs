
namespace NtCore
{
    /// <summary>
    /// Trading time zone types.
    /// </summary>
    public enum SpecificSessionHours
    {
        // Main session
        Electronic,                 // Electronic Session  

        // Regular and overnight sessions
        Regular,                    // Regular Session.
        OVN,                        // Overnight Session 

        // Major sessions
        Asian,                      // Asian Session 
        Asian_RS,                   // Asian Residual Session 
        European,                   // European Session 
        AmericanAndEuropean,        // American and European Session 
        American,                   // American Session 
        American_RS,                // American Residual Session 

        // American Minor sessions
        American_RS_EXT,            // American Residual Session - American Extra Time
        American_RS_EOD,            // American Residual Session - End Of Day
        American_RS_NWD,            // American Residual Session - New Day
        
        // BALANCES
        // --------
        // Regular Balances.
        Regular_IB,
        Regular_BB,
        Regular_FB,

        // Overnight Sessions
        OVN_IB,
        OVN_BB,
        OVN_FB,

        // Asian Balances
        Asian_IB,                   // Asian Session - Initial Balance
        Asian_BB,                   // Asian Session - Between Balances
        Asian_FB,                   // Asian Session - Final Balance

        // Asian Residual Balances
        Asian_RS_IB,                // American Residual Session - New Day Initial Balance
        Asian_RS_BB,                // American Residual Session - New Day Between Balances
        Asian_RS_FB,                // American Residual Session - New Day Final Balance

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

        // American Residual Balances
        American_RS_IB,             // American Residual Session - New Day Initial Balance
        American_RS_BB,             // American Residual Session - New Day Between Balances
        American_RS_FB,             // American Residual Session - New Day Final Balance

        // American Residual New Day Balances
        American_RS_NWD_IB,           // American Residual Session - New Day Initial Balance
        American_RS_NWD_BB,           // American Residual Session - New Day Between Balances
        American_RS_NWD_FB,           // American Residual Session - New Day Final Balance

    }
}
