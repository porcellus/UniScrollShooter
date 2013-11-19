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
        //remélem erre gondoltál
        private static ShipType _smallShip;
        private static ShipType _mediumShip;
        private static ShipType _bigShip;

        public static ShipType SmallShip
        {
            get { return _smallShip ?? (_smallShip = new ShipType("kicsi", 2)); }
        }
        public static ShipType MediumShip
        {
            get { return _mediumShip ?? (_mediumShip = new ShipType("közepes", 4)); }
        }
        public static ShipType BigShip
        {
            get { return _bigShip ?? (_bigShip = new ShipType("nagy", 8)); }
        }
        public String Name { get; set; }
        public Int32 Power { get; set; }

        private ShipType(String name, Int32 power)
        {
            Name = name;
            Power = power;
        }
    }

    public class BossType
    {
        private static BossType _instance;
        public static BossType Instance()
        {
            if(_instance==null)
                _instance = new BossType();
            return _instance;
        }

        private BossType() {}

    }

}
