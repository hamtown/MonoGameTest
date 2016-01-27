using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoShooterDX
{
    class TouchInput : Input
    {
        public override void Update(GameTime gametime)
        {
            //while (TouchPanel.IsGestureAvailable)
            //{
            //    GestureSample gesture = TouchPanel.ReadGesture();

            //    if (gesture.GestureType == GestureType.FreeDrag)
            //    {
            //        player.Position += gesture.Delta;
            //    }
            //}
        }
    }
}