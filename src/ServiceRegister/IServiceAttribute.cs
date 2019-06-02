using Microsoft.Extensions.DependencyInjection;
using System;

namespace ServiceRegister
{
    internal interface IServiceAttribute
    {
        bool ImplementationAsSelf { get; }

        void AddService(
            IServiceCollection services,
            Type serviceType,
            Type implementationType);

        void AddService(
            IServiceCollection services,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory);
    }
}
