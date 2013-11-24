using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.FixedReferences
{
    public class EnemyType
    {
        //remélem erre gondoltál
        private static EnemyType _small;
        private static EnemyType _medium;
        private static EnemyType _big;

        public static EnemyType Small
        {
            get { return _small ?? (_small = new EnemyType(1, 4, 10)); }
        }
        public static EnemyType Medium
        {
            get { return _medium ?? (_medium = new EnemyType(2, 10, 20)); }
        }
        public static EnemyType Big
        {
            get { return _big ?? (_big = new EnemyType(3, 25, 30)); }
        }

        public Double Speed { get; set; }
        public Int32 Damage { get; set; }
        public Int32 Value { get; set; }

        private EnemyType(Double speed, Int32 damage, Int32 value)
        {
            Speed = speed;
            Damage = damage;
            Value = value;
        }
    }
}
