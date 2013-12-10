using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.FixedReferences;

namespace Data
{
    public sealed class Enemy : ObjectBase
    {
        //private:
        private Double _vx;         //irányvektor
        private Double _vy;
        public EnemyType Type { get; private set; }

        //public:
        public Enemy(Double x, Double y, EnemyType type)
        {
            Health = 100;
            PosX = x;
            PosY = y;
            _vx = -0.5;
            _vy = 0;
            Type = type;

            Damage = Type.damage;
        }
        
        public Int32 value
        {
            get { return Type.value; }
        }

        public void Move(double elapsedTime)
        {
            PosX += Type.speed * _vx * elapsedTime / 20;
            PosX += Type.speed * _vy * elapsedTime / 20;
        }
    }
}
