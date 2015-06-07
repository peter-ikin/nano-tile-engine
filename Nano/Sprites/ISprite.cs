using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nano.Sprites
{
	public interface ISprite
	{
		Vector2 Position { get; set; }

		int Width { get; set; }

		int Height { get; set; }
            
        void Update(GameTime gameTime);

        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    
    }
}

