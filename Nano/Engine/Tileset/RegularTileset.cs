using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Nano.Engine.Tileset
{
	public class RegularTileset : ITileset
	{
		#region member data

		readonly string m_Name;
        readonly List<Rectangle> m_Bounds;

		#endregion

		#region public properties

		public Point Offset { get; set; }

		public Texture2D Texture { get; set; }

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

		public RegularTileset(string name, Texture2D texture, int columnCount, int rowCount, int tileWidth, int tileHeight)
		{
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

