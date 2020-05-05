using System;
using System.Drawing;
using System.IO;
using AliceInJpegLand;
using NUnit.Framework;

namespace GivenTests
{
    [TestFixture]
    public class ColorConversionTests
    {
        [Test]
        public void ColorConversion()
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(Color.FromArgb(0xff, 0xff, 0xff), ColorExtension.FromHexa("ffffff"));
                Assert.AreEqual(Color.FromArgb(0x00, 0x00, 0x00), ColorExtension.FromHexa("000000"));
                Assert.AreEqual(Color.FromArgb(0x12, 0x34, 0x56), ColorExtension.FromHexa("123456"));
                Assert.AreEqual(Color.FromArgb(0x52, 0xf4, 0xe2), ColorExtension.FromHexa("52f4e2"));
                Assert.AreEqual(Color.FromArgb(0xb1, 0xc1, 0xd8), ColorExtension.FromHexa("b1c1d8"));
                Assert.AreEqual(Color.FromArgb(0x42, 0x42, 0x42), ColorExtension.FromHexa("424242"));
                Assert.Throws<ArgumentException>(() => {ColorExtension.FromHexa("#424242");});
                Assert.Throws<ArgumentException>(() => {ColorExtension.FromHexa("42z242");});
                Assert.Throws<ArgumentException>(() => {ColorExtension.FromHexa("4242424");});
                Assert.Throws<ArgumentException>(() => {ColorExtension.FromHexa("424242-");});
            });
        }
    }
    
    [TestFixture]
    public class VerifyArgs
    {
        StringWriter stdout;
        StringWriter stderr;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            stdout = new StringWriter();
            stderr = new StringWriter();
            Console.SetOut(stdout);
            Console.SetError(stderr);
        }

        [SetUp]
        public void Setup()
        {
            stdout.GetStringBuilder().Clear();
            stderr.GetStringBuilder().Clear();
        }
        


        [Test]
        public void Grayscale()
        {
            Assert.Multiple(() =>
            {
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "-g", "output"}), "-g");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--grayscale", "output"}),
                    "--grayscale");
                Assert.False(ParameterManager.VerifyArgsValidity(new[] {"input", "--grayscale", "123", "output"}),
                    "--grayscale WRONG");
                String expectedOutput = "Invalid argument: 123\n"
                                        + "Usage: ./aliceInJpegLand inputFile [options ...] outputFile\n"
                                        + "Use -h or --help option to list all available options\n";

                Assert.AreEqual("", stdout.ToString());
                Assert.AreEqual(expectedOutput, stderr.ToString());
            });
        }

        [Test]
        public void Invert()
        {
            Assert.Multiple(() =>
            {
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "-i", "output"}), "-i");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--invert", "output"}), "--invert");
            });
        }

        [Test]
        public void Rotate()
        {
            Assert.Multiple(() =>
            {
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "-l", "output"}), "-l");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--rotate-left", "output"}),
                    "--rotate-left");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "-r", "output"}), "-r");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--rotate-right", "output"}),
                    "--rotate-right");
            });
        }

        [Test]
        public void Symmetry()
        {
            Assert.Multiple(() =>
            {
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "-x", "output"}), "-x");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--symmetry-x", "output"}), "--symmetry-x");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "-y", "output"}), "-y");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--symmetry-y", "output"}), "--symmetry-y");
            });
        }

        [Test]
        public void Convolution()
        {
            Assert.Multiple(() =>
            {
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--gauss", "output"}), "--gauss");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--sharpen", "output"}), "--sharpen");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--blur", "output"}), "--blur");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--edge-enhance", "output"}),
                    "--edge-enhance");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--edge-detect", "output"}),
                    "--edge-detect");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--emboss", "output"}), "--emboss");
            });
        }

        [Test]
        public void Brightness()
        {
            Assert.Multiple(() =>
            {
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "-b", "42", "output"}), "-b");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--brightness", "42", "output"}),
                    "--brightness");
                
                Assert.False(ParameterManager.VerifyArgsValidity(new[] {"input", "-b", "not_an_int", "output"}), "-b WRONG");
                String expectedOutput = "Invalid argument: -b\n"
                                        + "Usage: ./aliceInJpegLand inputFile [options ...] outputFile\n"
                                        + "Use -h or --help option to list all available options\n";

                Assert.AreEqual("", stdout.ToString());
                Assert.AreEqual(expectedOutput, stderr.ToString());
            });
        }

        [Test]
        public void Contrast()
        {
            Assert.Multiple(() =>
            {
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "-c", "42", "output"}), "-c");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--contrast", "42", "output"}),
                    "--contrast");
                Assert.False(ParameterManager.VerifyArgsValidity(new[] {"input", "--contrast", "not_an_int", "output"}),
                    "--contrast WRONG");
                String expectedOutput = "Invalid argument: --contrast\n"
                                        + "Usage: ./aliceInJpegLand inputFile [options ...] outputFile\n"
                                        + "Use -h or --help option to list all available options\n";

                Assert.AreEqual("", stdout.ToString());
                Assert.AreEqual(expectedOutput, stderr.ToString());
            });
        }


        [Test]
        public void Cover()
        {
            Assert.Multiple(() =>
            {
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "-m", "IMAGEPATH", "42", "output"}), "-m");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--cover", "IMAGEPATH", "42", "output"}),
                    "--cover");
                Assert.False(ParameterManager.VerifyArgsValidity(new[] {"input", "-m", "IMAGEPATH", "not_an_int", "output"}),
                    "-m WRONG");
                String expectedOutput = "Invalid argument: -m\n"
                                        + "Usage: ./aliceInJpegLand inputFile [options ...] outputFile\n"
                                        + "Use -h or --help option to list all available options\n";

                Assert.AreEqual("", stdout.ToString());
                Assert.AreEqual(expectedOutput, stderr.ToString());
            });
        }

        [Test]
        public void GradientMap()
        {
            Assert.Multiple(() =>
            {
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "-a", "ffffff", "000000", "output"}), "-a");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--gradient-map", "ffffff", "000000",
                    "output"}), "--gradient-map");
                Assert.False(ParameterManager.VerifyArgsValidity(new[] {"input", "-a", "ffgfff", "0z0000", "output"}),
                    "-a WRONG");
                String expectedOutput = "Invalid argument: -a\n"
                                        + "Usage: ./aliceInJpegLand inputFile [options ...] outputFile\n"
                                        + "Use -h or --help option to list all available options\n";

                Assert.AreEqual("", stdout.ToString());
                Assert.AreEqual(expectedOutput, stderr.ToString());
            });
        }

        [Test]
        public void Shift()
        {
            Assert.Multiple(() =>
            {
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--shift", "421", "422", "output"}),
                    "--shift");
                Assert.False(ParameterManager.VerifyArgsValidity(new[] {"input", "--shift", "not_an_int1", "not_an_int2",
                    "output"}), "--shift WRONG");
                String expectedOutput = "Invalid argument: --shift\n"
                                        + "Usage: ./aliceInJpegLand inputFile [options ...] outputFile\n"
                                        + "Use -h or --help option to list all available options\n";

                Assert.AreEqual("", stdout.ToString());
                Assert.AreEqual(expectedOutput, stderr.ToString());
            });
        }

        [Test]
        public void Resize()
        {
            Assert.Multiple(() =>
            {
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--resize", "421", "422", "output"}),
                    "--resize");
                Assert.False(ParameterManager.VerifyArgsValidity(new[] {"input", "--resize", "not_an_int1", "not_an_int2",
                    "output"}), "--resize WRONG");
            });
        }

        [Test]
        public void Encrypt()
        {
            Assert.Multiple(() =>
            {
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--encrypt-string", "STRING", "output"}),
                    "--encrypt-string");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--encrypt-image", "IMAGEPATH", "output"}),
                    "--encrypt-image");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--decrypt-string", "output"}),
                    "--decrypt-string");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--decrypt-image", "output"}),
                    "--decrypt-image");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input", "--decrypt-image"}), "output with a parameter name");
                Assert.False(ParameterManager.VerifyArgsValidity(new[] {"input", "--encrypt-image", "IMAGEPATH"}),
                    "missing output");
            });
        }

        [Test]
        public void ChainedFunctions()
        {
            Assert.Multiple(() =>
            {
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"input",
                            "--resize", "842", "665",
                            "--shift", "42", "-665",
                            "-a", "123456", "654321",
                            "-c", "55",
                            "--gauss",
                            "--encrypt-string", "ce test est valide",
                            "output"}), "multiple functions");
                Assert.True(ParameterManager.VerifyArgsValidity(new[] {"--resize",
                            "--resize", "842", "665",
                            "--shift", "42", "-665",
                            "-a", "123456", "654321",
                            "-c", "55",
                            "--gauss",
                            "--encrypt-string", "ce test est valide",
                            "-a"}), 
                        "multiple functions with function file names");
            });
        }
    }
}
