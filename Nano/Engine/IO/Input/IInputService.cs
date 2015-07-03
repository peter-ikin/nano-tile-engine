using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Nano.Engine.IO.Input
{
    public interface IInputService
    {
        #region state properties
        KeyboardState KeyboardState{ get; }
        KeyboardState LastKeyboardState{ get; }
        //GamePadState[] GamePadStates{ get; }
        //GamePadState[] LastGamePadStates{ get; }
        MouseState MouseState{ get; }
        MouseState LastMouseState{ get; }

        #endregion

        #region Keyboard
        bool KeyReleased(Keys key);
        bool KeyPressed(Keys key);
        bool KeyDown(Keys key);
        #endregion

        #region Mouse
        bool LMBReleased();
        bool LMBPressed();
        bool LMBDown();
        bool MMBReleased();
        bool MMBPressed();
        bool MMBDown();
        bool RMBReleased();
        bool RMBPressed();
        bool RMBDown();
        int MouseX();
        int MouseDeltaX();
        int MouseY();
        int MouseDeltaY();
        int MouseWheel();
        int MouseWheelDelta();
        #endregion

        #region Gamepad
        //bool GamePadButtonReleased(Buttons button, PlayerIndex index);
        //bool GamePadButtonPressed(Buttons button, PlayerIndex index);
        //bool GamePadButtonDown(Buttons button, PlayerIndex index);
        #endregion
    }
}

