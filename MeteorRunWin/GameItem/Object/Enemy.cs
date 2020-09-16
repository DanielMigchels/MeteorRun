using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorRunWin.GameItem.Object
{
    public class Enemy
    {
        private int _x;
        private int _y;
        private bool _drop;

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

        public bool Drop
        {
            get
            {
                return _drop;
            }
            set
            {
                _drop = value;
            }
        }

        public Enemy(int x, int y, bool drop)
        {
            _drop = drop;
            _y = y;
            _x = x;

        }
    }
}
