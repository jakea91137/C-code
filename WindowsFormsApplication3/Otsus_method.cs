using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Otsus_method
    {
        public int[,,] process(int[,,] rgb, int width, int height,int pixel)
        {
            int[] gray = new int[256];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {

                    int avg = (rgb[x, y, 0] + rgb[x, y, 1] + rgb[x, y, 2]) / 3;
                    gray[avg]++;
                }
            }
            var sum = 0;
            for (var i = 0; i < 256; i++)//全部加權值總和
                sum += i * gray[i];
            var sumB = 0; //背景加權值總和
            var wB = 0.0; //背景加權值
            var wF = 0.0; //前景加權值
            var mB = 0.0; //背景平均值
            var mF = 0.0; //前景平均值
            var max = 0.0; //最大平方變異數
            var between = 0.0; //平均平方變異數
            var threshold1 = 0.0;
            var threshold2 = 0.0;
            var threshold = 0.0; //閥值
            for (var i = 0; i < 256; ++i)
            {
                wB += gray[i];
                wF = (pixel - wB);


                sumB += i * gray[i];
                mB = sumB / wB;
                mF = (sum - sumB) / wF;
                between = (wB) * (wF) * (mB - mF) * (mB - mF);
                if (between >= max)
                {
                    threshold1 = i;
                    if (between > max)
                    {
                        threshold2 = i;
                    }
                    max = between;
                }
            }

            threshold = (threshold1 + threshold2) / 2.0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int avg = (rgb[x, y, 0] + rgb[x, y, 1] + rgb[x, y, 2]) / 3;
                    if (avg >= threshold)
                        avg = 255;
                    else
                        avg = 0;
                    rgb[x, y, 0] = avg;
                    rgb[x, y, 1] = avg;
                    rgb[x, y, 2] = avg;

                }
            }
            return rgb;
        }
    }
}
