namespace Nt.Core.Configuration
{
    public interface INinjascriptConfigurationSource
    {
        /// <summary>
        /// Builds the <see cref="INinjascriptConfigurationBuilder"/> for this source.
        /// </summary>
        /// <param name="builder">The<see cref="INinjascriptConfigurationBuilder"/>.</param>
        /// <returns>An <see cref="INinjascriptConfigurationProvider"/></returns>
        INinjascriptConfigurationProvider Build(INinjascriptConfigurationBuilder builder);
    }
}