
using Nt.Core.Internal;
using System;

namespace Nt.Core.Ninjascripts
{
    /// <summary>
    /// Delegates to a new <see cref="INinjascripts"/> instance using the full name of the given type, created by the
    /// provided <see cref="INinjascriptsFactory"/>.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public class Ninjascripts<T> : INinjascripts<T>
    {
        private readonly INinjascripts _ninjascripts;

        /// <summary>
        /// Creates a new <see cref="Ninjascripts{T}"/>.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public Ninjascripts(INinjascriptsFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _ninjascripts = factory.CreateNinjascript(TypeNameHelper.GetTypeDisplayName(typeof(T), includeGenericParameters: false, nestedTypeDelimiter: '.'));
        }
    }

    internal class Ninjascripts : INinjascripts
    {
    }


}
