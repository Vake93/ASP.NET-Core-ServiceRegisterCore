using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ServiceRegister;
using System;
using System.Reflection;
using TestServiceLibrary.TestServices;

namespace ServiceRegisterTests
{
    public class ServiceRegisterExternalLibTests
    {
        private readonly IServiceProvider serviceProvider;

        public ServiceRegisterExternalLibTests()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.ConfigureApplicationServices(
                Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(ITestExternalScopedService)));

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [Test]
        public void TestExternalScopedService()
        {
            var testExternalScopedServiceAsSelf = serviceProvider.GetService<TestExternalScopedService>();
            var testExternalScopedServiceAsInterface = serviceProvider.GetService<ITestExternalScopedService>();

            Assert.IsNull(testExternalScopedServiceAsSelf);
            Assert.IsNotNull(testExternalScopedServiceAsInterface);

            using (var scope = serviceProvider.CreateScope())
            {
                var newExternalScopedService = scope.ServiceProvider.GetService<ITestExternalScopedService>();

                Assert.IsNotNull(newExternalScopedService);

                Assert.AreNotSame(testExternalScopedServiceAsInterface, newExternalScopedService);
            }
        }
    }
}
