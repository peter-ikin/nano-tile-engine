using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nano.Engine.Sprites
{
    public class BasicSprite : ISprite
    {
        private Texture2D m_Texture;
        private Rectangle m_SourceRectangle;
        private Vector2 m_Position;

        #region ISprite implementation

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_Texture, m_Position, m_SourceRectangle, Color.White);
        }

        public Vector2 Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                m_Position = value;
            }
        }

        public int Width
        {
            get
            {
                return m_SourceRectangle.Width;
            }
        }

        public int Height
        {
            get
            {
                return m_SourceRectangle.Height;
            }
        }

        #endregion

        public BasicSprite(Texture2D texture)
        {
            m_Texture = texture;
            m_SourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
        }
    }
}

