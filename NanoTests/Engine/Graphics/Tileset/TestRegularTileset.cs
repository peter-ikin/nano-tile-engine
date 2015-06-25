using System;
using NUnit.Framework;
using Nano.Engine.Graphics.Tileset;
using Microsoft.Xna.Framework.Graphics;
using Moq;
using Nano.Engine.Graphics;
using Microsoft.Xna.Framework;

namespace NanoTests.Engine.Graphics.Tileset
{
    [TestFixture]
    public class TestRegularTileset
    {
        [Test]
        public void TestRegularTilesetConstruction()
        {
            var tex2D = new Mock<ITexture2D>();
            tex2D.SetupGet(f => f.Width).Returns(200);
            tex2D.SetupGet(f => f.Height).Returns(200);

            ITileset tset = new RegularTileset("testTset", tex2D.Object, 2, 2, 10, 10);

            Assert.That(tset,Is.Not.Null);
        }

        [TestCase(2,2,10,10,4, TestName="Test size of 2,2,10,10 tileset is 4")]
        [TestCase(5,5,10,10,25, TestName="Test size of 5,5,10,10 tileset is 25")]
        [TestCase(2,5,100,200,10, TestName="Test size of 2,5,100,200 tileset is 10")]
        [TestCase(5,2,15,12,10, TestName="Test size of 5,2,200,100 tileset is 10")]
        [TestCase(12,13,5,5,156, TestName="Test size of 5,2,200,100 tileset is 10")]
        public void TestRegularTilesetSize(int colCount, int rowCount,int tileWidth, int tileHeight, int size)
        {
            var tex2D = new Mock<ITexture2D>();
            tex2D.SetupGet(f => f.Width).Returns(2000);
            tex2D.SetupGet(f => f.Height).Returns(2000);

            ITileset tset = new RegularTileset("testTset", tex2D.Object, colCount,rowCount,tileWidth,tileHeight);

            Assert.That(tset.Size,Is.EqualTo(size));
        }

        [Test]
        public void TestRegularTilesetBounds()
        {
            var tex2D = new Mock<ITexture2D>();
            tex2D.SetupGet(f => f.Width).Returns(200);
            tex2D.SetupGet(f => f.Height).Returns(200);

            ITileset tset = new RegularTileset("testTset", tex2D.Object, 2, 2, 100, 100);

            Assert.That(tset.Bounds[0], Is.EqualTo(new Rectangle(0, 0, 100, 100)));
            Assert.That(tset.Bounds[1], Is.EqualTo(new Rectangle(100, 0, 100, 100)));
            Assert.That(tset.Bounds[2], Is.EqualTo(new Rectangle(0, 100, 100, 100)));
            Assert.That(tset.Bounds[3], Is.EqualTo(new Rectangle(100, 100, 100, 100)));
        }

        [Test]
        public void TestRegularTilesetDefaultOffset()
        {
            var tex2D = new Mock<ITexture2D>();
            tex2D.SetupGet(f => f.Width).Returns(20);
            tex2D.SetupGet(f => f.Height).Returns(20);

            ITileset tset = new RegularTileset("testTset", tex2D.Object, 2, 2, 10, 10);

            Assert.That(tset.Offset.X, Is.EqualTo(0));
            Assert.That(tset.Offset.Y, Is.EqualTo(0));
        }

        [Test]
        public void TestRegularTilesetThrowsWhenTextureTooSmall()
        {
            var tex2D = new Mock<ITexture2D>();
            tex2D.SetupGet(f => f.Width).Returns(50);
            tex2D.SetupGet(f => f.Height).Returns(50);

            Assert.That(() => new RegularTileset("testTset", tex2D.Object, 2,2,100,100),
                Throws.Exception
                .TypeOf<ArgumentException>());
            
        }

        [Test]
        public void TestRegularTilesetOKWhenTextureExactSize()
        {
            var tex2D = new Mock<ITexture2D>();
            tex2D.SetupGet(f => f.Width).Returns(200);
            tex2D.SetupGet(f => f.Height).Returns(200);

            var tset = new RegularTileset("testTset", tex2D.Object, 2, 2, 100, 100);
            Assert.That(tset, Is.Not.Null);
        }
    }
}

