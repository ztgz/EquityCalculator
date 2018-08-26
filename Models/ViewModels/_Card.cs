using Common.Helpers;

namespace Models.ViewModels
{
    public class _Card
    {
        public enum Suits : byte
        {
            None = 0,
            Hearts = 1,
            Spades = 2,
            Diamonds = 3,
            Clubs = 4
        }

        public byte Value { get; set; }
        public Suits Suit { get; set; }

        public _Card(string comb)
        {
            Value = RangeUtils.GetValueOfCard(comb[0]);
            switch (comb[1])
            {
                case 'h':
                    Suit = Suits.Hearts;
                    break;
                case 's':
                    Suit = Suits.Spades;
                    break;
                case 'd':
                    Suit = Suits.Diamonds;
                    break;
                case 'c':
                    Suit = Suits.Clubs;
                    break;
                default:
                    Suit = Suits.None;
                    break;
            }
        }

        public _Card(byte value, Suits suit)
        {
            Value = value;
            Suit  = suit;
        }

        public static Suits[] GetAllSuits()
        {
            return new []{Suits.Hearts, Suits.Spades, Suits.Diamonds, Suits.Clubs};
        }

        public static bool IsSame(_Card card1, _Card card2)
        {
            return card1.Value == card2.Value && card1.Suit == card2.Suit;
        }
    }

}
