using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kr.Core
{
    public static class Hosting
    {
        private static IHost _host;

        public static IHost Host 
        { 
            get 
            {
                if (_host == null)
                    CreateServices();

                return _host;    
            } 
        }


        public static void CreateServices()
        {
            _host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder().Build();
        }
    }
}
