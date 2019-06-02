using System;

namespace ServiceRegisterTests.TestServices.TransientServices
{
    public interface ITestTransientService
    {
        Guid ServiceId { get; }
    }
}