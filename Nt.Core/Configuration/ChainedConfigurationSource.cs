﻿namespace Nt.Core.Configuration
{
    /// <summary>
    /// Represents a chained <see cref="IConfiguration"/> as an <see cref="IConfigurationSource"/>.
    /// </summary>
    public class ChainedConfigurationSource : IConfigurationSource
    {
        /// <summary>
        /// The chained configuration.
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// Whether the chained configuration should be disposed when the
        /// configuration provider gets disposed.
        /// </summary>
        public bool ShouldDisposeConfiguration { get; set; }

        /// <summary>
        /// Builds the <see cref="ChainedConfigurationProvider"/> for this source.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>A <see cref="ChainedConfigurationProvider"/></returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
            => new ChainedConfigurationProvider(this);
    }
}
