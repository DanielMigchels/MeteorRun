using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorRunWin.GameItem
{
    public class BulletController
    {
        public int ShootDelay = 0;
        public List<Object.Bullet> Bullets = new List<Object.Bullet>();
        public States.GameState Game;

        public BulletController(States.GameState game)
        {
            Game = game;
        }

        public void UpAllBullets()
        {
            for (int i = 0; i <= Bullets.Count - 1; i++)
            {
                int X = Bullets[i].X;
                int Y = Bullets[i].Y - 2;

                Bullets[i] = new Object.Bullet(X, Y);
            }
        }

        public void Shoot()
        {
            if (ShootDelay == 0)
            {
                Bullets.Add(new Object.Bullet(Game.spaceship.X - 3, Game.spaceship.Y - 1));

                Game.Ammo--;
            }
            ShootDelay++;
        }

        public void RemoveHighestBullet()
        {
            int highestbullet = 0;

            for (int i = 0; i <= Bullets.Count - 1; i++)
            {
                if (Bullets[i].Y < highestbullet)
                {
                    highestbullet = i;
                }
            }

            if (Bullets.Count != 0)
            {
                Bullets.RemoveAt(highestbullet);
            }

        }

        public void DrawBullets()
        {
            if (Game.SpecialGun != 0)
            {
                Bullets.Clear();
                for (int i = 1; i <= (Game.spaceship.Y - 1); i++)
                {
                    Bullets.Add(new Object.Bullet(Game.spaceship.X - 3, Game.spaceship.Y - i));
                }
            }

            for (int i = 0; i <= Bullets.Count - 1; i++)
            {
                if (Bullets[i].Y <= 0)
                {
                    Bullets.RemoveAt(i);
                    break;
                }

                StringBuilder sb = new StringBuilder(Utilities.CliRenderer.view[Bullets[i].Y - 1]);
                sb[Bullets[i].X + 6] = '0';
                Utilities.CliRenderer.view[Bullets[i].Y - 1] = sb.ToString();
            }

        }
    }
}
