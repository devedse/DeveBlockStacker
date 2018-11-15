using Microsoft.Xna.Framework.Input;

namespace DeveBlockStacker.Shared.State
{
    public class InputStatifier
    {
        public KeyboardState CurrentKeyboardState { get; private set; } = new KeyboardState();
        public KeyboardState PreviousKeyboardState { get; private set; } = new KeyboardState();

        public void BeforeUpdate()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
        }

        public bool KeyPressed(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key);
        }
    }
}
