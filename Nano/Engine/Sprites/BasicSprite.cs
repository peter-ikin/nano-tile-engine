using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nano.Engine.Sprites
{
    /// <summary>
    /// Basic sprite.
    /// Basic non-animated sprite implementation, supports translation and rotation
    /// </summary>
    public class BasicSprite : ISprite
    {
        private readonly Texture2D m_Texture;
        private Rectangle m_SourceRectangle;
        private Vector2 m_Position;
        private float m_Rotation;
        private Vector2 m_Origin;

        #region ISprite implementation

        /// <summary>
        /// Update the sprite to the supplied GameTime
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public void Update(GameTime gameTime)
        {
            m_Rotation = MathHelper.WrapAngle(m_Rotation);
        }

        /// <summary>
        /// Draw the sprite
        /// </summary>
        /// <param name="spriteBatch">Sprite batch to draw with</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_Texture, m_Position, m_SourceRectangle, 
                Color.White, m_Rotation, m_Origin, 1.0f, SpriteEffects.None, 1);
        }

        /// <summary>
        /// Gets or sets the position of the sprite ( in screen co-ordinates )
        /// </summary>
        /// <value>The position.</value>
        public Vector2 Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        /// <summary>
        /// Gets the width of the sprite (in pixels)
        /// </summary>
        /// <value>The width.</value>
        public int Width
        {
            get { return m_SourceRectangle.Width; }
        }

        /// <summary>
        /// Gets the height of the sprite (in pixels)
        /// </summary>
        /// <value>The height.</value>
        public int Height
        {
            get { return m_SourceRectangle.Height; }
        }

        /// <summary>
        /// Gets or sets the rotation angle of the sprite (in radians)
        /// </summary>
        /// <value>The rotation.</value>
        public float Rotation
        {
            get { return m_Rotation; }
            set { m_Rotation = value; }
        }

        /// <summary>
        /// Gets or sets the rotation origin.
        /// This is the point (on the sprite) around which any rotation is performed.
        /// By default the rotation origin is set to be the center of the sprite
        /// </summary>
        /// <value>The rotation origin.</value>
        public Vector2 RotationOrigin 
        { 
            get{ return m_Origin; } 
            set{ m_Origin = value; }
        }

        /// <summary>
        /// Gets or sets the source rectangle of the sprite on its targeted texture
        /// </summary>
        /// <value>The source rectangle.</value>
        public Rectangle SourceRectangle
        {
            get { return m_SourceRectangle;}
            set { m_SourceRectangle = value; }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Nano.Engine.Sprites.BasicSprite"/> class.
        /// </summary>
        /// <param name="texture">The Texture that the sprite is based on</param>
        /// <param name="sourceRectangle">The rectangle of the supplied Texture2D that constitutes the drawable area of the sprite</param>
        public BasicSprite(Texture2D texture, Rectangle? sourceRectangle)
        {
            m_Texture = texture;

            if (!sourceRectangle.HasValue)
                m_SourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            else
                m_SourceRectangle = sourceRectangle.Value;
            
            m_Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }
    }
}

