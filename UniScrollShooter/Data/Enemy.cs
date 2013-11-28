using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.FixedReferences;

namespace Data
{
    public enum EnemyKind
    {
        Small, Medium, Big
    }

    public class Enemy : ObjectBase
    {
        //private:
        private Double _vx;         //irányvektor
        private Double _vy;
        private EnemyType _type;

        //public:
        public Enemy(Double x, Double y, Int32 width_, Int32 height_, EnemyKind kind_)
        {
            health = 100;
            posX = x;
            posY = y;
            width = width_;
            height = height_;
            _vx = -0.5;
            _vy = 0;
            switch (kind_)
            {
                case EnemyKind.Small:
                    {
                        _type = EnemyType.Small;
                        break;
                    }
                case EnemyKind.Medium:
                    {
                        _type = EnemyType.Medium;
                        break;
                    }
                case EnemyKind.Big:
                    {
                        _type = EnemyType.Big;
                        break;
                    }
            }
            damage = _type.damage;
        }

        public Int32 value
        {
            get { return _type.value; }
        }

        public void Move(double elapsedTime)
        {
            posX += _type.speed * _vx * elapsedTime / 20;
            posX += _type.speed * _vy * elapsedTime / 20;
        }
    }
}
