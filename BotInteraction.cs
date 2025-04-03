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
        phishing.Add("Phishing attacks trick users into revealing sensitive information by impersonating trusted entities.");
        phishing.Add("Look out for urgent language, unfamiliar senders, and requests for sensitive details in emails.");
        phishing.Add("Always verify links by hovering over them and never enter login credentials on suspicious websites.");

        passwords.Add("Create passwords that are at least 14 characters long for better security.");
        passwords.Add("Use a mix of uppercase and lowercase letters, numbers, and special characters to strengthen passwords.");
        passwords.Add("Avoid reusing passwords across multiple accounts to prevent credential leaks.");
        passwords.Add("Consider using passphrases, which are longer and easier to remember than random passwords.");

        security.Add("Cybersecurity is about protecting your digital identity, data, and devices from malicious threats.");
        security.Add("Enable multi-factor authentication (MFA) to add an extra layer of security.");
        security.Add("Regularly update your software and security patches to guard against new threats.");
        security.Add("Use a combination of strong passwords and biometric authentication where possible.");

        more.Add("Enable multi-factor authentication (MFA) on all important accounts for added security.");
        more.Add("Use a password manager to generate and store strong, unique passwords for different accounts.");
        more.Add("Keep your operating system, software, and antivirus programs updated to protect against vulnerabilities.");


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
