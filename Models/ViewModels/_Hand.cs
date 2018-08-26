using Common.Helpers;

namespace Models.ViewModels
{
    public class _Hand
    {
        public const byte HAND_SIZE = 2;

        public _Card Card1 { get; set; }
        public _Card Card2 { get; set; }

        public _Hand(string combination)
        {
            Card1 = new _Card(combination.Substring(0,2));
            Card2 = new _Card(combination.Substring(2,2));
        }

        public byte CardsOfSuit(_Card.Suits suit)
        {
            if (Card1.Suit == suit && Card2.Suit == suit)
            {
                return 2;
            }
            if (Card1.Suit != suit && Card2.Suit != suit)
            {
                return 1;
            }

            return 0;
        }

        public bool ContainsValue(byte value)
        {
            return Card1.Value == value || Card2.Value == value;
        }
    }
}
