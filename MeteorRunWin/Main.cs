using MeteorRunWin.States;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeteorRunWin
{
    public partial class Main : Form
    {
        public static MenuState menu;
        public static GameState game;
        public static HighscoreState highscore;
        public static InstructionState instruction;
        public static Main This;

        public static string Version = "1.6.1";

        public Main()
        {
            InitializeComponent();
            Init();
            This = this;
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            this.Text = "MeteorRun v" + Version + "";

            menu = new MenuState();

            menu.ShowMenu();
        }

        public void Init()
        {
            this.Size = new Size((int)Enums.Ratio.Width * 8, (int)Enums.Ratio.Height * 18);
        }
    }
}
