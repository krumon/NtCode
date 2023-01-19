using Nt.Core.Options;

namespace Nt.Core.Logging.File
{
    internal sealed class DefaultFileFormatterConfigureOptions : ConfigureOptions<FileFormatterOptions>
    {
        public DefaultFileFormatterConfigureOptions() : base(options => 
        {
            options.SingleLine = true;
            options.ShowLogLevel = true;
            options.TimestampFormat = "YYYY:MM:DD hh:mm:ss";
            options.UseUtcTimestamp = false;

        }) { }
    }
}
