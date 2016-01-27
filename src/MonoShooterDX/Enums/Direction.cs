using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoShooterDX
{
    [Flags]
    enum Direction
    {
        Up = 1,
        Down = 2,
        Right = 4,
        Left = 8
    }
}