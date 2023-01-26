using Nt.Core.Options;
using System.IO;

namespace Nt.Core.Logging.File
{
    internal sealed class DefaultFileLoggerConfigureOptions : ConfigureOptions<FileLoggerOptions>
    {
        public DefaultFileLoggerConfigureOptions() : base(options =>
        {
            //options.LogLevel = LogLevel.Debug;
            options.LogAtTop = false;
            options.Directory = Directory.GetCurrentDirectory();
            options.Name = "log.txt";

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
