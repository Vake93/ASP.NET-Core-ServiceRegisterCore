using ServiceRegister;
using System;

namespace ServiceRegisterTests.TestServices.TransientServices
{
    [TransientService(
        implementationExcludes: typeof(IEquatable<TestTransientService>), 
        implementationAsSelf: true)]
    public class TestTransientService : ITestTransientService, IEquatable<TestTransientService>
    {
        public TestTransientService()
        {
            ServiceId = Guid.NewGuid();
        }

        public Guid ServiceId { get; private set; }

        public bool Equals(TestTransientService other)
        {
            return (other?.ServiceId ?? Guid.Empty) == ServiceId;
        }
    }
}
