using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class Pilot
    {
        //private:
        private Int32 _exp;
        private Int32 _money;
        private Ship _ship;

        //public:
        public void Init(double x, double y, Int32 w, Int32 h)
        {
            _exp = 0;
            _money = 0;

            _ship = new Ship(w, h);
            _ship.posX = x;
            _ship.posY = y;
        }

        //interfész
        public Int32 exp 
        { 
            get { return _exp; } 
            set { _exp = value; } 
        }
        
        public Int32 money
        {
            get { return _money; }
            set { _money = value; }
        }    

        public Double posX { get { return _ship.posX; } }
        public Double posY { get { return _ship.posY; } }

        public Int32 width { get { return _ship.width; } }
        public Int32 height { get { return _ship.height; } }

        public Double health { get { return _ship.health; } }
        public Int32 damage { get { return _ship.damage; } }
        public void DamageOnShip(Int32 x)
        {
            _ship.DoDamage(x);
        }
        public BulletKind bulletKind { get { return _ship.bulletKind; } }

        public void setPosition(Double x, Double y)
        {
            _ship.posX = x;
            _ship.posY = y;
        }
    }
}
