using NtCore;
using System;

namespace NtConsole
{
    internal class Program
    {

        static void Main(string[] args)
        {
            foreach(var timeZone in TradingHoursType.Asian.ToArray())
                Console.WriteLine(timeZone.ToTradingTimeZone().ToString());

            Console.ReadKey();
        }
    }
}
