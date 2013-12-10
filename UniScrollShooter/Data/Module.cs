using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.FixedReferences;

namespace Data
{
    public class Module
    {
        //private:
        ModuleType _type;
        private ModuleKind _kind;
        private Int32 _size;

        private void SetTypeBySize()
        {
            switch (_size)
            {
                case 0:
                    _type = null;
                    break;
                case 1:
                    _type = ModuleType.Small;
                    break;
                case 2:
                    _type = ModuleType.Medium;
                    break;
                case 3:
                    _type = ModuleType.Big;
                    break;
            }
        }

        //public:
        public Module(ModuleKind kind)
        {
            _kind = kind;
            _size = 0;
            SetTypeBySize();
        }

        public Int32 bonus { get { return _size!=0 ? _type.bonus : 0; } }
        public ModuleKind kind { get { return _kind; } }
        public Int32 size { get { return _size; } }

        public String Hint()
        {
            return "Increase the " + StringValueAttribute.GetStringValue(_kind) + " of ship by " + bonus + "% of base value.";
        }

        public Boolean UpgradeModul()
        {
            if (_size < 3)
            {
                ++_size;
                SetTypeBySize();
                return true;
            }
            return false;
        }

        /*public Boolean DowngradeModul()
        {
            if (_size > 0)
            {
                --_size;
                SetSize();
                return true;
            }
            return false;
        }*/
    }
}
