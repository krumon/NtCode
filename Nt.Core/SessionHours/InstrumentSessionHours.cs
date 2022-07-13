
namespace NtCore
{
    /// <summary>
    /// Instrument session hours types.
    /// </summary>
    public enum InstrumentSessionHours
    {
        // Main session
        Electronic,                 // Electronic Session  

        // Regular and overnight sessions
        Regular,                    // Regular Session.
        OVN,                        // Overnight Session 
        Closed,                     // Session is Closed

        // Major sessions
        Asian,                      // Asian Session 
        European,                   // European Session 
        AmericanAndEuropean,        // American and European Session 
        American,                   // American Session 
        Residual_AM,                // American Residual Session 
        Residual_AS,                // Asian Residual Session 

        // Residual Minor sessions
        Residual_AM_AET,            // American Residual - American Extra Time Session
        Residual_AM_Break,          // American Residual - Break
        Residual_AM_EOD,            // American Residual - End Of Day Session
        Residual_AM_ND,             // American Residual - New Day Session

        // Asian Balances
        Asian_IB,                   // Asian Session - Initial Balance
        Asian_BB,                   // Asian Session - Between Balances
        Asian_FB,                   // Asian Session - Final Balance

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

        // New Day Balances
        Residual_AM_NDIB,           // American Residual Session - New Day Initial Balance
        Residual_AM_NDBB,           // American Residual Session - New Day Between Balances
        Residual_AM_NDFB,           // American Residual Session - New Day Final Balance

        // Asian Residual Balances
        Residual_AS_IB,             // American Residual Session - New Day Initial Balance
        Residual_AS_BB,             // American Residual Session - New Day Between Balances
        Residual_AS_FB,             // American Residual Session - New Day Final Balance

    }
}
