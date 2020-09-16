using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorRunWin.Utilities
{
    internal enum KeyCode : int
    {
        None = int.MaxValue,

        W = 87,
        A = 65,
        S = 83,
        D = 68,
        Space = 32,
        Shift = 16,
        O = 79,
        Enter = 13,

        Left = 37,
        Up = 38,
        Right = 39,
        Down = 40
    }

    internal static class Keyboard
    {
        private const int KeyPressed = 0x8000;

        public static bool IsKeyDown(KeyCode key)
        {            
            return (GetKeyState((int)key) & KeyPressed) != 0;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetKeyState(int key);
    }
}
