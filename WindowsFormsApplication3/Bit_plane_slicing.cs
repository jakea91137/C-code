using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Bit_plane_slicing
    {
        public int[,,] process(int[,,] rgb, int width, int height,int bit)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {

                    String binary_r = "";
                    String binary_g = "";
                    String binary_b = "";
                    int divisor = rgb[x, y, 0];
                    for (int i = 0; i < 8; i++)
                    {
                        binary_r += Convert.ToString(divisor % 2);
                        divisor = divisor / 2;
                    }
                    divisor = rgb[x, y, 1];
                    for (int i = 0; i < 8; i++)
                    {
                        binary_g += Convert.ToString(divisor % 2);
                        divisor = divisor / 2;
                    }
                    divisor = rgb[x, y, 2];
                    for (int i = 0; i < 8; i++)
                    {
                        binary_b += Convert.ToString(divisor % 2);
                        divisor = divisor / 2;
                    }

                    char[] c1 = binary_r.ToCharArray();
                    Array.Reverse(c1);
                    char[] c2 = binary_g.ToCharArray();
                    Array.Reverse(c2);
                    char[] c3 = binary_b.ToCharArray();
                    Array.Reverse(c3);
                    for (int i = bit; i < 8; i++)
                    {
                        c1[i] = '0';
                        c2[i] = '0';
                        c3[i] = '0';
                        
                        
                       
                    }

                    binary_r = new String(c1);
                    binary_g = new String(c2);
                    binary_b = new String(c3);


                    rgb[x, y, 0] = Convert.ToInt32(binary_r, 2);
                    rgb[x, y, 1] = Convert.ToInt32(binary_g, 2);
                    rgb[x, y, 2] = Convert.ToInt32(binary_b, 2);
                    

                }
            }
            return rgb;
        }


    }
}
