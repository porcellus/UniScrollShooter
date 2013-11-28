using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.FixedReferences;

namespace Data
{
    public enum BulletKind
    {
        Laser, Exploded, Rocket
    }

    public class Bullet : ObjectBase
    {
        //private:
        public Boolean _active; 
        private Double _speed;
        private BulletType _type;
        private Double _vx;  // irányvektor
        private Double _vy;

        //public:
        public Bullet(Double x_, Double y_, Int32 width_, Int32 height_, Int32 damage_, BulletKind kind_)
        {
            posX = x_;
            posY = y_;
            width = width_;
            height = height_;
            switch (kind_)
            {
                case BulletKind.Laser:
                    {
                        _type = BulletType.Laser;
                        break;
                    }
                case BulletKind.Exploded:
                    {
                        _type = BulletType.Exploded;
                        break;
                    }
                case BulletKind.Rocket:
                    {
                        _type = BulletType.Rocket;
                        break;
                    }
            }
            damage = damage_+_type._bonusdamage;
            _active = true;

            _vx = 1; _vy = 0;
            _speed = _type.speed;
        }

        public void Move(double elapsedTime)
        {
            posX += _vx * _speed * elapsedTime / 20;
            posY += _vy * _speed * elapsedTime / 20;
        }

        public Boolean active
        {
            get { return _active; }
            set { _active = value; }
        }

        public Double vx
        {
            get { return _vx; }
            set { _vx = value; }
        }

        public Double vy
        {
            get { return _vy; }
            set { _vy = value; }
        }
    }
}
