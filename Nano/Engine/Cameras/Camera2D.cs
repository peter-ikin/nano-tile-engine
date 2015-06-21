using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nano.Engine.Graphics.Sprites;
using Nano.Input;

namespace Nano.Engine.Cameras
{
    /// <summary>
    /// simple 2D camera class
    /// </summary>
	public class Camera2D
	{
		#region member data

		Vector2 m_Position;
		float m_Zoom;
        float m_Rotation;

		Rectangle m_ViewportRectangle;
        Matrix m_Transform;

		#endregion

		#region properties

		public Vector2 Position
		{
			get { return m_Position; }
            set { m_Position = value; }
		}

		public float Zoom
		{
			get { return m_Zoom; }
            set { m_Zoom = value; }
		}
            
		public Matrix Transformation
		{
            get { return m_Transform; }
            set { m_Transform = value; }
		}

        public float Rotation
        {
            get { return m_Rotation; }
            set { m_Rotation = value; }
        }

        public Matrix InverseTransform
        {
            get
            {
                return Matrix.Invert(m_Transform);
            }
        }

        public Rectangle ViewportRectangle
        {
            get 
            { 
                return new Rectangle(
                    m_ViewportRectangle.X,
                    m_ViewportRectangle.Y,
                    m_ViewportRectangle.Width,
                    m_ViewportRectangle.Height); 
            }
        }
		#endregion

		public Camera2D(Rectangle viewportRect)
		{
			m_ViewportRectangle = viewportRect;
		}

		public Camera2D(Rectangle viewportRect, Vector2 position)
		{
			m_ViewportRectangle = viewportRect;
			m_Position = position;
		}

        public void Update()
        {
            m_Zoom = MathHelper.Clamp(m_Zoom, 0.0f, 10.0f);

            m_Rotation = MathHelper.WrapAngle(m_Rotation);

            m_Transform = Matrix.CreateRotationZ(m_Rotation) *
                          Matrix.CreateScale(new Vector3(m_Zoom,m_Zoom,1)) *
                          Matrix.CreateTranslation(new Vector3(-m_Position, 0f));
		}
	}
}

