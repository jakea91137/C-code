using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class HOG
    {
        public int[,,] process(int[,,] rgb, int width, int height)
        {
            int[,,] rgb2 = new int[width, height, 3];
            double[] histogram = new double[9]; //梯度直方圖
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    double Gx = rgb[x + 1, y, 0] - rgb[x - 1, y, 0];
                    double Gy = rgb[x, y + 1, 0] - rgb[x, y - 1, 0];
                    int G = (int)Math.Sqrt(Gx * Gx + Gy * Gy); //梯度幅值
                    if (G > 255)
                        G = 255;
                    int theta = (int)((Math.Atan2(Gy, Gx)*180.0)/Math.PI);//梯度方向
                    rgb2[x, y, 0] = G;
                    rgb2[x, y, 1] = G;
                    rgb2[x, y, 2] = G;

                    if (theta > 180)
                        theta = theta - 180; // 設定0~180度範圍
                    if (theta > 0 && theta <= 20)
                    {
                        histogram[0] = G * (theta/20);
                        histogram[1] = G * ((20-theta)/20);
                    }
                    else if (theta > 20 && theta <= 40)
                    {
                        histogram[1] = G * (theta / 40);
                        histogram[2] = G - histogram[1];
                    }
                    else if (theta > 40 && theta <= 60)
                    {
                        histogram[2] = G * (theta / 60);
                        histogram[3] = G - histogram[2];
                    }
                    else if (theta > 60 && theta <= 80)
                    {
                        histogram[3] = G * (theta / 80);
                        histogram[4] = G - histogram[3];
                    }
                    else if (theta > 80 && theta <= 100)
                    {
                        histogram[4] = G * (theta / 100);
                        histogram[5] = G - histogram[4];
                    }
                    else if (theta > 100 && theta <= 120)
                    {
                        histogram[5] = G * (theta / 120);
                        histogram[6] = G - histogram[5];
                    }
                    else if (theta > 120 && theta <= 140)
                    {
                        histogram[6] = G * (theta / 140);
                        histogram[7] = G - histogram[6];
                    }
                    else if (theta > 140 && theta <= 160)
                    {
                        histogram[7] = G * (theta / 160);
                        histogram[8] = G - histogram[7];
                    }
                    else if (theta > 160 && theta <= 180)
                    {
                        histogram[8] = G * (theta / 180);
                        histogram[0] = G - histogram[8];
                    }

                }
            }
            return rgb2;
        }
    }
}
