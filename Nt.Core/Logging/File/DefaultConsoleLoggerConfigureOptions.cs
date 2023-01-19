using Nt.Core.Options;
using System.IO;

namespace Nt.Core.Logging.File
{
    internal sealed class DefaultFileLoggerConfigureOptions : ConfigureOptions<FileLoggerOptions>
    {
        public DefaultFileLoggerConfigureOptions() : base(options =>
        {
            options.FileLogs[0].LogLevel = LogLevel.Debug;
            options.FileLogs[0].LogTime = true;
            options.FileLogs[0].LogAtTop = true;
            options.FileLogs[0].Directory = Directory.GetCurrentDirectory();
            options.FileLogs[0].FileName = "syslog";

            //options.FileLogs[1].LogLevel = LogLevel.Debug;
            //options.FileLogs[1].LogTime = true;
            //options.FileLogs[1].LogAtTop = true;
            //options.FileLogs[1].Directory = Directory.GetCurrentDirectory();
            //options.FileLogs[1].FileName = "syslog";

            //options.FileLogs[2].LogLevel = LogLevel.Debug;
            //options.FileLogs[2].LogTime = true;
            //options.FileLogs[2].LogAtTop = true;
            //options.FileLogs[2].Directory = Directory.GetCurrentDirectory();
            //options.FileLogs[2].FileName = "syslog";

        })
        { }
    }
}
