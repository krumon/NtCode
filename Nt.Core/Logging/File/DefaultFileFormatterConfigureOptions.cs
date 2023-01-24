using Nt.Core.Options;

namespace Nt.Core.Logging.File
{
    internal sealed class DefaultFileFormatterConfigureOptions : ConfigureOptions<FileFormatterOptions>
    {
        public DefaultFileFormatterConfigureOptions() : base(options => 
        {
            options.SingleLine = true;
            options.LogLevel = true;
            options.TimestampOptions = new TimestampOptions();

        }) { }
    }
}
