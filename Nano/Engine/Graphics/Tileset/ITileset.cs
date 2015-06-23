using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Nano.Engine.Graphics.Tileset
{
	public interface ITileset
	{
        Point Offset { get; set; }

        IList<Rectangle> Bounds{get; }

        Texture2D Texture { get; set; }

		int Size { get; }
	}
}

