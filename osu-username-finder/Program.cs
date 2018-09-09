using System;
using System.Collections.Generic;
using System.IO;
using OsuSharp;
using OsuSharp.Interfaces;
using OsuSharp.Misc;
using OsuSharp.UserEndpoint;

namespace username_finder
{
    class Program
    {
        static void Main(string[] args)
        {
            if (new FileInfo(@"Resources\apikey.txt").Length == 0)
            {
                Console.WriteLine("Enter your api key: ");
                string apiInput = Console.ReadLine();

                File.WriteAllText(@"Resources\apikey.txt", apiInput);

                Console.Write(Environment.NewLine);
            }

            Console.WriteLine("What kind of username do you wish to find?");
            Console.Write(Environment.NewLine);
            Console.WriteLine("/words/");
            Console.WriteLine("     /three letter words/");
            Console.WriteLine("                       /three numbers/");
            Console.Write(Environment.NewLine);
            Console.Write("Use the numbers 1-3, \"all\" or type \"exit\" to quit. ");
            string input = Console.ReadLine().ToLower();

            while (input != "1" && input != "2" && input != "3" && input != "all" && input != "exit")
            {
                Console.WriteLine("Invalid input!");
                input = Console.ReadLine().ToLower();
            }
            if (input == "1")
            {
                FindWordNames();
                PrintErrorInfo();
            }
            else if (input == "2")
            {
                FindThreeLetterNames();
                PrintErrorInfo();
            }
            else if (input == "3")
            {
                FindThreeNumberNames();
                PrintErrorInfo();
            }
            else if (input == "all")
            {
                FindWordNames();
                FindThreeLetterNames();
                FindThreeNumberNames();
                PrintErrorInfo();
            }
            else if (input == "exit")
            {
                Environment.Exit(0);
            }

            Console.Write(Environment.NewLine);
            Console.Write("Type \"Exit\" to quit: ");

            input = Console.ReadLine().ToLower();

            while (input != "exit")
            {
                input = Console.ReadLine().ToLower();
            }
            Environment.Exit(0);
        }

        static string getApiKey()
        {
            string API_KEY = File.ReadAllText(@"Resources\apikey.txt");
            return API_KEY;
        }

        private IOsuApi Instance { get; }

        static void FindWordNames()
        {
            OsuApi Instance = new OsuApi(new OsuSharpConfiguration
            {
                ApiKey = getApiKey(),
                ModsSeparator = "|",
                LogLevel = LoggingLevel.Debug
            });

            var wordList = File.ReadLines(@"Resources\words.txt");

            List<string> availableUsernames = new List<string>();
            List<string> errorUsernames = new List<string>();

            foreach (var word in wordList)
            {
                try
                {
                    User user = Instance.GetUserByName(word);
                    if (user == null)
                    {
                        Console.WriteLine(word);
                        availableUsernames.Add(word);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Error occurred: {word}");
                    errorUsernames.Add(word);
                }
            }
            Console.Write(Environment.NewLine);
            Console.WriteLine($"Available usernames -> {availableUsernames.Count}");
            Console.WriteLine($"Errors occurred -> {errorUsernames.Count}");
            using (StreamWriter sw = File.AppendText(@"Results\WordListUsernames.txt"))
            {
                foreach (string freeName in availableUsernames)
                {
                    sw.WriteLine(freeName);
                }
                sw.Write(Environment.NewLine);
                sw.WriteLine($"Available usernames -> {availableUsernames.Count}");
                sw.WriteLine("-----------------------");
                sw.WriteLine($"Errors occurred -> {errorUsernames.Count}");
                sw.Write(Environment.NewLine);
                foreach (string errorName in errorUsernames)
                {
                    sw.WriteLine(errorName);
                }
                sw.WriteLine("-----------------------");
                sw.WriteLine("END");
            }
        }
        static void FindThreeLetterNames()
        {
            OsuApi Instance = new OsuApi(new OsuSharpConfiguration
            {
                ApiKey = getApiKey(),
                ModsSeparator = "|",
                LogLevel = LoggingLevel.Debug
            });
            var wordList = File.ReadLines(@"Resources\threeLetterWords.txt");

            List<string> availableUsernames = new List<string>();
            List<string> errorUsernames = new List<string>();

            foreach (var word in wordList)
            {
                try
                {
                    User user = Instance.GetUserByName(word);
                    if (user == null)
                    {
                        Console.WriteLine(word);
                        availableUsernames.Add(word);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Error occurred: {word}");
                    errorUsernames.Add(word);
                }
            }
            Console.Write(Environment.NewLine);
            Console.WriteLine($"Available usernames -> {availableUsernames.Count}");
            Console.WriteLine($"Errors occurred -> {errorUsernames.Count}");
            using (StreamWriter sw = File.AppendText(@"Results\ThreeLetterUsernames.txt"))
            {
                foreach (string freeName in availableUsernames)
                {
                    sw.WriteLine(freeName);
                }
                sw.Write(Environment.NewLine);
                sw.WriteLine($"Available usernames -> {availableUsernames.Count}");
                sw.WriteLine("-----------------------");
                sw.WriteLine($"Errors occurred -> {errorUsernames.Count}");
                sw.Write(Environment.NewLine);
                foreach (string errorName in errorUsernames)
                {
                    sw.WriteLine(errorName);
                }
                sw.WriteLine("-----------------------");
                sw.WriteLine("END");
            }
        }
        static void FindThreeNumberNames()
        {
            OsuApi Instance = new OsuApi(new OsuSharpConfiguration
            {
                ApiKey = getApiKey(),
                ModsSeparator = "|",
                LogLevel = LoggingLevel.Debug
            });

            List<string> availableUsernames = new List<string>();
            List<string> errorUsernames = new List<string>();

            for (char numberOne = '0'; numberOne < '9'; numberOne++)
            {
                for (char numberTwo = '0'; numberTwo < '9'; numberTwo++)
                {
                    for (char numberThree = '0'; numberThree < '9'; numberThree++)
                    {
                        string currentUsername = ($"{numberOne}{numberTwo}{numberThree}");
                        try
                        {
                            User user = Instance.GetUserByName(currentUsername);
                            if (user == null)
                            {
                                Console.WriteLine(currentUsername);
                                availableUsernames.Add(currentUsername);
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine($"Error occurred: {currentUsername}");
                            errorUsernames.Add(currentUsername);
                        }
                    }
                }
            }
            using (StreamWriter sw = File.AppendText(@"Results\ThreeNumberUsernames.txt"))
            {
                foreach (string freeName in availableUsernames)
                {
                    sw.WriteLine(freeName);
                }
                sw.Write(Environment.NewLine);
                sw.WriteLine($"Available usernames -> {availableUsernames.Count}");
                sw.WriteLine("-----------------------");
                sw.WriteLine($"Errors occurred -> {errorUsernames.Count}");
                sw.Write(Environment.NewLine);
                foreach (string errorName in errorUsernames)
                {
                    sw.WriteLine(errorName);
                }
                sw.WriteLine("-----------------------");
                sw.WriteLine("END");
            }
            Console.Write(Environment.NewLine);
            Console.WriteLine($"Available usernames -> {availableUsernames.Count}");
            Console.WriteLine($"Errors occurred -> {errorUsernames.Count}");
        }
        static void PrintErrorInfo()
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine("All usernames have been exported to their respective text files.");
        }
    }
}
