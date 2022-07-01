using NtCore;
using System;

namespace NtConsole
{
    internal class Program
    {

        static void Main(string[] args)
        {
            foreach(var timeZone in TradingTimeZoneType.Asian.ToArray())
                Console.WriteLine(timeZone.ToTradingTimeZone().ToString());

            Console.ReadKey();
        }
    }
}
