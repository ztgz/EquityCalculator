using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Helpers
{
    public static class RangeUtils
    {
        private const char CARD_SUITED = 's';
        private const char CARD_OFF_SUIT = 'o';
        private const string SUITS = "hsdc";

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
                if (!IsSpecificCombination(comb) && !IsCombinationOfCards(comb) && !IsRangeWithoutSuitSpecified(comb) &&
                    !IsRangeWithSuitSpecified(comb))
                {
                    return false;
                }
            }
            return true;

        }

        public static IList<string> GetHands(string combination)
        {
            List<string> hands = new List<string>();
            if (IsSpecificCombination(combination))
            {
                hands.Add(combination);
            }
            else if (IsCombinationOfCards(combination))
            {
                //Pocket pair
                if (combination[0] == combination[1])
                {
                    for (int i = 0; i < SUITS.Length - 1; i++)
                    {
                        for (int n = i + 1; n < SUITS.Length; n++)
                        {
                            hands.Add(combination[0].ToString() + SUITS[i] + combination[1] + SUITS[n]);
                        }
                    }
                }
                //Suit not specified
                else if (combination.Length == 2)
                {
                    hands.AddRange(GetHands(combination + CARD_SUITED));
                    hands.AddRange(GetHands(combination + CARD_OFF_SUIT));
                }
                //Suitded combination
                else if (combination[2] == CARD_SUITED)
                {
                    foreach (var suit in SUITS)
                    {
                        hands.Add(combination[0].ToString() + suit + combination[1] + suit);
                    }
                }
                //Of suit combination
                else if (combination[2] == CARD_OFF_SUIT)
                {
                    for (int i = 0; i < SUITS.Length; i++)
                    {
                        for (int n = 0; n < SUITS.Length; n++)
                        {
                            if (i != n)
                            {
                                hands.Add(combination[0].ToString() + SUITS[i] + combination[1] + SUITS[n]);
                            }
                        }
                    }
                }
            }
            else if (IsRangeWithSuitSpecified(combination))
            {
                for (byte i = GetValueOfCard(combination[1]); i >= GetValueOfCard(combination[5]); i--)
                {
                    hands.AddRange(GetHands(combination[0].ToString() + GetCardFromValue(i) + combination[2]));
                }
            }
            else if (IsRangeWithoutSuitSpecified(combination))
            {
                hands.AddRange(GetHands(combination.Substring(0, 2) + CARD_SUITED + "-" + combination.Substring(3, 2) + CARD_SUITED));
                hands.AddRange(GetHands(combination.Substring(0, 2) + CARD_OFF_SUIT + "-" + combination.Substring(3, 2) + CARD_OFF_SUIT));
            }
            //Set the order of cards
            return hands.Select(OrderHand).ToList();
        }

        #region HELPER METHODS TO DETERMINE WICH RANGE/COMBINATION STRING IS
        // AhAs, AhAd, Ah6h, Ah6
        private static bool IsSpecificCombination(string hand)
        {
            return Regex.IsMatch(hand, "^[AKQJT6-9][hcds][AKQJT6-9][hcds]$") && !(hand[0] == hand[2] && hand[1] == hand[3]);
        }
        //ATs, ATo, AT, 66
        private static bool IsCombinationOfCards(string hand)
        {
            return Regex.IsMatch(hand, "^[AKQJT6-9][AKQJT6-9][os]?$") && (hand.Length == 2 || hand[0] != hand[1]);
        }
        //Range of pairs 88-66, JT-J9, Range of cards not specified suit //
        private static bool IsRangeWithoutSuitSpecified(string hand)
        {
            return Regex.IsMatch(hand, "^[AKQJT6-9][AKQJT6-9][-][AKQJT6-9][AKQJT6-9]$") &&
                (hand[0] == hand[1] && hand[3] == hand[4] && hand[1] != hand[3] && HighestValueFirst(hand[0], hand[3])//88-66
                    || hand[0] == hand[3] && hand[1] != hand[4] && HighestValueFirst(hand[1], hand[4])); // JT-J8
        }
        //Range of cards not specified suit ATs-A6s, ATo-A6o
        private static bool IsRangeWithSuitSpecified(string hand)
        {
            return Regex.IsMatch(hand, "^[AKQJT6-9][AKQJT6-9][os][-][AKQJT6-9][AKQJT6-9][os]$") &&
                   hand[0] != hand[1] && hand[4] != hand[5] && hand[0] == hand[4] && hand[1] != hand[5] && hand[2] == hand[6]; //ATs-A6s, ATo-A6o
        }
        #endregion

        #region HELPER METHODS FOR CARD VALUE
        private static bool HighestValueFirst(char firstCard, char secondCard)
        {
            return GetValueOfCard(firstCard) > GetValueOfCard(secondCard);
        }

        public static byte GetValueOfCard(char card)
        {
            if (byte.TryParse(card.ToString(), out var num))
            {
                return num;
            }
            else if (card == 'T')
            {
                return 10;
            }
            else if (card == 'J')
            {
                return 11;
            }
            else if (card == 'Q')
            {
                return 12;
            }
            else if (card == 'K')
            {
                return 13;
            }
            else
            {
                return 14;
            }
        }

        private static char GetCardFromValue(byte value)
        {
            if (value < 10)
            {
                return value.ToString()[0];
            }
            if (value == 10)
            {
                return 'T';
            }
            else if (value == 11)
            {
                return 'J';
            }
            else if (value == 12)
            {
                return 'Q';
            }
            else if (value == 13)
            {
                return 'K';
            }
            else
            {
                return 'A';
            }
        }
        #endregion

        private static string OrderHand(string comb)
        {
            int valueFirst = GetValueOfCard(comb[0]);
            int valueSecond = GetValueOfCard(comb[2]);
            if (valueFirst > valueSecond)
            {
                return comb;
            }
            else if (valueFirst < valueSecond)
            {
                return Switch();
            }
            else
            {
                //compare suits (earlier should be first)
                if (SUITS.IndexOf(comb[1]) > SUITS.IndexOf(comb[3]))
                {
                    return Switch();
                }
                else
                {
                    return comb;
                }
            }

            //Local function
            string Switch() => comb[2].ToString() + comb[3] + comb[0] + comb[1];
        }
    }

}
