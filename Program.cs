using System;
using System.Media;
using System.IO;

internal class WelcomeMessage
{
    // Constructor
    public WelcomeMessage()
    {
        try
        {
            // Get the full location of the project
            string full_location = AppDomain.CurrentDomain.BaseDirectory;

            // Replace "bin\Debug" in the full location
            string new_path = full_location.Replace("bin\\Debug", "").Replace("bin\\Release", "");

            // Append the WAV file name
            string filePath = Path.Combine(new_path, "Greetings.wav");

            // Debugging Output: Print file path
            Console.WriteLine($"Checking for file at: {filePath}");

            // Check if file exists
            if (!File.Exists(filePath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Audio file not found.");
                Console.ResetColor();
                return;
            }

            using (SoundPlayer player = new SoundPlayer(filePath))
            {
                player.Load();
                player.Play();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Playing sound successfully.");
            Console.ResetColor();
        }
        catch (Exception error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {error.Message}");
            Console.ResetColor();
        }
    }
}

class Program
{
    static void Main()
    {
        // Play welcome message immediately
        new WelcomeMessage();

        // Display Logo
        Logo.Display();

        // Display ASCII Image
        ASCIIImage.Display();

        // Start Bot Interaction 
        BotInteraction.Start();
    }
}
