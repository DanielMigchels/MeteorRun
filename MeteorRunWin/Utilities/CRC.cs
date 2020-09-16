using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorRunWin.Utilities
{
    class CRC
    {
        public static byte Calculate(string input)
        {
            int total = 5;

            foreach (byte byteValue in Encoding.ASCII.GetBytes(input))
            {
                total += (byteValue * 8);
            }

            total = total * 4;
            total = total % 254;
            total = total >> 3;

            System.Diagnostics.Debug.WriteLine(total.ToString());

            return (byte)total;
        }
    }
}
