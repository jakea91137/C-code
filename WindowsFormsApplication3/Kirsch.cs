using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Kirsch
    {
        public int[,,] process(int[,,] rgb, int width, int height)
        {
            int[,] r = new int[width, height];
            int[,] g = new int[width, height];
            int[,] b = new int[width, height];

            int[,,] Kirsch_Filter_Mask = new int[4, 3, 3]
            {
               {{5,5,5}, {-3,0,-3}, {-3,-3,-3}},
               {{5,5,-3}, {5,0,-3}, {-3,-3,-3}},
               {{5,-3,-3}, {5,0,-3}, {5,-3,-3}},
               {{-3,-3,-3}, {5,0,-3}, {5,5,-3}},
            };
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    r[x, y] = rgb[x, y, 0];
                    g[x, y] = rgb[x, y, 1];
                    b[x, y] = rgb[x, y, 2];
                }
            }
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    int threshold = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        threshold = Kirsch_Filter_Mask[i, 0, 0] * r[x - 1, y + 1] +
                                    Kirsch_Filter_Mask[i, 0, 1] * r[x, y + 1] +
                                    Kirsch_Filter_Mask[i, 0, 2] * r[x + 1, y + 1] +
                                    Kirsch_Filter_Mask[i, 1, 0] * r[x - 1, y] +
                                    Kirsch_Filter_Mask[i, 1, 1] * r[x, y] +
                                    Kirsch_Filter_Mask[i, 1, 2] * r[x + 1, y] +
                                    Kirsch_Filter_Mask[i, 2, 0] * r[x - 1, y - 1] +
                                    Kirsch_Filter_Mask[i, 2, 1] * r[x, y - 1] +
                                    Kirsch_Filter_Mask[i, 2, 2] * r[x + 1, y - 1];
                    }
                    if (threshold > 255)
                        threshold = 255;
                    if (threshold < 0)
                        threshold = 0;
                    rgb[x, y, 0] = threshold;
                    rgb[x, y, 1] = threshold;
                    rgb[x, y, 2] = threshold;
                }
            }
            return rgb;
        }
    }
}
