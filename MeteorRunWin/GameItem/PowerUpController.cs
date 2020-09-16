using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorRunWin.GameItem
{
    public class PowerUpController
    {
        public States.GameState Game;
        public List<Object.PowerUp> boxes = new List<Object.PowerUp>();
        private string[] Types = new string[] { "i", "H", "H", "G", "G", "A", "A", "A", "A", "E", "M" };

        public string[] box = new string[]
        {
            " _____",
            "|     |",
            "|  0  |",
            "|_____|"
        };

        public PowerUpController(States.GameState game)
        {
            Game = game;
        }


        public void DownAllBoxes()
        {
            for (int i = 0; i <= boxes.Count - 1; i++)
            {
                string T = boxes[i].Type;
                int X = boxes[i].X;
                int Y = boxes[i].Y + 1;

                boxes[i] = new Object.PowerUp(T, X, Y);
            }
        }

        public Random rnd = new Random();

        public void AddBox()
        {
            if (rnd.Next(0, (Game.Health * 13)) == 1)
            {
                boxes.Add(new Object.PowerUp(Types[rnd.Next(0, Types.Length)], rnd.Next(1, (int)Enums.Ratio.Width - box[0].Length), -5));
            }
        }

        public void DrawBox()
        {
            for (int i = 0; i <= boxes.Count - 1; i++)
            {
                for (int ii = 0; ii <= 3; ii++)
                {
                    for (int iii = 0; iii <= 6; iii++)
                    {
                        if (boxes[i].Y + ii < (int)Enums.Ratio.Height && boxes[i].Y + ii > 1)
                        {
                            if (Utilities.CliRenderer.view[boxes[i].Y + ii][boxes[i].X + iii] == '-')
                            {
                                switch (boxes[i].Type)
                                {
                                    case "i":
                                        Game.Invincible++;
                                        Game.InvincibleDuration = 250;
                                        break;
                                    case "H":
                                        Game.Health += 5;
                                        break;
                                    case "A":
                                        Game.Ammo += 50;
                                        break;
                                    case "M":
                                        Game.meteor.Meteors.Clear();
                                        Game.Meteors++;
                                        Game.MeteorDuration = 500;
                                        break;
                                    case "E":
                                        Game.enemies.Enemies.Clear();
                                        Game.enemies.EnemyBullets.Clear();
                                        break;
                                    case "G":
                                        Game.SpecialGun = 1;
                                        Game.SpecialGunDuration = 100;
                                        break;
                                }

                                boxes.RemoveAt(i);

                                goto End;

                            }
                        }
                    }
                }
            }

            End:

            for (int i = 0; i <= boxes.Count - 1; i++)
            {
                for (int y = 0; y <= box.Length - 1; y++)
                {
                    string Type = boxes[i].Type;
                    int LocationX = boxes[i].X;
                    int LocationY = boxes[i].Y;
                    string[] CurrentBox = new string[4];

                    Array.Copy(box, CurrentBox, 4);
                    CurrentBox[2] = CurrentBox[2].Replace('0', Type.ToCharArray()[0]);

                    if (LocationY + y < (int)Enums.Ratio.Height && LocationY + y >= 0)
                    {
                        StringBuilder sb = new StringBuilder(Utilities.CliRenderer.view[LocationY + y]);

                        for (int z = 0; z <= CurrentBox[y].Length - 1; z++)
                        {
                            sb[LocationX + z] = CurrentBox[y].ToCharArray()[z];
                        }

                        Utilities.CliRenderer.view[LocationY + y] = sb.ToString();
                    }
                }

                if (boxes[i].Y >= (int)Enums.Ratio.Height)
                {
                    boxes.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
