namespace Commons.Helpers
{
    public static class RangeUtils
    {
        /* Allowed range: 
         * Suited combinations: ATs
         * Of suit combinations: ATo
         * All combinations: AT => ATs, ATo
         * Pairs combinations: 66
         * Range of pairs: 66-88 => 66, 77, 88
         * Range of cards: J9o-J6o => J9o, J8o, J7o, J6o
         *                 KQs-KJs => KQs, KJs
         * Specific Cards: AhAs, AhAx, Ah6h, Ah6x
         */
        public static bool IsValid(string range)
        {
            string[] combinations = range.Split(',');
            foreach (var comb in combinations)
            {
                if (comb is null)
                {
                    continue;
                }
                //var hand = comb.Trim();
                ////EG: AhAs, AhAx, Ah6h, Ah6x
                //string pattern = "^[AKQJT6-9][hcdsx][AKQJT6-9][hcdsx]";
                //if(Regex.IsMatch(hand, pattern) && IsSpecificCombination(hand)) continue;
                ////EG: ATs, ATo, AT, 66
                //pattern = "^[AKQJT6-9][AKQJT6-9][os]?$";
                //if (Regex.IsMatch(hand, pattern) && IsCombinationOfCards(hand)) continue;
                ////EG: 66-88, JT-J8, JTs-J8s, JTo-J8,
                //pattern = "^$[AKQJT6-9][AKQJT6-9][os]?-[AKQJT6-9][AKQJT6-9][os?]";
                //if (Regex.IsMatch(hand, pattern) && (IsRangeWithoutSuitSpecified(hand) || IsRangeWithSuitSpecified(hand))) continue;
                ////No match => hand is not a vaild combination
                //return false;
            }
            return true;
        }

        //#region HELPER METHOD ONLY WORKS ON TOGHETER WITH RIGHT REGEX EXPRESSION
        //// AhAs, AhAx, Ah6h, Ah6x
        //private static bool IsSpecificCombination(string hand)
        //{
        //    return !(hand[0] == hand[2] && hand[1] == hand[3]);
        //}

        ////ATs, ATo, AT, 66
        //private static bool IsCombinationOfCards(string hand)
        //{
        //    return hand.Length == 2 || hand[0] != hand[1];
        //}

        ////Range of pairs 66-88, Range of cards not specified suit //
        //private static bool IsRangeWithoutSuitSpecified(string hand)
        //{
        //    return hand.Length == 5 && hand[2] == '-' // xx-xx
        //                            && (hand[0] == hand[1] && hand[3] == hand[4] && hand[1] != hand[2] //66-88
        //                                || hand[0] == hand[3] && hand[1] != hand[4]); // JT-J8
        //}

        ////Range of cards not specified suit ATs-A6s, ATo-A6o
        //private static bool IsRangeWithSuitSpecified(string hand)
        //{
        //    return hand.Length == 7 && hand[3] == '-' // XXs-XXs
        //                            && hand[0] != hand[1] && hand[4] != hand[5] && hand[0] == hand[4] && hand[1] != hand[5] && hand[2] == hand[6]; //ATs-A6s, ATo-A6o
        //}
        //#endregion

    }
}
