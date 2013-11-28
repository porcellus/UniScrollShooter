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
        //private:
        private static ShipType _smallShip;
        private static ShipType _mediumShip;
        private static ShipType _bigShip;

        private String _name;
        private Int32 _speed;
        private Int32 _basedamage;
        
        private ShipType(String name_, Int32 speed_, Int32 bdamage_)
        {
            _name = name_;
            _speed = speed_;
            _basedamage = bdamage_;
        }

        //public:
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
            get { return _bigShip ?? (_bigShip = new ShipType("nagy", 8, 50)); }
        }

        public String name { get { return _name; } }
        public Int32 speed { get { return _speed; } }
        public Int32 basedamage { get { return _basedamage; } }

    }

}
