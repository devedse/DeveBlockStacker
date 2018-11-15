using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace DeveBlockStacker.Shared.State
{
    public class InputStatifier
    {
        public KeyboardState CurrentKeyboardState { get; private set; } = new KeyboardState();
        public KeyboardState PreviousKeyboardState { get; private set; } = new KeyboardState();

        public TouchCollection PreviousTouchState {get; private set; } = new TouchCollection();
        public TouchCollection CurrentTouchState {get; private set; } = new TouchCollection();

        public void BeforeUpdate()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

            PreviousTouchState = CurrentTouchState;
            CurrentTouchState = TouchPanel.GetState();
        }

        public bool KeyPressed(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key);
        }

        public bool IsTouchTapped()
        {
            if (CurrentTouchState.Count > 0)
            {
                if ((CurrentTouchState[0].State == TouchLocationState.Moved || CurrentTouchState[0].State == TouchLocationState.Pressed) && PreviousTouchState.Count == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
