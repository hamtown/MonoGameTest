﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoShooterDX
{
    class GamePadInput:Input
    {
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        public override void Update(GameTime gametime)
        {
            XDirection = currentGamePadState.ThumbSticks.Left.X;
            YDirection = currentGamePadState.ThumbSticks.Left.Y;

            Fire = (currentGamePadState.Buttons.X == ButtonState.Pressed);
            Exit = currentGamePadState.Buttons.Back == ButtonState.Pressed;
             
        }
    }
}