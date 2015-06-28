using System;
using NUnit.Framework;
using Nano.Engine;
using Moq;
using Nano.Engine.Graphics;
using Nano.Engine.Graphics.Tileset;
using System.Collections.Generic;
using NanoTests.Engine.Sys;
using Nano.Engine.Cameras;
using Microsoft.Xna.Framework;

namespace NanoTests.Engine
{
    [TestFixture]
    public class TestTileMap
    {
        [Test]
        public void TestTileMapConstruction()
        {
            var sm = new Mock<ISpriteManager>();
            var ts = new Mock<ITileset>();
            var tsl = new List<ITileset>();
            tsl.Add(ts.Object);
            var layer = new MapLayer("test", 2, 2);
            var mll = new List<MapLayer>();
            mll.Add(layer);

            var map = new TileMap(sm.Object, TileMapType.Square, 32, 32, tsl, mll);

            Assert.That(map,Is.Not.Null); 
        }

        [Test]
        public void TestTileMapDraw()
        {
            var sm = new Mock<ISpriteManager>();
          
            var tex2D = new Mock<ITexture2D>();
            tex2D.SetupGet(f => f.Width).Returns(64);
            tex2D.SetupGet(f => f.Height).Returns(64);

            var ts = new Mock<ITileset>();
            ts.SetupGet( t => t.Texture).Returns(tex2D.Object);
            ts.SetupGet(t => t.Bounds)
                .Returns(
                new List<Rectangle>
                {
                    new Rectangle(0, 0, 32, 32),
                    new Rectangle(32, 0, 32, 32),
                    new Rectangle(0, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32)
                });

            var tsl = new List<ITileset>();
            tsl.Add(ts.Object);

            var layer = new MapLayer("test", 2, 2);
            for (int y = 0; y < layer.Height; y++)
                for (int x = 0; x < layer.Width; x++)
                    layer.SetTile(x, y, new TilesetTile(0, 0));


            var mll = new List<MapLayer>();
            mll.Add(layer);

            var map = new TileMap(sm.Object, TileMapType.Square, 32, 32, tsl, mll);
            var camera = new Mock<ICamera>();
            camera.SetupGet(m => m.ViewportRectangle).Returns(new Rectangle(0, 0, 100, 100));
            camera.SetupGet(m => m.Position).Returns(new Vector2(50, 50));
            camera.SetupGet(m => m.Zoom).Returns(1.0f);

            map.Draw(camera.Object);

            sm.Verify(m => m.DrawTexture2D(
                It.IsAny<ITexture2D>(),
                It.IsAny<Rectangle>(),
                It.IsAny<Rectangle>()
            ),Times.Exactly(4));
        }
    }
}

