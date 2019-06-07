using ServiceRegister;
using System;

namespace ServiceRegisterTests.TestServices.ScopedServices
{
    [ScopedService(implementationAsSelf: true)]
    public class TestScopedService : ITestScopedService, IDisposable
    {
        public TestScopedService()
        {
            ServiceId = Guid.NewGuid();
        }

        public Guid ServiceId { get; private set; }

        public void Dispose()
        {
            ServiceId = Guid.Empty;
        }
    }
}
