using System;
using NUnit.Framework;
using Moq;
using Nano.Engine.Sys;
using Microsoft.Xna.Framework;

namespace NanoTests.Engine.Sys
{
    [TestFixture]
    public class TestServiceLocator
    {
        [Test]
        public void TestConstructLocator()
        {
            IServices services = ServiceLocator.Instance;
            Assert.That(services, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestAddServiceWithNoContainerSet()
        {
            ServiceLocator.Instance.SetServiceContainer(null);
            var testService = new Mock<ITestServiceA>();
            IServices services = ServiceLocator.Services;
            services.AddService<ITestServiceA>(testService.Object);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestAddServiceWithNoContainerSet2()
        {
            ServiceLocator.Instance.SetServiceContainer(null);
            var testService = new Mock<ITestServiceB>();
            IServices services = ServiceLocator.Services;
            services.AddService(typeof(ITestServiceB),testService.Object);
        }

        [Test]
        public void TestAddAndGetService()
        {
            GameServiceContainer container = new GameServiceContainer();
            ServiceLocator.Instance.SetServiceContainer(container);

            var testService = new Mock<ITestServiceC>();
            IServices services = ServiceLocator.Services;
            services.AddService<ITestServiceC>(testService.Object);

            Assert.That(services.GetService<ITestServiceC>(), Is.EqualTo(testService.Object));
        }

        [Test]
        public void TestAddAndGetService2()
        {
            GameServiceContainer container = new GameServiceContainer();
            ServiceLocator.Instance.SetServiceContainer(container);

            var testService = new Mock<ITestServiceD>();
            IServices services = ServiceLocator.Services;
            services.AddService(typeof(ITestServiceD),testService.Object);

            Assert.That(services.GetService(typeof(ITestServiceD)), Is.EqualTo(testService.Object));
        }

        [Test]
        public void TestRemoveAddedService()
        {
            GameServiceContainer container = new GameServiceContainer();
            ServiceLocator.Instance.SetServiceContainer(container);

            var testService = new Mock<ITestServiceE>();
            IServices services = ServiceLocator.Services;
            services.AddService(typeof(ITestServiceE),testService.Object);

            Assert.That(services.GetService(typeof(ITestServiceE)), Is.EqualTo(testService.Object));

            services.RemoveService(typeof(ITestServiceE));

            Assert.That(services.GetService(typeof(ITestServiceE)), Is.EqualTo(null));
        }

        [Test]
        public void TestRemoveNonExistantService()
        {
            GameServiceContainer container = new GameServiceContainer();
            ServiceLocator.Instance.SetServiceContainer(container);
           
            IServices services = ServiceLocator.Services;
            services.RemoveService(typeof(ITestServiceF));

            Assert.That(services.GetService(typeof(ITestServiceF)), Is.EqualTo(null));
        }
    }

    public interface ITestServiceA{}
    public interface ITestServiceB{}
    public interface ITestServiceC{}
    public interface ITestServiceD{}
    public interface ITestServiceE{}
    public interface ITestServiceF{}

}

