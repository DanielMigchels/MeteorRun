using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MeteorRunWin.Forms
{
    public partial class InputMessageBox : Form
    {
        public string output;
        public InputMessageBox(string text, string header)
        {
            InitializeComponent();
            this.Text = header;
            label1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            output = textBox1.Text;
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, null);
            }
        }

        public static string Show(string text, string header)
        {
            InputMessageBox input = new InputMessageBox(text, header);
            input.ShowDialog();
            return input.output;
        }
    }
}
