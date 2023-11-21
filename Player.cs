using System;
namespace PFRanger
{
    public enum PlayerAction
    {
        Fold,
        Check,
        Call,
        Raise,
        AllIn
    }

    public enum Position
    {
        BigBlind,
        SmallBlind,
        UnderTheGun,
        Hijack,
        Cutoff,
        Button
    }

    public class Player
    {
        public string Name { get; private set; }
        public int Stack { get; private set; }
        public Position Position { get; private set; }
        public List<Card> HoleCards { get; private set; }
        public PlayerAction LastAction { get; private set; }
        public int BetSize { get; private set; }
        public bool HasOpened { get; private set; }

        public Player(string name, int stack, Position position)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Player name cannot be empty or null.", nameof(name));
            }

            if (stack <= 0)
            {
                throw new ArgumentException("Player stack must be greater than zero.", nameof(stack));
            }

            Name = name;
            Stack = stack;
            Position = position;
            HoleCards = new List<Card>(2);
            ResetForNewHand();
        }

        public void ResetForNewHand()
        {
            HoleCards.Clear();
            LastAction = PlayerAction.Fold;
            BetSize = 0;
            HasOpened = false;
        }

        public void SetHoleCards(Card card1, Card card2)
        {
            HoleCards.Clear();
            HoleCards.Add(card1);
            HoleCards.Add(card2);
        }

        public void UpdateStack(int amount)
        {
            if (amount < 0 && Math.Abs(amount) > Stack)
            {
                throw new InvalidOperationException("Cannot deduct more than the player's current stack.");
            }

            Stack += amount;
        }

        public void PerformAction(PlayerAction action, int betAmount = 0)
        {
            LastAction = action;
            switch (action)
            {
                case PlayerAction.Fold:
                    // Handle fold logic.
                    break;
                case PlayerAction.Check:
                    // Handle check logic.
                    break;
                case PlayerAction.Call:
                case PlayerAction.Raise:
                    UpdateStack(-betAmount);
                    BetSize = betAmount;
                    break;
                case PlayerAction.AllIn:
                    BetSize = Stack;
                    UpdateStack(-Stack);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), "Unknown player action.");
            }
        }

        public void UpdatePosition(Position newPosition)
        {
            Position = newPosition;
        }
    }

}

