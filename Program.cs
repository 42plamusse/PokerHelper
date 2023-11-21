using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using PFRanger.PFRanger;

namespace PFRanger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HandRange? handRange;
            try
            {
                string filePath = "hand_ranks.json";
                string jsonText = File.ReadAllText(filePath);
                handRange = new HandRange(jsonText);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
                return;
            }
            catch (JsonException)
            {
                Console.WriteLine("Invalid JSON format.");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return;
            }

            if (handRange == null)
            {
                Console.WriteLine("Hand range data was not loaded correctly.");
                return;
            }

            while (true)
            {
                Console.Write("Enter a hand (e.g., AhKc) or 'exit' to quit: ");
                string? input = Console.ReadLine();

                if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                    break;

                if (input == null || input.Length != 4)
                {
                    Console.WriteLine("Invalid input. Please enter 4 characters representing a hand.");
                    continue;
                }

                try
                {
                    Hand hand = HandParser.Parse(input);
                    bool isTopHand = handRange.IsHandInTopRange(hand, 1, 10.0);

                    if (isTopHand)
                    {
                        Console.WriteLine($"{input} is in the top 10% of hands.");
                    }
                    else
                    {
                        Console.WriteLine($"{input} is NOT in the top 10% of hands.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
