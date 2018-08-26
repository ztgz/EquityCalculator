using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Models.ViewModels;
using Services.Interfaces;
using Common.Helpers;

namespace Services
{
    public class EquityService : IEquityService
    {
        private const byte SIZE_OF_MADE_HAND = 5;
        private readonly IRangeService _rangeService;
        
        public EquityService()
        {
            _rangeService = new RangeService();
        }

        public (int statusCode, _Equities data) GetEquities(_Equities inputData)
        {
            foreach (var range in inputData.Ranges)
            {
                if (!RangeUtils.IsValid(range))
                {
                    return (400, null);
                }
            }

            IList<_Hand>[] hands = new IList<_Hand>[inputData.Ranges.Count];
            for (int i = 0; i < inputData.Ranges.Count; i++)
            {
                hands[i] = _rangeService.GetHands(inputData.Ranges[i]);
            }

            var allBoards = _Board.GeneratePossibleBoards(
                new List<_Card>()
                {
                }, 
                new List<_Card>() { },
                new List<_Card>() { }
            );

            return (200, inputData);
        }

        private void CalculateEquities(_Equities equities, IList<_Hand>[] hands, IList<_Board> boards)
        {
            
            //byte[Player][The strength of players specific hand on this board]
            byte[][] handStrength = new byte[hands.Length][];
            IList<_Card> cards = new List<_Card>(_Board.BOARD_SIZE+_Hand.HAND_SIZE);
            //Foreach board calulate all the possible handstrengths 
            foreach (_Board board in boards)
            {
                for (int i = 0; i < _Board.BOARD_SIZE; i++)
                {
                    cards[i] = board.Cards[i];
                }
                for (int playerIndex = 0; playerIndex < hands.Length; playerIndex++)
                {
                    for (int handIndex = 0; handIndex < hands[playerIndex].Count; handIndex++)
                    {
                        cards[_Board.BOARD_SIZE + 1] = hands[playerIndex][handIndex].Card1;
                        cards[_Board.BOARD_SIZE + 2] = hands[playerIndex][handIndex].Card2;
                        handStrength[playerIndex][handIndex] = GetHandStrength(cards, equities.FlushBeatFullHouse, equities.TripsBeatStaright);
                    }
                }

                //Compare hands
            }
        }

        public byte GetHandStrength(IList<_Card> cards, bool flushBeatFullHouse, bool tripsBeatStraight)
        {
            bool isFlush = IsFlush(cards);
            if (isFlush && IsStraightFlush(cards))
            {
                return IsRoyalFlush(cards) ? HandStrength.RoyalFlush : HandStrength.StraightFlush;
            }
            if (IsFourOfAKind(cards))
            {
                return HandStrength.FourOfAKind;
            }
            if (flushBeatFullHouse && isFlush)
            {
                return HandStrength.Flush(true);
            }

            bool isPair = IsPair(cards);
            bool isThreeOfAKind = IsThreeOfAKind(cards);
            //Fullhouse
            if (isPair && isThreeOfAKind)
            {
                return HandStrength.FullHouse(flushBeatFullHouse);
            }
            if (isFlush)
            {
                return HandStrength.Flush(false);
            }

            bool isStraight = IsStraight(cards);
            if (!tripsBeatStraight && isStraight)
            {
                return HandStrength.Straight(false);
            }
            if (isThreeOfAKind)
            {
                return HandStrength.ThreeOfAKind(tripsBeatStraight);
            }
            if (isStraight)
            {
                return HandStrength.Straight(true);
            }
            if (isPair && IsTwoPair(cards))
            {
                return HandStrength.TwoPair;
            }
            if (isPair)
            {
                return HandStrength.Pair;
            }    

            return HandStrength.Nothing;
        }

        #region HELPERS TO DETERMINE TYPE OF MADE HAND

        //Returns true even if straight flush
        private bool IsFlush(IList<_Card> cards)
        {
            foreach (_Card.Suits suit in _Card.GetAllSuits())
            {
                if (cards.Count(c => c.Suit == suit) >= SIZE_OF_MADE_HAND)
                {
                    return true;
                }
            }

            return false;
        }

        //private bool IsFullHouse(IList<_Card> cards)
        //{
        //    return false;
        //}

        private bool IsFourOfAKind(IList<_Card> cards)
        {
            for (int i = 0; i < cards.Count - 3; i++)
            {
                if (cards.Count(c => c.Value == cards[i].Value) == 4)
                {
                    return true;
                }
            }

            return false;
        }

        //Returns true even straight flush
        private bool IsStraight(IList<_Card> cards)
        {
            //Ace works for lower straights
            byte count = cards.Any(c => c.Value == _Deck.HIGHEST_CARD) ? (byte)1 : (byte)0;
            //Look for consecutive cards
            for (byte b = _Deck.LOWEST_CARD; b <= _Deck.HIGHEST_CARD; b++)
            {
                if (cards.Any(c => c.Value == b))
                {
                    count++;
                    if (count == SIZE_OF_MADE_HAND)
                    {
                        return true;
                    }
                }
                else
                {
                    if (b > _Deck.HIGHEST_CARD - SIZE_OF_MADE_HAND)
                    {
                        break;
                    }
                    count = 0;
                }
            }
 
            return false;
        }

        //Returns true even if royal flush
        private bool IsStraightFlush(IList<_Card> cards)
        {
            foreach (_Card.Suits suit in _Card.GetAllSuits())
            {
                //There is a suit
                if (cards.Count(c => c.Suit == suit) >= SIZE_OF_MADE_HAND)
                {
                    List<_Card> orderdCards = cards.Where(c => c.Suit == suit).OrderBy(c => c.Value).ToList();
                    //Ace works for lower straights
                    byte count = orderdCards[0].Value == _Deck.LOWEST_CARD && orderdCards[orderdCards.Count-1].Value == _Deck.HIGHEST_CARD ? (byte)2 : (byte)1;
                    for (int i = 1; i < orderdCards.Count; i++)
                    {
                        //If card connected to previous card
                        if (orderdCards[i].Value == orderdCards[i - 1].Value + 1)
                        {
                            count++;
                            if (count == SIZE_OF_MADE_HAND)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            //It's the first card in possible new straight
                            count = 1;
                        }
                    }
                    break;
                }
            }

            return false;
        }

        private bool IsRoyalFlush(IList<_Card> cards)
        {
            foreach (_Card.Suits suit in _Card.GetAllSuits())
            {
                if (cards.Count(c => c.Suit == suit && c.Value >= RangeUtils.GetValueOfCard('T')) == SIZE_OF_MADE_HAND)
                {
                    return true;
                }
            }

            return false;
        }

        //Returns true even if two pair
        private bool IsPair(IList<_Card> cards)
        {
            for (int i = 0; i < cards.Count - 1; i++)
            {
                if (cards.Count(c => c.Value == cards[i].Value) == 2)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsThreeOfAKind(IList<_Card> cards)
        {
            for (int i = 0; i < cards.Count - 2; i++)
            {
                if (cards.Count(c => c.Value == cards[i].Value) == 3)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsTwoPair(IList<_Card> cards)
        {
            bool pairFound = false;
            for (int i = _Deck.LOWEST_CARD; i <= _Deck.HIGHEST_CARD; i++)
            {
                if (cards.Count(c => c.Value == i) == 2)
                {
                    if (pairFound)
                    {
                        //If the second pair was found
                        return true;
                    }
                    //First pair is found
                    pairFound = true;
                }
            }
            return false;
        }

    #endregion  
    } 
}
