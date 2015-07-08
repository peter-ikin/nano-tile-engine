using System;
using NUnit.Framework;
using Nano.Engine.Graphics.Map;
using Moq;
using Nano.Engine.Sys;

namespace NanoTests.Engine.Graphics.Map
{
    [TestFixture]
    public class TestBspMapGenerator
    {
        [Test]
        public void TestConstructBspMapGenerator()
        {
            IRandom random = new CMWC4096(16784);
            IMapGenerator generator = new BspMapGenerator(100, 100, random);
            Assert.That(generator, Is.Not.Null);
        }


    }
}

