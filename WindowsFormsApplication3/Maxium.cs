using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Maxium
    {
        public int[,,] process(int[,,] rgb, int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int max = 0;
                    if (rgb[x, y, 0] >= max)
                        max = rgb[x, y, 0];
                    if (rgb[x, y, 1] >= max)
                        max = rgb[x, y, 1];
                    if (rgb[x, y, 2] >= max)
                        max = rgb[x, y, 2];
                    rgb[x, y, 0] = max;
                    rgb[x, y, 1] = max;
                    rgb[x, y, 2] = max;

                }

            }
            return rgb;
        }
    }
}
