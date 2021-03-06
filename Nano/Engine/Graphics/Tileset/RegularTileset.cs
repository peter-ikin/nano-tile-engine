using System;

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Nano.Engine.Graphics.Tileset
{
	public class RegularTileset : ITileset
	{
		#region member data

		readonly string m_Name;
        readonly List<Rectangle> m_Bounds;

		#endregion

		#region public properties

		public Point Offset { get; set; }

		public ITexture2D Texture { get; set; }

		/// <summary>
		/// Gets the size of the tileset ( i.e. number of tiles)
		/// </summary>
		public int Size
		{
            get { return m_Bounds.Count; }
		}

		/// <summary>
		/// Gets the name of the tileset
		/// </summary>
		public string Name
		{
			get { return m_Name; }
		}

		public IList<Rectangle> Bounds 
		{
            get { return m_Bounds; }
		}

		#endregion

		public RegularTileset(string name, ITexture2D texture, int columnCount, int rowCount, int tileWidth, int tileHeight)
		{
            if (columnCount * tileWidth > texture.Width)
                throw new ArgumentException("tile set size exceeds texture size");

            if (rowCount * tileHeight > texture.Height)
                throw new ArgumentException("tile set size exceeds texture size");
            
			m_Name = name;
			Texture = texture;
			Offset = new Point(0,0);

            m_Bounds = new List<Rectangle>();

			for(int y = 0; y < rowCount; y++)
			{
				for(int x = 0; x < columnCount; x++)
				{
                    m_Bounds.Add(new Rectangle(x * tileWidth, 
                            y * tileHeight, 
                            tileWidth, 
                            tileHeight));
				}
			}
		}
	}
}

