using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeteorRunWin.States;
using MeteorRunWin.GameItem;

namespace MeteorRunWin.States
{
    public class GameState
    {
        public string name = "";
        public bool alive;
        public int Score;
        public int Health;
        public int Ammo;

        public int Invincible;
        public int InvincibleDuration = 100;

        public int Meteors;
        public int MeteorDuration = 100;

        public int SpecialGun;
        public int SpecialGunDuration = 100;

        public int UpOrDown;
        public int LeftOrRight;
        public bool Shoot;

        public bool Bossfight;
        public int BossTimer;
        public bool IncreaseBossTimer;

        public SpaceShipController spaceship;
        public PowerUpController boxes;
        public EnemyController enemies;
        public MeteorController meteor;
        public StarController stars;
        public BulletController bullet;
        public BossFightController bossfight;

        private System.Windows.Forms.Timer timer;

        public GameState()
        {
            name = "";
            IncreaseBossTimer = false;
            BossTimer = 0;
            Bossfight = false;
            SpecialGun = 0;
            UpOrDown = 1;
            LeftOrRight = 1;
            Shoot = false;
            Health = 5;
            Score = 0;
            Invincible = 0;
            Ammo = 100;
            Meteors = 0;
            alive = true;

            spaceship = new SpaceShipController(this);
            boxes = new PowerUpController(this);
            enemies = new EnemyController(this);
            meteor = new MeteorController(this);
            stars = new StarController(this);
            bullet = new BulletController(this);
            bossfight = new BossFightController(this);
        }

        public void Run()
        {
            Utilities.CliRenderer.BackColor = System.Drawing.Color.Black;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 25;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int multiplier = 1;
            Utilities.CliRenderer.view = Enumerable.Repeat(string.Empty.PadLeft((int)Enums.Ratio.Width), (int)Enums.Ratio.Height).ToArray();

            if (Utilities.Keyboard.IsKeyDown(Utilities.KeyCode.O))
            {
                ShopState shop = new ShopState();
                shop.showShop();
            }

            if (Utilities.Keyboard.IsKeyDown(Utilities.KeyCode.W) && Utilities.Keyboard.IsKeyDown(Utilities.KeyCode.S))
            {
                UpOrDown = 1;
            }
            else if (Utilities.Keyboard.IsKeyDown(Utilities.KeyCode.W))
            {
                UpOrDown = 2;
            }
            else if (Utilities.Keyboard.IsKeyDown(Utilities.KeyCode.S))
            {
                UpOrDown = 0;
            }

            if (Utilities.Keyboard.IsKeyDown(Utilities.KeyCode.D) && Utilities.Keyboard.IsKeyDown(Utilities.KeyCode.A))
            {
                UpOrDown = 1;
            }
            else if (Utilities.Keyboard.IsKeyDown(Utilities.KeyCode.D))
            {
                LeftOrRight = 2;
            }
            else if (Utilities.Keyboard.IsKeyDown(Utilities.KeyCode.A))
            {
                LeftOrRight = 0;
            }

            if (Utilities.Keyboard.IsKeyDown(Utilities.KeyCode.O))
            {
                Score = 74;
            }

            if (Utilities.Keyboard.IsKeyDown(Utilities.KeyCode.Space))
            {
                Shoot = true;
            }

            if (Utilities.Keyboard.IsKeyDown(Utilities.KeyCode.Shift))
            {
                multiplier = 2;
            }
            else
            {
                multiplier = 1;
            }

            if (Shoot)
            {
                if (Ammo != 0)
                {
                    bullet.Shoot();
                }
                Shoot = false;
            }
            if (UpOrDown == 0)
            {
                UpOrDown = 1;

                if (spaceship.Y <= (int)Enums.Ratio.Height - spaceship.ship.Length - 3)
                    spaceship.Y += (1 * multiplier);
            }
            if (UpOrDown == 2)
            {
                UpOrDown = 1;

                if (spaceship.Y >= 2)
                    spaceship.Y -= (1 * multiplier);

            }
            if (LeftOrRight == 0)
            {
                LeftOrRight = 1;

                if (spaceship.X >= 5)
                    spaceship.X -= (2 * multiplier);
            }
            if (LeftOrRight == 2)
            {
                LeftOrRight = 1;
                if (spaceship.X <= (int)Enums.Ratio.Width - spaceship.ship[1].Length - 5)
                    spaceship.X += (2 * multiplier);
            }



            if (Score % 75 == 0 && Score != 0)
            {
                Bossfight = true;
            }

            stars.AddStar();
            stars.DownAllStars();
            stars.DrawStar();

            bullet.UpAllBullets();
            bullet.DrawBullets();


            if (Bossfight)
            {
                if (Invincible == 0)
                {
                    spaceship.DrawShip();
                }
                else if (Invincible % 2 != 0)
                {
                    spaceship.DrawShip();
                    if (Invincible >= InvincibleDuration)
                    {
                        Invincible = 0;
                    }
                }
                else
                {
                }

                bossfight.RunBossScript();
            }

            enemies.Shoot();
            enemies.DrawBullets();
            enemies.DownAllBullets();
            enemies.DownAllEnemies();

            if (Invincible == 0)
            {
                spaceship.DrawShip();
            }
            else if (Invincible % 2 != 0)
            {
                spaceship.DrawShip();
                Invincible++;
                if (Invincible >= InvincibleDuration)
                {
                    Invincible = 0;
                }
            }
            else
            {
                Invincible++;
            }



            if (!Bossfight)
            {
                boxes.AddBox();
            }
            boxes.DownAllBoxes();
            boxes.DrawBox();

            if (Meteors == 0)
            {
                if (!Bossfight)
                {
                    meteor.AddMeteor();
                    meteor.DownAllMeteors();
                }
                meteor.DrawMeteor();
            }
            else
            {
                Meteors++;
                if (Meteors >= MeteorDuration)
                {
                    Meteors = 0;
                }
            }

            if (bullet.ShootDelay != 0)
            {
                bullet.ShootDelay++;
                if (bullet.ShootDelay >= 15)
                {
                    bullet.ShootDelay = 0;
                }
            }

            if (SpecialGun != 0)
            {
                SpecialGun++;
                if (SpecialGun >= SpecialGunDuration)
                {
                    SpecialGun = 0;
                    bullet.Bullets.Clear();
                }
            }


            if (!Bossfight)
            {
                enemies.AddEnemy();
            }
            enemies.AI();
            enemies.DrawEnemies();

            string scoreboard = "Score: " + Score + "    Ammo: " + Ammo + "     Health: ";

            for (int z = 0; z <= Health - 1; z++)
            {
                scoreboard += "[]";
            }

            if (Bossfight)
            {
                scoreboard += "    Boss HP: " + bossfight.BossHealth;
            }

            if (Invincible != 0)
            {
                scoreboard += "    Invincibility";
            }
            if (Meteors != 0)
            {
                scoreboard += "    Meteor Free";
            }
            if (SpecialGun != 0)
            {
                scoreboard += "    Laser Activated";
            }

            if (Health <= 0)
            {
                alive = false;
            }

            StringBuilder sb = new StringBuilder(Utilities.CliRenderer.view[0]);

            for (int z = 0; z <= scoreboard.Length - 1; z++)
            {
                sb[z] = scoreboard.ToCharArray()[z];
            }

            Utilities.CliRenderer.view[0] = sb.ToString();

            Utilities.CliRenderer.Render();

            if (!alive)
            {
                timer.Stop();
                Main.highscore = new HighscoreState();
                Main.highscore.Input();
                
                timer.Stop();
                Utilities.CliRenderer.view = Enumerable.Repeat(string.Empty.PadLeft((int)Enums.Ratio.Width), (int)Enums.Ratio.Height).ToArray();
                Utilities.CliRenderer.Render();

                Main.menu = new MenuState();

                Main.menu.ShowMenu();
            }
        }
    }
}
