using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        static Bitmap img = new Bitmap(1, 1);
        static Bitmap img_origin = new Bitmap(1, 1);
        static int thresholding = 175;
        int brightness = 10;
        int darkness = 10;
        String logcat_text = "";
        Stack<Bitmap> myStack = new Stack<Bitmap>();



        static int pixel = 0;
        
        Mean_value mean_value = new Mean_value();
        Mean_weight mean_weight = new Mean_weight();
        Maxium maxium = new Maxium();
        Logarithmic logarithmic = new Logarithmic();
        Otsus_method otsus_method = new Otsus_method();
        Brighten brighten = new Brighten();
        Darken darken = new Darken();
        Negative negative = new Negative();
        Histogram_equalization histogram_equalization = new Histogram_equalization ();
        Sharpening shaprening = new Sharpening();
        Median_filiter median_filiter = new Median_filiter();
        Mean_filiter mean_filiter = new Mean_filiter();
        Sobel sobel = new Sobel();
        Laplacian laplacian = new Laplacian();
        Kirsch kirsch = new Kirsch();
        Encryption encryption = new Encryption();
        Decryption decryption = new Decryption();
        Erosion erosion = new Erosion();
        Dilation dilation = new Dilation();
        K_means k_means = new K_means();
        Bit_plane_slicing bit_plane_slicing = new Bit_plane_slicing();
        HOG hog = new HOG();
        

        public Form1()
        {
            InitializeComponent();
        }

        private void button_open_image_Click(object sender, EventArgs e)
        {

            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Open File Dialog";
            //fdlg.InitialDirectory = @"D:\";
            fdlg.Filter = "All files (*.jpg)|*.*|All files (*.*)|*.*";
            fdlg.RestoreDirectory = true;
            
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                img = new Bitmap(Image.FromFile(fdlg.FileName));
                img_origin = new Bitmap(img);
               
                

                myStack.Clear();
                myStack.Push(img);


                pictureBox1.Image = img_origin;
                //clear
                pictureBox2.Image = null;
                pictureBox_result_r.Image = null;
                pictureBox_result_g.Image = null;
                pictureBox_result_b.Image = null;
                label_result_image_pixel.Text = "0x0";
                textBox_height.Text = img.Height.ToString();
                textBox_width.Text = img.Width.ToString();



                label_image_pixel.Text = "" + img.Width + "x" + img.Height;

                pixel = img.Width * img.Height;

               int[] origin_r = new int[256];
                int[] origin_g = new int[256];
                 int[] origin_b = new int[256];

                for (int x = 0; x < img.Width; x++)
                {
                    for (int y = 0; y < img.Height; y++)
                    {
                        Color pixelColor = img.GetPixel(x, y);
                        origin_r[pixelColor.R]++;
                        origin_g[pixelColor.G]++;
                        origin_b[pixelColor.B]++;
                    }
                }
                int max_times = 0;

                for (int i = 0; i < 256; i++)
                {
                    if (max_times < origin_r[i])
                        max_times = origin_r[i];
                    if (max_times < origin_g[i])
                        max_times = origin_g[i];
                    if (max_times < origin_b[i])
                        max_times = origin_b[i];
                }
                Bitmap img1 = new Bitmap(256, 100);
                for (int x = 0; x < img1.Width; x++)
                    for (int y = 0; y < (int)(((float)origin_r[x] / max_times) * 100); y++)
                        img1.SetPixel(x, img1.Height - 1 - y, Color.FromArgb(255, 0, 0));

                pictureBox_source_r.Image = img1;

                Bitmap img2 = new Bitmap(256, 100);
                for (int x = 0; x < img1.Width; x++)
                    for (int y = 0; y < (int)(((float)origin_g[x] / max_times) * 100); y++)
                        img2.SetPixel(x, img2.Height - 1 - y, Color.FromArgb(0, 255, 0));
                pictureBox_source_g.Image = img2;

                Bitmap img3 = new Bitmap(256, 100);
                for (int x = 0; x < img1.Width; x++)
                    for (int y = 0; y < (int)(((float)origin_b[x] / max_times) * 100); y++)
                        img3.SetPixel(x, img3.Height - 1 - y, Color.FromArgb(0, 0, 255));

                pictureBox_source_b.Image = img3;
                

            }
        }
        private void button_origin_Click(object sender, EventArgs e)
        {
            try
            {
                img = new Bitmap(img_origin);
                img = img.Clone(new Rectangle(0, 0, img.Width, img.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                pixel = img.Width * img.Height;
                textBox_height.Text = img.Height.ToString();
                textBox_width.Text = img.Width.ToString();
                logcat_text = "";

                myStack.Clear();
                myStack.Push(img_origin);
            }
            catch
            {

            }
            pictureBox2.Image = img;
            result_rgb_picture();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button_grayscale_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();
            int[,,] rgb = GetRGBData(img);
            img = SetRGBData(mean_value.process(rgb, img.Width, img.Height));
            pictureBox2.Image = img;
            logcat_text += ">> Grayscale -> mean-vaule Execute Time = " + sw.ElapsedMilliseconds + "ms\r\n";
            label_pixel_vaule.Text = "";
            /*for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    label_pixel_vaule.Text += rgb[x,y,0].ToString()+" "; 
                }
                label_pixel_vaule.Text += "\n";
            }*/

  
           
            img = SetRGBData(rgb);
            myStack.Push(img);
            result_rgb_picture();
            sw.Stop();
        }

        private void button_black_white_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();

           
            int[,,] rgb = GetRGBData(img);
            
            otsus_method.process(rgb, img.Width, img.Height, pixel);
            img = SetRGBData(rgb);
            pictureBox2.Image = img;
            logcat_text += ">> Thresholding -> Otsu's method Execute Time = " + sw.ElapsedMilliseconds + "ms\r\n";
            result_rgb_picture();
            myStack.Push(img);
            sw.Stop();

        }


        private void button_negative_Click(object sender, EventArgs e)
        {
            int[,,] rgb = GetRGBData(img);
            
            img = SetRGBData(negative.process(rgb, img.Width, img.Height));
            pictureBox2.Image = img;
            myStack.Push(img);
            result_rgb_picture();
        }

        private void button_brightness_Click(object sender, EventArgs e)
        {
            int[,,] rgb = GetRGBData(img);
            img = SetRGBData(brighten.process(rgb,img.Width,img.Height,10));
            pictureBox2.Image = img;
            myStack.Push(img);
            result_rgb_picture();
        }

        private void button_darkness_Click(object sender, EventArgs e)
        {
            int[,,] rgb = GetRGBData(img);
            img = SetRGBData(darken.process(rgb,img.Width,img.Height,10));
            pictureBox2.Image = img;
            myStack.Push(img);
            result_rgb_picture();
        }

        private void button_reverse_Click(object sender, EventArgs e)
        {
            int[,,] rgb = GetRGBData(img);
            int[,,] rgb2 = GetRGBData(img);
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    rgb2[x, y, 0] = rgb[img.Width - 1 - x, y, 0];
                    rgb2[x, y, 1] = rgb[img.Width - 1 - x, y, 1];
                    rgb2[x, y, 2] = rgb[img.Width - 1 - x, y, 2];
                }
            }
            img = SetRGBData(rgb2);
            pictureBox2.Image = img;
            myStack.Push(img);
            result_rgb_picture();
        }

        private void result_rgb_picture()
        {
            int[,,] rgb = GetRGBData(img);
            int[] result_r = new int[256];
            int[] result_g = new int[256];
            int[] result_b = new int[256];

            label_result_image_pixel.Text = img.Width + "x" + img.Height;
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    result_r[rgb[x, y, 0]]++;
                    result_g[rgb[x, y, 1]]++;
                    result_b[rgb[x, y, 2]]++;
                }
            }
            int max_times = 0; //RGB最多次數
   
            for (int i = 0; i < 256; i++)
            {
                if (max_times < result_r[i])
                    max_times = result_r[i];
                if (max_times < result_g[i])
                    max_times = result_g[i];
                if (max_times < result_b[i])
                    max_times = result_b[i];
            }
           
            Bitmap img1 = new Bitmap(256, 100);
            Bitmap img2 = new Bitmap(256, 100);
            Bitmap img3 = new Bitmap(256, 100);

            for (int x = 0; x < img1.Width; x++)
                for (int y = 0; y < (int)((result_r[x] / (float)max_times) * 100); y++)
                    img1.SetPixel(x, img1.Height - 1 - y, Color.FromArgb(255, 0, 0));

            for (int x = 0; x < img2.Width; x++)
                for (int y = 0; y < (int)((result_g[x] / (float)max_times) * 100); y++)
                    img2.SetPixel(x, img2.Height - 1 - y, Color.FromArgb(0, 255, 0));

            for (int x = 0; x < img3.Width; x++)
                for (int y = 0; y < (int)((result_b[x] / (float)max_times) * 100); y++)
                    img3.SetPixel(x, img3.Height - 1 - y, Color.FromArgb(0, 0, 255));

            pictureBox_result_r.Image = img1;
            pictureBox_result_g.Image = img2;
            pictureBox_result_b.Image = img3;

          
            label_logcat.Text = logcat_text;
            label_logcat.SelectionStart = label_logcat.Text.Length;
            label_logcat.ScrollToCaret();
        }

        private void button_histogram_equalization_Click(object sender, EventArgs e)
        {
            int[,,] rgb = GetRGBData(img);
            img = SetRGBData(histogram_equalization.process(rgb, img.Width, img.Height, pixel));   
            pictureBox2.Image = img;
            myStack.Push(img);
            result_rgb_picture();
        }

        private void button_save_image_Click(object sender, EventArgs e)
        {
            SaveFileDialog fdlg = new SaveFileDialog();
            fdlg.Title = "Save File Dialog";
            fdlg.InitialDirectory = @"D:\";
            fdlg.Filter = "All files (*.bmp)|*.*|All files (*.*)|*.*";
            //fdlg.RestoreDirectory = true;

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                img.Save(fdlg.FileName + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);

            }
        }

        private void button_median_filter_Click(object sender, EventArgs e)
        {
            int[,,] rgb = GetRGBData(img);
            img = SetRGBData(median_filiter.process(rgb,img.Width,img.Height));
            pictureBox2.Image = img;
            myStack.Push(img);
            result_rgb_picture();
        }

        private void button_mean_filiter_Click(object sender, EventArgs e)
        {
            int[,,] rgb = GetRGBData(img);
            img = SetRGBData(mean_filiter.process(rgb,img.Width,img.Height));
            pictureBox2.Image = img;
            myStack.Push(img);
            result_rgb_picture();
        }

        private void button_sobel_Click(object sender, EventArgs e)
        {
            int[,,] rgb = GetRGBData(img);
            
            img = SetRGBData(sobel.process(rgb,img.Width,img.Height));
            pictureBox2.Image = img;
            myStack.Push(img);
            result_rgb_picture();
        }

        private void button_erosion_Click(object sender, EventArgs e)
        {
            //button_black_white_Click(null, null);
           
            int[,,] rgb = GetRGBData(img);

            img = SetRGBData(erosion.process(rgb,img.Width,img.Height));
            pictureBox2.Image = img;
            myStack.Push(img);
            result_rgb_picture();
        }

        private void button_dilation_Click(object sender, EventArgs e)
        {
            //button_black_white_Click(null, null);
            int[,,] rgb = GetRGBData(img);
            img = SetRGBData(dilation.process(rgb,img.Width,img.Height));
            pictureBox2.Image = img;
            myStack.Push(img);
            result_rgb_picture();
        }

        private void button_opening_Click(object sender, EventArgs e)
        {
            button_dilation_Click(null, null);
            button_erosion_Click(null, null);
        }

        private void button_closing_Click(object sender, EventArgs e)
        {
            button_erosion_Click(null, null);
            button_dilation_Click(null, null);
        }

        private void button_laplacian_Click(object sender, EventArgs e)
        {
            int[,,] rgb = GetRGBData(img);

            
            img = SetRGBData(laplacian.process(rgb,img.Width,img.Height));
            pictureBox2.Image = img;
            myStack.Push(img);
            result_rgb_picture();
        }

        private void button_kirsch_filter_Click(object sender, EventArgs e)
        {
            int[,,] rgb = GetRGBData(img);
            img = SetRGBData(kirsch.process(rgb,img.Width,img.Height));
            pictureBox2.Image = img;
            myStack.Push(img);
            result_rgb_picture();
        }

        private void button_mean_weight_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();
            
            int[,,] rgb = GetRGBData(img);
            
            img = SetRGBData(mean_weight.process(rgb,img.Width,img.Height));
            pictureBox2.Image = img;
            logcat_text += ">> Grayscale -> mean-weight Execute Time = " + sw.ElapsedMilliseconds + "ms\r\n";
            result_rgb_picture();
            sw.Stop();
            myStack.Push(img);

        }

        private void button_sharpen_Click(object sender, EventArgs e)
        {
           
            int[,,] rgb_origin = GetRGBData(img); 
            button_laplacian_Click(null, null);
            int[,,] rgb_laplacian = GetRGBData(img);

            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    int r = rgb_origin[x, y, 0] + rgb_laplacian[x, y, 0];
                    if (r > 255)
                        r = 255;
                    else if (r < 0)
                        r = 0;

                    int g = rgb_origin[x, y, 1] + rgb_laplacian[x, y, 1];
                    if (g > 255)
                        g = 255;
                    else if (g < 0)
                        g = 0;
                    int b = rgb_origin[x, y, 2] + rgb_laplacian[x, y, 2];
                    if (b > 255)
                        b = 255;
                    else if (b < 0)
                        b = 0;
                    rgb_origin[x, y, 0] = r;
                    rgb_origin[x, y, 1] = g;
                    rgb_origin[x, y, 2] = b;
                }
            }
            img = SetRGBData(rgb_origin);
            pictureBox2.Image = img;
            myStack.Push(img);
        }
        //高效率用指標讀取影像資料
        public static int[,,] GetRGBData(Bitmap bitImg)
        {
            int height = bitImg.Height;
            int width = bitImg.Width;
            //鎖住Bitmap整個影像內容
            BitmapData bitmapData = bitImg.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            //取得影像資料的起始位置
            IntPtr imgPtr = bitmapData.Scan0;
            //影像scan的寬度
            int stride = bitmapData.Stride;
            //影像陣列的實際寬度
            int widthByte = width * 3;
            //所Padding的Byte數
            int skipByte = stride - widthByte;
            //設定預定存放的rgb三維陣列
            int[,,] rgbData = new int[width, height, 3];

            #region 讀取RGB資料
            //注意C#的GDI+內的影像資料順序為BGR, 非一般熟悉的順序RGB
            //因此我們把順序調回原來的陣列順序排放BGR->RGB
            unsafe
            {
                byte* p = (byte*)imgPtr;
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        //B Channel
                        rgbData[i, j, 2] = p[0];
                        p++;
                        //G Channel
                        rgbData[i, j, 1] = p[0];
                        p++;
                        //R Channel
                        rgbData[i, j, 0] = p[0];
                        p++;
                    }
                    p += skipByte;
                }
            }

            //解開記憶體鎖
            bitImg.UnlockBits(bitmapData);

            #endregion

            return rgbData;
        }

        //高效率圖形轉換工具--由陣列設定新的Bitmap
        public static Bitmap SetRGBData(int[,,] rgbData)
        {
            //宣告Bitmap變數
            Bitmap bitImg;
            int width = rgbData.GetLength(0);
            int height = rgbData.GetLength(1);

            //依陣列長寬設定Bitmap新的物件
            bitImg = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            //鎖住Bitmap整個影像內容
            BitmapData bitmapData = bitImg.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            //取得影像資料的起始位置
            IntPtr imgPtr = bitmapData.Scan0;
            //影像scan的寬度
            int stride = bitmapData.Stride;
            //影像陣列的實際寬度
            int widthByte = width * 3;
            //所Padding的Byte數
            int skipByte = stride - widthByte;
            #region 設定RGB資料
            //注意C#的GDI+內的影像資料順序為BGR, 非一般熟悉的順序RGB
            //因此我們把順序調回GDI+的設定值, RGB->BGR
            unsafe
            {
                byte* p = (byte*)(void*)imgPtr;
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        //B Channel
                        p[0] = (byte)rgbData[i, j, 2];
                        p++;
                        //G Channel
                        p[0] = (byte)rgbData[i, j, 1];
                        p++;
                        //B Channel
                        p[0] = (byte)rgbData[i, j, 0];
                        p++;
                    }
                    p += skipByte;
                }
            }

            //解開記憶體鎖
            bitImg.UnlockBits(bitmapData);

            #endregion
            return bitImg;
        }

        private void button_resize_Click(object sender, EventArgs e)
        {
            int resize_height = 0;
            int resize_width = 0;

            try
            {
                resize_height = Int32.Parse(textBox_height.Text);
                resize_width = Int32.Parse(textBox_width.Text);
                img = new Bitmap(img, new Size(resize_width, resize_height));
                label_result_image_pixel.Text = resize_width.ToString() + "x" + resize_height.ToString();
                pictureBox2.Image = img;
                pixel = img.Width * img.Height;
                textBox_height.Text = img.Height.ToString();
                textBox_width.Text = img.Width.ToString();
                result_rgb_picture();
            }
            catch
            {
                MessageBox.Show("Please select image!!", "Error");
            }
        }

        private void button_logarithmic_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();
            int[,,] rgb = GetRGBData(img);

           
            img = SetRGBData(logarithmic.process(rgb,img.Width,img.Height));
            pictureBox2.Image = img;
            result_rgb_picture();
            myStack.Push(img);
            sw.Stop();

        }
        private void button_undo_Click(object sender, EventArgs e)
        {
            try
            {
                myStack.Pop();
            }
            catch
            {

            }
            if (myStack.Count > 0)
            {
                img = myStack.Pop();
                pictureBox2.Image = img;
                result_rgb_picture();
                myStack.Push(img);

            }
            else if (myStack.Count == 0)
                myStack.Push(img_origin);
        }
        private void button_encryption_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();
            int[,,] rgb = GetRGBData(img);

            img = SetRGBData(encryption.process(rgb, img.Width, img.Height, textBox_encryption.Text));
            pictureBox2.Image = img;
            //pictureBox2.Image = SetRGBData(mean_value.process(rgb, img.Width, img.Height));
            logcat_text += ">> Encryption Execute Time = " + sw.ElapsedMilliseconds + "ms\r\n";
            img = SetRGBData(rgb);
            myStack.Push(img);
            result_rgb_picture();
            sw.Stop();
        }

        private void button_decrypt_Click(object sender, EventArgs e)
        {
            int[,,] rgb = GetRGBData(img);
            textBox_encryption.Text = decryption.process(rgb, img.Width, img.Height);
            
        }

        private void button_clear_text_Click(object sender, EventArgs e)
        {
            textBox_encryption.Text = "";
        }

        private void button_k_means_Click(object sender, EventArgs e)
        {
            
            int k_vaule = Int32.Parse(textBox_k.Text);
            int interation_level = Int32.Parse(textBox_interation_level.Text);
            int[,,] rgb = GetRGBData(img);
            img = SetRGBData(k_means.process(rgb,img.Width,img.Height,k_vaule,interation_level));
            myStack.Push(img);
            result_rgb_picture();
            pictureBox2.Image = img;
            

        }
        private void button_8_bit_plane_slicing_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();
            int[,,] rgb = GetRGBData(img);
            
            img = SetRGBData(bit_plane_slicing.process(rgb, img.Width, img.Height, Int32.Parse(textBox_bit.Text)));
            pictureBox2.Image = img;
            logcat_text += ">> Compression -> 8-bit plane slicing Execute Time = " + sw.ElapsedMilliseconds + "ms\r\n";
            img = SetRGBData(rgb);
            myStack.Push(img);
            result_rgb_picture();
            sw.Stop();
        }

        private void button_grayscale_maxium_Click(object sender, EventArgs e)
        {
            int[,,] rgb = GetRGBData(img);
            
            img = SetRGBData(maxium.process(rgb,img.Width,img.Height));
            pictureBox2.Image = img;
            myStack.Push(img);
            result_rgb_picture();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String s = "";
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Color color = img.GetPixel(x, y);                   
                    s += string.Format("{0:D3}", color.R) + " ";
                }
                s += "\n";
            }
            label_pixel_vaule.Text = s;
        }

        private void button_histogram_gradient_Click(object sender, EventArgs e)
        {
            
            button_mean_weight_Click(null, null);
            button_logarithmic_Click(null, null);
            int[,,] rgb = GetRGBData(img);
            img = SetRGBData(hog.process(rgb, img.Width, img.Height));
            pictureBox2.Image = img;
            myStack.Push(img);
            result_rgb_picture();

        }

        private void Label15_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
