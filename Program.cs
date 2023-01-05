using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace imagefunnies
{
    class Program
    {
        public static void Main()
        {
            double rescaleFactor = 4;
            float brightnessFactor = 1.5f;
            float saturationFactor = 2f;
            float contrastFactor = 10;
            string outPath = "out.png";

            Console.WriteLine("Alex's Super Amazing Awesome Image Frier");
            Console.WriteLine("Provide a path to the desired image (relative or absolute).");

            string inPath = GetPath(true);

            Console.WriteLine("Configure parameters? Y/N");
            string choice = GetPath(false);
            if (choice == "y" || choice == "yes") //lol
            {
                Console.WriteLine($"Provide resize factor (default {rescaleFactor})");
                rescaleFactor = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine($"Provide brightness factor (default {brightnessFactor})");
                brightnessFactor = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine($"Provide saturation factor (default {contrastFactor})");
                contrastFactor = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine($"Provide contrast factor (default {saturationFactor})");
                saturationFactor = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine($"Provide desired output location and filename");
                outPath = GetPath(false);

                Console.WriteLine("All parameters entered.");
            }

            Console.WriteLine("Starting...");
            try
            {
                using (Image image = Image.Load(inPath))
                {
                    // 'x' signifies the current image processing context.
                    if (rescaleFactor > 1)
                    {
                        image.Mutate(x => x.Resize((int)(image.Width / rescaleFactor), (int)(image.Height / rescaleFactor)));
                    }

                    if (brightnessFactor != 1)
                    {
                        image.Mutate(x => x.Brightness(brightnessFactor));
                    }

                    if (saturationFactor != 1)
                    {
                        image.Mutate(x => x.Saturate(saturationFactor));
                    }

                    if (contrastFactor != 1)
                    {
                        image.Mutate(x => x.Contrast(contrastFactor));
                    }

                    if (rescaleFactor > 1)
                    {
                        image.Mutate(x => x.Resize((int)(image.Width * rescaleFactor), (int)(image.Height * rescaleFactor)));
                    }

                    image.Save(outPath);
                    Console.WriteLine("Image fried successfully.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("whar???");
            }


        }

        static string GetPath(bool mustExist)
        {
            string input;

            while (true)
            {
                input = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("You must provide a valid file name.");
                    continue;
                }

                if (mustExist && !File.Exists(input))
                {
                    Console.WriteLine("That file does not exist!");
                    continue;
                }

                return input;
            }

        }

        static float GetNumber()
        {
            string input;
            float output;

            while (true)
            {
                input = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("You must provide a number.");
                    continue;
                }

                try
                {
                    output = Convert.ToSingle(input);
                }
                catch
                {
                    Console.WriteLine("Invalid input. Provide a number.");
                    continue;
                }

                return Math.Abs(output);
            }
        }
    }
}