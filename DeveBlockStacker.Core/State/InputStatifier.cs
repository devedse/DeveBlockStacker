﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace DeveBlockStacker.Core.State
{
    public class InputStatifier
    {
        public KeyboardState CurrentKeyboardState { get; private set; } = new KeyboardState();
        public KeyboardState PreviousKeyboardState { get; private set; } = new KeyboardState();

        public GamePadState CurrentGamePadState { get; private set; } = new GamePadState();
        public GamePadState PreviousGamePadState { get; private set; } = new GamePadState();

        public TouchCollection PreviousTouchState { get; private set; } = new TouchCollection();
        public TouchCollection CurrentTouchState { get; private set; } = new TouchCollection();

        public void BeforeUpdate()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

#if !BLAZOR
            PreviousGamePadState = CurrentGamePadState;
            CurrentGamePadState = GamePad.GetState(PlayerIndex.One);
#endif

            PreviousTouchState = CurrentTouchState;
            CurrentTouchState = TouchPanel.GetState();
        }

        public bool KeyPressed(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key);
        }

        public bool IsTouchTapped()
        {
            if (CurrentTouchState.Count > 0 && (CurrentTouchState[0].State == TouchLocationState.Moved || CurrentTouchState[0].State == TouchLocationState.Pressed) && PreviousTouchState.Count == 0)
            {
                return true;
            }
            return false;
        }

        public bool GamepadButtonPressed(Buttons button)
        {
            return CurrentGamePadState.IsButtonDown(button) && PreviousGamePadState.IsButtonUp(button);
        }

        public bool UserDoesAction()
        {
            return IsTouchTapped() || KeyPressed(Keys.Space) || KeyPressed(Keys.Enter) || GamepadButtonPressed(Buttons.A);
        }
    }
}
