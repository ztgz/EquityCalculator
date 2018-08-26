namespace Common.Helpers
{
    public static class HandStrength
    {
        public static byte RoyalFlush = 0;
        public static byte StraightFlush = 1;
        public static byte FourOfAKind = 2;
        public static byte TwoPair = 7;
        public static byte Pair = 8;
        public static byte Nothing = 9;
        public static byte NonPossibleHand = byte.MaxValue;

        public static byte Flush(bool beatsFullhouse)
        {
            if (beatsFullhouse)
            {
                return 3;
            }
            return 4;
        }

        public static byte FullHouse(bool beatenByFlush)
        {
            if (beatenByFlush)
            {
                return 4;
            }
            return 3;
        }

        public static byte Straight(bool beatenByThreeOfAKind)
        {
            if (beatenByThreeOfAKind)
            {
                return 6;
            }
            return 5;
        }

        public static byte ThreeOfAKind(bool beatsStraight)
        {
            if (beatsStraight)
            {
                return 5;
            }
            return 6;
        }
    }
}
