using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Nt.Core.Exceptions
{
    /// <summary>
    /// Stores in a dictionary any ninjascript error string.
    /// </summary>
    public static class NinjascriptErrors
    {

        private static Dictionary<string, string> Errors { get; }

        public static string NinjascriptLoadParametersException => GetString("NinjascriptLoadParametersException");
        public static string NinjascriptLoadedException => GetString("NinjascriptLoadedException");
        public static string NinjascriptConfigureException => GetString("NinjascriptConfigureException");
        public static string NinjaTraderNinjaScriptNullReferenceException => GetString("NinjaTraderNinjaScriptNullReferenceException");

        static NinjascriptErrors()
        {
            Errors = new Dictionary<string, string>
            {
                ["UnhandlerException"] = "Unhandler exception.",
                ["NinjascriptLoadParametersException"] = "The Ninjascript.Load method parameters cannot be null",
                ["NinjascriptLoadedException"] = "The Ninjascript objects must be initialized by Ninjascript.Load method in NinjaTrader \"OnStateChanged.Load\" event driven method.",
                ["NinjascriptConfigureException"] = "The Ninjascript objects must be configured by Ninjascript.Builder class in NinjaTrader \"OnStateChanged.Configure\" event driven method.",
                ["NinjaTraderNinjaScriptNullReferenceException"] = "the Ninjatrader.NinjaScript has not been loaded yet. \"SetDefault\" method must be called when the Ninjatrader.NinjaScript is loaded.",

            };
        }

        private static string GetString(string s, [CallerMemberName]string method = null, [CallerLineNumber]int line = 0)
        {
            if (Errors.ContainsKey(s))
                return $"{s}: Method: {method} - Line: {line} - Message: {Errors[s]}";
            else
                throw new KeyNotFoundException(s);
        }
    }
}
