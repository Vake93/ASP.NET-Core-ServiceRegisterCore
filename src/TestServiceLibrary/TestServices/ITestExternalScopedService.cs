using System;

namespace TestServiceLibrary.TestServices
{
    public interface ITestExternalScopedService
    {
        Guid ServiceId { get; }
    }
}