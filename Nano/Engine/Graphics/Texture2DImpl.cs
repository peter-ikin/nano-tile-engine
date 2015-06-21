using System;

namespace Nano.Engine.Graphics
{
    internal class Texture2DImpl : ITexture2D
    {
        int m_Width;
        int m_Height;

        public Texture2DImpl(int width, int height)
        {
            m_Width = width;
            m_Height = height;
        }

        #region ITexture2D implementation

        public int Width
        {
            get
            {
                return m_Width;
            }
        }

        public int Height
        {
            get
            {
                return m_Height;
            }
        }

        #endregion
    }
}

