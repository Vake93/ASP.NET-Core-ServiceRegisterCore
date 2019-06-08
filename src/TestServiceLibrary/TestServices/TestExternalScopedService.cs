using ServiceRegister;
using System;

namespace TestServiceLibrary.TestServices
{
    [ScopedService]
    public class TestExternalScopedService : ITestExternalScopedService
    {
        public TestExternalScopedService()
        {
            ServiceId = Guid.NewGuid();
        }

        public Guid ServiceId { get; private set; }
    }
}
