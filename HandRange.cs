using System;
using Newtonsoft.Json;
namespace PFRanger
{
    public class HandRanks
    {
        public List<string> EquityVs1 { get; set; } = new List<string>();
        public List<string> EquityVs2 { get; set; } = new List<string>();
        public List<string> EquityVs3 { get; set; } = new List<string>();
        public List<string> EquityVs4 { get; set; } = new List<string>();
        public List<string> EquityVs5 { get; set; } = new List<string>();
    }

    public class HandRange
    {
        private readonly HandRanks handRanks;

        public HandRange(string json)
        {
            handRanks = JsonConvert.DeserializeObject<HandRanks>(json) ?? throw new InvalidOperationException("Hand ranks data could not be loaded.");
        }

        public bool IsHandInTopRange(Hand hand, int playersLeft, double topPercentage)
        {
            string handNotation = hand.Notation();
            var rankedHands = playersLeft switch
            {
                1 => handRanks.EquityVs1,
                2 => handRanks.EquityVs2,
                3 => handRanks.EquityVs3,
                4 => handRanks.EquityVs4,
                5 => handRanks.EquityVs5,
                _ => throw new ArgumentException("Invalid number of players.", nameof(playersLeft)),
            };

            int topCount = (int)(rankedHands.Count * (topPercentage / 100.0));
            return rankedHands.Take(topCount).Contains(handNotation);
        }
    }

}

