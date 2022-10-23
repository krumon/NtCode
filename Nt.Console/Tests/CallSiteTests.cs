using Nt.Core.Ninjascript;
using Nt.Core.Services;
using Nt.Core.Services.Internal;

namespace ConsoleApp.Tests
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
