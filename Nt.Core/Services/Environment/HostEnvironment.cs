using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Core.Services
{
    internal class HostEnvironment
    {
        public string EnvironmentName { get; set; }

        public string ApplicationName { get; set; }

        public string ContentRootPath { get; set; }

        //public IFileProvider ContentRootFileProvider { get; set; }

    }
}
