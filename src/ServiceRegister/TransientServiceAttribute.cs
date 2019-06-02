using Microsoft.Extensions.DependencyInjection;
using System;

namespace ServiceRegister
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class TransientServiceAttribute : Attribute, IServiceAttribute
    {
        public TransientServiceAttribute(bool implementationAsSelf = false)
        {
            ImplementationAsSelf = implementationAsSelf;
        }

        public bool ImplementationAsSelf { get; }

        void IServiceAttribute.AddService(
            IServiceCollection services,
            Type serviceType,
            Type implementationType)
        {
            ServiceCollectionServiceExtensions.AddTransient(
                services, 
                serviceType, 
                implementationType);
        }

        void IServiceAttribute.AddService(
            IServiceCollection services,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory)
        {
            ServiceCollectionServiceExtensions.AddTransient(
                services,
                serviceType,
                implementationFactory);
        }
    }
}
