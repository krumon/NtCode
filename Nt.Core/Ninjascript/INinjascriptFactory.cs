using System;

namespace Nt.Core.Ninjascript
{
    /// <summary>
    /// Represents a type used to configure the ninjascript system and create instances of
    /// <see cref="INinjascript"/> from the registered <see cref="INinjascriptProvider"/>.
    /// </summary>
    public interface INinjascriptFactory : IDisposable
    {
        /// <summary>
        /// Creates a new <see cref="INinjascript"/> instance.
        /// </summary>
        /// <param name="categoryName">The category name for the ninjascript.</param>
        /// <returns>The <see cref="INinjascript"/>.</returns>
        INinjascript CreateNinjascript(string categoryName);

        /// <summary>
        /// Adds an <see cref="INinjascriptProvider"/> to the ninjascripts system.
        /// </summary>
        /// <param name="provider">The <see cref="INinjascriptProvider"/>.</param>
        void AddProvider(INinjascriptProvider provider);
    }
}
