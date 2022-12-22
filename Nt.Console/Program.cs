﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Logging;
using Nt.Core.Services;
using Nt.Scripts;
using Nt.Scripts.Ninjascripts.Design;
using System;

namespace ConsoleApp
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            //System.IServiceProvider sc;
            //sc.GetServices
            

            //Microsoft.Extensions.DependencyInjection.ServiceProvider
            //Microsoft.Extensions.DependencyInjection.ServiceDescriptor
            //Microsoft.Extensions.Options.OptionsBuilder
            IHost host = Hosting.CreateDefaultBuilder()
                .ConfigureHostOptions((options) =>
                {
                    options.IsInDesignMode = true;
                })
                .ConfigureServices((serviceCollection) => 
                {
                    serviceCollection
                    .AddLogging()
                    .Add<IGlobalsDataService>(new GlobalsDataDesignScript())
                    .Add<IChartDataService, ChartDataDesignScript>()
                    .AddSessions<ISessionsService>((builder) =>
                    {
                        builder.ConfigureFilters((options) =>
                        {
                            options.IncludePartialHolidays = false;
                            options.IncludeHolidays = false;
                            options.ExcludeHistoricalData = true;
                        });
                    })
                    ;
                })
                .Build();
            Nt.Core.Logging.ILogger<Program> logger = host.Services.GetService<Nt.Core.Logging.ILogger<Program>>();
            var globalsData = host.Services.GetService<IGlobalsDataService>();
            var sessionsIterator = host.Services.GetService<ISessionsIteratorService>();
            var sessionsFilters = host.Services.GetService<ISessionsFiltersService>();
            var session = host.Services.GetService<ISessionsService>();
            //var session2 = host.Services.GetService<ISessionScript>();

            sessionsIterator.ActualSessionBegin = DateTime.Now;

            host.Configure(null);
            host.DataLoaded(null);
            host.OnBarUpdate();
            host.OnMarketData();
            host.OnSessionUpdate();
            host.Dispose();
            
        }
    }
}
