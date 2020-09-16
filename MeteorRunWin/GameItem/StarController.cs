using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorRunWin.GameItem
{
    public class StarController
    {
        public List<Object.Star> stars;
        public Random rnd;
        public States.GameState Game;

        public StarController(States.GameState game)
        {
            Game = game;
            stars = new List<Object.Star>();
            rnd = new Random();
        }

        public void DownAllStars()
        {
            for (int i = 0; i <= stars.Count - 1; i++)
            {
                int x = stars[i].X;
                int y = stars[i].Y + 1;
                
                stars[i] = new Object.Star(x, y);
            }
        }

        public void AddStar()
        {
            for (int i = 0; i <= rnd.Next(0, 15) - 1; i++)
            {
                stars.Add(new Object.Star(rnd.Next(1, (int)Enums.Ratio.Width), 0));
            }
        }

        public void DrawStar()
        {
            for (int i = 0; i <= stars.Count - 1; i++)
            {
                StringBuilder sb = new StringBuilder(Utilities.CliRenderer.view[stars[i].Y - 1]);
                sb[stars[i].X] = '.';
                Utilities.CliRenderer.view[stars[i].Y - 1] = sb.ToString();

                if (stars[i].Y >= (int)Enums.Ratio.Height)
                {
                    stars.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
