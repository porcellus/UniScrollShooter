using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.FixedReferences
{
    /// <summary>
    /// Ship with common attributes, now name and power
    /// </summary>
    public class ShipType
    {
        private static ShipType _smallShip;
        private static ShipType _mediumShip;
        private static ShipType _bigShip;

        public static ShipType SmallShip
        {
            get { return _smallShip ?? (_smallShip = new ShipType("kicsi", 2, 10)); }
        }
        public static ShipType MediumShip
        {
            get { return _mediumShip ?? (_mediumShip = new ShipType("közepes", 4, 20)); }
        }
        public static ShipType BigShip
        {
            get { return _bigShip ?? (_bigShip = new ShipType("nagy", 8, 30)); }
        }
        public String name { get; private set; }
        public Int32 speed { get; private set; }
        public Int32 basedamage { get; private set; }

        private ShipType(String name_, Int32 speed_, Int32 bdamage_)
        {
            name = name_;
            speed = speed_;
            basedamage = bdamage_;
        }
    }

}
