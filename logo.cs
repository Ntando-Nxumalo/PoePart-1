using System;
using System.Drawing;

class Logo
{
    public static void Display()
    {
        try
        {
            string imagePath = "Assets/CyberSafetyBot.bmp"; // The path of image file

            if (!System.IO.File.Exists(imagePath))
            {
                throw new Exception("Image file not found at " + System.IO.Path.GetFullPath(imagePath));
            }

            // Resize for better resolution while keeping proportions
            int newWidth = 120;
            int newHeight = 60;
            int consoleWidth = Console.WindowWidth;

            using (Bitmap originalLogo = new Bitmap(imagePath))
            {
                using (Bitmap resizedLogo = new Bitmap(originalLogo, new Size(newWidth, newHeight)))
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                    // Full-width title and border
                    string border = new string('=', consoleWidth);
                    string title = "CYBER SECURITY BOT";
                    int titlePadding = (consoleWidth - title.Length) / 2;

                    Console.WriteLine(border);
                    Console.WriteLine(title.PadLeft(titlePadding + title.Length));
                    Console.WriteLine(border);
                    Console.ResetColor();

                    ConvertToASCII(resizedLogo);
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }
    }

    private static void ConvertToASCII(Bitmap image)
    {
        string asciiChars = "@%#*+=-:. "; // Optimized for contrast
        int consoleWidth = Console.WindowWidth;

        for (int y = 0; y < image.Height; y += 1) // No skipping for more details
        {
            string line = "";
            for (int x = 0; x < image.Width; x++)
            {
                Color pixel = image.GetPixel(x, y);

                // Adjustment for contrast and brightness to improve clarity
                int gray = (pixel.R + pixel.G + pixel.B) / 3;
                gray = Math.Min(255, Math.Max(0, (int)(gray * 1.2))); // Brightness Boost
                int index = gray * (asciiChars.Length - 1) / 255;

                line += asciiChars[index];
            }

            // Ensurong image stays centered in the console
            int padding = Math.Max(0, (consoleWidth - line.Length) / 2);
            Console.WriteLine(line.PadLeft(padding + line.Length));
        }

        Console.WriteLine(new string('=', consoleWidth)); // Closing the border
    }
}