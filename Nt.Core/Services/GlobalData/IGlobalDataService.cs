using Nt.Core.Hosting;
using System;

namespace Nt.Core.Services
{
    public interface IGlobalDataService : IHostedService
    {

        /// <summary>
        /// Gets the maximum available date for any ninjascript.
        /// </summary>
        DateTime MaxDate { get; }

        /// <summary>
        /// ets the minimum available date for any ninjascript.
        /// </summary>
        DateTime MinDate { get; }

        /// <summary>
        /// Gets the <see cref="TimeZoneInfo"/> configure in the platform.
        /// </summary>
        TimeZoneInfo UserConfigureTimeZoneInfo { get; }

        /// <summary>
        /// Gets the <see cref="TimeZoneInfo"/> configure in the primary bars.
        /// </summary>
        TimeZoneInfo BarsConfigureTimeZoneInfo { get; }

    }
}
