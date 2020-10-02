using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace TjuvOchPolisMattias
{

    abstract class Inventory
    {
        public int Keys { get; set; }
        public int CellPhone { get; set; }
        public int Money { get; set; }
        public int Watch { get; set; }
    }
}
