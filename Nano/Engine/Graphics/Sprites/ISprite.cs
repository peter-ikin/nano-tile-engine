using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nano.Engine.Graphics;

namespace Nano.Engine.Graphics.Sprites
{
	public interface ISprite
	{
		Vector2 Position { get; set; }

        Rectangle SourceRectangle {get;set;}

		int Width { get; }

		int Height { get; }

        float Rotation { get; set;}

        Vector2 RotationOrigin { get; set;}
            
        void Update(GameTime gameTime);

        /// <summary>
        /// Draw the sprite.
        /// No batching is performed by using this call, sprite is simply drawn to screen
        /// </summary>
        void Draw();
    
    }
}

