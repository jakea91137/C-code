using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Darken
    {
        public int[,,] process(int[,,] rgb, int width, int height, int darkness)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    rgb[x, y, 0] -= darkness;
                    rgb[x, y, 1] -= darkness;
                    rgb[x, y, 2] -= darkness;
                    if (rgb[x, y, 0] < 0)
                        rgb[x, y, 0] = 0;
                    if (rgb[x, y, 1] < 0)
                        rgb[x, y, 1] = 0;
                    if (rgb[x, y, 2] < 0)
                        rgb[x, y, 2] = 0;
                }
            }
            return rgb;
        }
    }
}
