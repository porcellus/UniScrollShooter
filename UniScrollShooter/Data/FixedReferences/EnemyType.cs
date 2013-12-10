using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.FixedReferences
{
    public class EnemyType
    {
        //private:
        private static EnemyType _small;
        private static EnemyType _medium;
        private static EnemyType _big;

        public Double _speed;
        public Int32 _damage;
        public Int32 _value;

        private EnemyType(Double speed_, Int32 damage_, Int32 value_, int height, int width)
        {
            _speed = speed_;
            _damage = damage_;
            _value = value_;
            Width = width;
            Height = height;
        }

        //public:
        public static EnemyType Small
        {
            get { return _small ?? (_small = new EnemyType(5, 4, 10, 70, 100)); }
        }
        public static EnemyType Medium
        {
            get { return _medium ?? (_medium = new EnemyType(10, 10, 20, 200, 200)); }
        }
        public static EnemyType Big
        {
            get { return _big ?? (_big = new EnemyType(15, 25, 30, 120, 120)); }
        }

        public Double speed { get { return _speed; } }
        public Int32 damage { get { return _damage; } }
        public Int32 value { get { return _value; } }
        public Int32 Height { get; private set; }
        public Int32 Width { get; private set; }

    }
}
