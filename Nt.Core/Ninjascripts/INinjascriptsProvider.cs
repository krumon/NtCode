using System;

namespace Nt.Core.Ninjascripts
{
    /// <summary>
    /// Represents a type that can create instances of <see cref="INinjascripts"/>.
    /// </summary>
    public interface INinjascriptsProvider : IDisposable
    {
        /// <summary>
        /// Creates a new <see cref="INinjascripts"/> instance.
        /// </summary>
        /// <param name="categoryName">The category name for ninjascript to be created.</param>
        /// <returns>The instance of <see cref="INinjascripts"/> that was created.</returns>
        INinjascripts CreateNinjascript(string categoryName);
    }
}
