using System;

namespace Nano.Engine.Graphics
{
    /// <summary>
    /// Interface to abstract 2D textures
    /// </summary>
    public interface ITexture2D
    {
        /// <summary>
        /// Gets the width of the texture in pixels.
        /// </summary>
        /// <value>The width in pixels.</value>
        int Width { get; }

        /// <summary>
        /// Gets the height of the texture in pixels.
        /// </summary>
        /// <value>The height in pixels.</value>
        int Height { get; }
    }
}

