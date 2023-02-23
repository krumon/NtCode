using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.DrawingTools;
using Nt.Core.DependencyInjection;
using System;

namespace Nt.Scripts.Services
{
    public static class NinjatraderServiceCollectionExtensions
    {

        public static IServiceCollection AddGlobalsData(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAdd(ServiceDescriptor.Singleton<IGlobalsData, GlobalsData>());

            return services;
        }

        public static IServiceCollection TryAddGlobalsData(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            try
            {
                AddGlobalsData(services);
            }
            catch { }

            return services;
        }

        public static IServiceCollection AddNinjascript(this IServiceCollection services, NinjaScriptBase ninjascript)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (ninjascript == null)
                throw new ArgumentNullException(nameof(ninjascript));

            // TODO: Delete this condition in future when develop the indicator, strategy,... services.
            if (ninjascript is NinjaScriptBase)
                services.TryAdd(ServiceDescriptor.Singleton<INinjascript>(new Ninjascript(ninjascript)));
            else if (ninjascript is IndicatorBase indicator)
                return services;
            else if (ninjascript is StrategyBase strategy)
                return services;
            else if (ninjascript is IDrawingTool drawingTool)
                return services;

            return services;
        }

        public static IServiceCollection TryAddNinjascript(this IServiceCollection services, NinjaScriptBase ninjascript)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            try
            {
                AddNinjascript(services, ninjascript);
            }
            catch 
            {
            }

            return services;
        }
        public static IServiceCollection AddChartBarsData(this IServiceCollection services, ChartBars chartBars)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (chartBars == null)
                throw new ArgumentNullException(nameof(chartBars));
             
            services.TryAdd(ServiceDescriptor.Singleton<IChartBarsData>(new ChartBarsData(chartBars)));

            return services;
        }

        public static IServiceCollection TryAddChartBarsData(this IServiceCollection services, ChartBars chartBars)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            try
            {
                AddChartBarsData(services, chartBars);
            }
            catch 
            {
            }

            return services;
        }
    }
}
