using Nt.Core.Services;
using Nt.Core.Services.Internal;

namespace Nt.Core.Tests
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
