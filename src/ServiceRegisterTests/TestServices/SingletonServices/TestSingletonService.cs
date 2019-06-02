using ServiceRegister;
using System;

namespace ServiceRegisterTests.TestServices.SingletonServices
{
    [SingletonService(implementationAsSelf: true)]
    public class TestSingletonService : ITestSingletonService
    {
        public TestSingletonService()
        {
            ServiceId = Guid.NewGuid();
            ServiceId = Guid.NewGuid();
        }

        public Guid ServiceId { get; private set; }

        public Guid TestKey { get; set; }
    }
}
