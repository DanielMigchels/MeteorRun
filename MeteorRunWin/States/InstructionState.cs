using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorRunWin.States
{
    public class InstructionState
    {
        public GameItem.StarController stars = new GameItem.StarController(new GameState());
        public bool choice = false;

        public string[] display = new string[]
        {
"                                                  ___   __    _  _______  _______  ______    __   __  _______  _______  ___   _______  __    _  _______ ",
"                                                 |   | |  |  | ||       ||       ||    _ |  |  | |  ||       ||       ||   | |       ||  |  | ||       |",
"                                                 |   | |   |_| ||  _____||_     _||   | ||  |  | |  ||       ||_     _||   | |   _   ||   |_| ||  _____|",
"                                                 |   | |       || |_____   |   |  |   |_||_ |  |_|  ||       |  |   |  |   | |  | |  ||       || |_____ ",
"                                                 |   | |  _    ||_____  |  |   |  |    __  ||       ||      _|  |   |  |   | |  |_|  ||  _    ||_____  |",
"                                                 |   | | | |   | _____| |  |   |  |   |  | ||       ||     |_   |   |  |   | |       || | |   | _____| |",
"                                                 |___| |_|  |__||_______|  |___|  |___|  |_||_______||_______|  |___|  |___| |_______||_|  |__||_______|",
"",
"",
"",
"",
"                                                                            /| |\\      ",
"                                                                           |-|_|-|] <-- THIS IS YOU! ",
"                                                                           |_   _|/       You can move yourself with the WASD keys.",
"                                                                           \\___/         You can fire your gun with the Space bar.",
"                                                                                          You can dodge bullets with the Shift key.",
"",
"                                                                           |  _  | ",
"                                                                           |=(_)=|  <--  TRY TO AVOID THESE",
"                                                                           |     |       They shoot back",
"                                                                                   You can shoot them",
"",
"                                                                             _..._   ",
"                                                                           .'   o `. ",
"                                                                          :    o    : <-- AVOID THESE AS WELL",
"                                                                          : o     o :       You can't shoot these.",
"                                                                          `.     o .'",
"                                                                            `-...-'  ",
"",
"                                                                            _____",
"                                                                           |     |",
"                                                                           |     |   <-- PICK THESE UP FOR MORE POWER",
"                                                                           |_____|",
        };

        System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
        public void ShowMenu()
        {
            choice = false;

            System.Threading.Thread.Sleep(300);

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
            
            ShowInstructions();

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

        public void ShowInstructions()
        {
            int left = 4;
            int top = 4;

            for (int i = 0; i <= display.Length - 1; i++)
            {
                StringBuilder sb = new StringBuilder(Utilities.CliRenderer.view[top + i]);

                for (int z = 0; z <= display[i].Length - 1; z++)
                {
                    sb[left + z] = display[i].ToCharArray()[z];
                }

                Utilities.CliRenderer.view[top + i] = sb.ToString();
            }
        }
    }
}
