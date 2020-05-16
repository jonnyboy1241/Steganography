using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public static WriteableBitmap EmbedImage(BitmapImage preStegbmp)
        {
            // Stride = (width) x (bytes per pixel)
            int stride = preStegbmp.PixelWidth * (preStegbmp.Format.BitsPerPixel + 7) / 8;

            // Check for the assumption I'm working with, look at later.
            if (preStegbmp.Format != PixelFormats.Bgr32)
            {
                Console.WriteLine("Wrong PixelFormat!");
                System.Windows.Application.Current.Shutdown();
            }

            byte[] pixels = GetPixels(preStegbmp, stride);

            for(int i = 0; i < pixels.Length; i+=4)
            {
                pixels[i] = pixels[i];          // Blue Channel
                pixels[i + 1] = pixels[i + 1];  // Green Channel
                pixels[i + 2] = 0xbf;           // Red Channel
                pixels[i + 3] = 0xff;           // Extra Space, we can use this whole byte
            }

            WriteableBitmap postStegImage = new WriteableBitmap(preStegbmp.PixelWidth,
                                                                preStegbmp.PixelHeight,
                                                                preStegbmp.DpiX,
                                                                preStegbmp.DpiY,
                                                                preStegbmp.Format,
                                                                null);


            postStegImage.WritePixels(new Int32Rect(0, 0, preStegbmp.PixelWidth, preStegbmp.PixelHeight), pixels, stride, 0);

            return postStegImage;
        }


        // Helper function that extracts the pixels from the BitmapImage
        private static byte[] GetPixels(BitmapImage bmi, int stride)
        {
            byte[] pixels = new byte[bmi.PixelHeight * stride];
            bmi.CopyPixels(pixels, stride, 0);

            return pixels;
        }
    }
}