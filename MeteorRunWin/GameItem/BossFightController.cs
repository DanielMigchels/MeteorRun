using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorRunWin.States
{
    public class BossFightController
    {
        public string[] BossLogo = new string[]
        {
            "__________                      ___________.__       .__     __   ",
            "\\______   \\ ____  ______ ______ \\_   _____/|__| ____ |  |___/  |_ ",
            " |    |  _//  _ \\/  ___//  ___/  |    __)  |  |/ ___\\|  |  \\   __\\",
            " |    |   (  <_> )___ \\ \\___ \\   |     \\   |  / /_/  >   Y  \\  |  ",
            " |______  /\\____/____  >____  >  \\___  /   |__\\___  /|___|  /__|  ",
            "        \\/           \\/     \\/       \\/      /_____/      \\/      "
        };

        public string[] Boss = new string[]
        {
            "\\           |   | |== | |==/    /",
            " \\__________|   | |__| |__/    / ",
            "  \\_________|___|_____________/  ",
            "    \\           |      /    /    ",
            "     `-_________|_____/___-'      ",
            "            \\. \\|/ ./           ",
            "              `-+-'               ",
            "                I                 ",
            "               [ ]                ",
            "            |==| |==]]            ",
            "               `-'                "
        };

        static bool Direction = true;
        static int left = 80;
        public int BossHealth = 100;
        public int start = 0;
        public States.GameState Game;

        public BossFightController(States.GameState game)
        {
            Game = game;
        }

        public void RunBossScript()
        {
            if (Game.BossTimer == 0)
            {
                Game.Ammo += 150;
                Game.Health += 10;

                start = 0;
                BossHealth = 100;
                left = 80;
                Direction = true;

                Game.IncreaseBossTimer = true;
                Game.enemies.Enemies.Clear();
                Game.meteor.Meteors.Clear();
                Game.bullet.Bullets.Clear();
            }

            if (Direction)
            {
                left++;
            }
            else
            {
                left--;
            }

            if (left >= 135)
            {
                Direction = false;
            }
            if (left <= 10)
            {
                Direction = true;
            }

            if (Game.BossTimer >= 0 && Game.BossTimer <= 100)
            {
                DrawBanner();
            }

            if (Game.BossTimer >= 130)
            {
                DrawBoss(left);
            }

            Random random = new Random();

            if (Game.BossTimer >= 150)
            {
                if (BossHealth <= 95 && BossHealth > 30)
                {
                    for (int i = 12; i <= 42 - 1; i += 1)
                    {
                        Game.enemies.Shoot(left + 10, i);
                    }
                }

                if (BossHealth <= 65 && BossHealth > 30)
                {
                    for (int i = Game.enemies.Enemies.Count; i <= 30; i += 1)
                    {
                        Game.enemies.AddEnemy(true);
                    }
                }

                if (BossHealth <= 30 && BossHealth > 1)
                {
                    if (Game.BossTimer % 65 == 0)
                    {
                        Game.enemies.Enemies.Clear();
                        Game.enemies.EnemyBullets.Clear();
                        Game.meteor.Meteors.Clear();

                        int skip = random.Next(0, 18);
                        start = Game.BossTimer;

                        for (int i = 0; i <= 18 - 1; i += 1)
                        {
                            if (i != skip)
                            {
                                Game.meteor.AddMeteor(i * 10, 11);
                            }
                        }
                    }

                    if (Game.BossTimer >= (start + 35))
                    {
                        Game.meteor.DownAllMeteors();
                    }
                }

                if (BossHealth <= 0)
                {
                    Game.BossTimer = 0;
                    BossHealth = 0;
                    Game.enemies.Enemies.Clear();
                    Game.meteor.Meteors.Clear();
                    Game.Bossfight = false;
                    Game.Score += 20;
                    return;
                }
            }

            if (Game.IncreaseBossTimer)
            {
                Game.BossTimer++;
            }
        }


        public void DrawBanner()
        {
            for (int i = 0; i <= BossLogo.Length - 1; i++)
            {
                StringBuilder sb = new StringBuilder(Utilities.CliRenderer.view[5 + i]);

                for (int z = 0; z <= BossLogo[i].Length - 1; z++)
                {
                    sb[60 + z] = BossLogo[i].ToCharArray()[z];
                }

                Utilities.CliRenderer.view[5 + i] = sb.ToString();
            }
        }

        public void DrawBoss(int left)
        {
            for (int ii = 0; ii <= 10; ii++)
            {
                for (int iii = 0; iii <= 33; iii++)
                {
                    if (Utilities.CliRenderer.view[0 + ii][left + iii] == '0')
                    {
                        Game.bullet.RemoveHighestBullet();
                        BossHealth--;
                        goto End;
                    }
                    if (Utilities.CliRenderer.view[0 + ii][left + iii] == '-')
                    {
                        if (Game.Invincible == 0)
                        {
                            Game.Health -= 2;
                            Game.InvincibleDuration = 60;
                            Game.Invincible++;
                        }
                        goto End;
                    }
                }
            }

            End:

            for (int i = 0; i <= Boss.Length - 1; i++)
            {
                StringBuilder sb = new StringBuilder(Utilities.CliRenderer.view[0 + i]);

                for (int z = 0; z <= Boss[i].Length - 1; z++)
                {
                    sb[left + z] = Boss[i].ToCharArray()[z];
                }

                Utilities.CliRenderer.view[0 + i] = sb.ToString();
            }
        }
    }
}
