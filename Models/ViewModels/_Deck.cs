using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.ViewModels
{
    public class _Deck
    {
        public const byte LOWEST_CARD  = 6;
        public const byte HIGHEST_CARD = 14;

        private readonly IList<_Card> _cards;

        public _Deck ()
        {
            _cards =new List<_Card>(36);
            for (byte i = LOWEST_CARD; i <= HIGHEST_CARD; i++)
            {
                foreach (var suit in _Card.GetAllSuits())
                {
                    _cards.Add(new _Card(i, suit));
                }
            }
        }

        public void RemoveCards(IList<_Card> remove)
        {
            foreach (_Card removeCard in remove)
            {
                for(int i = _cards.Count-1; i >= 0; i--) 
                {
                    if (_Card.IsSame(removeCard, _cards[i]))
                    {
                        _cards.RemoveAt(i);
                    }
                }
            }
        }

        public int CardsInDeck()
        {
            return _cards.Count;
        }

        public _Card CardAt(int index)
        {
            if (_cards.Count > index && index >= 0)
            {
                return _cards[index];
            }
            return null;
            
        }
    }
}
