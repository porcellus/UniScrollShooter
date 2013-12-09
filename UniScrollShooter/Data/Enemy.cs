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
        private readonly EnemyType _type;

        //public:
        public Enemy(Double x, Double y, EnemyType type)
        {
            Health = 100;
            PosX = x;
            PosY = y;
            _vx = -0.5;
            _vy = 0;
            _type = type;

            Damage = _type.damage;
        }
        
        public Int32 value
        {
            get { return _type.value; }
        }

        public void Move(double elapsedTime)
        {
            PosX += _type.speed * _vx * elapsedTime / 20;
            PosX += _type.speed * _vy * elapsedTime / 20;
        }
    }
}
