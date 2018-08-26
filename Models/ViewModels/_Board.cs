using System.Collections.Generic;

namespace Models.ViewModels
{
    public class _Board
    {
        public const byte BOARD_SIZE = 5;
        public _Card[] Cards { get; }
        private int _numberOfCards;

        private _Board()
        {
            Cards = new _Card[BOARD_SIZE];
            _numberOfCards = 0;
        }
        
        private _Board (IList<_Card> cards)
        {
            Cards = new _Card[BOARD_SIZE];
            for (int i = 0; i < cards.Count; i++)
            {
                Cards[i] = cards[i];
            }
            _numberOfCards = cards.Count;
        }

        private void AddCard(_Card card)
        {
            Cards[_numberOfCards] = card;
            _numberOfCards++;
        }

        public byte CardsOfSuit(_Card.Suits suit)
        {
            byte i = 0;
            foreach (var card in Cards)
            {
                if (card.Suit == suit)
                {
                    i++;
                }
            }

            return i;
        }

        public bool ContainsValue(byte value)
        {
            foreach (var card in Cards)
            {
                if (card.Value == value)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HandIsPossible(_Hand hand)
        {
            foreach (var card in Cards)
            {
                if (card.Value == hand.Card1.Value && card.Suit == hand.Card1.Suit ||
                    card.Value == hand.Card2.Value && card.Suit == hand.Card2.Suit)
                {
                    return false;
                }
            }

            return true;
        }

        public static IList<_Board> GeneratePossibleBoards(IList<_Card> dealtCards, IList<_Card> knownCards, IList<_Card> deadCards)
        {
            _Deck deck = new _Deck();
            deck.RemoveCards(dealtCards);
            deck.RemoveCards(knownCards);
            deck.RemoveCards(deadCards);

            List<_Board> boards = new List<_Board>();

            
            if (dealtCards.Count == 5)
            {
                boards.Add(new _Board(dealtCards));
            }
            else if (dealtCards.Count == 3)
            {
                for (int i = 0; i < deck.CardsInDeck()-1; i++)
                {
                    for (int n = i + 1; n < deck.CardsInDeck(); n++)
                    {
                        _Board board = new _Board(dealtCards);
                        board.AddCard(deck.CardAt(i));
                        board.AddCard(deck.CardAt(n));
                        boards.Add(board);
                    }
                }
            }
            else if (dealtCards.Count == 4)
            {
                for (int i = 0; i < deck.CardsInDeck(); i++)
                {
                    _Board board = new _Board(dealtCards);
                    board.AddCard(deck.CardAt(i));
                    boards.Add(board);
                }
            }
            else if (dealtCards.Count == 0)
            {
                //TODO REFACTOR OR REWRITE THIS CODE
                for (int i = 0; i < deck.CardsInDeck() - 4; i++)
                {
                    for (int n = i+1; n < deck.CardsInDeck() - 3; n++)
                    {
                        for (int z = n+1; z < deck.CardsInDeck() - 2; z++)
                        {
                            for (int q = z+1; q < deck.CardsInDeck() - 1; q++)
                            {
                                for (int j = q+1; j < deck.CardsInDeck(); j++)
                                {
                                    _Board board = new _Board();
                                    board.AddCard(deck.CardAt(i));
                                    board.AddCard(deck.CardAt(n));
                                    board.AddCard(deck.CardAt(z));
                                    board.AddCard(deck.CardAt(q));
                                    board.AddCard(deck.CardAt(j));
                                    boards.Add(board);
                                }
                            }
                        }
                    }
                }
            }
            return boards;
        }
    }
}
