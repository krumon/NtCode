using System;

namespace Nt.Scripts.Indicators
{
    /// <summary>
    /// Defines a rule used to filter indicators.
    /// </summary>
    public class IndicatorFilterRule
    {
        /// <summary>
        /// Creates a new <see cref="IndicatorFilterRule"/> instance.
        /// </summary>
        /// <param name="providerName">The provider name to use in this filter rule.</param>
        /// <param name="indicatorName">The indicator name to use in this filter rule.</param>
        /// <param name="filter">The filter to apply.</param>
        public IndicatorFilterRule(string providerName, string indicatorName, Func<string, string, int, bool> filter)
        {
            ProviderName = providerName;
            IndicatorName = indicatorName;
            Filter = filter;
            //LogLevel = logLevel;
        }

        /// <summary>
        /// Gets the logger provider type or alias this rule applies to.
        /// </summary>
        public string ProviderName { get; }

        /// <summary>
        /// Gets the indicator name this rule applies to.
        /// </summary>
        public string IndicatorName { get; }

        ///// <summary>
        ///// Gets the minimum <see cref="LogLevel"/> of messages.
        ///// </summary>
        //public LogLevel? LogLevel { get; }

        /// <summary>
        /// Gets the filter delegate that would be applied to indicators that passed the <see cref="int"/> value.
        /// </summary>
        public Func<string, string, int, bool> Filter { get; }

        ///// <inheritdoc/>
        //public override string ToString()
        //{
        //    return $"{nameof(ProviderName)}: '{ProviderName}', {nameof(CategoryName)}: '{CategoryName}', {nameof(LogLevel)}: '{LogLevel}', {nameof(Filter)}: '{Filter}'";
        //}
        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(ProviderName)}: '{ProviderName}', {nameof(IndicatorName)}: '{IndicatorName}'";
        }
    }
}
