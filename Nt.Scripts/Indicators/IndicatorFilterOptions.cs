using System.Collections.Generic;

namespace Nt.Scripts.Indicators
{
    /// <summary>
    /// The options for a IndicatorFilter.
    /// </summary>
    public class IndicatorFilterOptions
    {
        /// <summary>
        /// Creates a new <see cref="LoggerFilterOptions"/> instance.
        /// </summary>
        public IndicatorFilterOptions() { }

        /// <summary>
        /// Gets or sets value indicating whether logging scopes are being captured. Defaults to <c>true</c>
        /// </summary>
        public bool CaptureScopes { get; set; } = true;

        ///// <summary>
        ///// Gets or sets the minimum level of log messages if none of the rules match.
        ///// </summary>
        //public LogLevel MinLevel { get; set; }

        /// <summary>
        /// Gets the collection of <see cref="LoggerFilterRule"/> used for filtering log messages.
        /// </summary>
        public IList<IndicatorFilterRule> Rules => RulesInternal;

        // Concrete representation of the rule list
        internal List<IndicatorFilterRule> RulesInternal { get; } = new List<IndicatorFilterRule>();
    }
}
