using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.FixedReferences;

namespace Data
{
    public class Enemy
    {
        public Enemy(int kind, Double x, Double y, Int32 w, Int32 h)
        {
            health = 100;
            PosX = x;
            PosY = y;
            Width = w;
            Height = h;
            switch (kind)
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
        public Double PosX { get; set; }
        public Double PosY { get; set; }
        public Int32 Width { get; set; }
        public Int32 Height { get; set; }
        //irányvektor
        private Double vx = 0.5;
        private Double vy = 1.5;

        public EnemyType type;

        public void Move()
        {
            PosX += type.Speed * vx;
            PosX += type.Speed * vy;
        }
    }
}
