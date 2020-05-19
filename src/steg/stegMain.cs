using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Steganography
{
    class StegMain
    {
        // This is the main function where the file embedding occurs. This function is called from the front end
        public static WriteableBitmap EmbedImage(BitmapSource preStegsrc)
        {
            int width = preStegsrc.PixelWidth;
            int height = preStegsrc.PixelHeight;

            byte[] preStegPixelBytes = GetPixels(preStegsrc);

            WriteableBitmap stegWBMP = new WriteableBitmap(width,
                                                           height,
                                                           preStegsrc.DpiX,
                                                           preStegsrc.DpiY,
                                                           PixelFormats.Bgra32,     // Let's try this pixel format for now...
                                                           preStegsrc.Palette);

            int offset = 2;

            // Steganography goes here
            for (int pixelNum = 0; pixelNum < width * height * 4; pixelNum += 4)
            {
                // offset of:
                // 0 - Blue,
                // 1 - Green,
                // 2 - Red,
                // 3 - Alpha
                preStegPixelBytes[pixelNum + 0] >>= offset;
                preStegPixelBytes[pixelNum + 1] >>= offset;
                preStegPixelBytes[pixelNum + 2] >>= offset;
                preStegPixelBytes[pixelNum + 3] >>= offset;

                preStegPixelBytes[pixelNum + 0] <<= offset;
                preStegPixelBytes[pixelNum + 1] <<= offset;
                preStegPixelBytes[pixelNum + 2] <<= offset;
                preStegPixelBytes[pixelNum + 3] <<= offset;

            }

            stegWBMP.WritePixels(new Int32Rect(0, 0, width, height), preStegPixelBytes, preStegsrc.PixelWidth*4, 0);

            return stegWBMP;
        }


        // This function gets the Bgra32 pixels from the BitmapImage
        private static byte[] GetPixels(BitmapSource src)
        {
            if (src.Format != PixelFormats.Bgra32)
            {
                src = new FormatConvertedBitmap(src, PixelFormats.Bgra32, null, 0);
            }

            int width = src.PixelWidth;
            int height = src.PixelHeight;

            // Copy the pixels into a byte[]
            byte[] pixelInfo = new byte[width * height * 4];
            src.CopyPixels(pixelInfo, width * 4, 0);

            return pixelInfo;
        }
    }
}