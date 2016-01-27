using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;

namespace MonoShooterDX
{
    abstract class Input
    {
        //Movement Direction
        public float XDirection { get; set; }
        public float YDirection { get; set; }

        //Fire Weapon
        public bool Fire { get; set; }

        //Exit Game
        public bool Exit { get; set; }

        //Pause 
        public bool Pause { get; set; }

        public abstract void Update(GameTime gametime); 
    }
}