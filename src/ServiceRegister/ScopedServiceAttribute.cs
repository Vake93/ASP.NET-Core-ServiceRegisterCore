using Microsoft.Extensions.DependencyInjection;
using System;

namespace ServiceRegister
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class ScopedServiceAttribute : Attribute, IServiceAttribute
    {
        public ScopedServiceAttribute(bool implementationAsSelf = false)
        {
            ImplementationAsSelf = implementationAsSelf;
        }

        public bool ImplementationAsSelf { get; }

        void IServiceAttribute.AddService(
            IServiceCollection services,
            Type serviceType,
            Type implementationType)
        {
            ServiceCollectionServiceExtensions.AddScoped(
                services,
                serviceType,
                implementationType);
        }

        void IServiceAttribute.AddService(
            IServiceCollection services,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory)
        {
            ServiceCollectionServiceExtensions.AddScoped(
                services,
                serviceType,
                implementationFactory);
        }
    }
}
