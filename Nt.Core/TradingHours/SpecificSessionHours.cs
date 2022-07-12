
namespace NtCore
{
    /// <summary>
    /// Trading time zone types.
    /// </summary>
    public enum SpecificSessionHours
    {
        // Main session
        Electronic,                 // Electronic Trading Hours  

        // Regular and overnight sessions
        Regular,                    // Regular session.
        OVN,                        // Overnight session 

        // Major sessions
        Residual,                   // Residual session 
        Asian,                      // Asian Trading Hours 
        European,                   // European Trading Hours 
        AmericanAndEuropean,        // American and European Trading Hours 
        American,                   // American Trading Hours 

        // Minor sessions
        Residual_AET,               // Residual Trading Hours - American Extra Time
        Residual_EOD,               // Residual Trading Hours - End Of Day
        Residual_NDB,               // Residual Trading Hours - New Day Balance

        Asian_IB,                   // Asian Trading Hours - Initial Balance
        Asian_BB,                   // Asian Trading Hours - Between Balances
        Asian_FB,                   // Asian Trading Hours - Final Balance

        European_IB,                // European Trading Hours - Initial Balance
        European_BB,                // European Trading Hours - Between Balances
        European_FB,                // European Trading Hours - Final Balance

        AmericanAndEuropean_IB,     // American and European Trading Hours - Initial Balance
        AmericanAndEuropean_BB,     // American and European Trading Hours - Between Balances
        AmericanAndEuropean_FB,     // American and European Trading Hours - Final Balance

        American_IB,                // American Trading Hours - Initial Balance
        American_BB,                // American Trading Hours - Between Balances
        American_FB,                // American Trading Hours - Final Balance

        //Break,                      // Break hours.
        //Closed,                     // Closed hours.

    }
}
