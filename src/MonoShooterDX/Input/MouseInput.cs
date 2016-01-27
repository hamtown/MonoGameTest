using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoShooterDX
{
    class MouseInput : Input
    {
        MouseState currentMouseState;

        public override void Update(GameTime gametime)
        {
            //Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

            //if (currentMouseState.LeftButton == ButtonState.Pressed)
            //{
            //    Vector2 posDelta = mousePosition - player.Position;

            //    posDelta.Normalize();

            //    posDelta = posDelta * player.PlayerMoveSpeed;

            //    player.Position = player.Position + posDelta;
            //}
        }
    }
}