using System;
using NUnit.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nano.Engine.Graphics.Sprites;
using NUnit.Framework.Constraints;
using Moq;
using Nano.Engine.Graphics;

namespace NanoTests.Engine.Graphics.Sprites
{
    [TestFixture]
    public class TestBasicSprite
    {
        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void TestConstructBasicSprite()
        {
            var tex2D = new Mock<ITexture2D>();
            var sprite = new BasicSprite(tex2D.Object);
            Assert.That(sprite,Is.Not.Null);
        }

        [Test]
        public void TestConstructBasicSprite()
        {
            Assert.That(() => new BasicSprite(null),
                Throws.Exception
                .TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestUpdate()
        {
            Assert.Fail();
        }

        [Test]
        public void TestGetSetSpriteManager()
        {
            Assert.Fail();
        }

        [Test]
        public void TestDrawBasicSprite()
        {
            Assert.Fail();
        }

        [Test]
        public void TestPosition()
        {
            Assert.Fail();
        }

        [Test]
        public void TestWidth()
        {
            Assert.Fail();
        }

        [Test]
        public void TestHeight()
        {
            Assert.Fail();
        }

        [Test]
        public void TestRotation()
        {
            Assert.Fail();
        }

        [Test]
        public void TestRotationOrigin()
        {
            Assert.Fail();
        }

        [Test]
        public void TestSourceRectangle()
        {
            Assert.Fail();
        }
    }
}

