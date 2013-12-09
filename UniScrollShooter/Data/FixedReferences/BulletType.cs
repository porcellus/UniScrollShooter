using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.FixedReferences
{
    public class BulletType
    {
        //private:
        private static BulletType _laser;
        private static BulletType _exploded;
        private static BulletType _rocket;

        public Double _speed;
        public Int32 _bonusdamage;

        private BulletType(Double speed_, Int32 bdamage_)
        {
            _speed = speed_;
            _bonusdamage = bdamage_;
        }

        //public:
        public static BulletType Laser
        {
            get { return _laser ?? (_laser = new BulletType(15, 15)); }
        }
        public static BulletType Exploded
        {
            get { return _exploded ?? (_exploded = new BulletType(20, 10)); }
        }
        public static BulletType Rocket
        {
            get { return _rocket ?? (_rocket = new BulletType(22, 2)); }
        }

        public Double speed { get { return _speed; } }
        public Int32 bonusdamage { get { return _bonusdamage; } }
    }
}
