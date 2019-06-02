using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ServiceRegister;
using ServiceRegisterTests.TestServices.ScopedServices;
using ServiceRegisterTests.TestServices.SingletonServices;
using ServiceRegisterTests.TestServices.TransientServices;
using System;
using System.Linq;
using System.Reflection;

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
            var testTransientServiceAsSelf = serviceProvider.GetService<TestTransientService>();
            var testTransientServiceAsInterface = serviceProvider.GetService<ITestTransientService>();

            Assert.IsNotNull(testTransientServiceAsSelf);
            Assert.IsNotNull(testTransientServiceAsInterface);

            Assert.AreNotSame(testTransientServiceAsInterface, testTransientServiceAsSelf);
        }

        [Test]
        public void TestScopedServices()
        {
            var testScopedServiceAsSelf = serviceProvider.GetService<TestScopedService>();
            var testScopedServiceAsInterface = serviceProvider.GetService<ITestScopedService>();

            Assert.IsNotNull(testScopedServiceAsSelf);
            Assert.IsNotNull(testScopedServiceAsInterface);

            Assert.AreSame(testScopedServiceAsInterface, testScopedServiceAsSelf);

            using (var scope = serviceProvider.CreateScope())
            {
                var newScopeScopedService = scope.ServiceProvider.GetService<ITestScopedService>();

                Assert.IsNotNull(newScopeScopedService);

                Assert.AreNotSame(testScopedServiceAsSelf, newScopeScopedService);
            }
        }

        [Test]
        public void TestSingletonService()
        {
            var testSingletonServiceAsSelf = serviceProvider.GetService<TestSingletonService>();
            var testSingletonServiceAsInterface = serviceProvider.GetService<ITestSingletonService>();

            Assert.IsNotNull(testSingletonServiceAsSelf);
            Assert.IsNotNull(testSingletonServiceAsInterface);

            Assert.AreSame(testSingletonServiceAsInterface, testSingletonServiceAsSelf);

            using (var scope = serviceProvider.CreateScope())
            {
                var newScopeSingletonService = scope.ServiceProvider.GetService<ITestSingletonService>();

                Assert.IsNotNull(newScopeSingletonService);

                Assert.AreSame(testSingletonServiceAsSelf, newScopeSingletonService);
            }
        }

        [Test]
        public void TestImplementationExcludesImplicit()
        {
            var testScopedServiceTypeInfo = typeof(TestScopedService).GetTypeInfo();

            var implementsDisposable = testScopedServiceTypeInfo
                .ImplementedInterfaces
                .Contains(typeof(IDisposable));

            Assert.IsTrue(implementsDisposable);

            var testDisposable = serviceProvider.GetService<IDisposable>();

            Assert.IsNull(testDisposable);
        }

        [Test]
        public void TestImplementationExcludesExplicit()
        {
            var transientServiceTypeInfo = typeof(TestTransientService).GetTypeInfo();

            var implementsEquatable = transientServiceTypeInfo
                .ImplementedInterfaces
                .Contains(typeof(IEquatable<TestTransientService>));

            Assert.IsTrue(implementsEquatable);

            var testEquatable = serviceProvider.GetService<IEquatable<TestTransientService>>();

            Assert.IsNull(testEquatable);
        }
    }
}