namespace Nt.Core.Configuration
{
    /// <summary>
    /// Represents a source of configuration key/values for an application.
    /// </summary>
    public interface IConfigurationSource
    {
        /// <summary>
        /// Builds the Microsoft.Extensions.Configuration.IConfigurationProvider for this source.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>An <see cref="IConfigurationProvider"/></returns>
        IConfigurationProvider Build(IConfigurationBuilder builder);
    }
}