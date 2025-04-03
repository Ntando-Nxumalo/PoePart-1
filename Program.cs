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
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug", "").Replace("bin\\Release", ""), "Assets", "Greeting.wav");
            using (SoundPlayer player = new SoundPlayer(filePath))
            {
                player.Load();
                player.PlaySync();
                Console.WriteLine("WAV file played successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error reading WAV file: {ex.Message}");
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
