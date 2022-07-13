
namespace NtCore
{
    /// <summary>
    /// Trading time zone types.
    /// </summary>
    public enum SpecificSessionHours
    {
        // Main session
        Electronic,                 // Electronic Session  
        Closed,                     // Session is Closed

        // Regular and overnight sessions
        Regular,                    // Regular Session.
        OVN,                        // Overnight Session 

        // Major sessions
        Asian,                      // Asian Session 
        European,                   // European Session 
        AmericanAndEuropean,        // American and European Session 
        American,                   // American Session 

        // Residual sessions
        Residual_AM,                // American Residual Session 
        Residual_AS,                // Asian Residual Session 

        // Residual Minor sessions
        Residual_AM_AET,            // American Residual Session - American Extra Time
        Residual_AM_Break,          // American Residual Session - Break
        Residual_AM_EOD,            // American Residual Session - End Of Day
        Residual_AM_NDIB,           // American Residual Session - New Day Initial Balance
        Residual_AM_NDBB,           // American Residual Session - New Day Between Balances
        Residual_AM_NDFB,           // American Residual Session - New Day Final Balance
        Residual_AS_IB,             // American Residual Session - New Day Initial Balance
        Residual_AS_BB,             // American Residual Session - New Day Between Balances
        Residual_AS_FB,             // American Residual Session - New Day Final Balance

        // Asian Minor sessions
        Asian_IB,                   // Asian Session - Initial Balance
        Asian_BB,                   // Asian Session - Between Balances
        Asian_FB,                   // Asian Session - Final Balance

        // European Minor sessions
        European_IB,                // European Session - Initial Balance
        European_BB,                // European Session - Between Balances
        European_FB,                // European Session - Final Balance

        // American and European Minor sessions
        AmericanAndEuropean_IB,     // American and European Session - Initial Balance
        AmericanAndEuropean_BB,     // American and European Session - Between Balances
        AmericanAndEuropean_FB,     // American and European Session - Final Balance

        // American Minor sessions
        American_IB,                // American Session - Initial Balance
        American_BB,                // American Session - Between Balances
        American_FB,                // American Session - Final Balance

    }
}
