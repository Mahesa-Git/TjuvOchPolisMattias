using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvOchPolisMattias
{
    sealed class Police : Person
    {
        public Police(int movementYaxis, int movementXaxis, int direction, char playerIcon) 
            : base(movementYaxis, movementXaxis, direction, playerIcon)
        {
        }
    }
}
