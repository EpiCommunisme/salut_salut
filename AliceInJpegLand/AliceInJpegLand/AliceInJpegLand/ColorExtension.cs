using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace AliceInJpegLand
{
    public static class ColorExtension
    {
        /// <summary>
        /// Get a color from a string
        /// </summary>
        /// <param name="str">Hexadecimal color code (with no #)</param>
        public static Color FromHexa(string str)
        {
            string R = "";
            string G = "";
            string B = "";
            Color res = Color.FromArgb((int) Convert.ToInt64((R+ str[0] + str[1]), 16), (int) Convert.ToInt64((G + str[2] + str[3]), 16),
                (int) Convert.ToInt64((B + str[4] + str[5]), 16));



            return res;
        }

        /// <summary>
        /// Returns a color corresponding to the given color inverted
        /// </summary>
        /// <param name="color">The color to work on</param>
        public static Color Invert(this Color color)
        {
            List<int> colors = new List<int>();
            colors.Add(color.R);
            colors.Add(color.G);
            colors.Add(color.B);

            List<int> newColors = new List<int>();
            foreach (int c in colors)
            {
                if (c < 128)
                    newColors.Add(Restrict256(c + 2 * (128 - c)));
                else
                    newColors.Add(Restrict256(c + 2 * (c - 128)));
            }

            return Color.FromArgb(newColors[0], newColors[1], newColors[2]);
        }

        /// <summary>
        /// Return a color corresponding to the given color human grayscaled (see formula)
        /// </summary>
        /// <param name="color">The color to work on</param>
        public static Color Grayscale(this Color color)
        {
            List<int> colors = new List<int>();
            colors.Add(color.R);
            colors.Add(color.G);
            colors.Add(color.B);

            int result = Convert.ToInt32(Restrict256(0.21 * colors[0] + 0.72 * colors[1] + 0.07 * colors[2]));

            return Color.FromArgb(result, result, result);
        }

        /// <summary>
        /// OPTIONAL
        /// Limits an int n between 0 and 255
        /// </summary>
        private static int Restrict256(int n)
        {
            if (n > 255)
                n = 255;
            else if (n < 0)
                n = 0;
            return n;
        }

        /// <summary>
        /// OPTIONAL
        /// Limits a float f between 0 and 255
        /// </summary>
        private static int Restrict256(double f)
        {
            if (f > 255)
                f = 255;
            else if (f < 0)
                f = 0;
            return Convert.ToInt32(f);
        }

        /// <summary>
        /// Returns a color corresponding the given color with a modified brightness
        /// </summary>
        /// <param name="color">The color to work on</param>
        /// <param name="delta">Brightness modification (usually between -255 and 255)</param>
        public static Color Brightness(this Color color, int delta)
        {
            List<int> colors = new List<int>();
            colors.Add(color.R);
            colors.Add(color.G);
            colors.Add(color.B);

            List<int> newColors = new List<int>();
            newColors.Add(Restrict256(colors[0] + delta));
            newColors.Add(Restrict256(colors[1] + delta));
            newColors.Add(Restrict256(colors[2] + delta));

            return Color.FromArgb(newColors[0], newColors[1], newColors[2]);
        }

        /// <summary>
        /// Returns a contrasted color with a given factor
        /// </summary>
        /// <param name="color">Color to modify</param>
        /// <param name="factor">Factor of modification</param>
        public static Color Contrast(this Color color, double factor)
        {
            List<int> colors = new List<int>();
            colors.Add(color.R);
            colors.Add(color.G);
            colors.Add(color.B);

            List<int> newColors = new List<int>();
            newColors.Add(Restrict256(factor * (colors[0] - 128) + 128));
            newColors.Add(Restrict256(factor * (colors[1] - 128) + 128));
            newColors.Add(Restrict256(factor * (colors[2] - 128) + 128));

            return Color.FromArgb(newColors[0], newColors[1], newColors[2]);
        }

        /// <summary>
        /// Returns a color resulting from a gradient map
        /// </summary>
        /// <param name="color">The color to work on</param>
        /// <param name="blackMatch">Dark color match in the gradient</param>
        /// <param name="whiteMatch">Light color match in the gradient</param>
        public static Color GradientMap(this Color color,
                Color blackMatch, Color whiteMatch)
        {
            List<int> colors = new List<int>();
            colors.Add(color.R);
            colors.Add(color.G);
            colors.Add(color.B);

            int luminosity = (colors[0] + colors[1] + colors[2]) / 3;

            List<int> darkColors = new List<int>();
            darkColors.Add(blackMatch.R);
            darkColors.Add(blackMatch.G);
            darkColors.Add(blackMatch.B);

            List<int> brightColors = new List<int>();
            brightColors.Add(whiteMatch.R);
            brightColors.Add(whiteMatch.G);
            brightColors.Add(blackMatch.B);

            List<int> deltas = new List<int>();
            deltas.Add((brightColors[0] - darkColors[0]) / 2);
            deltas.Add((brightColors[1] - darkColors[1]) / 2);
            deltas.Add((brightColors[1] - darkColors[1]) / 2);


            List<int> newColors = new List<int>();
            newColors.Add(Restrict256(luminosity * deltas[0] + darkColors[0]));
            newColors.Add(Restrict256(luminosity * deltas[1] + darkColors[1]));
            newColors.Add(Restrict256(luminosity * deltas[2] + darkColors[2]));

            return Color.FromArgb(newColors[0], newColors[1], newColors[2]);
        }

        /// <summary>
        /// Returns the resulting color from two color with an opacity percentage
        /// </summary>
        /// <param name="a">Color to cover</param>
        /// <param name="b">Covering color</param>
        /// <param name="opacity">Opacity percentage</param>
        public static Color Cover(this Color a, Color b, int opacity)
        {
            List<int> aColors = new List<int>();
            aColors.Add(a.R);
            aColors.Add(a.G);
            aColors.Add(a.B);

            List<int> bColors = new List<int>();
            bColors.Add(b.R);
            bColors.Add(b.G);
            bColors.Add(b.B);

            List<int> newColors = new List<int>();
            newColors.Add(Restrict256(aColors[0] * (100 - opacity) + bColors[0] * opacity));
            newColors.Add(Restrict256(aColors[1] * (100 - opacity) + bColors[1] * opacity));
            newColors.Add(Restrict256(aColors[2] * (100 - opacity) + bColors[2] * opacity));

            return Color.FromArgb(newColors[0], newColors[1], newColors[2]);
        }
    }
}
