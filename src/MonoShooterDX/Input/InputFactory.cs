﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoShooterDX
{
    static class InputFactory
    {
        public static Input GetInput()
        {
            return new KeyboardInput();
            //return new GamePadInput();
        }
    }
}