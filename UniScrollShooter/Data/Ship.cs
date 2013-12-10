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

        private void RecalculateModuleEffect(ModuleKind k)
        {
            Module m = _modules[k];
            switch (k)
            {
                case ModuleKind.Shield:
                    _shield += (int)(_shieldDEFAULT * (m.bonus / 100));
                    break;
                case ModuleKind.Gun:
                    Damage = (int)(_type.basedamage * (m.bonus / 100));
                    break;
                case ModuleKind.Core:
                    Health = (int)(_healthDEFAULT * (m.bonus / 100));
                    break;
            }
        }
        
        private void RemoveModuleEffect(ModuleKind k)
        {
            Module m = _modules[k];
            switch (k)
            {
                case ModuleKind.Shield:
                    _shield -= (int)(_shieldDEFAULT * (m.bonus / 100));
                    break;
                case ModuleKind.Gun:
                    Damage -= (int)(_type.basedamage * (m.bonus / 100));
                    break;
                case ModuleKind.Core:
                    Health -= (int)(_healthDEFAULT * (m.bonus / 100));
                    break;
            }
        }

    //public:
        public Ship(Int32 width_, Int32 height_)
        {
            Health = 100;
            _shield = 100;
            _type = ShipType.SmallShip;
            Width = width_;
            Height = height_;
            Damage = _type.basedamage;
            _bulletkind = BulletKind.Laser;
            _modules = new Dictionary<ModuleKind, Module>();

            foreach (ModuleKind val in Enum.GetValues(typeof(ModuleKind)))
            {
                _modules.Add(val, new Module(val));
            }
        }

        public void DoDamage(int x)
        {
            if (_shield >= x)
                _shield -= x;
            else
            {
                Health -= x-_shield;
                if (Health < 0)
                    Health = 0;
                _shield = 0;
            }
        }

        public void IncreaseDurability(int x)
        {
            if (Health + x > 100)
            {
                _shield += Health + x - 100;
                Health = 100;
            }
            else
                Health += x;
        }

        public Int32 shield { get { return _shield; } set { _shield = value; } }
        public BulletKind bulletKind {  get { return _bulletkind; } }

        //fejlesztéshez műveletek
        public Boolean UpgradeShip()
        {
            //true->sikeres fejlesztés, false->nem az
            Damage -= _type.basedamage;
            if (_type.id == 0)
                _type = ShipType.MediumShip;
            else
                if (_type.id == 1)
                    _type = ShipType.BigShip;
            else return false;
            Damage += _type.basedamage;

            //minden modul vissza 0-ra
            foreach (ModuleKind val in Enum.GetValues(typeof(ModuleKind)))
            {
                RemoveModuleEffect(val);
                _modules.Remove(val);
                _modules.Add(val, new Module(val));
            }

            return true;
        }

        public void UpgradeShield(Int32 x)
        {
            _shield += x;
        }

        public void UpgradeHealth(Int32 x)
        {
            Health += x;
        }

        public void UpgradeDamage(Int32 x)
        {
            Damage += x;
        }

        public Boolean UpgradeBullet()
        {
            switch (_bulletkind)
            {
                case BulletKind.Laser:
                    _bulletkind = BulletKind.Exploded;
                    break;
                case BulletKind.Exploded:
                    _bulletkind = BulletKind.Rocket;
                    break;
                default:
                    return false;
            }
            return true;
        }

        public Boolean UpgradeModule(ModuleKind k)
        {
            if(_modules[k].UpgradeModul())
            {
                RecalculateModuleEffect(k);
                return true;
            }
            return false;
        }

        /*public void DowngradeModule(ModuleKind k)
        {
            RemoveModuleEffect(k);
            _modules[k].DowngradeModul();
        }*/

        //modul megjelenítéshez lekérdezés
        public List<Module> GetModuleList()
        {
            return _modules.Values.ToList();
        }

        public Int32 GetShipLevel()
        {
            return _type.id;
        }

        public Int32 GetBulletLevel()
        {
            return (Int32)_bulletkind;
        }
    }

}
