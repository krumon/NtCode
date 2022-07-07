using System;

namespace NtCore
{
    /// <summary>
    /// The type price of the bar.
    /// </summary>
    public enum SpecificTradingTime
    {

        Electronic_Open,        // Electronic Trading Hours - Open 
        Electronic_Close,       // Electronic Trading Hours - Close 
        Electronic_IB_Open,     // Electronic Trading Hours - Initial Balance - Open
        Electronic_IB_Close,    // Electronic Trading Hours - Initial Balance - Close
        Electronic_FB_Open,     // Electronic Trading Hours - Final Balance - Open
        Electronic_FB_Close,    // Electronic Trading Hours - Final Balance - Close

        Regular_Open,           // Regular Trading Hours - Open 
        Regular_Close,          // Regular Trading Hours - Close 
        Regular_IB_Open,        // Regular Trading Hours - Initial Balance - Open
        Regular_IB_Close,       // Regular Trading Hours - Initial Balance - Close
        Regular_FB_Open,        // Regular Trading Hours - Final Balance - Open
        Regular_FB_Close,       // Regular Trading Hours - Final Balance - Close

        DAY_Open,               // Daylight Trading Hours - Open 
        DAY_Close,              // Daylight Trading Hours - Close 
        DAY_IB_Open,            // Daylight Trading Hours - Initial Balance - Open
        DAY_IB_Close,           // Daylight Trading Hours - Initial Balance - Close
        DAY_FB_Open,            // Daylight Trading Hours - Final Balance - Open
        DAY_FB_Close,           // Daylight Trading Hours - Final Balance - Close

        OVN_Open,               // Overnight Trading Hours - Open 
        OVN_Close,              // Overnight Trading Hours - Close 
        OVN_IB_Open,            // Overnight Trading Hours - Initial Balance - Open
        OVN_IB_Close,           // Overnight Trading Hours - Initial Balance - Close
        OVN_FB_Open,            // Overnight Trading Hours - Final Balance - Open
        OVN_FB_Close,           // Overnight Trading Hours - Final Balance - Close
        
    }
}
