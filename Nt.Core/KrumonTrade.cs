using Nt.Core.Trading;

namespace Nt.Core
{
    public static class KrumonTrade
    {

        /// <summary>
        /// Create a <see cref="SessionBuilder"/> default instance.
        /// </summary>
        /// <remarks>The <see cref="SessionBuilder"/> object is used to create a day trading sessions 
        /// to controller the times, volume and prices in the historical data and in 
        /// real time markets.</remarks>
        public static SessionBuilder CreateTradingSessionBuilder() 
            => new SessionBuilder();


    }
}
