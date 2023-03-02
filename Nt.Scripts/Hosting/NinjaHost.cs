using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Logging;
using Nt.Scripts.Indicators;
using System;

namespace Nt.Scripts.Hosting
{
    public static class NinjaHost
    {

        private static IHost _host;

        /// <summary>
        /// Gets <see cref="IHost"/> container.
        /// </summary>
        public static IHost Host => _host;

        /// <summary>
        /// Gets <see cref="ISessionsIndicator"/> service.
        /// </summary>
        public static ISessionsIndicator SessionsManager => _host?.Services.GetService<ISessionsIndicator>();

        ///// <summary>
        ///// Gets <see cref="ISessionsIterator"/> service.
        ///// </summary>
        //public static ISessionsIterator SessionsIterator => _host?.Services.GetService<ISessionsIterator>();

        ///// <summary>
        ///// Gets <see cref="ISessionsFilters"/> service.
        ///// </summary>
        //public static ISessionsFilters SessionsFilters => _host?.Services.GetService<ISessionsFilters>();

        /// <summary>
        /// Gets <see cref="ILogger{TCategoryName}"/> service.
        /// </summary>
        /// <typeparam name="T">The category type.</typeparam>
        /// <returns>The <see cref="ILogger{TCategoryName}"/> service.</returns>
        public static ILogger Logger<T>() => _host?.Services.GetService<ILogger<T>>();

        /// <summary>
        /// Create a <see cref="NinjaHost"/> with the configure host.
        /// </summary>
        /// <param name="host"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Create(IHost host)
        {
            _host = host ?? throw new ArgumentNullException(nameof(host));                
        }
    }
}
