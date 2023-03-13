using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Logging;
using Nt.Scripts.Indicators;
using Nt.Scripts.Ninjascripts;
using System;

namespace Nt.Scripts.Hosting.Design
{
    public static class DesignNinjaHost
    {

        private static IHost _host;

        /// <summary>
        /// Gets <see cref="IHost"/> container.
        /// </summary>
        public static IHost Host => _host;

        /// <summary>
        /// Gets <see cref="ISessions"/> service.
        /// </summary>
        public static INinjascript Ninjascript => _host?.Services.GetService<INinjascript<Sessions>>();

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
