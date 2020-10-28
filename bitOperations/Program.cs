using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace bitOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            Encript("Hello", "Image.jpg");
            Console.WriteLine(DecriptToString("out.png"));
        }
        static void Encript(string text, string image_name)
        {
            Image<Rgba32> image = (Image<Rgba32>)Image.Load(image_name);
            byte[] text_bytes = Encoding.UTF8.GetBytes(text);
            int iteration = 0;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    if (iteration < text_bytes.Length)
                    {
                        byte segment_1 = (byte)(text_bytes[iteration] >> 5);
                        byte segment_2 = (byte)((text_bytes[iteration] >> 2) & 7);
                        byte segment_3 = (byte)(((text_bytes[iteration] & 3) << 1) | 1);
                        byte R = (byte)((image[x, y].R & 248) | segment_1);
                        byte G = (byte)((image[x, y].G & 248) | segment_2);
                        byte B = (byte)((image[x, y].B & 248) | segment_3);
                        image[x, y] = new Rgba32(R, G, B);
                        iteration++;
                    }
                    else
                    {
                        byte R = image[x, y].R;
                        byte G = image[x, y].G;
                        byte B = (byte)(image[x, y].B & 254);
                        image[x, y] = new Rgba32(R, G, B);
                    }
                }
            }
            image.SaveAsPng("OUT.png");
        }
        static string DecriptToString(string image_name)
        {
            Image<Rgba32> image = (Image<Rgba32>)Image.Load(image_name);
            List<byte> text_bytes = new List<byte>();
            bool flag = false;
            for (int y = 0; y < image.Height; y++)
            {
                if (flag)
                    break;
                for (int x = 0; x < image.Width; x++)
                {
                    if ((image[x, y].B & 1) == 1)
                    {
                        byte segment_1 = (byte)(image[x, y].R & 7);
                        byte segment_2 = (byte)(image[x, y].G & 7);
                        byte segment_3 = (byte)((image[x, y].B & 7) >> 1);
                        byte full_segment = (byte)((segment_1 << 5) + (segment_2 << 2) + segment_3);
                        text_bytes.Add(full_segment);
                    }
                    else
                    {
                        flag = true;
                        break;
                    }

                }
            }
            return Encoding.UTF8.GetString(text_bytes.ToArray());
        }
        static void Encript(byte[] information, string image_name)
        {
            Image<Rgba32> image = (Image<Rgba32>)Image.Load(image_name);
            int iteration = 0;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    if (iteration < information.Length)
                    {
                        byte segment_1 = (byte)(information[iteration] >> 5);
                        byte segment_2 = (byte)((information[iteration] >> 2) & 7);
                        byte segment_3 = (byte)(((information[iteration] & 3) << 1) | 1);
                        byte R = (byte)((image[x, y].R & 248) | segment_1);
                        byte G = (byte)((image[x, y].G & 248) | segment_2);
                        byte B = (byte)((image[x, y].B & 248) | segment_3);
                        image[x, y] = new Rgba32(R, G, B);
                        iteration++;
                    }
                    else
                    {
                        byte R = image[x, y].R;
                        byte G = image[x, y].G;
                        byte B = (byte)(image[x, y].B & 254);
                        image[x, y] = new Rgba32(R, G, B);
                    }
                }
            }
            image.Save("OUT.bmp");
        }
        static byte[] DecriptToBytes(string image_name)
        {
            Image<Rgba32> image = (Image<Rgba32>)Image.Load(image_name);
            List<byte> information = new List<byte>();
            bool flag = false;
            for (int y = 0; y < image.Height; y++)
            {
                if (flag)
                    break;
                for (int x = 0; x < image.Width; x++)
                {
                    if ((image[x, y].B & 1) == 1)
                    {
                        byte segment_1 = (byte)(image[x, y].R & 7);
                        byte segment_2 = (byte)(image[x, y].G & 7);
                        byte segment_3 = (byte)((image[x, y].B & 7) >> 1);
                        byte full_segment = (byte)((segment_1 << 5) + (segment_2 << 2) + segment_3);
                        information.Add(full_segment);
                    }
                    else
                    {
                        flag = true;
                        break;
                    }

                }
            }
            return information.ToArray();
        }
    }
}
