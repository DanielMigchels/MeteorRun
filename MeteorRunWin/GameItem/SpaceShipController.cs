using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorRunWin.GameItem
{
    public class SpaceShipController
    {
        public int X = 0;
        public int Y = 0;
        public States.GameState Game;

        public string[] ship = new string[]
        {
            " /| |\\  ",
            "|-|_|-|]",
            "|_   _|/",
            " \\___/  "
        };

        public SpaceShipController(States.GameState game)
        {
            Game = game;
            X = 100;
            Y = 35;
        }

        public void DrawShip()
        {
            for (int ii = 0; ii <= ship[1].Length; ii++)
            {
                for (int iii = 0; iii <= ship.Length - 1; iii++)
                {
                    if (Utilities.CliRenderer.view[Y + iii][X + ii] == '0')
                    {
                        if (Game.Invincible == 0)
                        {
                            Game.Health--;
                            Game.InvincibleDuration = 25;
                        }
                        Game.Invincible++;
                    }
                }
            }

            for (int i = 0; i <= ship.Length - 1; i++)
            {
                StringBuilder sb = new StringBuilder(Utilities.CliRenderer.view[Y + i]);

                for (int z = 0; z <= ship[i].Length - 1; z++)
                {
                    sb[X + z] = ship[i].ToCharArray()[z];
                }

                Utilities.CliRenderer.view[Y + i] = sb.ToString();
            }
        }
    }
}
