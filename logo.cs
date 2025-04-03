using System;
using System.Drawing;

class Logo
{
    public static void Display()
    {
        try
        {
            string imagePath = "Assets/CyberSafetyBot.bmp";

            if (!System.IO.File.Exists(imagePath))
            {
                throw new Exception("Image file not found at " + System.IO.Path.GetFullPath(imagePath));
            }

            int newWidth = 50;  // Adjust width for better visibility
            int newHeight = 25; // Adjust height for proportional display
            int consoleWidth = Console.WindowWidth; // Get the full width of the console

            using (Bitmap originalLogo = new Bitmap(imagePath))
            {
                using (Bitmap resizedLogo = new Bitmap(originalLogo, new Size(newWidth, newHeight)))
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                    // Full-width border
                    string border = new string('=', consoleWidth);
                    string title = "CYBER SECURITY BOT";
                    int titlePadding = (consoleWidth - title.Length) / 2;

                    Console.WriteLine(border); // Full-width border
                    Console.WriteLine(title.PadLeft(titlePadding + title.Length)); // Center title
                    Console.WriteLine(border); // Full-width border
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
        string asciiChars = "@#S%?*+;:,. ";
        int consoleWidth = Console.WindowWidth; // Get the console width for centering

        for (int y = 0; y < image.Height; y += 2) // Skipping lines to maintain aspect ratio
        {
            string line = "";
            for (int x = 0; x < image.Width; x++)
            {
                Color pixel = image.GetPixel(x, y);
                int gray = (pixel.R + pixel.G + pixel.B) / 3;
                int index = gray * (asciiChars.Length - 1) / 255;
                line += asciiChars[index];
            }

            // Ensure padding does not cause a crash
            int padding = Math.Max(0, (consoleWidth - line.Length) / 2);
            Console.WriteLine(line.PadLeft(padding + line.Length));
        }

        // Print closing border
        Console.WriteLine(new string('=', consoleWidth));
    }
}
