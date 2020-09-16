using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MeteorRunWin.States
{
    public class HighscoreState
    {
        public List<string> scores = new List<string>();
        public GameItem.StarController stars = new GameItem.StarController(new GameState());
        
        public bool choice = false;

        public static string[] art = new string[]
        {
            " __   __  ___   _______  __   __  _______  _______  _______  ______    _______ ",
            "|  | |  ||   | |       ||  | |  ||       ||       ||       ||    _ |  |       |",
            "|  |_|  ||   | |    ___||  |_|  ||  _____||       ||   _   ||   | ||  |    ___|",
            "|       ||   | |   | __ |       || |_____ |       ||  | |  ||   |_||_ |   |___ ",
            "|       ||   | |   ||  ||       ||_____  ||      _||  |_|  ||    __  ||    ___|",
            "|   _   ||   | |   |_| ||   _   | _____| ||     |_ |       ||   |  | ||   |___ ",
            "|__| |__||___| |_______||__| |__||_______||_______||_______||___|  |_||_______|"
        };

        public string[] menu = new string[]
        {
             "Start Game",
             "Highscore",
             "Instructions",
             "Exit",
        };

        public void Input()
        {
            Utilities.CliRenderer.view = Enumerable.Repeat(string.Empty.PadLeft((int)Enums.Ratio.Width), (int)Enums.Ratio.Height).ToArray();
            DrawArt();
            Utilities.CliRenderer.Render();
            
            string name = Forms.InputMessageBox.Show("Your score is " + Main.game.Score + ". Enter your name for the highscore", "Highscore");

            if (!string.IsNullOrWhiteSpace(name))
            {
                Utilities.Network.PutScore(name, Main.game.Score);
                scores = Utilities.Network.GetScore();
            }
        }

        System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
        public void ShowMenu()
        {
            choice = false;
            scores = Utilities.Network.GetScore();

            System.Threading.Thread.Sleep(50);

            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 33;
            tmr.Tick += Tmr_Tick;
            tmr.Start();
        }

        private void Tmr_Tick(object sender, EventArgs e)
        {
            Utilities.CliRenderer.view = Enumerable.Repeat(string.Empty.PadLeft((int)Enums.Ratio.Width), (int)Enums.Ratio.Height).ToArray();

            stars.AddStar();
            stars.DownAllStars();
            stars.DrawStar();

            DrawArt();
            DrawScore();

            if (Utilities.Keyboard.IsKeyDown(Utilities.KeyCode.Enter))
            {
                choice = true;
                tmr.Stop();

                Main.menu = new MenuState();

                Main.menu.ShowMenu();
                return;
            }

            Utilities.CliRenderer.Render();
        }

        public void DrawArt()
        {
            int left = 50;
            int top = 4;

            for (int i = 0; i <= art.Length - 1; i++)
            {
                StringBuilder sb = new StringBuilder(Utilities.CliRenderer.view[top + i]);

                for (int z = 0; z <= art[i].Length - 1; z++)
                {
                    sb[left + z] = art[i].ToCharArray()[z];
                }

                Utilities.CliRenderer.view[top + i] = sb.ToString();
            }
        }

        public void DrawScore()
        {
            int left = (((int)Enums.Ratio.Width - (art[0].Length / 2) ) / 2);
            int top = 15;

            for (int i = 0; i <= scores.Count - 1; i++)
            {
                StringBuilder sb = new StringBuilder(Utilities.CliRenderer.view[top + i]);

                for (int z = 0; z <= scores[i].Length - 1; z++)
                {
                    sb[left + z] = scores[i].ToCharArray()[z];
                }

                Utilities.CliRenderer.view[top + i] = sb.ToString();
            }
        }
    }
}
