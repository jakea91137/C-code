using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Negative
    {
        public int[,,] process(int[,,] rgb, int width, int height)
        {           
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    rgb[x, y, 0] = 255 - rgb[x, y, 0];
                    rgb[x, y, 1] = 255 - rgb[x, y, 1];
                    rgb[x, y, 2] = 255 - rgb[x, y, 2];
                }
            }
            return rgb;
        }
    }
}
