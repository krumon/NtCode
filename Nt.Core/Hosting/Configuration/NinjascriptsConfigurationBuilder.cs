using System;
using System.Collections.Generic;

namespace Nt.Core.Hosting.Configuration
{
    public class NinjascriptsConfigurationBuilder : INinjascriptsConfigurationBuilder
    {

        /// <summary>
        /// Returns the sources used to obtain configuration values.
        /// </summary>
        public IList<INinjascriptsConfigurationSource> Sources { get; } = new List<INinjascriptsConfigurationSource>();

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
        public INinjascriptsConfigurationBuilder Add(INinjascriptsConfigurationSource source)
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
        public INinjascriptsConfigurationRoot Build()
        {
            var providers = new List<INinjascriptsConfigurationProvider>();
            foreach (INinjascriptsConfigurationSource source in Sources)
            {
                INinjascriptsConfigurationProvider provider = source.Build(this);
                providers.Add(provider);
            }
            return new NinjascriptsConfigurationRoot(providers);
        }

    }

}
