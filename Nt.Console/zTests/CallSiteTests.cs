using ConsoleApp;
using ConsoleApp.Internal;

namespace ConsoleApp
{
    internal class CallSiteTests
    {
        private CallSiteTest CallSiteTest;

        public NinjascriptServiceDescriptor[] Descriptors => CallSiteTest.Descriptors;

        public CallSiteTests()
        {
            CallSiteTest = new CallSiteTest();
        }

    }
}
