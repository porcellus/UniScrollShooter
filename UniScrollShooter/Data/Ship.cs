using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.FixedReferences;

namespace Data
{
    public class Ship : ObjectBase
    {
    //private:
        private const Int32 _shieldDEFAULT = 100;
        private const Int32 _healthDEFAULT = 100;

        private Int32 _shield;
        private ShipType _type;
        private BulletKind _bulletkind;
        private Dictionary<ModuleKind, Module> _modules;

        private void CommitModulEffect(ModuleKind k)
        {
            Module m = _modules[k];
            switch (k)
            {
                case ModuleKind.Shield:
                    _shield += (int)(_shieldDEFAULT * (m.bonus / 100));
                    break;
                case ModuleKind.Gun:
                    damage = (int)(_type.basedamage * (m.bonus / 100));
                    break;
                case ModuleKind.Core:
                    health = (int)(_healthDEFAULT * (m.bonus / 100));
                    break;
            }
        }
        
        private void RemoveModulEffect(ModuleKind k)
        {
            Module m = _modules[k];
            switch (k)
            {
                case ModuleKind.Shield:
                    _shield -= (int)(_shieldDEFAULT * (m.bonus / 100));
                    break;
                case ModuleKind.Gun:
                    damage -= (int)(_type.basedamage * (m.bonus / 100));
                    break;
                case ModuleKind.Core:
                    health -= (int)(_healthDEFAULT * (m.bonus / 100));
                    break;
            }
        }

    //public:
        public Ship(Int32 width_, Int32 height_)
        {
            health = 100;
            _shield = 100;
            _type = ShipType.SmallShip;
            width = width_;
            height = height_;
            damage = _type.basedamage;
            _bulletkind = BulletKind.Laser;
            _modules = new Dictionary<ModuleKind, Module>();
        }

        public void DoDamage(int x)
        {
            if (_shield >= x)
                _shield -= x;
            else
            {
                health -= x-_shield;
                if (health < 0)
                    health = 0;
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
     
        public Int32 shield { get { return _shield; } }
        public BulletKind bulletKind {  get { return _bulletkind; } }

        //fejlesztéshez műveletek
        public Boolean UpgradeShip()
        {
            //true->sikeres fejlesztés, false->nem az
            //ha fejlesztek, minden modul elveszik és a tulajdonságok frissül amit Shipekből nyerek
            damage -= _type.basedamage;
            if (_type.id == 0)
                _type = ShipType.MediumShip;
            else
                if (_type.id == 1)
                    _type = ShipType.BigShip;
            else return false;
            damage += _type.basedamage;

            foreach (ModuleKind mk in _modules.Keys)
                RemoveModulEffect(mk);
            _modules.Clear();
            
            
            return true;
        }

        public void UpgradeShield(Int32 x)
        {
            _shield += x;
        }

        public void UpgradeHealth(Int32 x)
        {
            health += x;
        }

        public void UpgradeBullet(BulletKind x)
        {
            _bulletkind = x;
        }

        public void UpgradeDamage(Int32 x)
        {
            damage += x;
        }

        public void AddModule(ModuleKind k, Int32 size)
        {
            RemoveModul(k);
            _modules.Add(k, new Module(k, size));
            CommitModulEffect(k);
        }

        public void RemoveModul(ModuleKind k)
        {
            if (_modules.ContainsKey(k))
            {
                RemoveModulEffect(k);
                _modules.Remove(k);
            }
        }

    }

}
