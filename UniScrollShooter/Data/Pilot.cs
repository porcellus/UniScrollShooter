using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// Represent an atribute of pilot, (name, value) pair
//using Attribute = System.Tuple<System.String, System.Int32>;

namespace Data
{
    class Pilot
    {
        public Pilot()
        {
            _exp = 0;
            _money = 0;
            _ship = new Ship();
        }

        /// <summary>
        /// Experience
        /// </summary>
        private Int32 _exp 
        {
            get { return _exp; }
            set { _exp += value; }
        }

        /// <summary>
        /// Money
        /// </summary>
        private Int32 _money
        {
            get { return _money; }
            set { _money += value; }
        }

        /// <summary>
        /// Add new attribute to the pilot
        /// </summary>
        /// <param name="name">name of attribute</param>
        /// <param name="value">initialize value</param>
        public void AddNewAttribute(String name, Int32 value)
        {
            _attrs.Add(name, value);
        }
        /// <summary>
        /// Increase(+) or decrease(-) a value of attribute of pilot
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="value">value to change</param>
        public void ModifyAttributeValue(String name, Int32 value)
        {
            _attrs[name] += value;
        }

        /// <summary>
        /// Attributes (key, value) pairs for fast accessing and extending
        /// </summary>
        private Dictionary<String, Int32> _attrs { get; set; }
        private Ship _ship;

        /// <summary>
        /// The ship of pilot is dead or not
        /// </summary>
        /// <returns></returns>
        public bool isAlive()
        {
            return _ship.Hp != 0;
        }

    }
}
