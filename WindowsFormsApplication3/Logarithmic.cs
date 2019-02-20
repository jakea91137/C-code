using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Logarithmic
    {
        public int[,,] process(int[,,] rgb, int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    rgb[x, y, 0] = (int)(40 * Math.Log(rgb[x, y, 0] + 1));
                    rgb[x, y, 1] = (int)(40 * Math.Log(rgb[x, y, 1] + 1));
                    rgb[x, y, 2] = (int)(40 * Math.Log(rgb[x, y, 2] + 1));

                }
            }
            return rgb;
        }
    }
}
