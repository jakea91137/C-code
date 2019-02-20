using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Histogram_equalization
    {
        public int[,,] process(int [,,]rgb,int width,int height,int pixel)
        {
            int[] r = new int[256];
            int[] g = new int[256];
            int[] b = new int[256];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    r[rgb[x, y, 0]]++;
                    g[rgb[x, y, 1]]++;
                    b[rgb[x, y, 2]]++;
                }
            }
            int sum = 0;
            for (int i = 0; i < 256; i++)
            {
                sum = sum + r[i];
                r[i] = (int)((float)sum / (float)pixel * 255.0);
            }
            sum = 0;
            for (int i = 0; i < 256; i++)
            {
                sum = sum + g[i];
                g[i] = (int)((float)sum / (float)pixel * 255.0);
            }
            sum = 0;
            for (int i = 0; i < 256; i++)
            {
                sum = sum + b[i];
                b[i] = (int)((float)sum / (float)pixel * 255.0);
            }
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    rgb[x, y, 0] = r[rgb[x, y, 0]];
                    rgb[x, y, 1] = g[rgb[x, y, 1]];
                    rgb[x, y, 2] = b[rgb[x, y, 2]];

                }
            }

            return rgb;
        }
    }
}
