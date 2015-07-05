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
using System.Reflection;
using System.Threading;
using Nano.Engine.Graphics.Map;

namespace NanoTests.Engine
{
    [TestFixture]
    public class TestTileMap
    {

        Mock<ISpriteManager> m_SpriteMgrMock;
        Mock<ITileset> m_TileSetMock;
        Mock<ITexture2D> m_Tex2D;

        [SetUp]
        public void SetUp()
        {
            //setup global mocks for sprite manager , texture and tileset
            //to be used for each test

            m_SpriteMgrMock = new Mock<ISpriteManager>();

            m_Tex2D = new Mock<ITexture2D>();
            m_Tex2D.SetupGet(f => f.Width).Returns(64);
            m_Tex2D.SetupGet(f => f.Height).Returns(64);

            m_TileSetMock = new Mock<ITileset>();

            m_TileSetMock.SetupGet( t => t.Texture).Returns(m_Tex2D.Object);
            m_TileSetMock.SetupGet(t => t.Bounds)
                .Returns(
                    new List<Rectangle>
                    {
                        new Rectangle(0, 0, 32, 32),
                        new Rectangle(32, 0, 32, 32),
                        new Rectangle(0, 32, 32, 32),
                        new Rectangle(32, 32, 32, 32)
                    });
        }

        /////////////////////////////////////////////////////////////////////////////////////
        /// Square Tile Map Tests
        /////////////////////////////////////////////////////////////////////////////////////

        [Test]
        public void TestTileMapConstruction_Square()
        {
            var tsl = new List<ITileset>();
            tsl.Add(m_TileSetMock.Object);
         
            var layer = new MapLayer("test", 2, 2);
            var mll = new List<MapLayer>();
            mll.Add(layer);

            var map = new TileMap(m_SpriteMgrMock.Object, TileMapType.Square, 32, 32, tsl, mll);

            Assert.That(map,Is.Not.Null); 
        }

        [Test]
        public void TestTileMapOrigin_Square()
        {
            var tsl = new List<ITileset>();
            tsl.Add(m_TileSetMock.Object);

            var layer = new MapLayer("test", 2, 2);
            var mll = new List<MapLayer>();
            mll.Add(layer);

            var map = new TileMap(m_SpriteMgrMock.Object, TileMapType.Square, 32, 32, tsl, mll);

            Assert.That(map.Origin,Is.EqualTo( new Point(0,0))); 
        }

        [Test]
        public void TestTileMapDrawIsCalledCorrectNumberOfTimes_Square()
        {
            var tsl = new List<ITileset>();
            tsl.Add(m_TileSetMock.Object);

            var layer = new MapLayer("test", 2, 2);
            for (int y = 0; y < layer.Height; y++)
                for (int x = 0; x < layer.Width; x++)
                    layer.SetTile(x, y, new TilesetTile(0, 0));


            var mll = new List<MapLayer>();
            mll.Add(layer);

            var map = new TileMap(m_SpriteMgrMock.Object, TileMapType.Square, 32, 32, tsl, mll);
            var camera = new Mock<ICamera>();
            camera.SetupGet(m => m.ViewportRectangle).Returns(new Rectangle(0, 0, 100, 100));
            camera.SetupGet(m => m.Position).Returns(new Vector2(50, 50));
            camera.SetupGet(m => m.Zoom).Returns(1.0f);

            map.Draw(camera.Object);

            m_SpriteMgrMock.Verify(m => m.DrawTexture2D(
                It.IsAny<ITexture2D>(),
                It.IsAny<Rectangle>(),
                It.IsAny<Rectangle>()
            ),Times.Exactly(4));
        }

        [Test]
        public void TestTileMapDrawIsCalledWithCorrectTexture_Square()
        {
            var tsl = new List<ITileset>();
            tsl.Add(m_TileSetMock.Object);

            var layer = new MapLayer("test", 2, 2);
            for (int y = 0; y < layer.Height; y++)
                for (int x = 0; x < layer.Width; x++)
                    layer.SetTile(x, y, new TilesetTile(0, 0));


            var mll = new List<MapLayer>();
            mll.Add(layer);

            var map = new TileMap(m_SpriteMgrMock.Object, TileMapType.Square, 32, 32, tsl, mll);
            var camera = new Mock<ICamera>();
            camera.SetupGet(m => m.ViewportRectangle).Returns(new Rectangle(0, 0, 100, 100));
            camera.SetupGet(m => m.Position).Returns(new Vector2(50, 50));
            camera.SetupGet(m => m.Zoom).Returns(1.0f);

            map.Draw(camera.Object);

            m_SpriteMgrMock.Verify(m => m.DrawTexture2D(
                It.Is<ITexture2D>(t => t.Height == m_Tex2D.Object.Height && t.Width == m_Tex2D.Object.Width),
                It.IsAny<Rectangle>(),
                It.IsAny<Rectangle>()
            ),Times.Exactly(4));
        }

        [Test]
        public void TestTileMapDrawIsCalledWithCorrectSourceRectangle_Square()
        {
            var tsl = new List<ITileset>();
            tsl.Add(m_TileSetMock.Object);

            var layer = new MapLayer("test", 2, 2);
            for (int y = 0; y < layer.Height; y++)
                for (int x = 0; x < layer.Width; x++)
                    layer.SetTile(x, y, new TilesetTile(0, 0));
            
            layer.SetTile(1, 1, new TilesetTile(3, 0));

            var mll = new List<MapLayer>();
            mll.Add(layer);

            var map = new TileMap(m_SpriteMgrMock.Object, TileMapType.Square, 32, 32, tsl, mll);
            var camera = new Mock<ICamera>();
            camera.SetupGet(m => m.ViewportRectangle).Returns(new Rectangle(0, 0, 100, 100));
            camera.SetupGet(m => m.Position).Returns(new Vector2(50, 50));
            camera.SetupGet(m => m.Zoom).Returns(1.0f);

            var sourceRects = new List<Rectangle>();
            m_SpriteMgrMock.Setup(m => m.DrawTexture2D(It.IsAny<ITexture2D>(), It.IsAny<Rectangle>(), It.IsAny<Rectangle>()))
                .Callback<ITexture2D,Rectangle,Rectangle>((tex,destRect,sourceRect) => sourceRects.Add(sourceRect));

            map.Draw(camera.Object);

            Assert.That(sourceRects.Count, Is.EqualTo(4));
            Assert.That(sourceRects[0],Is.EqualTo(new Rectangle(0, 0, 32, 32)));
            Assert.That(sourceRects[1],Is.EqualTo(new Rectangle(0, 0, 32, 32)));
            Assert.That(sourceRects[2],Is.EqualTo(new Rectangle(0, 0, 32, 32)));
            Assert.That(sourceRects[3],Is.EqualTo(new Rectangle(32, 32, 32, 32)));
        }

        [Test]
        public void TestTileMapDrawIsCalledWithCorrectDestinationRectangle_Square()
        {
            var tsl = new List<ITileset>();
            tsl.Add(m_TileSetMock.Object);

            var layer = new MapLayer("test", 2, 2);
            for (int y = 0; y < layer.Height; y++)
                for (int x = 0; x < layer.Width; x++)
                    layer.SetTile(x, y, new TilesetTile(0, 0));


            var mll = new List<MapLayer>();
            mll.Add(layer);

            var map = new TileMap(m_SpriteMgrMock.Object, TileMapType.Square, 32, 32, tsl, mll);
            var camera = new Mock<ICamera>();
            camera.SetupGet(m => m.ViewportRectangle).Returns(new Rectangle(0, 0, 100, 100));
            camera.SetupGet(m => m.Position).Returns(new Vector2(50, 50));
            camera.SetupGet(m => m.Zoom).Returns(1.0f);

            var destRects = new List<Rectangle>();
            m_SpriteMgrMock.Setup(m => m.DrawTexture2D(It.IsAny<ITexture2D>(), It.IsAny<Rectangle>(), It.IsAny<Rectangle>()))
                .Callback<ITexture2D,Rectangle,Rectangle>((tex,destRect,sourceRect) => destRects.Add(destRect));
            
            map.Draw(camera.Object);

            Assert.That(destRects.Count, Is.EqualTo(4));
            Assert.That(destRects[0],Is.EqualTo(new Rectangle(0, 0, 32, 32)));
            Assert.That(destRects[1],Is.EqualTo(new Rectangle(32, 0, 32, 32)));
            Assert.That(destRects[2],Is.EqualTo(new Rectangle(0, 32, 32, 32)));
            Assert.That(destRects[3],Is.EqualTo(new Rectangle(32, 32, 32, 32)));
        }


        /////////////////////////////////////////////////////////////////////////////////////
        /// Isometric Tile Map Tests
        ///////////////////////////////////////////////////////////////////////////////////// 

        [Test]
        public void TestTileMapConstruction_Isometric()
        {
            var tsl = new List<ITileset>();
            tsl.Add(m_TileSetMock.Object);

            var layer = new MapLayer("test", 2, 2);
            var mll = new List<MapLayer>();
            mll.Add(layer);

            var map = new TileMap(m_SpriteMgrMock.Object, TileMapType.Isometric, 32, 32, tsl, mll);

            Assert.That(map,Is.Not.Null); 
        }

        [Test]
        public void TestTileMapOrigin_Isometric()
        {
            var tsl = new List<ITileset>();
            tsl.Add(m_TileSetMock.Object);

            var layer = new MapLayer("test", 2, 2);
            var mll = new List<MapLayer>();
            mll.Add(layer);

            var map = new TileMap(m_SpriteMgrMock.Object, TileMapType.Isometric, 32, 32, tsl, mll);

            Assert.That(map.Origin,Is.EqualTo( new Point(16,32))); 
        }

        [Test]
        public void TestTileMapDrawIsCalledCorrectNumberOfTimes_Isometric()
        {
            var tsl = new List<ITileset>();
            tsl.Add(m_TileSetMock.Object);

            var layer = new MapLayer("test", 2, 2);
            for (int y = 0; y < layer.Height; y++)
                for (int x = 0; x < layer.Width; x++)
                    layer.SetTile(x, y, new TilesetTile(0, 0));


            var mll = new List<MapLayer>();
            mll.Add(layer);

            var map = new TileMap(m_SpriteMgrMock.Object, TileMapType.Isometric, 32, 32, tsl, mll);
            var camera = new Mock<ICamera>();
            camera.SetupGet(m => m.ViewportRectangle).Returns(new Rectangle(0, 0, 100, 100));
            camera.SetupGet(m => m.Position).Returns(new Vector2(50, 50));
            camera.SetupGet(m => m.Zoom).Returns(1.0f);

            map.Draw(camera.Object);

            m_SpriteMgrMock.Verify(m => m.DrawTexture2D(
                It.IsAny<ITexture2D>(),
                It.IsAny<Rectangle>(),
                It.IsAny<Rectangle>()
            ),Times.Exactly(4));
        }

        [Test]
        public void TestTileMapDrawIsCalledWithCorrectTexture_Isometric()
        {
            var tsl = new List<ITileset>();
            tsl.Add(m_TileSetMock.Object);

            var layer = new MapLayer("test", 2, 2);
            for (int y = 0; y < layer.Height; y++)
                for (int x = 0; x < layer.Width; x++)
                    layer.SetTile(x, y, new TilesetTile(0, 0));


            var mll = new List<MapLayer>();
            mll.Add(layer);

            var map = new TileMap(m_SpriteMgrMock.Object, TileMapType.Isometric, 32, 32, tsl, mll);
            var camera = new Mock<ICamera>();
            camera.SetupGet(m => m.ViewportRectangle).Returns(new Rectangle(0, 0, 100, 100));
            camera.SetupGet(m => m.Position).Returns(new Vector2(50, 50));
            camera.SetupGet(m => m.Zoom).Returns(1.0f);

            map.Draw(camera.Object);

            m_SpriteMgrMock.Verify(m => m.DrawTexture2D(
                It.Is<ITexture2D>(t => t.Height == m_Tex2D.Object.Height && t.Width == m_Tex2D.Object.Width),
                It.IsAny<Rectangle>(),
                It.IsAny<Rectangle>()
            ),Times.Exactly(4));
        }

        [Test]
        public void TestTileMapDrawIsCalledWithCorrectSourceRectangle_Isometric()
        {
            var tsl = new List<ITileset>();
            tsl.Add(m_TileSetMock.Object);

            var layer = new MapLayer("test", 2, 2);
            for (int y = 0; y < layer.Height; y++)
                for (int x = 0; x < layer.Width; x++)
                    layer.SetTile(x, y, new TilesetTile(0, 0));

            layer.SetTile(1, 1, new TilesetTile(3, 0));

            var mll = new List<MapLayer>();
            mll.Add(layer);

            var map = new TileMap(m_SpriteMgrMock.Object, TileMapType.Isometric, 32, 32, tsl, mll);
            var camera = new Mock<ICamera>();
            camera.SetupGet(m => m.ViewportRectangle).Returns(new Rectangle(0, 0, 100, 100));
            camera.SetupGet(m => m.Position).Returns(new Vector2(50, 50));
            camera.SetupGet(m => m.Zoom).Returns(1.0f);

            var sourceRects = new List<Rectangle>();
            m_SpriteMgrMock.Setup(m => m.DrawTexture2D(It.IsAny<ITexture2D>(), It.IsAny<Rectangle>(), It.IsAny<Rectangle>()))
                .Callback<ITexture2D,Rectangle,Rectangle>((tex,destRect,sourceRect) => sourceRects.Add(sourceRect));

            map.Draw(camera.Object);

            Assert.That(sourceRects.Count, Is.EqualTo(4));
            Assert.That(sourceRects[0],Is.EqualTo(new Rectangle(0, 0, 32, 32)));
            Assert.That(sourceRects[1],Is.EqualTo(new Rectangle(0, 0, 32, 32)));
            Assert.That(sourceRects[2],Is.EqualTo(new Rectangle(0, 0, 32, 32)));
            Assert.That(sourceRects[3],Is.EqualTo(new Rectangle(32, 32, 32, 32)));
        }

        [Test]
        public void TestTileMapDrawIsCalledWithCorrectDestinationRectangle_Isometric()
        {
            var tsl = new List<ITileset>();
            tsl.Add(m_TileSetMock.Object);

            var layer = new MapLayer("test", 2, 2);
            for (int y = 0; y < layer.Height; y++)
                for (int x = 0; x < layer.Width; x++)
                    layer.SetTile(x, y, new TilesetTile(0, 0));


            var mll = new List<MapLayer>();
            mll.Add(layer);

            var map = new TileMap(m_SpriteMgrMock.Object, TileMapType.Isometric, 32, 32, tsl, mll);
            var camera = new Mock<ICamera>();
            camera.SetupGet(m => m.ViewportRectangle).Returns(new Rectangle(0, 0, 100, 100));
            camera.SetupGet(m => m.Position).Returns(new Vector2(50, 50));
            camera.SetupGet(m => m.Zoom).Returns(1.0f);

            var destRects = new List<Rectangle>();
            m_SpriteMgrMock.Setup(m => m.DrawTexture2D(It.IsAny<ITexture2D>(), It.IsAny<Rectangle>(), It.IsAny<Rectangle>()))
                .Callback<ITexture2D,Rectangle,Rectangle>((tex,destRect,sourceRect) => destRects.Add(destRect));

            map.Draw(camera.Object);

            Assert.That(destRects.Count, Is.EqualTo(4));
            Assert.That(destRects[0],Is.EqualTo(new Rectangle(16, 32, 32, 32)));
            Assert.That(destRects[1],Is.EqualTo(new Rectangle(0, 48, 32, 32)));
            Assert.That(destRects[2],Is.EqualTo(new Rectangle(32, 48, 32, 32)));
            Assert.That(destRects[3],Is.EqualTo(new Rectangle(16, 64, 32, 32)));
        }

        ////////////////////////////////////////////////////////////////
        /// Alternative construction tests
        ////////////////////////////////////////////////////////////////
        [Test] 
        public void TestConstructionWithMapGenerator()
        {
            var tsl = new List<ITileset>();
            tsl.Add(m_TileSetMock.Object);

            var layer = new MapLayer("test", 2, 2);
            var mll = new List<MapLayer>();
            mll.Add(layer);

            var generator = new Mock<IMapGenerator>();
            generator.Setup(m => m.GenerateLayers()).Returns(mll);
            generator.Setup(m => m.GenerateTilesets()).Returns(tsl);

            var map = new TileMap(m_SpriteMgrMock.Object, TileMapType.Square, 32, 32, generator.Object);

            Assert.That(map,Is.Not.Null); 
        }

        [Test] 
        public void TestConstructionHasCorrectMapParameters()
        {
            var tsl = new List<ITileset>();
            tsl.Add(m_TileSetMock.Object);

            var layer = new MapLayer("test", 2, 2);
            var mll = new List<MapLayer>();
            mll.Add(layer);

            var generator = new Mock<IMapGenerator>();

            generator.Setup(m => m.GenerateLayers()).Returns(mll);
            generator.Setup(m => m.GenerateTilesets()).Returns(tsl);

            var map = new TileMap(m_SpriteMgrMock.Object, TileMapType.Square, 32, 32, generator.Object);

            Assert.That(map,Is.Not.Null); 
            Assert.That(map.HeightInPixels, Is.EqualTo(32*2));
            Assert.That(map.Origin, Is.EqualTo(new Point(0,0)));
            Assert.That(map.TileHeight, Is.EqualTo(32));
            Assert.That(map.TileMapType, Is.EqualTo(TileMapType.Square));
            Assert.That(map.TileWidth, Is.EqualTo(32));
        }

    }
}

