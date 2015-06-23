using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Nano.Engine.Graphics.Tileset
{
    /// <summary>
    /// Irregular tileset.
    /// Tileset consisting of tiles specified by arbitrary rectangles
    /// </summary>
    public class IrregularTileset : ITileset
    {
        #region ITileset implementation
        public Point Offset
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public IList<Rectangle> Bounds
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public Texture2D Texture
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public int Size
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}

