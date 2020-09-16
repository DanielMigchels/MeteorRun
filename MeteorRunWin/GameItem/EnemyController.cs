using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorRunWin.GameItem
{
    public class EnemyController
    {
        public List<Object.Enemy> Enemies = new List<Object.Enemy>();
        public List<Object.Bullet> EnemyBullets = new List<Object.Bullet>();
        public States.GameState Game;

        public EnemyController(States.GameState game)
        {
            Game = game;
        }

        public string[] ship1 = new string[]
        {
            "|  _  |",
            "|=(_)=|",
            "|     |"
        };
        public string[] ship2 = new string[]
        {
            " /  _  \\ ",
            "|-=(_)=-|",
            " \\     / "
        };

        public void AddEnemy()
        {
            Random random = new Random();

            if (random.Next(0, 20) == 1)
            {
                Enemies.Add(new Object.Enemy(random.Next(5, (int)Enums.Ratio.Width - ship1[0].Length), -5, false));
            }
        }

        public void DownAllEnemies()
        {
            Random random = new Random();

            for (int i = 0; i <= Enemies.Count - 1; i++)
            {
                if (!Enemies[i].Drop)
                {
                    int x = Enemies[i].X;
                    int y = Enemies[i].Y + 1;

                    if (y == 15)
                    {
                        Enemies[i] = new Object.Enemy(x, y, false);
                    }
                    else
                    {
                        Enemies[i] = new Object.Enemy(x, y, true);
                    }
                }
            }
        }

        public void AddEnemy(bool force)
        {
            Random random = new Random();

            Enemies.Add(new Object.Enemy(random.Next(3, (int)Enums.Ratio.Width - ship1[0].Length), random.Next(10, 16), false));
        }

        public void DrawEnemies()
        {
            for (int i = 0; i <= Enemies.Count - 1; i++)
            {
                for (int ii = 0; ii <= 2; ii++)
                {
                    for (int iii = 0; iii <= 7; iii++)
                    {
                        if (Enemies[i].Y + ii < (int)Enums.Ratio.Height && Enemies[i].Y + ii > 1)
                        {
                            if (Utilities.CliRenderer.view[Enemies[i].Y + ii][Enemies[i].X + iii] == '0')
                            {
                                Enemies.RemoveAt(i);
                                if (!Game.Bossfight)
                                {
                                    Game.Score++;
                                }

                                goto End;
                            }
                        }
                    }
                }
            }

            End:

            for (int i = 0; i <= Enemies.Count - 1; i++)
            {
                for (int y = 0; y <= ship1.Length - 1; y++)
                {
                    int LocationY = Enemies[i].Y;
                    int LocationX = Enemies[i].X;

                    if (LocationY + y < (int)Enums.Ratio.Height && LocationY + y >= 0)
                    {
                        StringBuilder sb = new StringBuilder(Utilities.CliRenderer.view[LocationY + y]);

                        for (int z = 0; z <= ship1[y].Length - 1; z++)
                        {
                            sb[LocationX + z] = ship1[y].ToCharArray()[z];
                        }

                        Utilities.CliRenderer.view[LocationY + y] = sb.ToString();
                    }
                }
            }
        }

        public void AI()
        {
            Random random = new Random();

            int NewY;
            int NewX;

            for (int i = 0; i <= Enemies.Count - 1; i++)
            {
                switch (random.Next(0, 7))
                {
                    case 1:
                        NewY = Enemies[i].Y + 1;

                        if (NewY <= 30)
                        {
                            Enemies[i] = new Object.Enemy(Enemies[i].X, NewY, Enemies[i].Drop);
                        }
                        break;
                    case 2:
                        NewY = Enemies[i].Y - 1;

                        if (NewY >= 5 && !Game.Bossfight)
                        {
                            Enemies[i] = new Object.Enemy(Enemies[i].X, NewY, Enemies[i].Drop);
                        }
                        break;
                    case 3:
                        NewX = Enemies[i].X - 1;

                        if (NewX >= 5)
                        {
                            Enemies[i] = new Object.Enemy(NewX, Enemies[i].Y, Enemies[i].Drop);
                        }
                        break;
                    case 4:
                        NewX = Enemies[i].X + 1;
                        if (NewX <= 150)
                        {
                            Enemies[i] = new Object.Enemy(NewX, Enemies[i].Y, Enemies[i].Drop);
                        }
                        break;
                    default:

                        break;
                }
            }
        }

        public void Shoot()
        {
            Random random = new Random();

            for (int i = 0; i <= Enemies.Count - 1; i++)
            {
                if (random.Next(0, 40) == 3)
                {
                    EnemyBullets.Add(new Object.Bullet(Enemies[i].X - 3, Enemies[i].Y + 5));
                }
            }
        }

        public void Shoot(int x, int y)
        {
            EnemyBullets.Add(new Object.Bullet(x, y));
        }

        public void DownAllBullets()
        {
            for (int i = 0; i <= EnemyBullets.Count - 1; i++)
            {
                int x = EnemyBullets[i].X;
                int y = EnemyBullets[i].Y + 2;

                EnemyBullets[i] = new Object.Bullet(x, y);
            }
        }

        public void DrawBullets()
        {
            for (int i = 0; i <= EnemyBullets.Count - 1; i++)
            {
                if (EnemyBullets[i].Y < (int)Enums.Ratio.Height && EnemyBullets[i].Y >= 1)
                {
                    StringBuilder sb = new StringBuilder(Utilities.CliRenderer.view[EnemyBullets[i].Y - 1]);
                    sb[EnemyBullets[i].X + 6] = '0';
                    Utilities.CliRenderer.view[EnemyBullets[i].Y - 1] = sb.ToString();

                    if (EnemyBullets[i].Y >= (int)Enums.Ratio.Height)
                    {
                        EnemyBullets.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }
}
