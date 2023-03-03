﻿using Kr.Core.Helpers;
using System;

namespace Nt.Scripts.Indicators
{
    /// <summary>
    /// Delegates to a new <see cref="IIndicator"/> instance using the full name of the given type, created by the
    /// provided <see cref="IIndicatorFactory"/>.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public class Indicator<T> : IIndicator<T>
    {
        private readonly IIndicator _indicator;

        /// <summary>
        /// Creates a new <see cref="Indicator{T}"/>.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public Indicator(IIndicatorFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _indicator = factory.CreateIndicator(TypeNameHelper.GetTypeDisplayName(typeof(T), includeGenericParameters: false, nestedTypeDelimiter: '.'));
        }

        /// <inheritdoc />
        bool IIndicator.IsEnabled(IndicatorState state)
        {
            return _indicator.IsEnabled(state);
        }
    }
}