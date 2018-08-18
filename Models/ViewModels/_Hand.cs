namespace Models.ViewModels
{
    public class _Hand
    {
        public class Card
        {
            public enum Suits
            {
                NONE     = 0,
                HEARTS   = 1,
                SPADES   = 2,
                DIAMONDS = 3,
                CLUBS    = 4
            }

            public int   Value { get; set; }
            public Suits Suit  { get; set; }
        }

        public Card Card1 { get; set; }
        public Card Card2 { get; set; }
        
    }
}
