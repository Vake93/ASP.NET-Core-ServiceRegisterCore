using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ServiceRegister;
using ServiceRegisterTests.TestServices.ScopedServices;
using ServiceRegisterTests.TestServices.SingletonServices;
using ServiceRegisterTests.TestServices.TransientServices;
using System;

namespace Tests
{
    public class ServiceRegisterTests
    {
        private readonly IServiceProvider serviceProvider;

        public ServiceRegisterTests()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.ConfigureApplicationServices();

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [Test]
        public void TestTransientService()
        {
            var testTransientServiceAsSelf = serviceProvider.GetRequiredService<TestTransientService>();
            var testTransientServiceAsInterface = serviceProvider.GetRequiredService<ITestTransientService>();

            Assert.IsNotNull(testTransientServiceAsSelf);
            Assert.IsNotNull(testTransientServiceAsInterface);

            Assert.AreNotSame(testTransientServiceAsInterface, testTransientServiceAsSelf);
        }

        [Test]
        public void TestScopedServices()
        {
            var testScopedServiceAsSelf = serviceProvider.GetRequiredService<TestScopedService>();
            var testScopedServiceAsInterface = serviceProvider.GetRequiredService<ITestScopedService>();

            Assert.IsNotNull(testScopedServiceAsSelf);
            Assert.IsNotNull(testScopedServiceAsInterface);

            Assert.AreSame(testScopedServiceAsInterface, testScopedServiceAsSelf);

            using (var scope = serviceProvider.CreateScope())
            {
                var newScopeScopedService = scope.ServiceProvider.GetRequiredService<ITestScopedService>();

                Assert.IsNotNull(newScopeScopedService);

                Assert.AreNotSame(testScopedServiceAsSelf, newScopeScopedService);
            }
        }

        [Test]
        public void TestSingletonService()
        {
            var testSingletonServiceAsSelf = serviceProvider.GetRequiredService<TestSingletonService>();
            var testSingletonServiceAsInterface = serviceProvider.GetRequiredService<ITestSingletonService>();

            Assert.IsNotNull(testSingletonServiceAsSelf);
            Assert.IsNotNull(testSingletonServiceAsInterface);

            Assert.AreSame(testSingletonServiceAsInterface, testSingletonServiceAsSelf);

            using (var scope = serviceProvider.CreateScope())
            {
                var newScopeSingletonService = scope.ServiceProvider.GetRequiredService<ITestSingletonService>();

                Assert.IsNotNull(newScopeSingletonService);

                Assert.AreSame(testSingletonServiceAsSelf, newScopeSingletonService);
            }
        }
    }
}