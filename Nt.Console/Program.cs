using NtCore;
using System;

namespace NtConsole
{
    internal class Program
    {

        static void Main(string[] args)
        {
            foreach(var timeZone in TradingSession.Asian.ToArray())
                Console.WriteLine(timeZone.ToSessionHours().ToString());

            Console.ReadKey();
        }
    }
}
