using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.FixedReferences
{
    class ShipType
    {
        private static ShipType _smallShip;
        public static ShipType SmallShip
        {
            get { return _smallShip ?? (_smallShip = new ShipType("kicsi")); }
        }

        public string Name { get; set; }

        private ShipType(string name)
        {
            Name = name;
        }
    }
}
