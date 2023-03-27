using NinjaTrader.NinjaScript;
using Nt.Core.Configuration;
using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using System;
using System.Collections.Generic;

namespace Nt.Scripts.Hosting
{
    public interface INinjaHostBuilder : IHostBuilder
    {


        /// <summary>
        /// Run the given actions to initialize the host. This can only be called once.
        /// </summary>
        /// <returns>An initialized <see cref="IHost"/></returns>
        INinjaHost Build(NinjaScriptBase ninjaScript);

    }
}