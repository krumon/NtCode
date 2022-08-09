using NtConnect;
using NtCore;
using System;
using System.Threading;

namespace ConsoleApp
{
    internal class Program
    {

        static SimulatorTests simulatorTests = new SimulatorTests();
        static SessionTimeTests sessionTimeTests = new SessionTimeTests();

        static void Main(string[] args)
        {
            sessionTimeTests.Run();
        }

    }
}
