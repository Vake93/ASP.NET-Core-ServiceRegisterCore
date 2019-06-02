﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace ServiceRegister
{
    public static class ServicesServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetCallingAssembly();

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
                var serviceTypeInfo = serviceType.GetTypeInfo();
                var serviceTypeInterfaces = serviceTypeInfo
                    .ImplementedInterfaces
                    .Where(i => i != typeof(IDisposable))
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
                        serviceAttribute.AddService(services, serviceType, serviceType);
                    }

                    //1 interface, so gets DI'ed as interface type
                    serviceAttribute.AddService(services, serviceTypeInterfaces[0], serviceType);
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
