using System;

namespace NtCore
{
    /// <summary>
    /// The type price of the bar.
    /// </summary>
    public enum TradingHourType
    {

        ETH_Open,           // Electronic Trading Hours - Open 
        ETH_Close,          // Electronic Trading Hours - Close 
        ETH_IB_Open,        // Electronic Trading Hours - Initial Balance - Open
        ETH_IB_Close,       // Electronic Trading Hours - Initial Balance - Close
        ETH_FB_Open,        // Electronic Trading Hours - Final Balance - Open
        ETH_FB_Close,       // Electronic Trading Hours - Final Balance - Close

        Asian_Open,         // Asian Trading Hours - Open 
        Asian_Close,        // Asian Trading Hours - Close 
        Asian_IB_Open,      // Asian Trading Hours - Initial Balance - Open
        Asian_IB_Close,     // Asian Trading Hours - Initial Balance - Close
        Asian_FB_Open,      // Asian Trading Hours - Final Balance - Open
        Asian_FB_Close,     // Asian Trading Hours - Final Balance - Close

        American_Open,      // American Trading Hours - Open 
        American_Close,     // American Trading Hours - Close 
        American_IB_Open,   // American Trading Hours - Initial Balance - Open
        American_IB_Close,  // American Trading Hours - Initial Balance - Close
        American_FB_Open,   // American Trading Hours - Final Balance - Open
        American_FB_Close,  // American Trading Hours - Final Balance - Close

        European_Open,      // European Trading Hours - Open 
        European_Close,     // European Trading Hours - Close 
        European_IB_Open,   // European Trading Hours - Initial Balance - Open
        European_IB_Close,  // European Trading Hours - Initial Balance - Close
        European_FB_Open,   // European Trading Hours - Final Balance - Open
        European_FB_Close,  // European Trading Hours - Final Balance - Close

    }
}
