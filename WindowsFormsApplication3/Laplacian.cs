using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Laplacian
    {
        public int[,,] process(int[,,] rgb, int width, int height)
        {
            int[,] r = new int[width, height];
            int[,] g = new int[width, height];
            int[,] b = new int[width, height];
            int[,] Laplacian_Mask = new int[3, 3]
            {
               {-1,-1,-1}, {-1,8,-1}, {-1,-1,-1}
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
                    int threshold_r = Laplacian_Mask[0, 0] * r[x - 1, y + 1] +
                                    Laplacian_Mask[0, 1] * r[x, y + 1] +
                                    Laplacian_Mask[0, 2] * r[x + 1, y + 1] +
                                    Laplacian_Mask[1, 0] * r[x - 1, y] +
                                    Laplacian_Mask[1, 1] * r[x, y] +
                                    Laplacian_Mask[1, 2] * r[x + 1, y] +
                                    Laplacian_Mask[2, 0] * r[x - 1, y - 1] +
                                    Laplacian_Mask[2, 1] * r[x, y - 1] +
                                    Laplacian_Mask[2, 2] * r[x + 1, y - 1];
                    if (threshold_r > 255)
                        threshold_r = 255;
                    if (threshold_r < 0)
                        threshold_r = 0;
                    int threshold_g = Laplacian_Mask[0, 0] * g[x - 1, y + 1] +
                                    Laplacian_Mask[0, 1] * g[x, y + 1] +
                                    Laplacian_Mask[0, 2] * g[x + 1, y + 1] +
                                    Laplacian_Mask[1, 0] * g[x - 1, y] +
                                    Laplacian_Mask[1, 1] * g[x, y] +
                                    Laplacian_Mask[1, 2] * g[x + 1, y] +
                                    Laplacian_Mask[2, 0] * g[x - 1, y - 1] +
                                    Laplacian_Mask[2, 1] * g[x, y - 1] +
                                    Laplacian_Mask[2, 2] * g[x + 1, y - 1];
                    if (threshold_g > 255)
                        threshold_g = 255;
                    if (threshold_g < 0)
                        threshold_g = 0;
                    int threshold_b = Laplacian_Mask[0, 0] * b[x - 1, y + 1] +
                                    Laplacian_Mask[0, 1] * b[x, y + 1] +
                                    Laplacian_Mask[0, 2] * b[x + 1, y + 1] +
                                    Laplacian_Mask[1, 0] * b[x - 1, y] +
                                    Laplacian_Mask[1, 1] * b[x, y] +
                                    Laplacian_Mask[1, 2] * b[x + 1, y] +
                                    Laplacian_Mask[2, 0] * b[x - 1, y - 1] +
                                    Laplacian_Mask[2, 1] * b[x, y - 1] +
                                    Laplacian_Mask[2, 2] * b[x + 1, y - 1];
                    if (threshold_b > 255)
                        threshold_b = 255;
                    if (threshold_b < 0)
                        threshold_b = 0;
                    rgb[x, y, 0] = threshold_r;
                    rgb[x, y, 1] = threshold_g;
                    rgb[x, y, 2] = threshold_b;
                }
            }
            return rgb;
        }
    }
}
