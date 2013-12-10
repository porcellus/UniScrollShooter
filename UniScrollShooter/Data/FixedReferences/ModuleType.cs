using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.FixedReferences
{
    public class StringValueAttribute : System.Attribute
    {

        private string _value;

        public StringValueAttribute(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }

    }    
    
    public enum ModuleKind
    {
        [StringValue("shield")]
        Shield = 0,
        [StringValue("damage")]
        Gun = 1,
        [StringValue("health")]
        Core = 2
    }
    class ModuleType
    {
        //private:
        private static ModuleType _small;
        private static ModuleType _medium;
        private static ModuleType _big;

        private Int32 _bonuspercent;

        private ModuleType(Int32 bonus_)
        {
            _bonuspercent = bonus_;
        }

        //public:
        public static ModuleType Small
        {
            get { return _small ?? (_small = new ModuleType(10)); }
        }
        public static ModuleType Medium
        {
            get { return _medium ?? (_medium = new ModuleType(25)); }
        }
        public static ModuleType Big
        {
            get { return _big ?? (_big = new ModuleType(40)); }
        }

        public Int32 bonus { get { return _bonuspercent; } }

    }
}
