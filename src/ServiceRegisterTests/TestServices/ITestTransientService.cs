using System;

namespace ServiceRegisterTests.TestServices
{
    public interface ITestTransientService
    {
        Guid ServiceId { get; }
    }
}