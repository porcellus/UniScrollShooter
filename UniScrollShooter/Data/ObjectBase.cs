using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class ObjectBase
    {
        //private:
        private Int32 _health;
        private Int32 _damage;
        private Double _posX;
        private Double _posY;
        private Int32 _width;
        private Int32 _height;

        //public:
        public Int32 health
        {
            get { return _health; }
            set { _health = value; }
        }
        public Int32 damage
        {
            get { return _damage; }
            set { _damage = value; }
        }
        public Double posX
        {
            get { return _posX; }
            set { _posX = value; }
        }
        public Double posY
        {
            get { return _posY; }
            set { _posY = value; }
        }
        public Int32 width
        {
            get { return _width; }
            set { _width = value; }
        }
        public Int32 height
        {
            get { return _height; }
            set { _height = value; }
        }
    }
}
