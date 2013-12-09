﻿using System;
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

        public Ship Ship
        {
            get { return _ship; }
            set { _ship = value; }
        }

        //public:
        public void Init(double x, double y, Int32 w, Int32 h)
        {
            _exp = 0;
            _expMultiplier = 1;
            _money = 0;
            _moneyMultiplier = 1;

            _ship = new Ship(w, h);
            _ship.PosX = x;
            _ship.PosY = y;
        }

        //interfész
        public Int32 Exp 
        { 
            get { return _exp; } 
            set { _exp = (int)(value*_expMultiplier); } 
        }
        
        public Int32 Money
        {
            get { return _money; }
            set { _money = (int)(value * _moneyMultiplier); }
        }    

        public Double PosX { get { return _ship.PosX; } }
        public Double PosY { get { return _ship.PosY; } }

        public Int32 Width { get { return _ship.Width; } }
        public Int32 Height { get { return _ship.Height; } }

        public Int32 Health
        { 
            get { return _ship.Health; }
            set { _ship.Health = value; }
        }

        public Int32 Shield 
        {
            get { return _ship.shield; }
            set { _ship.shield = value; }
        }

        public Int32 Damage 
        { 
            get { return _ship.Damage; }
            set { _ship.Damage = value; }
        }

        public void DamageOnShip(Int32 x)
        {
            _ship.DoDamage(x);
        }
        public BulletKind BulletKind { get { return _ship.bulletKind; } }

        public void SetPosition(Double x, Double y)
        {
            _ship.PosX = x;
            _ship.PosY = y;
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
