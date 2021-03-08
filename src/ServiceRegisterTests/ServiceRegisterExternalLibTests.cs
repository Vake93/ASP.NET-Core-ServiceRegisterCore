using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ServiceRegister;
using System;
using System.Reflection;
using TestServiceLibrary.Documents;
using TestServiceLibrary.GenericService;
using TestServiceLibrary.Module;
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
                Assembly.GetAssembly(typeof(TestServiceModule)));

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [Test]
        public void TestExternalScopedService()
        {
            var testExternalScopedServiceAsSelf = serviceProvider.GetService<TestExternalScopedService>();
            var testExternalScopedServiceAsInterface = serviceProvider.GetService<ITestExternalScopedService>();

            Assert.IsNull(testExternalScopedServiceAsSelf);
            Assert.IsNotNull(testExternalScopedServiceAsInterface);

            using var scope = serviceProvider.CreateScope();

            var newExternalScopedService = scope.ServiceProvider.GetService<ITestExternalScopedService>();

            Assert.IsNotNull(newExternalScopedService);

            Assert.AreNotSame(testExternalScopedServiceAsInterface, newExternalScopedService);
        }

        [Test]
        public void TestGenericService()
        {
            var genericService1AsSelf = serviceProvider.GetService<DocumentOpertation<TestDocumentOne>>();
            var genericService1AsInterface = serviceProvider.GetService<IDocumentOpertation<TestDocumentOne>>();

            Assert.IsNull(genericService1AsSelf);
            Assert.IsNotNull(genericService1AsInterface);

            var genericService2AsSelf = serviceProvider.GetService<DocumentOpertation<TestDocumentTwo>>();
            var genericService2AsInterface = serviceProvider.GetService<IDocumentOpertation<TestDocumentTwo>>();

            Assert.IsNull(genericService2AsSelf);
            Assert.IsNotNull(genericService2AsInterface);


            var genericService3AsInterface = serviceProvider.GetService<IDocumentOpertation<TestDocumentTwo>>();

            Assert.AreEqual(genericService2AsInterface, genericService3AsInterface);
        }
    }
}
