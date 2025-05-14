README: ST10456704_CyberSecurityBot P1 & P2
Project Overview
Project Name: ST10456704_CyberSecurityBot
Framework: .NET Framework 4.0.8
Template: Console App C#

Part 1 Features
1. Audio Greeting
Plays a welcome audio file (Greetings.wav) upon startup

Automatic file location detection

Error handling for missing files

Manual path configuration option (e.g., c://users/Rc_student/repos/source/Greetings.wav)

2. ASCII Logo Display
Converts CyberSafetyBot.bmp to ASCII art

Graceful error handling for missing images

Customizable image path

3. User Interaction
text
Welcome to Cyber Security bot

Please enter your name: Ntando
Hello, Ntando! Welcome to Cyber Security Bot.

What would you like to ask me?
Type 'exit' to quit.

Ntando:> Tell me about passwords.
AI:> Passwords need to be protected at all times.
4. Cybersecurity Topics
Password security

Phishing

Online Security

General best practices

5. Error Handling
Handles non-cybersecurity questions

Validates empty inputs

6. Exit Command
Type exit to quit the application

Part 2 Enhancements
1. Dynamic Keyword Recognition
Recognizes cybersecurity keywords:

Password safety tips

Phishing detection

Privacy protection

Malware prevention

Example:

User: How can I improve my password security?
Bot: Consider using a password manager to generate and store complex passwords securely.
2. Random Response Selection
Multiple response variations for each topic

Randomized selection for more natural conversations

3. Conversation Memory
Remembers user preferences and interests

Personalizes subsequent responses

Example:

User: I'm interested in privacy
Bot: I'll remember you're interested in privacy. Here's a tip: Regularly review your social media privacy settings.
4. Sentiment Detection
Adjusts tone based on user emotions:

Worried → More reassuring

Frustrated → More patient

Curious → More detailed

Example:

User: I'm worried about online scams
Bot: I understand this can be concerning. Let me share some tips to help you stay safe...
5. Enhanced Error Handling
Graceful handling of unrecognized inputs

Context-aware suggestions

No crashes on unexpected inputs

6. Optimized Code Structure
Dictionary-based keyword storage

Modular response handling

Efficient data structures

7. Smart Name Recognition & Correction
Personalized Interaction: The bot remembers your name throughout the session

Typo Correction: Automatically fixes common cybersecurity term misspellings

Examples:

Name Recognition:

Please enter your name: Ntando
Hello, Ntando! I'm your Cybersecurity Assistant.

Ntando:> What's your name?
Bot: You can call me CyberGuard, Ntando!
Term Correction:

Ntando:> Tell me about fishing attacks
Bot: I think you meant "phishing" attacks. These are fraudulent attempts to obtain sensitive information by disguising as trustworthy entities.
Contextual Memory:

Ntando:> I'm interested in pasword safety
Bot: I'll remember your interest in password safety, Ntando. Did you mean "password" safety? Here's what you should know...
8. Advanced Text Processing Features
Spell Check: Corrects common cybersecurity term misspellings:

fishing → phishing

pasword → password

maleware → malware

ransomeware → ransomware

Natural Language Understanding: Recognizes questions even with minor typos

9. Personalized Learning
The bot learns your frequent topics of interest

Provides tailored suggestions based on your conversation history

Example Interaction:

Ntando:> I keep hearing about maleware
Bot: I notice you often ask about security threats, Ntando. Did you mean "malware"? 
     It's malicious software designed to harm your computer. Would you like me to 
     explain how to protect against it?
     
10. Per-User Persistent Memory
This bot remembers each user's cybersecurity interests and discussed topics across sessions by saving them to a file unique to each username.
•	How it works:
•	When you start the bot, you are prompted to enter your name.
•	The bot loads your preferences and discussed topics from a file named userprefs_{YourName}.txt (e.g., userprefs_Ntando.txt).
•	If you are a new user,the bot starts fresh with no saved interests or topics.
•	When you express interest in a topic (e.g., “I’m interested in privacy”), the bot saves it as your favorite topic.
•	Each new topic you discuss is added to your personal discussed topics list.
•	When you exit, your interests and discussed topics are saved to your personal file.
•	The next time you use the bot and enter your name, your interests and topics are recalled

 
•	What is saved:
•	Your favorite topic (the last topic you expressed interest in).
•	The list of all unique topics you have discussed with the bot.
•	What is not saved:
•	No other personal information is stored.
•	Each user’s data is kept separate and private.
Example: If you enter your name as Ntando, your preferences are saved in userprefs_Ntando.txt:

Author
Developed by: Ntando Nxumalo
Student Number: ST10456704


