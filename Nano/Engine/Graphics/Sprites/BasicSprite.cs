using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nano.Engine.Graphics;

namespace Nano.Engine.Graphics.Sprites
{
    /// <summary>
    /// Basic sprite.
    /// Basic non-animated sprite implementation, supports translation and rotation
    /// </summary>
    public class BasicSprite : ISprite
    {
        private readonly ITexture2D m_Texture;
        private Rectangle m_SourceRectangle;
        private Vector2 m_Position;
        private float m_Rotation;
        private Vector2 m_Origin;
        private ISpriteManager m_Manager;

        public ISpriteManager SpriteManager
        {
            get { return m_Manager; }
            set { m_Manager = value; }
        }

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
        /// Draw the sprite.
        /// No batching is performed by using this call, sprite is simply drawn to screen
        /// </summary>
        public void Draw()
        {
            #if DEBUG
            if(SpriteManager == null)
                throw new Exception("SpriteManager not set");
            #endif

            SpriteManager.DrawTexture2D(m_Texture, m_Position, m_SourceRectangle, m_Rotation, m_Origin);
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
        /// Initializes a new instance of the <see cref="Nano.Engine.Graphics.Sprites.BasicSprite"/> class.
        /// </summary>
        /// <param name="texture">The Texture that the sprite is based on</param>
        /// <param name="sourceRectangle">The rectangle of the supplied Texture2D that constitutes the drawable area of the sprite</param>
        public BasicSprite(ITexture2D texture, Rectangle sourceRectangle)
        {
            if(texture == null)
                throw new ArgumentNullException("texture","texture constructor parameter cannot be null");
            
            m_Texture = texture;

            m_SourceRectangle = sourceRectangle;
            
            m_Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Nano.Engine.Graphics.Sprites.BasicSprite"/> class.
        /// </summary>
        /// <param name="texture">The Texture that the sprite is based on</param>
        public BasicSprite(ITexture2D texture)
        {
            if(texture == null)
                throw new ArgumentNullException("texture","texture constructor parameter cannot be null");
            
            m_Texture = texture;

            m_SourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);

            m_Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }
    }
}

