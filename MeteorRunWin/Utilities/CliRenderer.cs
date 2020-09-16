using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorRunWin.Utilities
{
    public class CliRenderer
    {
        public static System.Drawing.Color ForeColor = System.Drawing.Color.White;
        public static System.Drawing.Color BackColor = System.Drawing.Color.DarkRed;

        public static string[] view = Enumerable.Repeat(string.Empty.PadLeft((int)Enums.Ratio.Width) , (int)Enums.Ratio.Height).ToArray();

        public static void Render()
        {
            string scene = "";

            for (int row = 0; row < view.Count(); row++)
            {
                scene += view[row];
                scene += '\n';
            }

            using (Bitmap bitmap = new Bitmap(Main.This.Width, Main.This.Height))
            {
                using (Graphics gx = Graphics.FromImage(bitmap))
                {
                    using (System.Drawing.Font drawFont = new System.Drawing.Font("Consolas", Main.This.Width / 150))
                    {
                        using (System.Drawing.SolidBrush foreBrush = new System.Drawing.SolidBrush(ForeColor))
                        {
                            using (System.Drawing.SolidBrush backBrush = new System.Drawing.SolidBrush(BackColor))
                            {
                                using (System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat())
                                {
                                    float x = 0;
                                    float y = 0;
                                    string drawString = scene;

                                    using (Brush blackBrush = new SolidBrush(Color.Black))
                                    {
                                        gx.FillRectangle(backBrush, x, y, Main.This.Width, Main.This.Height);
                                        gx.DrawString(drawString, drawFont, foreBrush, x, y, drawFormat);
                                        
                                        Main.This.BackgroundImage = (Image)bitmap.Clone();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
