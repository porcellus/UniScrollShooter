using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.FixedReferences;

namespace Data
{
    /// <summary>
    /// Ship with attributes that depend on current ship
    /// </summary>
    public class Ship
    {
        public Ship(Int32 w, Int32 h)
        {
            health = 100;
            shield = 100;
            type = ShipType.BigShip;
            width = w;
            height = h;
            //Modules = new List<Module>();
        }

        public void DoDamage(int x)
        {
            if (shield >= x)
                shield -= x;
            else
            {
                health -= x-shield;
                shield = 0;
            }
        }

        public void IncreaseDurability(int x)
        {
            if (health + x > 100)
            {
                shield += health + x - 100;
                health = 100;
            }
            else
                health += x;
        }

        public Int32 health { get; set; }
        public Int32 shield { get; set; }

        public ShipType type { get; set; }
        public Double posX { get; set; }
        public Double posY { get; set; }
        public Int32 width { get; set; }
        public Int32 height { get; set; }
        //public List<Modules> Modules { get; set; }
        
    }

}
