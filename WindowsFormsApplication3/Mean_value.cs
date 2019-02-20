using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Mean_value
    {
        public int[,,] process(int [,,] rgb , int width,int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int gray = (int)((rgb[x, y, 0] + rgb[x, y, 1] + rgb[x, y, 2]) / 3);
                    rgb[x, y, 0] = gray;
                    rgb[x, y, 1] = gray;
                    rgb[x, y, 2] = gray;
                }
            }
            return rgb;
        }
    }
}
