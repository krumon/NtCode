using NtCore;
using System;

namespace NtConsole
{
    internal class Program
    {

        static void Main(string[] args)
        {
            TimeTarget time = new TimeTarget();
            Console.WriteLine(time.GetTime().ToString());

            //Console.WriteLine("Hello world");

            Console.ReadKey();
        }
    }
}
