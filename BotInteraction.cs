using System;
using System.Collections.Generic;
using System.Linq;

class BotInteraction
{
    private static string username = "";
    private static string currentTopic = "";
    private static bool inConversation = false;
    private static readonly Random random = new Random();

    // User preference and history
    private static readonly Dictionary<string, string> userPreferences = new Dictionary<string, string>();
    private static readonly List<string> discussedTopics = new List<string>();
    private static readonly Dictionary<string, List<int>> shownResponses = new Dictionary<string, List<int>>();

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

    private static readonly Dictionary<string, string> spellingCorrections = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        {"fishing", "phishing"},
        {"phisihng", "phishing"},
        {"phising", "phishing"},
        {"passsword", "password"},
        {"pasword", "password"},
        {"scamm", "scam"},
        {"privasy", "privacy"},
        {"securty", "security"},
        {"cybersec", "cyber security"}
    };

    private static readonly Dictionary<string, string> keywordToTopic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        {"password", "passwords"},
        {"passwords", "passwords"},
        {"strong password", "passwords"},
        {"login", "passwords"},
        {"credentials", "passwords"},
        {"phishing", "phishing"},
        {"phish", "phishing"},
        {"email scam", "phishing"},
        {"scam", "scams"},
        {"scams", "scams"},
        {"fraud", "scams"},
        {"con", "scams"},
        {"privacy", "privacy"},
        {"private", "privacy"},
        {"data protection", "privacy"},
        {"personal data", "privacy"},
        {"security", "security"},
        {"cyber", "security"},
        {"cybersecurity", "security"},
        {"safe browsing", "security"},
        {"hack", "security"},
        {"malware", "security"},
        {"virus", "security"}
    };

    private static readonly string[] positiveWords = { "great", "good", "awesome", "happy", "thanks", "thank you", "cool", "excellent", "love", "like", "fantastic", "wonderful", "perfect" };
    private static readonly string[] negativeWords = { "bad", "sad", "angry", "frustrated", "worried", "scared", "afraid", "hate", "annoyed", "terrible", "awful", "horrible", "stupid" };

    public static void Start()
    {
        try
        {
            InitializeConversation();
            MainConversationLoop();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"A critical error occurred: {ex.Message}");
            Console.WriteLine("The program will now exit. Please try again later.");
            Console.ResetColor();
            Environment.Exit(1);
        }
    }

    private static void InitializeConversation()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Enter your name: ");
        Console.ResetColor();

        username = Console.ReadLine()?.Trim();
        while (string.IsNullOrWhiteSpace(username))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("I didn't get your name. Please enter your name:");
            Console.ResetColor();
            username = Console.ReadLine()?.Trim();
        }
        userPreferences["name"] = username;

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

                input = CorrectMisspellings(input);

                var sentimentAnalysis = AnalyzeSentiment(input);

                // Handle context-aware follow-ups
                if (input.Contains("what should i know") || input.Contains("what else should i know"))
                {
                    if (!string.IsNullOrEmpty(currentTopic))
                    {
                        DisplayRandomResponse(currentTopic, sentimentAnalysis, true);
                        continue;
                    }
                    else if (userPreferences.ContainsKey("favoriteTopic"))
                    {
                        currentTopic = userPreferences["favoriteTopic"];
                        DisplayRandomResponse(currentTopic, sentimentAnalysis, true);
                        continue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("What topic would you like to know more about?");
                        Console.ResetColor();
                        continue;
                    }
                }

                ProcessInput(input, sentimentAnalysis);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }
    }
    //Misspelling handling
    private static string CorrectMisspellings(string input)
    {
        foreach (var correction in spellingCorrections)
        {
            if (input.Contains(correction.Key))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Did you mean '{correction.Value}' instead of '{correction.Key}'?");
                Console.ResetColor();
                input = input.Replace(correction.Key, correction.Value);
            }
        }
        return input;
    }
    //Prompt Display
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
    //Empty input handling
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
            DisplayRandomResponse(currentTopic, sentimentAnalysis, false);
        }
        else
        {
            HandleUnknownInput(input);
        }
    }
    //Topic interest detection
    private static bool ProcessInterestStatement(string input)
    {
        var interestPhrases = new List<string>
        {
            "i'm interested in",
            "i am interested in",
            "i like",
            "i love",
            "i enjoy",
            "i want to know about",
            "tell me about"
        };

        if (!interestPhrases.Any(phrase => input.Contains(phrase)))
        {
            return false;
        }

        string detectedTopic = DetectTopic(input);
        if (!string.IsNullOrEmpty(detectedTopic))
        {
            userPreferences["favoriteTopic"] = detectedTopic;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Great! I'll remember that you're interested in {detectedTopic}.");
            Console.ResetColor();
            currentTopic = detectedTopic;
            inConversation = true;
            DisplayRandomResponse(currentTopic, ("neutral", ""), false);
            return true;
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("That sounds interesting! Could you tell me more about which cybersecurity topic you're interested in?");
        Console.ResetColor();
        return true;
    }

    private static bool ProcessSpecialCommands(string input)
    {
        var commands = new Dictionary<string, Action>
        {
            { "how are you", () => RespondToHowAreYou(AnalyzeSentiment(input)) },
            { "what's your purpose", () => DisplayPurpose() },
            { "what is your purpose", () => DisplayPurpose() },
            { "what can i ask you about", () => DisplayAvailableTopics() },
            { "more", () => DisplayRandomResponse(currentTopic, AnalyzeSentiment(input), true) },
            { "another tip", () => DisplayRandomResponse(currentTopic, AnalyzeSentiment(input), true) },
            { "tell me more", () => DisplayRandomResponse(currentTopic, AnalyzeSentiment(input), true) },
            { "remember", () => RecallUserPreferences() },
            { "recall", () => RecallUserPreferences() },
            { "help", () => DisplayHelp() },
            { "topics", () => DisplayAvailableTopics() },
            { "what is my name", () => RecallUserName() },
            { "what's my name", () => RecallUserName() }
        };

        foreach (var command in commands)
        {
            if (input.Contains(command.Key))
            {
                command.Value();
                return true;
            }
        }

        return false;
    }
    //Recall Username
    private static void RecallUserName()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        if (userPreferences.ContainsKey("name") && !string.IsNullOrWhiteSpace(userPreferences["name"]))
        {
            Console.WriteLine($"Your name is {userPreferences["name"]}.");
        }
        else
        {
            Console.WriteLine("I don't seem to have your name saved. Please tell me your name!");
        }
        Console.ResetColor();
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

    // Enhanced: Avoid repeating the same response until all have been shown
    private static void DisplayRandomResponse(string topic, (string sentiment, string emotion) sentimentAnalysis, bool isFollowUp)
    {
        if (string.IsNullOrEmpty(topic) || !responseLibrary.ContainsKey(topic) || responseLibrary[topic].Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("I seem to be missing information on that topic. Let's try something else.");
            Console.ResetColor();
            return;
        }

        if (!shownResponses.ContainsKey(topic))
            shownResponses[topic] = new List<int>();

        var availableIndexes = Enumerable.Range(0, responseLibrary[topic].Count).Except(shownResponses[topic]).ToList();
        if (availableIndexes.Count == 0)
        {
            shownResponses[topic].Clear();
            availableIndexes = Enumerable.Range(0, responseLibrary[topic].Count).ToList();
        }

        int responseIndex = availableIndexes[random.Next(availableIndexes.Count)];
        shownResponses[topic].Add(responseIndex);

        string response = responseLibrary[topic][responseIndex];
        response = AdjustResponseBySentiment(response, sentimentAnalysis);

        Console.ForegroundColor = ConsoleColor.Green;
        if (isFollowUp)
        {
            Console.WriteLine($"About {topic}, here's another tip: {response}");
        }
        else
        {
            Console.WriteLine(response);
        }
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
                response = "I understand this can be worrying. " + response + " Remember, you're taking the right step by learning about this.";
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
    //Topics Display
    private static void DisplayPurpose()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("My purpose is to assist you with cybersecurity knowledge and keep you safe online. I can help with:");
        Console.WriteLine("- Password safety and management");
        Console.WriteLine("- Recognizing and preventing phishing attempts");
        Console.WriteLine("- Identifying and avoiding scams");
        Console.WriteLine("- Protecting your privacy online");
        Console.WriteLine("- General security best practices");
        Console.ResetColor();
    }
    
    private static void DisplayAvailableTopics()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("You can ask me about these cybersecurity topics:");
        Console.WriteLine("- Passwords (creating strong passwords, password managers)");
        Console.WriteLine("- Phishing (recognizing scam emails, protecting yourself)");
        Console.WriteLine("- Scams (common online scams, how to avoid them)");
        Console.WriteLine("- Privacy (protecting your personal information online)");
        Console.WriteLine("- Security (general cybersecurity best practices)");
        Console.WriteLine("- Malware (viruses, ransomware, protection)");
        Console.ResetColor();
    }
    //Help Display
    private static void DisplayHelp()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("I can help you with cybersecurity information. Here's how to interact with me:");
        Console.WriteLine("- Ask about specific topics (e.g., 'tell me about phishing')");
        Console.WriteLine("- Say 'more' or 'what else should I know' to get additional information on the current topic");
        Console.WriteLine("- Say 'remember' to recall your preferences");
        Console.WriteLine("- Express your feelings and I'll adapt my responses");
        Console.WriteLine("- Type 'exit' to end our conversation");
        Console.ResetColor();
    }
    //User Preferences
    private static void RecallUserPreferences()
    {
        if (userPreferences.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("I don't have any preferences saved for you yet. You can tell me things like:");
            Console.WriteLine("- 'I'm interested in privacy'");
            Console.WriteLine("- 'I love learning about passwords'");
            Console.WriteLine("- 'Remember that I use a password manager'");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Here's what I remember about you:");
        foreach (var pref in userPreferences)
        {
            Console.WriteLine($"- {pref.Key.Replace("favorite", "favorite ")}: {pref.Value}");
        }
        Console.ResetColor();

        if (userPreferences.ContainsKey("favoriteTopic"))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Since you're interested in {userPreferences["favoriteTopic"]}, would you like to discuss that now?");
            Console.ResetColor();
        }
    }
    //Unknown input control
    private static void HandleUnknownInput(string input)
    {
        if (input.EndsWith("?") || input.StartsWith("what") || input.StartsWith("how") || input.StartsWith("why"))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("I'm not sure I understand your question. Could you try rephrasing it or ask about a specific cybersecurity topic?");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("I didn't quite understand that. Could you rephrase or ask about a cybersecurity topic?");
            Console.WriteLine("Try asking about: passwords, phishing, scams, privacy, or general security.");
            Console.WriteLine("Or type 'help' for assistance.");
            Console.ResetColor();
        }

        inConversation = false;
        currentTopic = "";
    }
    //Key words control
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
    //Exit control Message
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
            Console.WriteLine("Here's a quick summary of what we covered:");

            foreach (var topic in discussedTopics)
            {
                if (responseLibrary.ContainsKey(topic) && responseLibrary[topic].Count > 0)
                {
                    Console.WriteLine($"- {topic}: {responseLibrary[topic][0]}");
                }
            }
        }

        Console.ResetColor();
    }
    //Error Handling
    private static void HandleError(Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Oops! Something went wrong: {ex.Message}");
        Console.WriteLine("Let's try that again. If the problem persists, you may need to restart the conversation.");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"Error details: {ex.GetType().Name} in {ex.TargetSite?.DeclaringType?.Name}.{ex.TargetSite?.Name}");
        Console.ResetColor();
    }//End of program
}

