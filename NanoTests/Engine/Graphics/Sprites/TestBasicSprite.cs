using System;
using NUnit.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nano.Engine.Graphics.Sprites;
using NUnit.Framework.Constraints;
using Moq;
using Nano.Engine.Graphics;
using Microsoft.Xna.Framework;

namespace NanoTests.Engine.Graphics.Sprites
{
    [TestFixture]
    public class TestBasicSprite
    {
        Rectangle m_TestRectA;
        Rectangle m_TestRectB;

        [SetUp]
        public void Init()
        {
            m_TestRectA = new Rectangle(0, 0, 10, 10);
            m_TestRectB = new Rectangle(3, 5, 12, 43);
        }

        [Test]
        public void TestConstructBasicSprite()
        {
            var tex2D = new Mock<ITexture2D>();
            var sprite = new BasicSprite(tex2D.Object);
            Assert.That(sprite,Is.Not.Null);
        }

        [Test]
        public void TestConstructBasicSpriteNullTexture2D()
        {
            Assert.That(() => new BasicSprite(null),
                Throws.Exception
                .TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestConstructBasicSpriteWithSourceRect()
        {
            var tex2D = new Mock<ITexture2D>();
            var sprite = new BasicSprite(tex2D.Object, m_TestRectA);
            Assert.That(sprite,Is.Not.Null);
        }

        [Test]
        public void TestConstructBasicSpriteNullTexture2DWithSourceRect()
        {
            Assert.That(() => new BasicSprite(null, m_TestRectB),
                Throws.Exception
                .TypeOf<ArgumentNullException>());
        }

        [TestCase( (float)(0.5*Math.PI), (float)(0.5*Math.PI),TestName="rotate 1/2 PI then update")]
        [TestCase( (float)(1.5*Math.PI), (float)(-0.5*Math.PI),TestName="rotate 1 1/2 PI then update")]
        [TestCase( (float)(2.0*Math.PI), 0f, TestName="rotate 2 PI then update")]
        [TestCase( (float)(2.25*Math.PI), (float)(0.25*Math.PI),TestName="rotate 2 1/4 PI then update")]
        [TestCase( (float)(-0.5*Math.PI), (float)(-0.5*Math.PI),TestName="rotate -1/2 PI then update")]
        [TestCase( (float)(-2.5*Math.PI), (float)(-0.5*Math.PI),TestName="rotate -2 1/2 PI then update")]
        [TestCase( (float)(-2.0*Math.PI), 0f, TestName="rotate -2 PI then update")]
        [TestCase( (float)(-1.5*Math.PI), (float)(0.5*Math.PI),TestName="rotate -1 1/2 PI then update")]
        [TestCase( (float)(-1.25*Math.PI), (float)(0.75*Math.PI),TestName="rotate -1 1/4 PI then update")]
        [TestCase( (float)(1.25*Math.PI), (float)(-0.75*Math.PI),TestName="rotate 1 1/4 PI then update")]
        public void TestUpdate(float rotation, float expectedResult)
        {
            var tex2D = new Mock<ITexture2D>();
            var sprite = new BasicSprite(tex2D.Object, m_TestRectA);
            sprite.Rotation = rotation;
            sprite.Update(new GameTime());
            Assert.That(sprite.Rotation, Is.EqualTo(expectedResult).Within(0.00001));
        }

        [Test]
        public void TestGetSetSpriteManager()
        {
            var tex2D = new Mock<ITexture2D>();
            var mgr = new Mock<ISpriteManager>();

            var sprite = new BasicSprite(tex2D.Object, m_TestRectA);
            sprite.SpriteManager = mgr.Object;

            Assert.That(sprite.SpriteManager, Is.EqualTo(mgr.Object));
        }

        [Test]
        public void TestDrawBasicSpriteUsesCorrectTexture()
        {
            var tex2D = new Mock<ITexture2D>();
            tex2D.SetupGet(f => f.Height).Returns(128);
            tex2D.SetupGet(f => f.Width).Returns(64);

            var mgr = new Mock<ISpriteManager>();

            var sprite = new BasicSprite(tex2D.Object, m_TestRectA);
            sprite.SpriteManager = mgr.Object;

            sprite.Draw();

            mgr.Verify(m => m.DrawTexture2D(
                It.Is<ITexture2D>(t=>t.Height==128 && t.Width==64),
                It.IsAny<Vector2>(),
                It.IsAny<Rectangle>(),
                It.IsAny<float>(),
                It.IsAny<Vector2>()
            ));
        }

        [Test]
        public void TestDrawBasicSpriteAtPosition()
        {
            var tex2D = new Mock<ITexture2D>();

            var mgr = new Mock<ISpriteManager>();

            var sprite = new BasicSprite(tex2D.Object, m_TestRectA);
            sprite.SpriteManager = mgr.Object;
            sprite.Position = new Vector2(12, 54);

            sprite.Draw();

            mgr.Verify(m => m.DrawTexture2D(
                It.IsAny<ITexture2D>(),
                It.Is<Vector2>( f => f.X == 12 && f.Y == 54),
                It.IsAny<Rectangle>(),
                It.IsAny<float>(),
                It.IsAny<Vector2>()
            ));
        }

        [Test]
        public void TestDrawBasicSpriteWithSourceRect()
        {
            var tex2D = new Mock<ITexture2D>();

            var mgr = new Mock<ISpriteManager>();

            var sprite = new BasicSprite(tex2D.Object, m_TestRectB);
            sprite.SpriteManager = mgr.Object;

            sprite.Draw();

            mgr.Verify(m => m.DrawTexture2D(
                It.IsAny<ITexture2D>(),
                It.IsAny<Vector2>(),
                It.Is<Rectangle>(r => r.X ==3 && r.Y == 5 && r.Width == 12 && r.Height == 43),
                It.IsAny<float>(),
                It.IsAny<Vector2>()
            ));
        }

        [Test]
        public void TestDrawBasicSpriteWithRotation()
        {
            var tex2D = new Mock<ITexture2D>();

            var mgr = new Mock<ISpriteManager>();
            var sprite = new BasicSprite(tex2D.Object, m_TestRectB);
            sprite.SpriteManager = mgr.Object;
            float angle = (float)(0.5 * Math.PI);
            sprite.Rotation = angle;

            sprite.Draw();

            mgr.Verify(m => m.DrawTexture2D(
                It.IsAny<ITexture2D>(),
                It.IsAny<Vector2>(),
                It.IsAny<Rectangle>(),
                It.Is<float>(r => r.Equals(angle)),
                It.IsAny<Vector2>()
            ));
        }
            
        [Test]
        public void TestRotationOriginDefault()
        {
            var tex2D = new Mock<ITexture2D>();
            tex2D.SetupGet(f => f.Height).Returns(128);
            tex2D.SetupGet(f => f.Width).Returns(64);

            var sprite = new BasicSprite(tex2D.Object);

            Assert.That(sprite.RotationOrigin.X,Is.EqualTo(32));
            Assert.That(sprite.RotationOrigin.Y, Is.EqualTo(64));
        }

        [Test]
        public void TestRotationOriginSet()
        {
            var tex2D = new Mock<ITexture2D>();
            tex2D.SetupGet(f => f.Height).Returns(128);
            tex2D.SetupGet(f => f.Width).Returns(64);

            var sprite = new BasicSprite(tex2D.Object);

            sprite.RotationOrigin = new Vector2(10, 10);

            Assert.That(sprite.RotationOrigin.X,Is.EqualTo(10));
            Assert.That(sprite.RotationOrigin.Y, Is.EqualTo(10));
        }

        [Test]
        public void TestSourceRectangleDefault()
        {
            var tex2D = new Mock<ITexture2D>();
            tex2D.SetupGet(f => f.Height).Returns(128);
            tex2D.SetupGet(f => f.Width).Returns(64);

            var sprite = new BasicSprite(tex2D.Object);

            Assert.That(sprite.SourceRectangle.X, Is.EqualTo(0));
            Assert.That(sprite.SourceRectangle.Y, Is.EqualTo(0));
            Assert.That(sprite.SourceRectangle.Width, Is.EqualTo(64));
            Assert.That(sprite.SourceRectangle.Height, Is.EqualTo(128));
        }

        [Test]
        public void TestSourceRectangleSet()
        {
            var tex2D = new Mock<ITexture2D>();
            tex2D.SetupGet(f => f.Height).Returns(128);
            tex2D.SetupGet(f => f.Width).Returns(64);

            var sprite = new BasicSprite(tex2D.Object, m_TestRectB);

            Assert.That(sprite.SourceRectangle.X, Is.EqualTo(3));
            Assert.That(sprite.SourceRectangle.Y, Is.EqualTo(5));
            Assert.That(sprite.SourceRectangle.Width, Is.EqualTo(12));
            Assert.That(sprite.SourceRectangle.Height, Is.EqualTo(43));
        }
    }
}

