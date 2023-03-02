using Nt.Core.Options;

namespace Nt.Scripts.Indicators.Internal
{
    internal sealed class DefaultIndicatorFilterConfigureOptions : ConfigureOptions<IndicatorFilterOptions>
    {
        public DefaultIndicatorFilterConfigureOptions(object filter) : base(options => options.CaptureScopes = false)
        {
        }
    }
}
