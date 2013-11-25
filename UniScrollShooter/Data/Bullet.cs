using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class Bullet
    {
        public Bullet(Double x, Double y, Int32 width_, Int32 height_, Int32 damage_)
        {
            posX = x;
            posY = y;
            width = width_;
            height = height_;
            damage = damage_;
            active = true;

            vx = 0; vy = 1;
            speed = 20f;
        }

        public void Move()
        {
            posX += vx*speed;
            posY += vy*speed;
        }

        public Int32 damage;
        public Double posX { get; set; }
        public Double posY { get; set; }
        public Int32 width { get; set; }
        public Int32 height { get; set; }
        public Boolean active { get; set; }
        // irányvektor
        public Double vx;
        public Double vy;

        private float speed;

    }
}
