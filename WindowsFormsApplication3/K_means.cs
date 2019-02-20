using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class K_means
    {
        public int[,,] process(int[,,] rgb, int width, int height, int k_vaule, int interation_level)
        {
            int[,] k_pixel = new int[k_vaule, 3]; //  K個RGB的像素點
            double[,] tag = new double[width, height]; //每點座標的分類標籤
            // 產生k個中心點

            for (int i = 0; i < k_vaule; i++)
            {
                k_pixel[i, 0] = (255 / k_vaule) * (i);  //R
                k_pixel[i, 1] = (255 / k_vaule) * (i); //G
                k_pixel[i, 2] = (255 / k_vaule) * (i); //B
            }
           
            for (int z = 0; z < interation_level; z++)
            {
                //計算像素距離並給予分類標籤
                double[] distance = new double[k_vaule]; //離每個中心的像素距離
                double[] sort = new double[k_vaule]; // 像素距離排序
               

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int k = 0; k < k_vaule; k++)
                        {
                            distance[k] =  //使用歐式距離公式算出每一點離K個中心點的距離

                            Math.Sqrt(
                            Math.Pow((double)(k_pixel[k, 0] - rgb[x, y, 0]), 2.0) +
                            Math.Pow((double)k_pixel[k, 1] - rgb[x, y, 1], 2.0) +
                            Math.Pow((double)k_pixel[k, 2] - rgb[x, y, 2], 2.0));

                            sort[k] = distance[k];
                        }
                        Array.Sort(sort); //將每個點到K個中心點距離排序
                        for (int k = 0; k < k_vaule; k++)
                        {
                            if (sort[0] == distance[k])//找出排序後最短距離的中心點
                            {
                                tag[x, y] = k;//加入座標的分類標籤
                                break;
                            }
                        }
                    }
                }

                //將所有同一個標籤計算出平均值產生新的中心點

                int[] pixel_sum = new int[3]; //同個標籤RGB像素總和
                int pixel_num = 0; // 同個標籤像素個數
                

                for (int k = 0; k < k_vaule; k++) // 尋找標籤 總和後平均成為新的中心點
                {
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            if (tag[x, y] == k)
                            {
                                pixel_sum[0] += rgb[x, y, 0];
                                pixel_sum[1] += rgb[x, y, 1];
                                pixel_sum[2] += rgb[x, y, 2];

                                pixel_num++;
                            }
                        }
                    }
                    try {
                        k_pixel[k, 0] = pixel_sum[0] / pixel_num; // R
                        k_pixel[k, 1] = pixel_sum[1] / pixel_num; // G
                        k_pixel[k, 2] = pixel_sum[2] / pixel_num; //B
                    }
                    catch
                    {
                        
                    }
                    pixel_num = 0;
                    pixel_sum[0] = 0;
                    pixel_sum[1] = 0;
                    pixel_sum[2] = 0;
                }
            }
            //將圖片RGB重新設定像素

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int k = 0; k < k_vaule; k++)
                    {
                        if (tag[x, y] == k)
                        {
                            rgb[x, y, 0] = k_pixel[k, 0];
                            rgb[x, y, 1] = k_pixel[k, 1];
                            rgb[x, y, 2] = k_pixel[k, 2];
                        }
                    }
                }
            }
            return rgb;
        }
    }
}
