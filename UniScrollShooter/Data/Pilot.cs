using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.FixedReferences;

namespace Data
{
    public class Pilot
    {
        //private:
        private Int32 _exp;
        private Double _expMultiplier;
        private Int32 _money;
        private Double _moneyMultiplier;
        private Ship _ship;
        private int _stat1;
        private int _stat2;
        private int _stat3;

        public Ship Ship
        {
            get { return _ship; }
            set { _ship = value; }
        }

        public int stat1
        {
            get { return _stat1; }
            set { _stat1 = value; }
        }

        public int stat2
        {
            get { return _stat2; }
            set { _stat2 = value; }
        }

        public int stat3
        {
            get { return _stat3; }
            set { _stat3 = value; }
        }

        //public:
        public void Init(double x, double y, Int32 w, Int32 h)
        {
            _exp = 0;
            _expMultiplier = 1;
            _money = 0;
            _moneyMultiplier = 1;

            _stat1 = 10;
            _stat2 = 10;
            _stat3 = 10;

            _ship = new Ship(w, h);
            _ship.posX = x;
            _ship.posY = y;
        }

        //interfész
        public Int32 exp 
        { 
            get { return _exp; } 
            set { _exp = (int)(value*_expMultiplier); } 
        }
        
        public Int32 money
        {
            get { return _money; }
            set { _money = (int)(value * _moneyMultiplier); }
        }    

        public Double posX { get { return _ship.posX; } }
        public Double posY { get { return _ship.posY; } }

        public Int32 width { get { return _ship.width; } }
        public Int32 height { get { return _ship.height; } }

        public Double health { get { return _ship.health; } }
        public Double shield { get { return _ship.shield; } }
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

        //pilóta: fejlesztéshez
        public void IncreaseExpMultiplier(Double x)
        {
            _expMultiplier += x;
        }

        public void IncreaseMoneyMultiplier(Double x)
        {
            _moneyMultiplier += x;
        }
        
        //hajó: fejlesztés elérés
        public Boolean UpgradeShip()
        {
            return _ship.UpgradeShip();
        }

        public void UpgradeShieldOfShip(Int32 x)
        {
            _ship.UpgradeShield(x);
        }

        public void UpgradeHealthOfShip(Int32 x)
        {
            _ship.UpgradeHealth(x);
        }

        public void UpgradeBulletOfShip(BulletKind x)
        {
            _ship.UpgradeBullet(x);
        }

        public void UpgradeDamageOfShip(Int32 x)
        {
            _ship.UpgradeDamage(x);
        }

        public void AddModuleOfShip(ModuleKind k, Int32 size)
        {
            _ship.AddModule(k, size);
        }

        public void RemoveModuleOfShip(ModuleKind k)
        {
            _ship.RemoveModul(k);
        }
    }
}
