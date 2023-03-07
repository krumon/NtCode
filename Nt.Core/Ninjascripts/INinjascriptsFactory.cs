using System;

namespace Nt.Core.Ninjascripts
{
    /// <summary>
    /// Represents a type used to configure the ninjascript system and create instances of
    /// <see cref="INinjascripts"/> from the registered <see cref="INinjascriptsProvider"/>.
    /// </summary>
    public interface INinjascriptsFactory : IDisposable
    {
        /// <summary>
        /// Creates a new <see cref="INinjascripts"/> instance.
        /// </summary>
        /// <param name="categoryName">The category name for the ninjascript.</param>
        /// <returns>The <see cref="INinjascripts"/>.</returns>
        INinjascripts CreateNinjascript(string categoryName);

        /// <summary>
        /// Adds an <see cref="INinjascriptsProvider"/> to the ninjascripts system.
        /// </summary>
        /// <param name="provider">The <see cref="INinjascriptsProvider"/>.</param>
        void AddProvider(INinjascriptsProvider provider);
    }
}
