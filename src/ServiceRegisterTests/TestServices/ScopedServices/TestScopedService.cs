using ServiceRegister;
using System;

namespace ServiceRegisterTests.TestServices.ScopedServices
{
    [ScopedService(implementationAsSelf: true)]
    public class TestScopedService : ITestScopedService
    {
        public TestScopedService()
        {
            ServiceId = Guid.NewGuid();
        }

        public Guid ServiceId { get; private set; }
    }
}
