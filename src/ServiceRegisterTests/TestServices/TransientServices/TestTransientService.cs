using ServiceRegister;
using System;

namespace ServiceRegisterTests.TestServices.TransientServices
{
    [TransientService(implementationAsSelf: true)]
    public class TestTransientService : ITestTransientService
    {
        public TestTransientService()
        {
            ServiceId = Guid.NewGuid();
        }

        public Guid ServiceId { get; private set; }
    }
}
