using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Encryption
    {
        public int[,,] process(int[,,] rgb, int width, int height,String str)
        {
            str += "\0";
            char[] c = str.ToCharArray();
            int[] ascii = new int [c.Length];
            
            for(int i = 0;i < ascii.Length; i++)
            {
                ascii[i] = c[i];
            }
            int count = 0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (rgb[x, y, 0] > 255-9) {
                        rgb[x, y, 0] -= 9;
                        if (rgb[x, y, 0] < 0)
                            rgb[x, y, 0] = 0;
                    }
                    if (rgb[x, y, 1] > 255 - 9)
                    {
                        rgb[x, y, 1] -= 9;
                        if (rgb[x, y, 1] < 0)
                            rgb[x, y, 1] = 0;
                    }
                    if (rgb[x, y, 2] > 255 - 9)
                    {
                        rgb[x, y, 2] -= 9;
                        if (rgb[x, y, 2] < 0)
                            rgb[x, y, 2] = 0;
                    }



                }
            }
            for (int x = 1; x < width; x++)
            {
                for (int y = 1; y < height; y++)
                {
                    if (x % 2 == 0 && y % 2 == 0)
                    {
                        if (count < ascii.Length)
                        {
                            rgb[x, y, 0] = rgb[x - 1, y, 0];
                            rgb[x, y, 1] = rgb[x - 1, y, 1];
                            rgb[x, y, 2] = rgb[x - 1, y, 2];
                        }
                    }
                }
            }
            for (int x = 1; x < width; x++)
            {
                for (int y = 1; y < height; y++)
                {
                    if (x % 2 == 0 && y % 2 == 0)
                    {
                        if (count < ascii.Length)
                        {
                            rgb[x, y, 0] = rgb[x - 1, y, 0] + ascii[count]/100;
                            rgb[x, y, 1] = rgb[x - 1, y, 1] + ascii[count]%100/10;
                            rgb[x, y, 2] = rgb[x - 1, y, 2] + ascii[count]%10;
                            count++;
                        }
                    }
                }
            }
            return rgb;
        }
    }
}
