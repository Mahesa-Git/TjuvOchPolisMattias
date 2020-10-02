using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvOchPolisMattias
{
    sealed class Thief : Person
    {
        public bool ThiefImprisoned { get; set; }
        public int PrisonTimer { get; set; }
        public int PrisonerID { get; set; }

        public Thief (int movementYaxis, int movementXaxis, int direction, char playerIcon, bool thiefImprisoned, int prisonTimer)
            : base(movementYaxis, movementXaxis, direction, playerIcon)
        {
            ThiefImprisoned = thiefImprisoned;
            PrisonTimer = prisonTimer;
        }
        public override void PrisonCheck()
        {
            ThiefImprisoned = true;
        }
        public override void PrisonIdUpdate(int input)
        {
            PrisonerID = input;
        }
    }
}
