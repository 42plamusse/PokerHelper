using System;
namespace PFRanger
{
    public class Hand
    {
        public Card Card1 { get; }
        public Card Card2 { get; }

        private static readonly Dictionary<Suit, string> SuitNotations = new Dictionary<Suit, string>
        {
            {Suit.Clubs, "c" },
            {Suit.Diamonds, "d" },
            {Suit.Hearts, "h" },
            {Suit.Spades, "s" },
        };

        private static readonly Dictionary<Rank, string> RankNotations = new Dictionary<Rank, string>
        {
            { Rank.Two, "2" },
            { Rank.Three, "3" },
            { Rank.Four, "4" },
            { Rank.Five, "5" },
            { Rank.Six, "6" },
            { Rank.Seven, "7" },
            { Rank.Eight, "8" },
            { Rank.Nine, "9" },
            { Rank.Ten, "T" },
            { Rank.Jack, "J" },
            { Rank.Queen, "Q" },
            { Rank.King, "K" },
            { Rank.Ace, "A" }
        };

        public Hand(Card card1, Card card2)
        {
            if (card1 == null || card2 == null)
            {
                throw new ArgumentNullException("Cards cannot be null");
            }

            Card1 = card1;
            Card2 = card2;
        }

        public bool IsSuited()
        {
            return Card1.Suit == Card2.Suit;
        }

        public bool IsPair()
        {
            return Card1.Rank == Card2.Rank;
        }

        public string Notation()
        {
            var highCard = Card1.Rank > Card2.Rank ? Card1 : Card2;
            var lowCard = Card1.Rank > Card2.Rank ? Card2 : Card1;

            var highRankNotation = RankNotations[highCard.Rank];
            var lowRankNotation = RankNotations[lowCard.Rank];
            var suitedNotation = IsSuited() ? "s" : (IsPair() ? "" : "o");

            return $"{highRankNotation}{lowRankNotation}{suitedNotation}";
        }

        public override int GetHashCode()
        {
            // Using unchecked to prevent exceptions from overflow.
            // example:
            // unchecked
            // {
            //    int max = int.MaxValue;
            //    int result = max + 1;
            // }
            // This would normally throw an OverflowException, but in an unchecked context, it wraps around to int.MinValue.
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + Card1.GetHashCode();
                hash = hash * 31 + Card2.GetHashCode();
                // This ensures that the hash is the same regardless of the order of the cards.
                hash = hash * 31 + (Card1.GetHashCode() ^ Card2.GetHashCode());
                return hash;
            }
        }

        public override string ToString() => $"{Card1} and {Card2}";
    }
}

