using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Median_filiter
    {
        public int[,,] process(int[,,] rgb, int width, int height)
        {
            int[,] r = new int[width, height];
            int[,] g = new int[width, height];
            int[,] b = new int[width, height];

            for (int x = 1; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {

                    r[x, y] = rgb[x, y, 0];
                    g[x, y] = rgb[x, y, 1];
                    b[x, y] = rgb[x, y, 2];
                }
            }

            int[] median = new int[9];

            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    median[0] = r[x - 1, y + 1];
                    median[1] = r[x, y + 1];
                    median[2] = r[x + 1, y + 1];
                    median[3] = r[x - 1, y];
                    median[4] = r[x, y];
                    median[5] = r[x + 1, y];
                    median[6] = r[x - 1, y - 1];
                    median[7] = r[x, y - 1];
                    median[8] = r[x + 1, y - 1];

                    Array.Sort(median);

                    int Mr = median[4];

                    median[0] = g[x - 1, y + 1];
                    median[1] = g[x, y + 1];
                    median[2] = g[x + 1, y + 1];
                    median[3] = g[x - 1, y];
                    median[4] = g[x, y];
                    median[5] = g[x + 1, y];
                    median[6] = g[x - 1, y - 1];
                    median[7] = g[x, y - 1];
                    median[8] = g[x + 1, y - 1];

                    Array.Sort(median);

                    int Mg = median[4];

                    median[0] = b[x - 1, y + 1];
                    median[1] = b[x, y + 1];
                    median[2] = b[x + 1, y + 1];
                    median[3] = b[x - 1, y];
                    median[4] = b[x, y];
                    median[5] = b[x + 1, y];
                    median[6] = b[x - 1, y - 1];
                    median[7] = b[x, y - 1];
                    median[8] = b[x + 1, y - 1];

                    Array.Sort(median);

                    int Mb = median[4];
                    rgb[x, y, 0] = Mr;
                    rgb[x, y, 1] = Mg;
                    rgb[x, y, 2] = Mb;

                }
            }
            return rgb;
        }
    }
}
