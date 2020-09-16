using MeteorRunWin.GameItem.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorRunWin.GameItem
{
    public class MeteorController
    {
        public List<Meteor> Meteors;
        public States.GameState Game;

        public MeteorController(States.GameState game)
        {
            Game = game;
            Meteors = new List<Meteor>();
        }

        public string[] ascii = new string[]
        {
            "   _..._   ",
            " .'   o `. ",
            ":    o    :",
            ": o     o :",
            "`.     o .'",
            "  `-...-'  "
        };

        public void DownAllMeteors()
        {
            for (int i = 0; i <= Meteors.Count - 1; i++)
            {
                int x = Meteors[i].X;
                int y = Meteors[i].Y + 1;

                Meteors[i] = new Object.Meteor(x, y);
            }
        }

        public Random rnd = new Random();

        public void AddMeteor()
        {
            Random random = new Random();

            if (random.Next(0, 7) == 1)
            {
                Meteors.Add(new Object.Meteor(rnd.Next(0, (int)Enums.Ratio.Width - ascii[0].Length), -7));
            }
        }

        public void AddMeteor(int x, int y)
        {
            Meteors.Add(new Object.Meteor(x, y));
        }

        public void DrawMeteor()
        {
            for (int i = 0; i <= Meteors.Count - 1; i++)
            {
                for (int ii = 0; ii <= 10; ii++)
                {
                    for (int iii = 0; iii <= 5; iii++)
                    {
                        if (Meteors[i].Y + iii < (int)Enums.Ratio.Height && Meteors[i].Y + iii > 1)
                        {
                            if (Utilities.CliRenderer.view[Meteors[i].Y + iii][Meteors[i].X + ii] == '-')
                            {
                                if (Game.Invincible == 0)
                                {
                                    Game.Health -= 4;
                                    Game.InvincibleDuration = 25;
                                }
                                Game.Invincible++;
                            }
                        }
                    }
                }

                for (int y = 0; y <= ascii.Length - 1; y++)
                {
                    int LocationY = Meteors[i].Y;
                    int LocationX = Meteors[i].X;

                    if (LocationY + y < (int)Enums.Ratio.Height && LocationY + y >= 0)
                    {
                        StringBuilder sb = new StringBuilder(Utilities.CliRenderer.view[LocationY + y]);

                        for (int z = 0; z <= ascii[y].Length - 1; z++)
                        {
                            sb[LocationX + z] = ascii[y].ToCharArray()[z];
                        }

                        Utilities.CliRenderer.view[LocationY + y] = sb.ToString();
                    }
                }

                if (Meteors[i].Y >= (int)Enums.Ratio.Height)
                {
                    Meteors.RemoveAt(i);
                }
            }
        }
    }
}
