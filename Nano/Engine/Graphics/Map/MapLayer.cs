using System;
using Nano.Engine.Graphics.Tileset;

namespace Nano.Engine.Graphics.Map
{
	/// <summary>
	/// Class that represents a layer of tiles in the tilemap.
	/// </summary>
	public class MapLayer
	{
		readonly TilesetTile[][] m_Map;

		/// <summary>
		/// The name of the MapLayer
		/// </summary>
		/// <value>The layer name</value>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the width of the MapLayer.
		/// </summary>
		/// <value>The width of the layer (number of tiles)</value>
		public int Width { get ; private set; }

		/// <summary>
		/// Gets the height of the MapLayer.
		/// </summary>
		/// <value>The height of the layer (number of tiles)</value>
		public int Height { get ; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Nano.Engine.MapLayer"/> class.
		/// </summary>
		/// <param name="name">Name of the MapLayer</param>
		/// <param name="width">Width of the MapLayer (number of tiles)</param>
		/// <param name="height">Height of the MapLayer (number of tiles)</param>
		public MapLayer(string name, int width, int height)
		{
            if (width < 1)
                throw new ArgumentOutOfRangeException("width","width must be greater than or equal to 1");
            if (height < 1)
                throw new ArgumentOutOfRangeException("height", "height must be greater than or equal to 1");

            Name = name;
			Width = width;
			Height = height;
            
			m_Map = new TilesetTile[height][];
            for (int i = 0; i < m_Map.Length; i++)
            {
                m_Map[i] = new TilesetTile[width];
            }

			for(int y = 0; y < height; y++)
			{
				for(int x = 0; x < width; x++)
				{
					m_Map[y][x] = new TilesetTile(-1,-1);
				}
			}
		}

		/// <summary>
		/// Gets the tile at the specified x,y co-ordinates of the MapLayer
		/// </summary>
		/// <returns>The tile.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public TilesetTile GetTile(int x, int y)
		{
			return m_Map[y][x];
		}

		/// <summary>
		/// Sets the tile at the specified x,y co-ordinates of the MapLayer
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="tile">The Tile to set</param>
		public void SetTile(int x, int y, TilesetTile tile)
		{
			m_Map[y][x] = tile;
		}
	}
}

