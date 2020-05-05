using System;
using System.Drawing;

namespace AliceInJpegLand
{
    public static class ParameterManager
    {
        /// <summary>
        /// Displays an help message
        /// </summary>
        private static void Help()
        {
            Console.WriteLine("Soyez plus intteligent");
        }

        /// <summary>
        /// Verify args validity for the program
        /// Prints an error message in error console if the arguments are invalid
        /// </summary>
        /// <param name="args">CLI arguments array</param>
        /// <returns>true if the arguments are valid, false otherwise</returns>

        public static bool IsColor(string hexa)
        {
            if (hexa.Length != 6)
                return false;
            bool res = true;
            int len = hexa.Length;
            int i = 0;
            while (i < len && res)
            {
                bool comp = (hexa[i] >= 65 && hexa[i] <= 70) || (hexa[i] >= 48 && hexa[i] <= 57) || (hexa[i] >= 97 && hexa[i] <= 102);
                if (comp)
                    i++;
                else
                    res = false;
            }

            return res;
        }
        
        public static bool VerifyArgsValidity(string[] args)
        {
            if (args[0] == "--help" || args[0] == "-h")
            {
                Help();
            }
            else
            {
                string arg = "";
                bool res = true;
                int i = 1;
                int len = args.Length;
                while (res && i < len - i)
                {
                    arg = "";
                    switch (args[i])
                    {
                        case "-i":
                        case "--invert":
                            i++;
                            break;
                        case "-g":
                        case "--grayscale":
                            i++;
                            break;
                        case "-b":
                        case "--brightness":
                            if (Int32.TryParse(args[i + 1], out int z))
                            {
                                i += 2;
                                break;
                                
                            }

                            res = false;
                            arg += args[i + 1];
                            break;
                        
                        case "-c":
                        case "--contrast":
                            if (Int32.TryParse(args[i + 1], out int t))
                            {
                                i += 2;
                                break;
                            }

                            res = false;
                            arg += args[i + 1];
                            break;
                        
                        case "-a":
                        case "--gradient-map":
                            if (IsColor(args[i+1])&& IsColor(args[i+2]))
                            {
                                break;
                                i += 2;
                            }

                            res = false;

                            if (!IsColor(args[i + 1]) && IsColor(args[i + 2]))
                            {
                                arg += args[i];
                                break;
                            }
                            if (!IsColor(args[i + 1]) && !IsColor(args[i + 2]))
                            {
                                arg += args[i];
                                break;
                            }

                            arg += args[i];
                            break;
                        
                        case "-m":
                        case "--cover":
                            if (Int32.TryParse(args[i + 2], out int g))
                            {
                                i += 3;
                                break;
                            }

                            res = false;
                            arg += args[i];
                            break;
                        
                        case "-l":
                        case "--rotate-left":
                            i++;
                            break;
                        
                        case "-r":
                        case "--rotate-right":
                            i++;
                            break;
                        
                        case "-x":
                        case "--symmetry-x":
                            i++;
                            break;
                        
                        case "-y":
                        case "--symmetry-y":
                            i++;
                            break;
                        
                        case "--resize":
                            if (Int32.TryParse(args[i + 1], out int r) && Int32.TryParse(args[i + 2], out int e))
                            {
                                i += 3;
                                break;
                            }

                            res = false;
                            
                            if (!Int32.TryParse(args[i + 1], out r) && Int32.TryParse(args[i + 2], out  e))
                            {
                                arg += args[i + 1];
                                break;
                            }
                            if (Int32.TryParse(args[i + 1], out r) && !Int32.TryParse(args[i + 2], out  e))
                            {
                                arg += args[i + 2];
                                break;
                            }

                            arg += args[i + 1];
                            break;
                        
                        case "--shift":
                            if (Int32.TryParse(args[i + 1], out r) && Int32.TryParse(args[i + 2], out e))
                            {
                                i += 3;
                                break;
                            }

                            res = false;
                            
                            if (!Int32.TryParse(args[i + 1], out r) && Int32.TryParse(args[i + 2], out  e))
                            {
                                arg += args[i];
                                break;
                            }
                            if (Int32.TryParse(args[i + 1], out r) && !Int32.TryParse(args[i + 2], out  e))
                            {
                                arg += args[i];
                                break;
                            }

                            arg += args[i];

                            break;
                        
                        case "--encrypt-string":
                        case "--encrypt-image":
                            if (i + 1 == len - 1)
                            {
                                res = false;
                                break;
                            }

                            i += 2;
                            break;
                        
                        case "--gauss":
                        case "--sharpen":
                        case "--blur":
                        case "--edge-enhance":
                        case "--edge-detect":
                        case "--emboss":
                        case "--decrypt-string":
                        case "--decrypt-image":
                        case "--h":
                        case "--help":
                            i += 1;
                            break;
                        
                        
                        default:
                            res = false;
                            arg += args[i];

                            break;



                    }
                }
                if (!res)
                    Console.Error.Write("Invalid arguments: " + arg + "\nUsage: ./aliceInJpegLand inputFile [options ...] outputFile\nUse -h or --help option to list all available options\n");
                
                return res;
            }

            return false;
        }

        /// <summary>
        /// Applies verified arguments on image
        /// </summary>
        /// <param name="image">Image to modify</param>
        /// <param name="args">CLI arguments array</param>
        public static void ApplyArgs(ref Bitmap image, string[] args)
        {
            if (VerifyArgsValidity(args))
            {
                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "-i":
                        case "--invert":
                            Basics.Invert(image);
                            break;
                        
                        case "-g":
                        case "--grayscale":
                            Basics.Grayscale(image);
                            break;
                        
                        case "-b":
                        case "--brightness":
                            Basics.Brightness(image, Int32.Parse(args[i+1]));
                            break;
                        
                        case "-c":
                        case "--contrast":
                            Basics.Contrast(image, Int32.Parse(args[i+1]));
                            break;
                            
                        case "-a":
                        case "--gradient-map":
                            Basics.GradientMap(image, Color.Black, Color.White);
                            break;
                            
                        case "-m":
                        case "--cover":
                            Basics.Cover(image, args[i+1], Int32.Parse(args[i+2]));
                            break;
                        
                        case "-l":
                        case "--rotate-left":
                            Geometry.RotateLeft(image);
                            break;
                        
                        case "-r":
                        case "--rotate-right":
                            Geometry.RotateRight(image);
                            break;
                        
                        case "-x":
                        case "--symmetry-x":
                            Geometry.SymmetryX(image);
                            break;
                        
                        case "-y":
                        case "--symmetry-y":
                            Geometry.SymmetryY(image);
                            break;
                        
                        case "--resize":
                            Geometry.Resize(image, Int32.Parse(args[i + 1]), Int32.Parse(args[i + 2]));
                            break;

                        case "--shift":
                            Geometry.Shift(image, Int32.Parse(args[i + 1]), Int32.Parse(args[i + 2]));
                            break;

                        case "--encrypt-string":
                            Steganography.EncryptString(image, args[i+1]);
                            break;
                        
                        case "--encrypt-image":
                            Steganography.EncryptImage(image, args[i+1]);
                            break;

                        case "--decrypt-string":
                            Steganography.DecryptString(image);
                            break;
                        
                        case "--decrypt-image":
                            Steganography.DecryptImage(image);
                            break;
                        
                        case "--gauss":
                            Convolution.Gauss(image);
                            break;
                        
                        case "--sharpen":
                            Convolution.Sharpen(image);
                            break;

                        case "--blur":
                            Convolution.Blur(image);
                            break;

                        case "--edge-enhance":
                            Convolution.EdgeEnhance(image);
                            break;

                        case "--edge-detect":
                            Convolution.EdgeDetect(image);
                            break;

                        case "--emboss":
                            Convolution.Emboss(image);
                            break;
                        
                        case "--h":
                        case "--help":
                            Help();
                            break;
                    }
                }
            }
        }
    }
}
