using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Erosion
    {
        public int[,,] process(int[,,] rgb, int width, int height)
        {
            int[,] black = new int[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {

                    black[x, y] = rgb[x, y, 0];
                }
            }
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    int[] erosion = new int[9];
                    erosion[0] = black[x - 1, y + 1];
                    erosion[1] = black[x, y + 1];
                    erosion[2] = black[x + 1, y + 1];
                    erosion[3] = black[x - 1, y];
                    erosion[4] = black[x, y];
                    erosion[5] = black[x + 1, y];
                    erosion[6] = black[x - 1, y - 1];
                    erosion[7] = black[x, y - 1];
                    erosion[8] = black[x + 1, y - 1];
                    int temp = 255;
                    for (int i = 0; i < 9; i++)
                        if (erosion[i] == 0)
                            temp = 0;

                    rgb[x, y, 0] = temp;
                    rgb[x, y, 1] = temp;
                    rgb[x, y, 2] = temp;
                }
            }

            return rgb;
        }
    }
}
