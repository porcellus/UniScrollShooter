using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.FixedReferences;

using Position = System.Tuple<double, double>;

namespace Data
{
    /// <summary>
    /// Ship with attributes that depend on current ship
    /// </summary>
    class Ship
    {
        public Ship()
        {
            Hp = 100;
            Shield = 100;
            Type = ShipType.BigShip;
            //Modules = new List<Module>();
        }

        /// <summary>
        /// Handle damages, first decrease shield to 0, after health
        /// </summary>
        /// <param name="x">damage</param>
        public void DoDamage(int x)
        {
            if (Shield >= x)
                Shield -= x;
            else
            {
                Hp -= Shield;
                Shield = 0;
            }
        }
        /// <summary>
        /// Add more healt or shield, first increase health
        /// </summary>
        /// <param name="x">Value</param>
        public void IncreaseDurability(int x)
        {
            if (Hp + x > 100)
            {
                Shield += Hp + x - 100;
                Hp = 100;
            }
            else
                Hp += x;
        }

        public Int32 Hp { get; private set; }
        public Int32 Shield { get; private set; }

        public ShipType Type { get; set; }
        //public List<Modules> Modules { get; set; }
        public Position Pos { get; set; }
    }

}
