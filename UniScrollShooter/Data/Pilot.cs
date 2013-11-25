using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class Pilot
    {
        public void Init(double x, double y, Int32 w, Int32 h)
        {
            exp = 0;
            money = 0;

            ship = new Ship(w, h);
            ship.posX = x;
            ship.posY = y;
        }

        public Int32 exp { get; set; }
        private Int32 money { get; set; }
        private Dictionary<String, Int32> _attrs { get; set; }
        public Ship ship;
       
        public void AddNewAttribute(String name, Int32 value)
        {
            _attrs.Add(name, value);
        }

        public void ModifyAttributeValue(String name, Int32 value)
        {
            _attrs[name] += value;
        }

        public Int32 getAttribute(String name)
        {
            return _attrs[name];
        }

        public void setPosition(Double x, Double y)
        {
            ship.posX = x;
            ship.posY = y;
        }

    }
}
