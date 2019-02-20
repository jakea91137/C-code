using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Sobel
    {
        public int[,,] process(int[,,] rgb, int width, int height)
        {
            int[,] r = new int[width, height];
            int[,] g = new int[width, height];
            int[,] b = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    r[x, y] = rgb[x, y, 0];
                    g[x, y] = rgb[x, y, 1];
                    b[x, y] = rgb[x, y, 2];
                }
            }
            int Gx = 0;
            int Gy = 0;

            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    Gx = (r[x + 1, y - 1] + 2 * r[x + 1, y] + r[x + 1, y + 1]) - (r[x - 1, y - 1] + 2 * r[x - 1, y] + r[x - 1, y + 1]);

                    Gy = (r[x - 1, y - 1] + 2 * r[x, y - 1] + r[x + 1, y - 1]) - (r[x - 1, y + 1] + 2 * r[x, y + 1] + r[x + 1, y + 1]);

                    int Gr = (int)Math.Sqrt((Math.Pow(Gx, 2) + Math.Pow(Gy, 2)));

                    if (Gr > 255)
                        Gr = 255;


                    Gx = (g[x + 1, y - 1] + 2 * g[x + 1, y] + g[x + 1, y + 1]) - (g[x - 1, y - 1] + 2 * g[x - 1, y] + g[x - 1, y + 1]);

                    Gy = (g[x - 1, y - 1] + 2 * g[x, y - 1] + g[x + 1, y - 1]) - (g[x - 1, y + 1] + 2 * g[x, y + 1] + g[x + 1, y + 1]);

                    int Gg = (int)Math.Sqrt((Math.Pow(Gx, 2) + Math.Pow(Gy, 2)));

                    if (Gg > 255)
                        Gg = 255;


                    Gx = (b[x + 1, y - 1] + 2 * b[x + 1, y] + b[x + 1, y + 1]) - (b[x - 1, y - 1] + 2 * b[x - 1, y] + b[x - 1, y + 1]);

                    Gy = (b[x - 1, y - 1] + 2 * b[x, y - 1] + b[x + 1, y - 1]) - (b[x - 1, y + 1] + 2 * b[x, y + 1] + b[x + 1, y + 1]);

                    int Gb = (int)Math.Sqrt((Math.Pow(Gx, 2) + Math.Pow(Gy, 2)));

                    if (Gb > 255)
                        Gb = 255;

                    rgb[x, y, 0] = Gr;
                    rgb[x, y, 1] = Gg;
                    rgb[x, y, 2] = Gb;
                }
            }
            return rgb;
        }
    }
}
