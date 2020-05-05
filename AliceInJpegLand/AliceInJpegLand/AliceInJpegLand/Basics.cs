using System;
using System.Drawing;

namespace AliceInJpegLand
{
    public static class Basics
    {
        /// <summary>
        /// Applies a color modification function on each pixel of an image
        /// </summary>
        /// <param name="image">Image to modify</param>
        /// <param name="modify">Function to apply</param>
        public static void ForEachPixel(this Bitmap image, Func<Color, Color> modify)
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    image.SetPixel(x, y, modify(image.GetPixel(x, y)));
                }
            }
        }

        /// <summary>
        /// Invert the colors of the image
        /// </summary>
        /// <param name="image">Image to modify</param>
        public static void Invert(this Bitmap image)
        {
            ForEachPixel(image, ColorExtension.Invert);
        }

        /// <summary>
        /// Applies a grayscale on the image
        /// </summary>
        /// <param name="image">Image to modify</param>
        public static void Grayscale(this Bitmap image)
        {
            ForEachPixel(image, ColorExtension.Grayscale);
        }

        /// <summary>
        /// Changes brightness of the image
        /// </summary>
        /// <param name="image">Image to modify</param>
        /// <param name="delta">Between -255 and 255, if less, black, if more, white</param>
        public static void Brightness(this Bitmap image, int delta)
        {
            Func<Color, Color> brightness = (color => ColorExtension.Brightness(color, delta));
            ForEachPixel(image, brightness);
        }

        /// <summary>
        /// Increase or decrease the contrast of an image
        /// </summary>
        /// <param name="image">Image to modify</param>
        /// <param name="delta">if greater than 511, image will be inverted
        /// between -255 and 255, normal contrast change
        /// between 255 and 511, between very contrasted and inverted
        /// http://www.dfstudios.co.uk/articles/programming/image-programming-algorithms/image-processing-algorithms-part-5-contrast-adjustment/
        /// </param>
        public static void Contrast(this Bitmap image, int delta)
        {
            if (delta == 259)
                delta = 258;

            double factor = (259 * (delta + 255)) / (255 * (259 - delta));
            Func<Color, Color> contrast = (color => ColorExtension.Contrast(color, factor));
            ForEachPixel(image, contrast);
        }

        /// <summary>
        /// Changes the image according to a gradient between 2 colors
        /// Black pixels will be replaced with dark color
        /// White pixels will be replaced with bright color
        /// Other pixels are replaced by their position on the gradient
        /// </summary>
        /// <param name="image">Image to modify</param>
        /// <param name="dark">Darkest color of the gradient</param>
        /// <param name="bright">Brightest color of the gradient</param>
        public static void GradientMap(this Bitmap image, Color dark, Color bright)
        {
            Func<Color, Color> gradientMap = (color => ColorExtension.GradientMap(color, dark, bright));
            ForEachPixel(image, gradientMap);
        }

        /// <summary>
        /// Covers an image with another one
        /// </summary>
        /// <param name="image">Image to modify</param>
        /// <param name="path">Path of the image to cover with</param>
        /// <param name="opacity">Percentage for the opacity of the cover</param>
        /// <exception cref="ArgumentException">throws if images don't have the same size</exception>
        public static void Cover(this Bitmap image, string path, int opacity)
        {
            if (opacity < 0)
                opacity = 0;
            else if (opacity > 100)
                opacity = 100;

            Bitmap image2 = new Bitmap(path);

            if (image.Size != image2.Size)
                throw new ArgumentException("The image to cover with the source image must have the same size.");

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    image.SetPixel(x, y, ColorExtension.Cover(image.GetPixel(x, y), image2.GetPixel(x, y), opacity));
                }
            }
        }
    }
}
