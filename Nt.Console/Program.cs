using Nt.Connect;
using Nt.Core;
using System;
using System.Threading;

namespace ConsoleApp
{
    internal class Program
    {

        static SimulatorTests simulatorTests = new SimulatorTests();
        static SessionTimeTests sessionTimeTests = new SessionTimeTests();
        static SessionHoursTests sessionHoursTests = new SessionHoursTests();

        static void Main(string[] args)
        {
            //sessionTimeTests.Run();
            sessionHoursTests.Run();
        }

    }
}
