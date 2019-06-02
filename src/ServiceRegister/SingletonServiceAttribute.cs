using Microsoft.Extensions.DependencyInjection;
using System;

namespace ServiceRegister
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class SingletonServiceAttribute : Attribute, IServiceAttribute
    {
        public SingletonServiceAttribute(bool implementationAsSelf = false)
        {
            ImplementationAsSelf = implementationAsSelf;
        }

        public SingletonServiceAttribute(
            bool implementationAsSelf = false,
            params Type[] implementationExcludes)
            : this(implementationAsSelf)
        {
            ImplementationExcludes = implementationExcludes;
        }

        public bool ImplementationAsSelf { get; }

        public Type[] ImplementationExcludes { get; }

        void IServiceAttribute.AddService(
           IServiceCollection services,
           Type serviceType,
           Type implementationType)
        {
            ServiceCollectionServiceExtensions.AddSingleton(
                services,
                serviceType,
                implementationType);
        }

        void IServiceAttribute.AddService(
            IServiceCollection services,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory)
        {
            ServiceCollectionServiceExtensions.AddSingleton(
                services,
                serviceType,
                implementationFactory);
        }
    }
}
