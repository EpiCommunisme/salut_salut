using System;
using System.Drawing;

namespace AliceInJpegLand
{
    public static class Convolution
    {
        /// <summary>
        /// Gauss convolution Matrix
        /// </summary>
        private static readonly float[,] GaussMatrix = {
            {1/9f, 2/9f, 1/9f},
            {2/9f, 4/9f, 2/9f},
            {1/9f, 2/9f, 1/9f}
        };

        /// <summary>
        /// Sharpen convolution Matrix
        /// </summary>
        private static readonly float[,] SharpenMatrix = {
            { 0f, -1f,  0f},
            {-1f,  5f, -1f},
            { 0f, -1f,  0f}
        };

        /// <summary>
        /// Blur convolution Matrix
        /// </summary>
        private static readonly float[,] BlurMatrix = {
            {1/9f, 1/9f, 1/9f},
            {1/9f, 1/9f, 1/9f},
            {1/9f, 1/9f, 1/9f}
        };

        /// <summary>
        /// Edge enhance convolution Matrix
        /// </summary>
        private static readonly float[,] EdgeEnhanceMatrix = {
            { 0f, 0f, 0f},
            {-1f, 1f, 0f},
            { 0f, 0f, 0f}
        };

        /// <summary>
        /// Edge detect convolution Matrix
        /// </summary>
        private static readonly float[,] EdgeDetectMatrix = {
            {0f,  1f, 0f},
            {1f, -4f, 1f},
            {0f,  1f, 0f}
        };

        /// <summary>
        /// Emboss convolution Matrix
        /// </summary>
        private static readonly float[,] EmbossMatrix = {
            {-2f, -1f, 0f},
            {-1f,  1f, 1f},
            { 0f,  1f, 2f}
        };

        /// <summary>
        /// Returns a copy of the image with a convolution mask applied
        /// </summary>
        /// <param name="image">Image to work on</param>
        /// <param name="mask">Convolution matrix to apply</param>
        /// <returns>A copy of image with convolution mask applied</returns>
        public static Bitmap Convolute(this Bitmap image, float[,] mask)
        {
            throw new NotImplementedException("The clock is ticking");
        }


        /// <summary>
        /// Applies gauss convolution
        /// </summary>
        /// <param name="image">Image to work on</param>
        public static Bitmap Gauss(this Bitmap image)
        {
            throw new NotImplementedException("The clock is ticking");
        }

        /// <summary>
        /// Applies sharpen convolution
        /// </summary>
        /// <param name="image">Image to work on</param>
        public static Bitmap Sharpen(this Bitmap image)
        {
            throw new NotImplementedException("The clock is ticking");
        }

        /// <summary>
        /// Applies blur convolution
        /// </summary>
        /// <param name="image">Image to work on</param>
        public static Bitmap Blur(this Bitmap image)
        {
            throw new NotImplementedException("The clock is ticking");
        }

        /// <summary>
        /// Applies edge enhance convolution
        /// </summary>
        /// <param name="image">Image to work on</param>
        public static Bitmap EdgeEnhance(this Bitmap image)
        {
            throw new NotImplementedException("The clock is ticking");
        }

        /// <summary>
        /// Applies edge detect convolution
        /// </summary>
        /// <param name="image">Image to work on</param>
        public static Bitmap EdgeDetect(this Bitmap image)
        {
            throw new NotImplementedException("The clock is ticking");
        }

        /// <summary>
        /// Applies emboss convolution
        /// </summary>
        /// <param name="image">Image to work on</param>
        public static Bitmap Emboss(this Bitmap image)
        {
            throw new NotImplementedException("The clock is ticking");
        }

    }
}
