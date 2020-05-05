using System;
using System.Drawing;

namespace AliceInJpegLand
{
    public static class Geometry
    {
        /// <summary>
        /// Gets a copy of the image rotated 90 degrees clockwise
        /// </summary>
        /// <param name="image">Image to work on</param>
        public static Bitmap RotateRight(this Bitmap image)
        {
            Bitmap res = new Bitmap(image.Height, image.Width);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Width; y++)
                {
                    res.SetPixel(image.Height-1-x, y, image.GetPixel(y, x));
                }
            }

            return res;
        }

        /// <summary>
        /// Gets a copy of the image rotated 90 degrees anti-clockwise
        /// </summary>
        /// <param name="image">Image to work on</param>
        public static Bitmap RotateLeft(this Bitmap image)
        {
            Bitmap res = new Bitmap(image.Height, image.Width);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Width; y++)
                {
                    res.SetPixel(y, image.Width-1-x, image.GetPixel(x, y));
                }
            }

            return res;
        }

        /// <summary>
        /// Applies a vertical rotation
        /// </summary>
        /// <param name="image">Image to work on</param>
        public static void SymmetryX(this Bitmap image)
        {
            Bitmap newBitmap = new Bitmap(image);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Width; y++)
                {
                    image.SetPixel(image.Width-1-x, y, newBitmap.GetPixel(x, y));
                }
            }
        }

        /// <summary>
        /// Applies a horizontal rotation
        /// </summary>
        /// <param name="image">Image to work on</param>
        public static void SymmetryY(this Bitmap image)
        {
            Bitmap newBitmap = new Bitmap(image);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Width; y++)
                {
                    image.SetPixel(y, image.Height-1-x, newBitmap.GetPixel(y,x));
                }
            }
        }

        /// <summary>
        /// Gets a copy of the image resized
        /// </summary>
        /// <param name="image">Image to work on</param>
        /// <param name="x">Width of the new image</param>
        /// <param name="y">Height of the new image</param>
        public static Bitmap Resize(this Bitmap image, int x, int y)
        {
            Bitmap res = new Bitmap(x, y);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    res.SetPixel(i, j, image.GetPixel(i * image.Width / x, j * image.Height / y));
                }
            }

            return res;
        }


        /// <summary>
        /// Gets a copy of the image shifted
        /// </summary>
        /// <param name="image">Image to work on</param>
        /// <param name="x">Horizontal shift</param>
        /// <param name="y">Vertical shift</param>
        public static Bitmap Shift(this Bitmap image, int x, int y)
        {
            Bitmap res = new Bitmap(x, y);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    res.SetPixel((i+x) % image.Width, (j+y) % image.Height, image.GetPixel(i, j));
                }
            }

            return res;
        }
    }
}
