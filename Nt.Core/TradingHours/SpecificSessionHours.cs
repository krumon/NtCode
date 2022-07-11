
namespace NtCore
{
    /// <summary>
    /// Trading time zone types.
    /// </summary>
    public enum SpecificSessionHours
    {

        Electronic,                 // Electronic Trading Hours  
        Electronic_IB,              // Electronic Trading Hours - Initial Balance
        Electronic_FB,              // Electronic Trading Hours - Final Balance

        Regular,                    // Regular trading hours.
        Regular_IB,                 // Regular Trading Hours - Initial Balance
        Regular_FB,                 // Regular Trading Hours - Final Balance

        DAY,                        // Daylight Trading Hours 
        DAY_IB,                     // Daylight Trading Hours - Initial Balance
        DAY_FB,                     // Daylight Trading Hours - Final Balance

        OVN,                        // Overnight Trading Hours 
        OVN_IB,                     // Overnight Trading Hours - Initial Balance
        OVN_FB,                     // Overnight Trading Hours - Final Balance

        Asian,                      // Asian Trading Hours 
        Asian_IB,                   // Asian Trading Hours - Initial Balance
        Asian_FB,                   // Asian Trading Hours - Final Balance

        American,                   // American Trading Hours 
        American_IB,                // American Trading Hours - Initial Balance
        American_FB,                // American Trading Hours - Final Balance

        European,                   // European Trading Hours 
        European_IB,                // European Trading Hours - Initial Balance
        European_FB,                // European Trading Hours - Final Balance

        AmericanAndEuropean,        // American and European Trading Hours 
        AmericanAndEuropean_IB,     // American and European Trading Hours - Initial Balance
        AmericanAndEuropean_FB,     // American and European Trading Hours - Final Balance

    }
}
