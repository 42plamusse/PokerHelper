using System;
namespace PFRanger
{
    using System;
    using System.Collections.Generic;

    namespace PFRanger
    {
        public static class HandParser
        {
            private static readonly Dictionary<char, Rank> RankMappings = new Dictionary<char, Rank>
            {
                { '2', Rank.Two },
                { '3', Rank.Three },
                { '4', Rank.Four },
                { '5', Rank.Five },
                { '6', Rank.Six },
                { '7', Rank.Seven },
                { '8', Rank.Eight },
                { '9', Rank.Nine },
                { 'T', Rank.Ten },
                { 'J', Rank.Jack },
                { 'Q', Rank.Queen },
                { 'K', Rank.King },
                { 'A', Rank.Ace }
            };

            private static readonly Dictionary<char, Suit> SuitMappings = new Dictionary<char, Suit>
            {
                { 'c', Suit.Clubs },
                { 'd', Suit.Diamonds },
                { 'h', Suit.Hearts },
                { 's', Suit.Spades }
            };

            public static Hand Parse(string handNotation)
            {
                if (handNotation == null) throw new ArgumentNullException(nameof(handNotation));
                if (handNotation.Length != 4) throw new ArgumentException("Hand notation must be 4 characters long.");

                Rank rank1 = ParseRank(handNotation[0]);
                Suit suit1 = ParseSuit(handNotation[1]);

                Rank rank2 = ParseRank(handNotation[2]);
                Suit suit2 = ParseSuit(handNotation[3]);

                return new Hand(new Card(suit1, rank1), new Card(suit2, rank2));
            }

            private static Rank ParseRank(char rankChar)
            {
                if (RankMappings.TryGetValue(rankChar, out Rank rank))
                {
                    return rank;
                }
                throw new ArgumentException($"Invalid rank character: {rankChar}");
            }

            private static Suit ParseSuit(char suitChar)
            {
                if (SuitMappings.TryGetValue(suitChar, out Suit suit))
                {
                    return suit;
                }
                throw new ArgumentException($"Invalid suit character: {suitChar}");
            }
        }
    }

}

