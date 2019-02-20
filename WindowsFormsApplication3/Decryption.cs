using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Decryption
    {

        public String process(int[,,] rgb, int width, int height)
        {
            String s = "";
            
            int length = 10000;
            char[] c = new char[length];
            int count = 0;
            for (int x = 1; x < width; x++)
            {
                for (int y = 1; y < height; y++)
                {
                    if (x % 2 == 0 && y % 2 == 0)
                    {
                        if (count < length)
                        {
                            c[count] = (char)((rgb[x, y, 0] - rgb[x - 1, y, 0]) * 100 +
                                             (rgb[x, y, 1] - rgb[x - 1, y, 1]) * 10 +
                                             (rgb[x, y, 2] - rgb[x - 1, y, 2]));
                           
                            count++;
                        }
                    }
                }
            }
            for (int i = 0; i < c.Length; i++)
            {
                s += c[i];
            }
            return s;
        }
    }
}
