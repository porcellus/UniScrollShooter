﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.FixedReferences;

namespace Data
{
    /// <summary>
    /// Ship with attributes that depend on current ship
    /// </summary>
    class Ship : ObjectBase
    {
        //private:
        private Int32 _shield;
        private ShipType _type;

        //public:
        public Ship(Int32 width_, Int32 height_)
        {
            health = 100;
            _shield = 100;
            _type = ShipType.BigShip;
            width = width_;
            height = height_;
            damage = _type.basedamage;
        }

        public void DoDamage(int x)
        {
            if (_shield >= x)
                _shield -= x;
            else
            {
                health -= x-_shield;
                _shield = 0;
            }
        }

        public void IncreaseDurability(int x)
        {
            if (health + x > 100)
            {
                _shield += health + x - 100;
                health = 100;
            }
            else
                health += x;
        }
     
        public Int32 shield
        {
            get { return _shield; }
        }
        
    }

}
