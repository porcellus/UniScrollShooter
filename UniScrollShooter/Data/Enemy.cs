using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.FixedReferences;

namespace Data
{
    public class Enemy
    {
        public Enemy(int kind_, Double x, Double y, Int32 widht_, Int32 height_)
        {
            health = 100;
            posX = x;
            posY = y;
            width = widht_;
            height = height_;
            vx = -0.5;
            vy = 0;
            switch (kind_)
            {
                case 0:
                    {
                        type = EnemyType.Small;
                        break;
                    }
                case 1:
                    {
                        type = EnemyType.Medium;
                        break;
                    }
                default:
                    {
                        type = EnemyType.Big;
                        break;
                    }
            }
        }

        public Double health { get; set; }
        public Double posX { get; set; }
        public Double posY { get; set; }
        public Int32 width { get; set; }
        public Int32 height { get; set; }
        //irányvektor
        private Double vx;
        private Double vy;

        public EnemyType type;

        public void Move()
        {
            posX += type.speed * vx;
            posX += type.speed * vy;
        }
    }
}
