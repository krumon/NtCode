using Nt.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Core.Logging.Console
{
    internal class ConsoleLoggerConfigureOptions : ConfigureOptions<ConsoleLoggerOptions>
    {
        public ConsoleLoggerConfigureOptions(Action<ConsoleLoggerOptions> action) : base(action)
        {
        }
    }
}
