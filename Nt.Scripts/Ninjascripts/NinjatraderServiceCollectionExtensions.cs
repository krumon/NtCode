using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.DrawingTools;
using Nt.Core.DependencyInjection;
using System;

namespace Nt.Scripts.Ninjascripts
{
    public static class NinjatraderServiceCollectionExtensions
    {

        public static IServiceCollection AddNinjatraderObjects(this IServiceCollection services, object[] ninjatraderObjects)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (ninjatraderObjects == null || ninjatraderObjects.Length == 0)
                return services;

            AddNinjatraderServices(services, ninjatraderObjects);

            return services;
        }

        private static void AddNinjatraderServices(IServiceCollection services, object[] ninjatraderObjects)
        {
            foreach (var ninjatraderObject in ninjatraderObjects)
            {
                if (ninjatraderObject == null)
                {
                    // TODO: Delete this line. Is available for tests.
                    AddNinjascriptService(services, null);
                    continue;
                }

                if (ninjatraderObject is NinjaScriptBase ninjascript)
                    AddNinjascriptService(services, ninjascript);
                if (ninjatraderObject is ChartBars chartBars)
                    return;
            }
        }

        // TODO: Delete default ninjascript parameter value (null).
        private static void AddNinjascriptService(IServiceCollection services, object ninjascript)
        {
            // TODO: Delete this condition. Is only for console testing.
            if (ninjascript == null)
                services.TryAddEnumerable(ServiceDescriptor.Singleton<INinjascript>(new Ninjascript()));
            //// TODO: Delete this condition in future when develop the indicator, strategy,... services.
            //else if (ninjascript != null)
            //    services.TryAddEnumerable(ServiceDescriptor.Singleton<INinjascript>(new Ninjascript(ninjascript)));
            else if (ninjascript is IndicatorBase indicator)
                return;
            else if (ninjascript is StrategyBase strategy)
                return;
            else if (ninjascript is IDrawingTool drawingTool)
                return;
        }
    }
}
