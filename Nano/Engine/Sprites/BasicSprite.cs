using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nano.Engine.Sprites
{
    public class BasicSprite : ISprite
    {
        private readonly Texture2D m_Texture;
        private Rectangle m_SourceRectangle;
        private Vector2 m_Position;
        private float m_Rotation;
        private Vector2 m_Origin;

        #region ISprite implementation

        public void Update(GameTime gameTime)
        {
            m_Rotation = MathHelper.WrapAngle(m_Rotation);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_Texture, m_Position, m_SourceRectangle, 
                Color.White, m_Rotation, m_Origin, 1.0f, SpriteEffects.None, 1);
        }

        public Vector2 Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public int Width
        {
            get { return m_SourceRectangle.Width; }
        }

        public int Height
        {
            get { return m_SourceRectangle.Height; }
        }

        public float Rotation
        {
            get { return m_Rotation; }
            set { m_Rotation = value; }
        }

        public Vector2 RotationOrigin 
        { 
            get{ return m_Origin; } 
            set{ m_Origin = value; }
        }
        #endregion

        public BasicSprite(Texture2D texture)
        {
            m_Texture = texture;
            m_SourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            m_Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }
    }
}

