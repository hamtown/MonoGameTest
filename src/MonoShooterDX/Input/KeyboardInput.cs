using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoShooterDX
{
    class KeyboardInput : Input
    {
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        public override void Update(GameTime gametime)
        {
            //ResetDirections();

            XDirection = 0;
            YDirection = 0;

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            // Keyboard and GamePad Dpad Controls
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                XDirection += -1;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                XDirection += 1;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                YDirection += -1;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                YDirection += 1;
            }

            Fire = currentKeyboardState.IsKeyDown(Keys.Space);
            Exit = currentKeyboardState.IsKeyDown(Keys.Escape);
        }
    }
}