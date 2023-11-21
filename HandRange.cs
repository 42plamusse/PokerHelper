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

        public List<string> GetTopHandsByScenario(string scenario, double topPercentage)
        {
            var hands = scenario switch
            {
                "EquityVs1" => handRanks.EquityVs1,
                "EquityVs2" => handRanks.EquityVs2,
                "EquityVs3" => handRanks.EquityVs3,
                "EquityVs4" => handRanks.EquityVs4,
                "EquityVs5" => handRanks.EquityVs5,
                _ => throw new ArgumentException("Invalid scenario", nameof(scenario)),
            };

            int topCount = (int)(hands.Count * (topPercentage / 100.0));
            return hands.Take(topCount).ToList();
        }
    }

}

