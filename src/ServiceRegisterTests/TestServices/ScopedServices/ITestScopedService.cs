using System;

namespace ServiceRegisterTests.TestServices.ScopedServices
{
    public interface ITestScopedService
    {
        Guid ServiceId { get; }
    }
}