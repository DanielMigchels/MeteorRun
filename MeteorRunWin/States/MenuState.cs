using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using MeteorRunWin.Utilities;
using MeteorRunWin.States;

namespace MeteorRunWin.States
{
    public class MenuState
    {
        private string[] _logo = new string[]
        {
            " __   __  _______  _______  _______  _______  ______      ______    __   __  __    _ ",
            "|  |_|  ||       ||       ||       ||       ||    _ |    |    _ |  |  | |  ||  |  | |",
            "|       ||    ___||_     _||    ___||   _   ||   | ||    |   | ||  |  | |  ||   |_| |",
            "|       ||   |___   |   |  |   |___ |  | |  ||   |_||_   |   |_||_ |  |_|  ||       |",
            "|       ||    ___|  |   |  |    ___||  |_|  ||    __  |  |    __  ||       ||  _    |",
            "| ||_|| ||   |___   |   |  |   |___ |       ||   |  | |  |   |  | ||       || | |   |",
            "|_|   |_||_______|  |___|  |_______||_______||___|  |_|  |___|  |_||_______||_|  |__|",
            "                              By Daniel Migchels | PST7                              "
        };
        private string[] _menuitems = new string[]
        {
             "Start Game",
             "Highscore",
             "Instructions",
             "Exit",
        };
        private bool choice = false;
        private int index = 0;
        private GameItem.StarController stars = new GameItem.StarController(new GameState());
        private Timer timer;
        private Timer timer2;

        public int Option
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }

        public MenuState()
        {

        }

        public void ShowMenu()
        {
            Utilities.CliRenderer.BackColor = System.Drawing.Color.DarkRed;

            choice = false;

            /*while (Keyboard.IsKeyDown(KeyCode.Enter))
            {
                CliRenderer.view = Enumerable.Repeat(string.Empty.PadLeft((int)Enums.Ratio.Width), (int)Enums.Ratio.Height).ToArray();

                stars.AddStar();
                stars.DownAllStars();
                stars.DrawStar();

                DrawArt();
                DrawMenu();

                CliRenderer.Render();
                System.Threading.Thread.Sleep(33);
            }*/

            System.Threading.Thread.Sleep(120);

            timer = new Timer();
            timer.Interval = 33;
            timer.Tick += Timer_Tick;
            timer.Start();

            timer2 = new Timer();
            timer2.Interval = 33;
            timer2.Tick += Timer2_Tick;
            timer2.Start();
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            CliRenderer.view = Enumerable.Repeat(string.Empty.PadLeft((int)Enums.Ratio.Width), (int)Enums.Ratio.Height).ToArray();

            stars.AddStar();
            stars.DownAllStars();
            stars.DrawStar();

            DrawArt();
            DrawMenu();
            CliRenderer.Render();

            if (!Keyboard.IsKeyDown(KeyCode.W) && !Keyboard.IsKeyDown(KeyCode.S) && !Keyboard.IsKeyDown(KeyCode.Up) && !Keyboard.IsKeyDown(KeyCode.Down))
            {
                System.Diagnostics.Debug.WriteLine("Out");
                timer2.Stop();
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CliRenderer.view = Enumerable.Repeat(string.Empty.PadLeft((int)Enums.Ratio.Width), (int)Enums.Ratio.Height).ToArray();

            stars.AddStar();
            stars.DownAllStars();
            stars.DrawStar();

            DrawArt();
            DrawMenu();

            if (Keyboard.IsKeyDown(KeyCode.Enter))
            {
                choice = true;
            }

            if (Keyboard.IsKeyDown(KeyCode.W) || Keyboard.IsKeyDown(KeyCode.Up))
            {
                if (index != 0)
                {
                    index--;
                }

                timer.Stop();
                timer2.Start();
            }

            if (Keyboard.IsKeyDown(KeyCode.S) || Keyboard.IsKeyDown(KeyCode.Down))
            {
                if (index != 3)
                {
                    index++;
                }

                timer.Stop();
                timer2.Start();
            }

            Utilities.CliRenderer.Render();

            if (choice)
            {
                timer.Stop();
                Select(index);
            }
        }

        public void Select(int option)
        {
            switch (option)
            {
                case 0:
                    Main.game = new GameState();
                    Main.game.Run();
                    break;
                case 1:
                    Main.highscore = new HighscoreState();
                    Main.highscore.ShowMenu();
                    break;
                case 2:
                    Main.instruction = new InstructionState();
                    Main.instruction.ShowMenu();
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        }

        public void DrawArt()
        {
            int left = 50;
            int top = 4;

            for (int i = 0; i <= _logo.Length - 1; i++)
            {
                StringBuilder sb = new StringBuilder(CliRenderer.view[top + i]);

                for (int z = 0; z <= _logo[i].Length - 1; z++)
                {
                    sb[left + z] = _logo[i].ToCharArray()[z];
                }

                CliRenderer.view[top + i] = sb.ToString();
            }
        }

        public void DrawMenu()
        {
            int left = 87;
            int top = 28;

            for (int i = 0; i <= _menuitems.Length - 1; i++)
            {
                StringBuilder sb = new StringBuilder(CliRenderer.view[top + i]);

                for (int z = 0; z <= _menuitems[i].Length - 1; z++)
                {
                    sb[left + z] = _menuitems[i].ToCharArray()[z];
                }

                if (index == i)
                {
                    sb[left - 2] = '>';
                }

                CliRenderer.view[top + i] = sb.ToString();
            }
        }
    }
}
