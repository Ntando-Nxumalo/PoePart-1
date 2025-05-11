using System;
using System.Collections.Generic;
using System.Linq;

class BotInteraction
{
    private static string username = "";
    private static string currentTopic = "";
    private static bool inConversation = false;
    private static readonly Random random = new Random();

    // User preferences and history
    private static readonly Dictionary<string, string> userPreferences = new Dictionary<string, string>();
    private static readonly List<string> discussedTopics = new List<string>();

    private static readonly Dictionary<string, List<string>> responseLibrary = new Dictionary<string, List<string>>()
    {
        {"phishing", new List<string>
            {
                "Phishing attacks trick users into revealing sensitive information by impersonating trusted entities.",
                "Look out for urgent language, unfamiliar senders, and requests for sensitive details in emails.",
                "Always verify links by hovering over them and never enter login credentials on suspicious websites.",
                "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
                "Check the sender's email address carefully - phishing emails often use addresses that look similar to legitimate ones."
            }
        },
        {"passwords", new List<string>
            {
                "Create passwords that are at least 14 characters long for better security.",
                "Use a mix of uppercase and lowercase letters, numbers, and special characters to strengthen passwords.",
                "Avoid reusing passwords across multiple accounts to prevent credential leaks.",
                "Consider using passphrases, which are longer and easier to remember than random passwords.",
                "Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords."
            }
        },
        {"security", new List<string>
            {
                "Cybersecurity is about protecting your digital identity, data, and devices from malicious threats.",
                "Enable multi-factor authentication (MFA) to add an extra layer of security.",
                "Regularly update your software and security patches to guard against new threats.",
                "Use a combination of strong passwords and biometric authentication where possible.",
                "Be wary of public Wi-Fi networks. Use a VPN when accessing sensitive information on public networks."
            }
        },
        {"scams", new List<string>
            {
                "Scams often try to create a sense of urgency to make you act without thinking.",
                "If an offer seems too good to be true, it probably is. Be skeptical of unexpected prizes or winnings.",
                "Never give out personal or financial information to someone who contacts you unexpectedly.",
                "Tech support scams often call claiming your computer has a virus. Legitimate companies don't operate this way.",
                "Romance scams target people on dating sites, often asking for money for emergencies or travel expenses."
            }
        },
        {"privacy", new List<string>
            {
                "Protect your privacy by reviewing app permissions and only granting access to what's necessary.",
                "Use privacy-focused browsers and search engines to minimize tracking of your online activities.",
                "Regularly check your social media privacy settings to control who can see your information.",
                "Be cautious about what personal information you share online - once it's out there, it's hard to take back.",
                "Consider using encrypted messaging apps for sensitive communications to protect your privacy."
            }
        },
        {"general", new List<string>
            {
                "Enable multi-factor authentication (MFA) on all important accounts for added security.",
                "Use a password manager to generate and store strong, unique passwords for different accounts.",
                "Keep your operating system, software, and antivirus programs updated to protect against vulnerabilities.",
                "Backup your important data regularly to protect against ransomware attacks.",
                "Be careful what you share on social media - attackers can use personal information to guess passwords or answers to security questions."
            }
        }
    };

    // Keyword mapping to topics
    private static readonly Dictionary<string, string> keywordToTopic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        {"password", "passwords"},
        {"passwords", "passwords"},
        {"strong password", "passwords"},
        {"phishing", "phishing"},
        {"phish", "phishing"},
        {"scam", "scams"},
        {"scams", "scams"},
        {"fraud", "scams"},
        {"privacy", "privacy"},
        {"private", "privacy"},
        {"data protection", "privacy"},
        {"security", "security"},
        {"cyber", "security"},
        {"safe browsing", "security"}
    };

    // Sentiment analysis words
    private static readonly string[] positiveWords = { "great", "good", "awesome", "happy", "thanks", "thank you", "cool", "excellent", "love", "like" };
    private static readonly string[] negativeWords = { "bad", "sad", "angry", "frustrated", "worried", "scared", "afraid", "hate", "annoyed" };

    public static void Start()
    {
        InitializeConversation();
        MainConversationLoop();
    }

    private static void InitializeConversation()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Enter your name: ");
        Console.ResetColor();

        username = Console.ReadLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nHello, {username}! Welcome to Cyber Security Bot.\n");
        Console.ResetColor();
    }

    private static void MainConversationLoop()
    {
        while (true)
        {
            try
            {
                DisplayPrompt();
                string input = GetUserInput();

                if (string.IsNullOrWhiteSpace(input))
                {
                    HandleEmptyInput();
                    continue;
                }

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    DisplayExitMessage();
                    break;
                }

                var sentimentAnalysis = AnalyzeSentiment(input);
                ProcessInput(input, sentimentAnalysis);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }
    }

    private static void DisplayPrompt()
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
    }

    private static string GetUserInput()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{username}: ");
        Console.ResetColor();

        string input = Console.ReadLine()?.Trim();
        Console.WriteLine();
        return input?.ToLower() ?? "";
    }

    private static void HandleEmptyInput()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("I didn't hear anything. Could you repeat that?");
        Console.ResetColor();
    }

    private static void ProcessInput(string input, (string sentiment, string emotion) sentimentAnalysis)
    {
        if (ProcessInterestStatement(input)) return;
        if (ProcessSpecialCommands(input)) return;

        string detectedTopic = DetectTopic(input);
        if (!string.IsNullOrEmpty(detectedTopic))
        {
            currentTopic = detectedTopic;
            inConversation = true;
            if (!discussedTopics.Contains(currentTopic)) discussedTopics.Add(currentTopic);
            DisplayRandomResponse(currentTopic, sentimentAnalysis);
        }
        else
        {
            HandleUnknownInput();
        }
    }

    private static bool ProcessInterestStatement(string input)
    {
        if (!input.Contains("i'm interested in") &&
            !input.Contains("i am interested in") &&
            !input.Contains("i like") &&
            !input.Contains("i love") &&
            !input.Contains("i enjoy"))
        {
            return false;
        }

        string detectedTopic = DetectTopic(input);
        if (!string.IsNullOrEmpty(detectedTopic))
        {
            userPreferences["favoriteTopic"] = detectedTopic;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Great! I'll remember that you're interested in {detectedTopic}. It's a crucial part of staying safe online.");
            Console.ResetColor();
            currentTopic = detectedTopic;
            inConversation = true;
        }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("That sounds interesting! Could you tell me more about which cybersecurity topic you're interested in?");
            Console.ResetColor();
        return true;
    }

    private static bool ProcessSpecialCommands(string input)
    {
        if (input.Contains("how are you"))
        {
            RespondToHowAreYou(AnalyzeSentiment(input));
            return true;
        }
        if (input.Contains("what's your purpose") || input.Contains("what is your purpose"))
        {
            DisplayPurpose();
            return true;
        }
        if (input.Contains("what can i ask you about") || input.Contains("what can I ask you about"))
        {
            DisplayAvailableTopics();
            return true;
        }
        }
        }
        return false;
    }

    private static string DetectTopic(string input)
    {
        foreach (var kvp in keywordToTopic)
        {
            if (input.Contains(kvp.Key))
            {
                return kvp.Value;
            }
        }
        return null;
    }

    private static void DisplayRandomResponse(string topic, (string sentiment, string emotion) sentimentAnalysis)
    {
        if (!responseLibrary.ContainsKey(topic) || responseLibrary[topic].Count == 0) return;

        string response = responseLibrary[topic][random.Next(responseLibrary[topic].Count)];
        response = AdjustResponseBySentiment(response, sentimentAnalysis);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(response);
        Console.ResetColor();
        Console.WriteLine();
    }

    private static string AdjustResponseBySentiment(string response, (string sentiment, string emotion) sentimentAnalysis)
    {
        if (sentimentAnalysis.sentiment == "positive")
        {
            response += " Great to see you're interested in staying safe!";
        }
        else if (sentimentAnalysis.sentiment == "negative")
        {
            if (sentimentAnalysis.emotion == "worried")
            {
                response = "I understand this can be worrying. " + response + " The important thing is to take it step by step.";
            }
            else if (sentimentAnalysis.emotion == "frustrated")
            {
                response = "I know this can be frustrating. " + response + " Let's break it down to make it easier.";
            }
            else
            {
                response = "I understand cybersecurity can be concerning. " + response + " Let me know if you'd like more help with this.";
            }
        }
        return response;
    }

    private static void RespondToHowAreYou((string sentiment, string emotion) sentimentAnalysis)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        if (sentimentAnalysis.sentiment == "positive")
        {
            Console.WriteLine($"I'm doing great, thank you for asking, {username}! I'm glad you're in a good mood. How about we talk about cybersecurity?");
        }
        else if (sentimentAnalysis.sentiment == "negative")
        {
            if (sentimentAnalysis.emotion == "worried")
            {
                Console.WriteLine($"I'm here to help, {username}. I sense you might be feeling worried. Cybersecurity can be overwhelming, but I'll help you navigate it safely.");
            }
            else if (sentimentAnalysis.emotion == "frustrated")
            {
                Console.WriteLine($"I understand cybersecurity can be frustrating, {username}. Let's take it one step at a time. What's troubling you?");
            }
            else
            {
                Console.WriteLine($"I'm here to help, {username}. I sense you might be feeling down. Remember, staying safe online can help reduce stress and problems.");
            }
        }
        else
        {
            Console.WriteLine($"I'm doing well, {username}! Ready to help with any cybersecurity questions you have.");
        }
        Console.ResetColor();
    }

    private static void DisplayPurpose()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("My purpose is to assist you with cybersecurity knowledge and keep you safe online. I can help with topics like password safety, phishing prevention, scam awareness, privacy protection, and general security tips.");
        Console.ResetColor();
    }

    private static void DisplayAvailableTopics()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("You can ask me about:\n- Password safety\n- Phishing prevention\n- Scam awareness\n- Privacy protection\n- General security tips\n- Safe browsing practices");
        Console.ResetColor();
    }

    private static void HandleMoreRequest((string sentiment, string emotion) sentimentAnalysis)
    {
        if (!string.IsNullOrEmpty(currentTopic))
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            if (userPreferences.ContainsKey("favoriteTopic") && userPreferences["favoriteTopic"] == currentTopic)
            {
                Console.WriteLine($"Here's another tip about {currentTopic}, since you're particularly interested in this:");
            }
            else if (discussedTopics.Count > 1)
            {
                Console.WriteLine($"We've discussed {string.Join(" and ", discussedTopics)} so far. Here's more about {currentTopic}:");
            }
            else
            {
                Console.WriteLine($"Here's another tip about {currentTopic}:");
            }

            Console.ResetColor();
    }

    private static void RecallUserPreferences()
    {
        if (userPreferences.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("I don't have any preferences saved for you yet. You can tell me things like 'I'm interested in privacy' and I'll remember them.");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Here's what I remember about you:");
        foreach (var pref in userPreferences)
        {
            Console.WriteLine($"- {pref.Key}: {pref.Value}");
        }
        Console.ResetColor();

        if (userPreferences.ContainsKey("favoriteTopic"))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Since you're interested in {userPreferences["favoriteTopic"]}, would you like to discuss that now?");
            Console.ResetColor();
        }
    }

    private static void HandleUnknownInput()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("I didn't quite understand that. Could you rephrase or ask about a cybersecurity topic?");
        Console.WriteLine("Try asking about: passwords, phishing, scams, privacy, or general security.");
        Console.ResetColor();
        inConversation = false;
        currentTopic = "";
    }

    private static (string sentiment, string emotion) AnalyzeSentiment(string input)
    {
        string sentiment = "neutral";
        string emotion = "";

        if (positiveWords.Any(word => input.Contains(word)))
        {
            sentiment = "positive";
        }
        else if (negativeWords.Any(word => input.Contains(word)))
        {
            sentiment = "negative";

            if (input.Contains("worried") || input.Contains("scared") || input.Contains("afraid"))
            {
                emotion = "worried";
            }
            else if (input.Contains("frustrated") || input.Contains("angry") || input.Contains("annoyed"))
            {
                emotion = "frustrated";
            }
        }

        return (sentiment, emotion);
    }

    private static void DisplayExitMessage()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Goodbye, {username}! Remember to stay safe online.");

        if (userPreferences.ContainsKey("favoriteTopic"))
        {
            Console.WriteLine($"Don't forget to practice what we discussed about {userPreferences["favoriteTopic"]}!");
        }

        if (discussedTopics.Count > 0)
        {
            Console.WriteLine($"We discussed these topics today: {string.Join(", ", discussedTopics)}.");
        }

        Console.ResetColor();
    }

    private static void HandleError(Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Oops! Something went wrong: {ex.Message}");
        Console.WriteLine("Let's try that again.");
        Console.ResetColor();
    }
}