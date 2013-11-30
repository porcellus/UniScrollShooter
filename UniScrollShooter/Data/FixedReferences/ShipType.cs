using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.FixedReferences
{
    public class ShipType
    {
        //private:
        private static ShipType _smallShip;
        private static ShipType _mediumShip;
        private static ShipType _bigShip;

        private Int32 _id;
        private Int32 _speed;
        private Int32 _basedamage;
        
        private ShipType(Int32 speed_, Int32 basedamage_, Int32 id_)
        {
            _id = id_;
            _speed = speed_;
            _basedamage = basedamage_; 
        }

        //public:
        public static ShipType SmallShip
        {
            get { return _smallShip ?? (_smallShip = new ShipType(2, 10, 0)); }
        }
        public static ShipType MediumShip
        {
            get { return _mediumShip ?? (_mediumShip = new ShipType(4, 20, 1)); }
        }
        public static ShipType BigShip
        {
            get { return _bigShip ?? (_bigShip = new ShipType(8, 50, 2)); }
        }

        public Int32 id { get { return _id; } }
        public Int32 speed { get { return _speed; } }
        public Int32 basedamage { get { return _basedamage; } }

    }

}
