using System;
using System.Collections.Generic;

namespace Nt.Core.Hosting.Configuration
{
    public class NinjascriptConfigurationBuilder : INinjascriptConfigurationBuilder
    {

        /// <summary>
        /// Returns the sources used to obtain configuration values.
        /// </summary>
        public IList<INinjascriptConfigurationSource> Sources { get; } = new List<INinjascriptConfigurationSource>();

        /// <summary>
        /// Gets a key/value collection that can be used to share data between the <see cref="IConfigurationBuilder"/>
        /// and the registered <see cref="IConfigurationProvider"/>s.
        /// </summary>
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Adds a new configuration source.
        /// </summary>
        /// <param name="source">The configuration source to add.</param>
        /// <returns>The same <see cref="IConfigurationBuilder"/>.</returns>
        public INinjascriptConfigurationBuilder Add(INinjascriptConfigurationSource source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Sources.Add(source);
            return this;
        }

        /// <summary>
        /// Builds an <see cref="IConfiguration"/> with keys and values from the set of providers registered in
        /// <see cref="Sources"/>.
        /// </summary>
        /// <returns>An <see cref="IConfigurationRoot"/> with keys and values from the registered providers.</returns>
        public INinjascriptConfigurationRoot Build()
        {
            var providers = new List<INinjascriptConfigurationProvider>();
            foreach (INinjascriptConfigurationSource source in Sources)
            {
                INinjascriptConfigurationProvider provider = source.Build(this);
                providers.Add(provider);
            }
            return new NinjascriptConfigurationRoot(providers);
        }

    }

}
