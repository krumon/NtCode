﻿using Nt.Core.Internal;
using System;

namespace Nt.Core.Ninjascripts
{
    /// <summary>
    /// Delegates to a new <see cref="INinjascript"/> instance using the full name of the given type, created by the
    /// provided <see cref="INinjascriptFactory"/>.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public class Ninjascript<T> : INinjascript<T>
    {
        private readonly INinjascript _ninjascript;

        /// <summary>
        /// Creates a new <see cref="Ninjascript{T}"/>.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public Ninjascript(INinjascriptFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _ninjascript = factory.CreateNinjascript(TypeNameHelper.GetTypeDisplayName(typeof(T), includeGenericParameters: false, nestedTypeDelimiter: '.'));
        }

        /// <inheritdoc />
        bool INinjascript.IsEnabled(NinjascriptLevel ninjascriptLevel)
        {
            return _ninjascript.IsEnabled(ninjascriptLevel);
        }

        /// <inheritdoc/>
        void INinjascript.Calculate(NinjascriptState state)
        {
            _ninjascript.Calculate(state);
        }

    }
}
