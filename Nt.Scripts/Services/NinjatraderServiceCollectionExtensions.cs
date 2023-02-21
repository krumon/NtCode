using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.DrawingTools;
using Nt.Core.DependencyInjection;
using System;

namespace Nt.Scripts.Services
{
    public static class NinjatraderServiceCollectionExtensions
    {

        public static IServiceCollection AddNinjatraderObjects(this IServiceCollection services, object[] ninjatraderObjects)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (ninjatraderObjects == null || ninjatraderObjects.Length == 0)
                return services;

            AddDataServices(services,ninjatraderObjects);
            AddNinjascripts(services, ninjatraderObjects);

            return services;
        }

        public static IServiceCollection AddNinjascripts(IServiceCollection services, object[] ninjatraderObjects)
        {
            // TODO: Delete this condition. Is obny necesary for tests.
            if (ninjatraderObjects == null || ninjatraderObjects.Length == 0)
                //throw new ArgumentNullException(nameof(ninjatraderObjects));
                AddNinjascriptService(services, null);

            foreach (var ninjatraderObject in ninjatraderObjects)
            {
                if (ninjatraderObject is NinjaScriptBase ninjascript)
                    AddNinjascriptService(services, ninjascript);
                else if (ninjatraderObject is ChartBars chartBars)
                    services.TryAddEnumerable(ServiceDescriptor.Singleton<IChart>(new Chart(chartBars))); ;
            }
            return services;
        }

        public static IServiceCollection AddDataServices(IServiceCollection services, object[] ninjatraderObjects)
        {
            services.AddGlobalsData();
            // TODO: Delete this condition. Is obny necesary for tests.
            if (ninjatraderObjects == null || ninjatraderObjects.Length == 0)
                //throw new ArgumentNullException(nameof(ninjatraderObjects));
                AddChartService(services, null);

            foreach (var ninjatraderObject in ninjatraderObjects)
            {
                if (ninjatraderObject is ChartBars chartBars)
                    services.TryAddEnumerable(ServiceDescriptor.Singleton<IChart>(new Chart(chartBars))); ;
            }
            return services;
        }

        // TODO: Delete default ninjascript parameter value (null).
        private static void AddNinjascriptService(IServiceCollection services, NinjaScriptBase ninjascript)
        {
            // TODO: Delete this condition. Is only for console testing.
            if (ninjascript == null)
                services.TryAdd(ServiceDescriptor.Singleton<INinjascript>(new Ninjascript()));
            // TODO: Delete this condition in future when develop the indicator, strategy,... services.
            else if (ninjascript is NinjaScriptBase)
                services.TryAdd(ServiceDescriptor.Singleton<INinjascript>(new Ninjascript(ninjascript)));
            else if (ninjascript is IndicatorBase indicator)
                return;
            else if (ninjascript is StrategyBase strategy)
                return;
            else if (ninjascript is IDrawingTool drawingTool)
                return;
        }
        // TODO: Delete default ninjascript parameter value (null).
        private static void AddChartService(IServiceCollection services, ChartBars chartBars)
        {
            // TODO: Delete this condition. Is only for console testing.
            if (chartBars == null)
                services.TryAdd(ServiceDescriptor.Singleton<IChart>(new Chart()));
            // TODO: Delete this condition in future when develop the indicator, strategy,... services.
            else 
                services.TryAdd(ServiceDescriptor.Singleton<IChart>(new Chart(chartBars)));
        }

    }
}
