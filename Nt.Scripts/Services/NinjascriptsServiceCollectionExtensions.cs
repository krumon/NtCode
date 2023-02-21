using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.DrawingTools;
using Nt.Core.DependencyInjection;
using System;

namespace Nt.Scripts.Services
{
    public static class NinjascriptsServiceCollectionExtensions
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
            // TODO: Delete this condition. Is obny necesary for tests.
            if (ninjatraderObjects == null || ninjatraderObjects.Length == 0)
                //throw new ArgumentNullException(nameof(ninjatraderObjects));
                AddNinjascriptService(services, null);

            foreach (var ninjatraderObject in ninjatraderObjects)
            {
                if (ninjatraderObject is NinjaScriptBase ninjascript)
                    AddNinjascriptService(services, ninjascript);
                else if (ninjatraderObject is ChartBars chartBars)
                    return;
            }
        }
        // TODO: Delete default ninjascript parameter value (null).
        private static void AddNinjascriptService(IServiceCollection services, object ninjascript)
        {
            // TODO: Delete this condition. Is only for console testing.
            if (ninjascript == null)
                services.TryAddEnumerable(ServiceDescriptor.Singleton<INinjascript>(new Ninjascript()));
            // TODO: Delete this condition in future when develop the indicator, strategy,... services.
            else if (ninjascript is NinjaScriptBase script)
                services.TryAddEnumerable(ServiceDescriptor.Singleton<INinjascript>(new Ninjascript(script)));
            else if (ninjascript is IndicatorBase indicator)
                return;
            else if (ninjascript is StrategyBase strategy)
                return;
            else if (ninjascript is IDrawingTool drawingTool)
                return;
        }

    }
}
