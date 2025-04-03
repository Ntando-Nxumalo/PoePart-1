using System;
using System.Collections;

class BotInteraction
{
    static string username = "";

    public static void Start()
    {
        // Ask for user name first
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Enter your name: ");
        Console.ResetColor();

        username = Console.ReadLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nHello, {username}! Welcome to Cyber Security Bot.\n");
        Console.ResetColor();

        // Initialize ArrayList for responses
        ArrayList phishing = new ArrayList();
        ArrayList passwords = new ArrayList();
        ArrayList security = new ArrayList();
        ArrayList more = new ArrayList();

        // Adding topics to ArrayLists
        phishing.Add("Phishing is a type of cyber attack where attackers pretend to be someone else to steal personal information.");
        phishing.Add("You can recognize phishing emails by checking for spelling errors, suspicious links, and unexpected attachments.");
        phishing.Add("To defend against phishing, never click on unknown links and always verify the sender’s email address.");

        passwords.Add("A strong password should be at least 12-16 characters long.");
        passwords.Add("It should include uppercase, lowercase, numbers, and special symbols.");
        passwords.Add("Avoid using personal information like your birthdate.");
        passwords.Add("Use a password manager to generate and store complex passwords.");

        security.Add("Online security involves keeping your devices, accounts, and personal data safe from cyber threats.");
        security.Add("Enable two-factor authentication (2FA) for extra security.");
        security.Add("Keep your software and operating system updated to fix vulnerabilities.");
        security.Add("Use unique passwords for different accounts.");

        more.Add("Always use a secure internet connection, avoid using public Wi-Fi for sensitive transactions.");
        more.Add("Be careful with social media; never share too much personal information online.");
        more.Add("Regularly back up important data to avoid data loss in case of cyber attacks.");

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("What would you like to ask me?");
            Console.WriteLine("Type 'exit' to quit.");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{username}: ");
            Console.ResetColor();

            string input = Console.ReadLine()?.Trim().ToLower();
            Console.WriteLine();

            if (input == "exit")
            {
                break;
            }

            ProcessInput(input, phishing, passwords, security, more);
        }
    }

    static void ProcessInput(string input, ArrayList phishing, ArrayList passwords, ArrayList security, ArrayList more)
    {
        if (input.Contains("how are you"))
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"I'm doing great, thank you for asking, {username}! How about you? Stay safe online!");
            Console.ResetColor();
        }
        else if (input.Contains("what's your purpose") || input.Contains("what is your purpose"))
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("My purpose is to assist you with cybersecurity knowledge and keep you safe online. I can help with topics like password safety, phishing prevention, and safe browsing.");
            Console.ResetColor();
        }
        else if (input.Contains("what can i ask you about") || input.Contains("what can I ask you about"))
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("You can ask me about password safety, phishing prevention, safe browsing, and general online security tips.");
            Console.ResetColor();
        }
        else if (input.Contains("phishing"))
        {
            DisplayResponse(phishing);
        }
        else if (input.Contains("strong password") || input.Contains("password safety"))
        {
            DisplayResponse(passwords);
        }
        else if (input.Contains("online security"))
        {
            DisplayResponse(security);
        }
        else if (input.Contains("more"))
        {
            DisplayResponse(more);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("I didn't quite understand that. Could you rephrase?");
            Console.ResetColor();
        }
    }

    static void DisplayResponse(ArrayList responseList)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (var response in responseList)
        {
            Console.WriteLine(response);
        }
        Console.ResetColor();
        Console.WriteLine();  // Add an extra newline for better readability
    }
}
