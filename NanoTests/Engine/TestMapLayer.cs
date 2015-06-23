using System;
using NUnit.Framework;
using Nano.Engine;
using Nano.Engine.Graphics.Tileset;

namespace NanoTests.Engine
{
    [TestFixture]
    public class TestMapLayer
    {
        [Test]
        public void TestConstructMapLayer()
        {
            var layer = new MapLayer("test", 2, 2);
            Assert.That(layer, Is.Not.Null);
        }

        [TestCase(0,0,TestName="Construct MapLayer with 0 sized grid")]
        [TestCase(10,0,TestName="Construct MapLayer with 0 sized height")]
        [TestCase(0,10,TestName="Construct MapLayer with 0 sized width")]
        [TestCase(-10,-10,TestName="Construct MapLayer with -ve sized grid")]
        [TestCase(-10,10,TestName="Construct MapLayer with -ve sized width")]
        [TestCase(10,-10,TestName="Construct MapLayer with -ve sized height")]
        public void TestConstructMapLayerWithBadArguments(int width, int height)
        {
            Assert.That(() => new MapLayer("test", width, height),
                Throws.Exception
                .TypeOf<ArgumentOutOfRangeException>());
        }
             
        [TestCase(1,1, TestName="Test MapLayer default values for 1x1 map")]      
        [TestCase(2,2, TestName="Test MapLayer default values for 2x2 map")]      
        [TestCase(5,1, TestName="Test MapLayer default values for 5x1 map")]      
        [TestCase(1,5, TestName="Test MapLayer default values for 1x5 map")]      
        [TestCase(10,10, TestName="Test MapLayer default values for 10x10 map")]      
        public void TestMapLayerDefaultValues(int width, int height)
        {
            var layer = new MapLayer("test", width, height);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var tile = layer.GetTile(x, y);
                    Assert.That(tile.TileIndex, Is.EqualTo(-1));
                    Assert.That(tile.TilesetId, Is.EqualTo(-1));
                }
            }
        }

        [Test]
        public void TestMapLayerName()
        {
            var layer = new MapLayer("test", 2, 2);
            Assert.That(layer.Name, Is.EqualTo("test"));
        }

        [Test]
        public void TestMapLayerWidth()
        {
            var layer = new MapLayer("test", 31, 32);
            Assert.That(layer.Width, Is.EqualTo(31));
        }

        [Test]
        public void TestMapLayerHeight()
        {
            var layer = new MapLayer("test", 2, 12);
            Assert.That(layer.Height, Is.EqualTo(12));
        }
            
        [TestCase(1,1,1,2,0,0, TestName="Test MapLayer set value for 1x1 map in bounds")]      
        [TestCase(2,2,4,12,1,0, TestName="Test MapLayer set value for 2x2 map in bounds")]      
        [TestCase(5,1,5,1,4,0, TestName="Test MapLayer set value for 5x1 map in bounds")]      
        [TestCase(1,5,2,2,0,4, TestName="Test MapLayer set value for 1x5 map in bounds")]      
        [TestCase(10,10,900,12,5,9, TestName="Test MapLayer set value for 10x10 map in bounds")]      
        public void TestMapLayerSetTile(int width, int height,int tileIndex, int tilesetId,int x, int y)
        {
            var layer = new MapLayer("test", width, height);
          
            TilesetTile tileIn = new TilesetTile(tileIndex, tilesetId);

            layer.SetTile(x, y, tileIn);

            var tileOut = layer.GetTile(x, y);
            Assert.That(tileOut.TileIndex, Is.EqualTo(tileIndex));
            Assert.That(tileOut.TilesetId, Is.EqualTo(tilesetId));
        }
            
        [TestCase(2,2,2,1,12,12, TestName="Test MapLayer set value for 2x2 map out of bounds with x=12, y=12")]
        [TestCase(2,2,2,1,-1,10, TestName="Test MapLayer set value for 2x2 map out of bounds with x=-1, y=10")]
        [TestCase(2,2,2,1,-2,-2, TestName="Test MapLayer set value for 2x2 map out of bounds with x=-2, y=-2")]
        public void TestMapLayerSetTileOutOfBounds(int width, int height,int tileIndex, int tilesetId,int x, int y)
        {
            var layer = new MapLayer("test", width, height);

            TilesetTile tileIn = new TilesetTile(tileIndex, tilesetId);

            Assert.That(() => layer.SetTile(x,y,tileIn), 
                Throws.Exception
                .TypeOf<IndexOutOfRangeException>());
        }
    }
}

