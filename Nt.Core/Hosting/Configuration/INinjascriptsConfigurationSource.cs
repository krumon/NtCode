namespace Nt.Core.Hosting.Configuration
{
    public interface INinjascriptsConfigurationSource
    {
        /// <summary>
        /// Builds the <see cref="INinjascriptsConfigurationBuilder"/> for this source.
        /// </summary>
        /// <param name="builder">The<see cref="INinjascriptsConfigurationBuilder"/>.</param>
        /// <returns>An <see cref="INinjascriptsConfigurationProvider"/></returns>
        INinjascriptsConfigurationProvider Build(INinjascriptsConfigurationBuilder builder);
    }
}