using System;
using System.Collections;
using System.Linq;

class BotInteraction
{
    static string username = "";
    static string currentTopic = "";
    static bool inConversation = false;
    static Random random = new Random();

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
        ArrayList scams = new ArrayList();
        ArrayList privacy = new ArrayList();

        // Adding topics to ArrayLists with multiple responses for variety
        phishing.Add("Phishing attacks trick users into revealing sensitive information by impersonating trusted entities.");
        phishing.Add("Look out for urgent language, unfamiliar senders, and requests for sensitive details in emails.");
        phishing.Add("Always verify links by hovering over them and never enter login credentials on suspicious websites.");
        phishing.Add("Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.");
        phishing.Add("Check the sender's email address carefully - phishing emails often use addresses that look similar to legitimate ones.");

        passwords.Add("Create passwords that are at least 14 characters long for better security.");
        passwords.Add("Use a mix of uppercase and lowercase letters, numbers, and special characters to strengthen passwords.");
        passwords.Add("Avoid reusing passwords across multiple accounts to prevent credential leaks.");
        passwords.Add("Consider using passphrases, which are longer and easier to remember than random passwords.");
        passwords.Add("Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords.");

        security.Add("Cybersecurity is about protecting your digital identity, data, and devices from malicious threats.");
        security.Add("Enable multi-factor authentication (MFA) to add an extra layer of security.");
        security.Add("Regularly update your software and security patches to guard against new threats.");
        security.Add("Use a combination of strong passwords and biometric authentication where possible.");
        security.Add("Be wary of public Wi-Fi networks. Use a VPN when accessing sensitive information on public networks.");

        more.Add("Enable multi-factor authentication (MFA) on all important accounts for added security.");
        more.Add("Use a password manager to generate and store strong, unique passwords for different accounts.");
        more.Add("Keep your operating system, software, and antivirus programs updated to protect against vulnerabilities.");
        more.Add("Backup your important data regularly to protect against ransomware attacks.");
        more.Add("Be careful what you share on social media - attackers can use personal information to guess passwords or answers to security questions.");

        scams.Add("Scams often try to create a sense of urgency to make you act without thinking.");
        scams.Add("If an offer seems too good to be true, it probably is. Be skeptical of unexpected prizes or winnings.");
        scams.Add("Never give out personal or financial information to someone who contacts you unexpectedly.");
        scams.Add("Tech support scams often call claiming your computer has a virus. Legitimate companies don't operate this way.");
        scams.Add("Romance scams target people on dating sites, often asking for money for emergencies or travel expenses.");

        privacy.Add("Protect your privacy by reviewing app permissions and only granting access to what's necessary.");
        privacy.Add("Use privacy-focused browsers and search engines to minimize tracking of your online activities.");
        privacy.Add("Regularly check your social media privacy settings to control who can see your information.");
        privacy.Add("Be cautious about what personal information you share online - once it's out there, it's hard to take back.");
        privacy.Add("Consider using encrypted messaging apps for sensitive communications to protect your privacy.");

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (!inConversation)
            {
                Console.WriteLine("What would you like to ask me about cybersecurity?");
                Console.WriteLine("You can ask about: passwords, phishing, scams, privacy, or general security.");
            }
            else
            {
                Console.WriteLine($"What else would you like to know about {currentTopic}?");
                Console.WriteLine("(Or ask about a new topic, or type 'exit' to quit)");
            }
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

            // Analyze sentiment before processing input
            string sentiment = AnalyzeSentiment(input);
            ProcessInput(input, phishing, passwords, security, more, scams, privacy, sentiment);
        }
    }

    static void ProcessInput(string input, ArrayList phishing, ArrayList passwords, ArrayList security,
                            ArrayList more, ArrayList scams, ArrayList privacy, string sentiment)
    {
        // Keyword recognition with priority
        if (input.Contains("password") || input.Contains("passwords") || input.Contains("strong password"))
        {
            currentTopic = "password safety";
            inConversation = true;
            DisplayRandomResponse(passwords, sentiment);
        }
        else if (input.Contains("phishing") || input.Contains("phish"))
        {
            currentTopic = "phishing";
            inConversation = true;
            DisplayRandomResponse(phishing, sentiment);
        }
        else if (input.Contains("scam") || input.Contains("scams") || input.Contains("fraud"))
        {
            currentTopic = "scams";
            inConversation = true;
            DisplayRandomResponse(scams, sentiment);
        }
        else if (input.Contains("privacy") || input.Contains("private") || input.Contains("data protection"))
        {
            currentTopic = "privacy";
            inConversation = true;
            DisplayRandomResponse(privacy, sentiment);
        }
        else if (input.Contains("security") || input.Contains("cyber") || input.Contains("safe browsing"))
        {
            currentTopic = "security";
            inConversation = true;
            DisplayRandomResponse(security, sentiment);
        }
        else if (input.Contains("how are you"))
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (sentiment == "positive")
            {
                Console.WriteLine($"I'm doing great, thank you for asking, {username}! I'm glad you're in a good mood. How about we talk about cybersecurity?");
            }
            else if (sentiment == "negative")
            {
                Console.WriteLine($"I'm here to help, {username}. I sense you might be feeling down. Remember, staying safe online can help reduce stress and problems.");
            }
            else
            {
                Console.WriteLine($"I'm doing well, {username}! Ready to help with any cybersecurity questions you have.");
            }
            Console.ResetColor();
        }
        else if (input.Contains("what's your purpose") || input.Contains("what is your purpose"))
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("My purpose is to assist you with cybersecurity knowledge and keep you safe online. I can help with topics like password safety, phishing prevention, scam awareness, privacy protection, and general security tips.");
            Console.ResetColor();
        }
        else if (input.Contains("what can i ask you about") || input.Contains("what can I ask you about"))
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("You can ask me about:\n- Password safety\n- Phishing prevention\n- Scam awareness\n- Privacy protection\n- General security tips\n- Safe browsing practices");
            Console.ResetColor();
        }
        else if (input.Contains("more") || input.Contains("another tip") || input.Contains("tell me more"))
        {
            if (!string.IsNullOrEmpty(currentTopic))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Here's another tip about {currentTopic}:");
                Console.ResetColor();

                // Display another random response about the current topic
                switch (currentTopic)
                {
                    case "password safety":
                        DisplayRandomResponse(passwords, sentiment);
                        break;
                    case "phishing":
                        DisplayRandomResponse(phishing, sentiment);
                        break;
                    case "scams":
                        DisplayRandomResponse(scams, sentiment);
                        break;
                    case "privacy":
                        DisplayRandomResponse(privacy, sentiment);
                        break;
                    case "security":
                        DisplayRandomResponse(security, sentiment);
                        break;
                    default:
                        DisplayRandomResponse(more, sentiment);
                        break;
                }
            }
            else
            {
                DisplayRandomResponse(more, sentiment);
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("I didn't quite understand that. Could you rephrase or ask about a cybersecurity topic?");
            Console.WriteLine("Try asking about: passwords, phishing, scams, privacy, or general security.");
            Console.ResetColor();
            inConversation = false;
            currentTopic = "";
        }
    }

    static void DisplayRandomResponse(ArrayList responseList, string sentiment)
    {
        if (responseList.Count == 0) return;

        // Get a random response
        int index = random.Next(responseList.Count);
        string response = responseList[index].ToString();

        // Adjust response based on sentiment
        if (sentiment == "positive")
        {
            response += " Great to see you're interested in staying safe!";
        }
        else if (sentiment == "negative")
        {
            response = "I understand cybersecurity can be concerning. " + response + " Let me know if you'd like more help with this.";
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(response);
        Console.ResetColor();
        Console.WriteLine();  // Add an extra newline for better readability
    }

    static string AnalyzeSentiment(string input)
    {
        // Simple sentiment analysis based on keywords
        string[] positiveWords = { "great", "good", "awesome", "happy", "thanks", "thank you", "cool", "excellent" };
        string[] negativeWords = { "bad", "sad", "angry", "frustrated", "worried", "scared", "afraid", "hate" };

        if (positiveWords.Any(word => input.Contains(word)))
        {
            return "positive";
        }
        else if (negativeWords.Any(word => input.Contains(word)))
        {
            return "negative";
        }

        return "neutral";
    }
}