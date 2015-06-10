using System;
using Microsoft.Xna.Framework;

namespace Nano.Engine.Cameras
{
    /// <summary>
    /// Camera interface
    /// </summary>
    public interface ICamera
    {       
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the zoom value of the camera.
        /// </summary>
        /// <value>The zoom.</value>
        float Zoom { get; set;}

        /// <summary>
        /// Gets or sets the transformation.
        /// </summary>
        /// <value>The transformation.</value>
        Matrix Transformation { get; set;}

        /// <summary>
        /// Gets or sets the rotation angle (radians).
        /// </summary>
        /// <value>The rotation.</value>
        float Rotation { get; set; }

        /// <summary>
        /// Gets the inverse transform.
        /// </summary>
        /// <value>The inverse transform.</value>
        Matrix InverseTransform { get; }

        /// <summary>
        /// Gets the viewport rectangle.
        /// </summary>
        /// <value>The viewport rectangle.</value>
        Rectangle ViewportRectangle { get; }
    }
}

