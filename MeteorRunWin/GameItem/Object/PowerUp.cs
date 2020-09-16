using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorRunWin.GameItem.Object
{
    public class PowerUp
    {
        private string _type;
        private int _x;
        private int _y;

        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public PowerUp(string type, int x, int y)
        {
            _type = type;
            _x = x;
            _y = y;
        }
    }
}
