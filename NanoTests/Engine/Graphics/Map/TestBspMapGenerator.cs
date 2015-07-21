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
            IMapGenerator generator = new BspMapGenerator(100, 100, random, 2);
            Assert.That(generator, Is.Not.Null);
        }

        [Test]
        public void TestGenerateLayersReturnsListOfMapLayers()
        {
            IRandom random = new CMWC4096(16784);
            IMapGenerator generator = new BspMapGenerator(100, 100, random, 2);
            var layers = generator.GenerateLayers();
            Assert.That(layers, Is.Not.Null);
        }

        [Test]
        public void TestGenerateLayersReturnsNonEmptyListOfMapLayers()
        {
            IRandom random = new CMWC4096(16784);
            IMapGenerator generator = new BspMapGenerator(100, 100, random, 2);
            var layers = generator.GenerateLayers();
            Assert.That(layers.Count, Is.GreaterThan(0));
        }

        //TODO fix problem with non rectangular map sizes getting stuck
        [TestCase(100,100,TestName = "Generate layers of dimension 100,100")]
        //[TestCase(100,1000,TestName = "Generate layers of dimension 10,100")]
        //[TestCase(1000,100,TestName = "Generate layers of dimension 100,10")]
        //[TestCase(300,1230,TestName = "Generate layers of dimension 30,123")]
        //[TestCase(1230,300,TestName = "Generate layers of dimension 123,30")]
        public void TestGenerateLayersReturnsCorrectlyDimensionedMapLayers(int width, int height)
        {
            IRandom random = new CMWC4096(16784);
            IMapGenerator generator = new BspMapGenerator(width, height, random, 2);
            var layers = generator.GenerateLayers();
            foreach(MapLayer layer in layers)
            {
                Assert.That(layer.Width, Is.EqualTo(width));
                Assert.That(layer.Height, Is.EqualTo(height));
            }
        }
    }
}

