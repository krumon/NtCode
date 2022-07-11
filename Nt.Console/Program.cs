using NtCore;
using System;

namespace NtConsole
{
    internal class Program
    {

        static void Main(string[] args)
        {
            foreach(var timeZone in SpecificTradingHours.Asian.ToArray())
                Console.WriteLine(timeZone.ToTradingHours().ToString());

            Console.ReadKey();
        }
    }
}
