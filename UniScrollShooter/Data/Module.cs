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

        //public:
        public Module(ModuleKind kind, Int32 size)
        {
            _kind = kind;
            switch (size)
            {
                case 0:
                    _type = ModuleType.Small;
                    break;
                case 1:
                    _type = ModuleType.Medium;
                    break;
                case 2:
                    _type = ModuleType.Big;
                    break;
                default:
                    _type = ModuleType.Small;
                    break;
            }
            
        }

        public Int32 bonus { get { return _type.bonus; } }
        public ModuleKind kind { get { return _kind; } }
    }
}
