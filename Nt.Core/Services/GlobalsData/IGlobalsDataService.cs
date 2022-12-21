using Nt.Core.Hosting;
using System;

namespace Nt.Core.Services
{
    /// <summary>
    /// Represents the properties and methods to create a default implementation of <see cref="GlobalsDataService"/>.
    /// </summary>
    public interface IGlobalsDataService : IHostedService, IConfigureService
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

    }
}
