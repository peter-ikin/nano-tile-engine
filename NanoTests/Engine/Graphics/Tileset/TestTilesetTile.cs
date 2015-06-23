using System;
using NUnit.Framework;
using Nano.Engine.Graphics.Tileset;

namespace NanoTests.Engine.Graphics.Tileset
{
    [TestFixture]
    public class TestTilesetTile
    {
        [TestCase(0,0, TestName="Create new TilesetTile( 0 , 0 )")]
        [TestCase(-1,-1, TestName="Create new TilesetTile( -1 , -1 )")]
        [TestCase(1023,99, TestName="Create new TilesetTile( 1023 , 99 )")]
        [TestCase(1,8, TestName="Create new TilesetTile( 1 , 8 )")]
        [TestCase(int.MaxValue,int.MinValue, TestName="Create new TilesetTile( int.MaxValue , int.MinValue )")]
        public void TestTilesetTileConstruction(int tileIndex, int tilesetId)
        {
            var tile = new TilesetTile(tileIndex,tilesetId);
            Assert.That(tile, Is.Not.Null);
        }

        [TestCase(0,0, TestName="Test TilesetTile TileIndex 0")]
        [TestCase(-1,-1, TestName="Test TilesetTile TileIndex -1")]
        [TestCase(1023,99, TestName="Test TilesetTile TileIndex 1023")]
        [TestCase(1,8, TestName="Test TilesetTile TileIndex 1")]
        [TestCase(int.MaxValue,int.MinValue, TestName="Test TilesetTile TileIndex int MaxValue")]
        public void TestTileIndex(int tileIndex, int tilesetId)
        {
            var tile = new TilesetTile(tileIndex, tilesetId);
            Assert.That(tile.TileIndex, Is.EqualTo(tileIndex));
        }

        [TestCase(0,0, TestName="Test TilesetTile TilesetId 0")]
        [TestCase(-1,-1, TestName="Test TilesetTile TilesetId -1")]
        [TestCase(1023,99, TestName="Test TilesetTile TilesetId 99")]
        [TestCase(1,8, TestName="Test TilesetTile TilesetId 8")]
        [TestCase(int.MaxValue,int.MinValue, TestName="Test TilesetTile TilesetId int.MinValue")]
        public void TestTilesetId(int tileIndex, int tilesetId)
        {
            var tile = new TilesetTile(tileIndex, tilesetId);
            Assert.That(tile.TilesetId, Is.EqualTo(tilesetId));
        }
    }
}

