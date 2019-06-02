using System;

namespace ServiceRegisterTests.TestServices.SingletonServices
{
    public interface ITestSingletonService
    {
        Guid ServiceId { get; }
        Guid TestKey { get; set; }
    }
}