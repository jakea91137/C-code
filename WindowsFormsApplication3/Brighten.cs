using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Brighten
    {
        public int[,,] process(int[,,] rgb, int width, int height, int brightness)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    rgb[x, y, 0] += brightness;
                    rgb[x, y, 1] += brightness;
                    rgb[x, y, 2] += brightness;
                    if (rgb[x, y, 0] > 255)
                        rgb[x, y, 0] = 255;
                    if (rgb[x, y, 1] > 255)
                        rgb[x, y, 1] = 255;
                    if (rgb[x, y, 2] > 255)
                        rgb[x, y, 2] = 255;
                }
            }
            return rgb;
        }
    }
}
