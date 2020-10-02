using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvOchPolisMattias
{
    sealed class Citizen : Person
    {
        public Citizen (int movementYaxis, int movementXaxis, int direction, char playerIcon)
            : base(movementYaxis, movementXaxis, direction, playerIcon)
        {
        }
    }
}
