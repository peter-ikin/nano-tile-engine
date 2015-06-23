using System;

namespace Nano.Engine.Graphics.Tileset
{
	/// <summary>
	/// Represents an individual Tile in the tileset
	/// </summary>
	public class TilesetTile
	{
        /// <summary>
        /// Gets or sets the index of the tile within its specified tileset
        /// </summary>
        /// <value>The index of the tile.</value>
		public int TileIndex { get; set; } 

        /// <summary>
        /// Gets or sets the tileset identifier. i.e. the Id of the tileset this tile uses.
        /// </summary>
        /// <value>The tileset identifier.</value>
        public int TilesetId { get; set; }

		public TilesetTile(int tileIndex, int tilesetId)
		{
			TileIndex = tileIndex;
			TilesetId = tilesetId;
		}
	}
}

