using System;
using System.Drawing;

class ASCIIImage
{
    public static void Display()
    {
        try
        {
            string imagePath = "Assets/CyberSecurity.bmp"; // Ensure the path is correct
            int newWidth = 100;  // Set desired width
            int newHeight = 50;  // Set desired height

            using (Bitmap originalImage = new Bitmap(imagePath))
            {
                using (Bitmap resizedImage = new Bitmap(originalImage, new Size(newWidth, newHeight)))
                {
                    Console.WriteLine($"CyberSecurity Image Loaded - Width: {resizedImage.Width}, Height: {resizedImage.Height}");
                    ConvertToASCII(resizedImage);
                }
            }
        }
        catch (Exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("CyberSecurity image not found.");
            Console.ResetColor();
        }
    }

    private static void ConvertToASCII(Bitmap image)
    {
        string asciiChars = "@#S%?*+;:,.";
        for (int y = 0; y < image.Height; y += 2) // Skipping rows to maintain aspect ratio
        {
            for (int x = 0; x < image.Width; x++)
            {
                Color pixel = image.GetPixel(x, y);
                int gray = (pixel.R + pixel.G + pixel.B) / 3;
                int index = gray * (asciiChars.Length - 1) / 255;
                Console.Write(asciiChars[index]);
            }
            Console.WriteLine();
        }
    }
}
