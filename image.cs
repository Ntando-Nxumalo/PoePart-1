using System;
using System.Drawing;

class ASCIIImage
{
    public static void Display()
    {
        try
        {
            string imagePath = "Assets/CyberSecurity.bmp"; //path for image 
            int newWidth = 120;  // Width for better clarity
            int newHeight = 60;  // Height for better details

            if (!System.IO.File.Exists(imagePath))
            {
                throw new Exception($"Error: Image file not found at {System.IO.Path.GetFullPath(imagePath)}");
            }

            using (Bitmap originalImage = new Bitmap(imagePath))
            {
                using (Bitmap resizedImage = new Bitmap(originalImage, new Size(newWidth, newHeight)))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"CyberSecurity Image Loaded - Width: {resizedImage.Width}, Height: {resizedImage.Height}");
                    Console.ResetColor();

                    ConvertToASCII(resizedImage);
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"CyberSecurity image not found. {ex.Message}");
            Console.ResetColor();
        }
    }

    private static void ConvertToASCII(Bitmap image)
    {
        string asciiChars = "@B#8&WM0oahkbdpqwmZO0QLCJUYXzcvunxrjft/|()1{}[]?-_+~<>i!lI;:,. ";
        int consoleWidth = Console.WindowWidth; // Get the console width for centering

        for (int y = 0; y < image.Height; y += 2) // Skip lines to keep aspect ratio
        {
            string line = "";
            for (int x = 0; x < image.Width; x++)
            {
                Color pixel = image.GetPixel(x, y);
                int gray = (pixel.R + pixel.G + pixel.B) / 3; // Convert to grayscale
                int index = gray * (asciiChars.Length - 1) / 255;
                line += asciiChars[index]; // Map grayscale value to ASCII char
            }

            // Ensuring the image is centered
            int padding = Math.Max(0, (consoleWidth - line.Length) / 2);
            Console.WriteLine(line.PadLeft(padding + line.Length));
        }

        // Print closing border
        Console.WriteLine(new string('=', consoleWidth));
    }
}