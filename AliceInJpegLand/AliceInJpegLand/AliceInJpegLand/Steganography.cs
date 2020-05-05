using System;
using System.Collections.Generic;
using System.Drawing;

namespace AliceInJpegLand
{
    public static class Steganography
    {
        /// <summary>
        /// BONUS
        /// Encrypt a string in the least significant bits of the image
        /// </summary>
        /// <param name="image">Image to modify</param>
        /// <param name="str">String to encrypt</param>
        /// <exception cref="ArgumentException">throws if the text is too long for the image</exception>
        public static void EncryptString(this Bitmap image, String str)
        {
            throw new NotImplementedException("The clock is ticking");
        }

        /// <summary>
        /// BONUS
        /// Decrypt a string from the least significant bits of the image
        /// </summary>
        /// <param name="image">Image to modify</param>
        /// <returns>The encrypted string</returns>
        public static String DecryptString(this Bitmap image)
        {
            throw new NotImplementedException("The clock is ticking");
        }


        /// <summary>
        /// BONUS
        /// Encrypts an image in another by storing most significant bits in less significant bits
        /// </summary>
        /// <param name="image">Image to modify</param>
        /// <param name="path">Path of the image to encrypt</param>
        /// <exception cref="ArgumentException">throws if the image to encrypt is bigger than the actual image</exception>
        public static void EncryptImage(this Bitmap image, String path)
        {
            throw new NotImplementedException("The clock is ticking");
        }

        /// <summary>
        /// BONUS
        /// Decrypts an image from another by getting less significant bits as most significant bits
        /// </summary>
        /// <param name="image">Image to modify</param>
        /// <returns>The resulting image</returns>
        public static Bitmap DecryptImage(this Bitmap image)
        {
            throw new NotImplementedException("The clock is ticking");
        }
    }
}
