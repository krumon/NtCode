using Nt.Core.Hosting;
using System;

namespace Nt.Core.Services
{
    public interface ISessionIteratorService : IHostedService, IOnBarUpdateService
    {

        /// <summary>
        /// Represents the actual session start time converted to the user's configured Time Zone.
        /// </summary>
        DateTime ActualSessionBegin { get; }

        /// <summary>
        /// Represents the actual session end time converted to the user's configured Time Zone.
        /// </summary>
        DateTime ActualSessionEnd { get; }

        /// <summary>
        /// Represents the user's configured <see cref="TimeZoneInfo"/>.
        /// </summary>
        TimeZoneInfo UserTimeZoneInfo { get; }

        /// <summary>
        /// Represents the bar's configured <see cref="TimeZoneInfo"/>.
        /// </summary>
        TimeZoneInfo BarsTimeZoneInfo { get; }

        /// <summary>
        /// The sessions counter.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Is true when a new session bars enter in a new session.
        /// </summary>
        bool IsNewSession { get; }

        /// <summary>
        /// Is true when a service is correctly configured.
        /// </summary>
        bool IsConfigured { get; }

        /// <summary>
        /// Is true when a service is correctly configured when the ninjascript data is loaded.
        /// </summary>
        bool IsDataLoaded { get; }

        /// <summary>
        /// Is true if the bars configured by the 
        /// </summary>
        bool? IsBarsIntraday { get; }

    }
}
