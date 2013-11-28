using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class Bullet : ObjectBase
    {
        //private:
        public Boolean _active; 
        private float _speed;

        private Double _vx;  // irányvektor
        private Double _vy;

        //public:
        public Bullet(Double x, Double y, Int32 width_, Int32 height_, Int32 damage_)
        {
            posX = x;
            posY = y;
            width = width_;
            height = height_;
            damage = damage_;
            _active = true;

            _vx = 1; _vy = 0;
            _speed = 20f;
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
