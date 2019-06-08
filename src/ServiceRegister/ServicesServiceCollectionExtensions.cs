using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace ServiceRegister
{
    public static class ServicesServiceCollectionExtensions
    {
        private static readonly Type[] DefaultImplementationExcludes = new Type[]
        {
            typeof(IDisposable)
        };

        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetCallingAssembly();

            AddApplicationServices(services, assembly);
        }

        public static void ConfigureApplicationServices(
            this IServiceCollection services,
            params Assembly[] scanAssemblies)
        {
            foreach (var assembly in scanAssemblies)
            {
                AddApplicationServices(services, assembly);
            } 
        }

        private static void AddApplicationServices(IServiceCollection services, Assembly assembly)
        {
            AddToServiceCollection<SingletonServiceAttribute>(assembly, services);

            AddToServiceCollection<ScopedServiceAttribute>(assembly, services);

            AddToServiceCollection<TransientServiceAttribute>(assembly, services);
        }

        private static void AddToServiceCollection<T>(Assembly assembly, IServiceCollection services)
            where T : IServiceAttribute
        {
            var serviceTypes = assembly
                .GetTypes()
                .Where(t => t.IsPublic && t.IsClass && t.GetCustomAttribute(typeof(T)) != null);

            foreach (var serviceType in serviceTypes)
            {
                var serviceAttribute = serviceType.GetCustomAttribute(typeof(T)) as IServiceAttribute;

                var implementationExcludes = serviceAttribute.ImplementationExcludes ??
                    DefaultImplementationExcludes;

                var serviceTypeInfo = serviceType.GetTypeInfo();
                var serviceTypeInterfaces = serviceTypeInfo
                    .ImplementedInterfaces
                    .Where(i => !implementationExcludes.Contains(i))
                    .ToArray();

                if (serviceTypeInterfaces.Length == 0)
                {
                    //No Interfaces, so gets DI'ed as class type
                    serviceAttribute.AddService(services, serviceType, serviceType);
                }
                else if (serviceTypeInterfaces.Length == 1)
                {
                    if (serviceAttribute.ImplementationAsSelf)
                    {
                        //Gets DI'ed as class type and as the interface type
                        serviceAttribute.AddService(services, serviceType, serviceType);

                        serviceAttribute.AddService(
                            services,
                            serviceTypeInterfaces[0],
                            x => x.GetRequiredService(serviceType));
                    }
                    else
                    {
                        //1 interface, gets DI'ed as interface type
                        serviceAttribute.AddService(services, serviceTypeInterfaces[0], serviceType);
                    }
                }
                else
                {
                    //multiple interfaces, since we need all interfaces to return the
                    //same instance we create the service as class type
                    //and forward the interfaces to class type instance
                    serviceAttribute.AddService(services, serviceType, serviceType);

                    foreach (var serviceTypeInterface in serviceTypeInterfaces)
                    {
                        serviceAttribute.AddService(
                            services,
                            serviceTypeInterface,
                            x => x.GetRequiredService(serviceType));
                    }
                }
            }
        }
    }
}
