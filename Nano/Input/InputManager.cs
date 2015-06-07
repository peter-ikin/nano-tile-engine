using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Nano.Input
{
	public class InputManager : GameComponent, IInputService
	{
		#region member data

		private KeyboardState m_KeyboardState;
		private KeyboardState m_LastKeyboardState;

		private GamePadState[] m_GamePadStates;
		private GamePadState[] m_LastGamePadStates;

		private MouseState m_MouseState;
		private MouseState m_LastMouseState;

		#endregion

		#region Properties

		public KeyboardState KeyboardState
		{
			get { return m_KeyboardState; }
		}

		public KeyboardState LastKeyboardState
		{
			get { return m_LastKeyboardState; }
		}

		public GamePadState[] GamePadStates
		{
			get { return m_GamePadStates; }
		}

		public GamePadState[] LastGamePadStates
		{
			get { return m_LastGamePadStates; }
		}

		public MouseState LastMouseState
		{
			get { return m_LastMouseState; }
		}

		public MouseState MouseState
		{
			get { return m_MouseState; }
		}

		#endregion

		public InputManager(Game game)
			: base(game)
		{
			m_KeyboardState = Keyboard.GetState();

			m_GamePadStates = new GamePadState[Enum.GetValues (typeof(PlayerIndex)).Length];
			foreach (PlayerIndex idx in Enum.GetValues(typeof(PlayerIndex)))
			{
				m_GamePadStates[(int)idx] = GamePad.GetState (idx);
			}

			m_MouseState = Mouse.GetState();
		}

		#region GameComponent overrides
	
		public override void Update(GameTime gameTime)
		{
			m_LastKeyboardState = m_KeyboardState;
			m_KeyboardState = Keyboard.GetState();

			m_LastGamePadStates = m_GamePadStates.Clone() as GamePadState[];
			foreach (PlayerIndex idx in Enum.GetValues(typeof(PlayerIndex)))
			{
				m_GamePadStates[(int)idx] = GamePad.GetState(idx);
			}

			m_LastMouseState = m_MouseState;
			m_MouseState = Mouse.GetState();

			base.Update(gameTime);
		}

		#endregion

		#region keyboard member functions

        /// <summary>
        /// checks if the specified key was released
        /// </summary>
        /// <returns><c>true</c>, if key was released, <c>false</c> otherwise.</returns>
        /// <param name="key">Key.</param>
		public bool KeyReleased(Keys key)
		{
			return m_KeyboardState.IsKeyUp(key) && m_LastKeyboardState.IsKeyDown(key);
		}

		public bool KeyPressed(Keys key)
		{
			return m_KeyboardState.IsKeyDown(key) && m_LastKeyboardState.IsKeyUp(key);
		}

		public bool KeyDown(Keys key)
		{
			return m_KeyboardState.IsKeyDown(key);
		}
		#endregion

		#region mouse member functions

		/// <summary>
		/// Checks if the Left Mouse Button (LMB) is released.
		/// </summary>
        public bool LMBReleased()
		{
			return m_MouseState.LeftButton == ButtonState.Released && m_LastMouseState.LeftButton == ButtonState.Pressed;
		}

		/// <summary>
		/// Checks if the Left Mouse Button (LMB) is pressed.
		/// </summary>
		public bool LMBPressed()
		{
			return m_MouseState.LeftButton == ButtonState.Pressed && m_LastMouseState.LeftButton == ButtonState.Released;
		}

		/// <summary>
		/// Checks if the Left Mouse Button LMB is down.
		/// </summary>
		public bool LMBDown()
		{
			return m_MouseState.LeftButton == ButtonState.Pressed;
		}

		/// <summary>
		/// Checks if the Middle Mouse Button (MMB) is released.
		/// </summary>
		public bool MMBReleased()
		{
			return m_MouseState.MiddleButton == ButtonState.Released && m_LastMouseState.MiddleButton == ButtonState.Pressed;
		}

		/// <summary>
		/// Checks if the Middle Mouse Button (MMB) is pressed.
		/// </summary>
		public bool MMBPressed()
		{
			return m_MouseState.MiddleButton == ButtonState.Pressed && m_LastMouseState.MiddleButton == ButtonState.Released;
		}

		/// <summary>
		/// Checks if the Middle Mouse Button MMB is down.
		/// </summary>
		public bool MMBDown()
		{
			return m_MouseState.MiddleButton == ButtonState.Pressed;
		}

		/// <summary>
		/// Checks if the Right Mouse Button (RMB) is released.
		/// </summary>
		public bool RMBReleased()
		{
			return m_MouseState.RightButton == ButtonState.Released && m_LastMouseState.RightButton == ButtonState.Pressed;
		}

		/// <summary>
		/// Checks if the Right Mouse Button (RMB) is pressed.
		/// </summary>
		public bool RMBPressed()
		{
			return m_MouseState.RightButton == ButtonState.Pressed && m_LastMouseState.RightButton == ButtonState.Released;
		}

		/// <summary>
		/// Checks if the Middle Mouse Button RMB is down.
		/// </summary>
		public bool RMBDown()
		{
			return m_MouseState.RightButton == ButtonState.Pressed;
		}

		/// <summary>
		/// The current X co-ordinate of the mouse
		/// </summary>
		/// <returns>The X mouse co-ordinate value</returns>
		public int MouseX()
		{
			return m_MouseState.X;
		}
	

		public int MouseDeltaX()
		{
			return m_MouseState.X - m_LastMouseState.X;
		}

		public int MouseY()
		{
			return m_MouseState.Y;
		}

		public int MouseDeltaY()
		{
			return m_MouseState.Y - m_LastMouseState.Y;
		}

		public int MouseWheel()
		{
			return m_MouseState.ScrollWheelValue;
		}

		public int MouseWheelDelta()
		{
			return m_MouseState.ScrollWheelValue - m_LastMouseState.ScrollWheelValue;
		}
		#endregion

		#region game pad member functions

		public bool GamePadButtonReleased(Buttons button, PlayerIndex index)
		{
			return m_GamePadStates[(int)index].IsButtonUp(button) && 
				m_LastGamePadStates[(int)index].IsButtonDown(button);
		}

		public bool GamePadButtonPressed(Buttons button, PlayerIndex index)
		{
			return m_GamePadStates[(int)index].IsButtonDown(button) &&
				m_LastGamePadStates[(int)index].IsButtonUp(button);
		}

		public bool GamePadButtonDown(Buttons button,PlayerIndex index)
		{
			return m_GamePadStates[(int)index].IsButtonDown(button);
		}
		#endregion
	}
}

