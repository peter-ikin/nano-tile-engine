﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nano.Engine.Sprites
{
	public interface ISprite
	{
		Vector2 Position { get; set; }

		int Width { get; }

		int Height { get; }
            
        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    
    }
}

