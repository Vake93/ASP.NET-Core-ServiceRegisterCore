using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ServiceRegister;
using ServiceRegisterTests.TestServices;
using System;

namespace Tests
{
    public class ServiceRegisterTests
    {
        private IServiceProvider serviceProvider;

        [SetUp]
        public void Setup()
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
    }
}