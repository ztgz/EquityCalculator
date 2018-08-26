using System.Collections.Generic;
using Common.Helpers;
using Models.ViewModels;
using Services;
using Xunit;

namespace MadeHands
{
    public class HandOrder
    {
        private readonly EquityService _equityService;
        public HandOrder()
        {
            _equityService = new EquityService();
        }

        [Fact]
        public void IsRoyal()
        {
            List<_Card> cards = new List<_Card>(7)
            {
                new _Card("Ah"),
                new _Card("Kh"),
                new _Card("Th"),
                new _Card("6s"),
                new _Card("Jh"),
                new _Card("Ts"),
                new _Card("Qh"),
            };
            byte result = _equityService.GetHandStrength(cards, false, false);
            Assert.Equal(HandStrength.RoyalFlush, result);

        }

        [Fact]
        public void IsStraightFlush()
        {
            List<_Card> cards = new List<_Card>(7)
            {
                new _Card("9h"),
                new _Card("Kh"),
                new _Card("Th"),
                new _Card("6s"),
                new _Card("Jh"),
                new _Card("Ts"),
                new _Card("Qh"),
            };
            byte result = _equityService.GetHandStrength(cards, false, false);
            Assert.Equal(HandStrength.StraightFlush, result);

            cards = new List<_Card>(7)
            {
                new _Card("9s"),
                new _Card("Kh"),
                new _Card("8s"),
                new _Card("7s"),
                new _Card("Jh"),
                new _Card("6s"),
                new _Card("As"),
            };
            result = _equityService.GetHandStrength(cards, false, false);
            Assert.Equal(HandStrength.StraightFlush, result);
        }

        [Fact]
        public void IsFourOfAKind()
        {
            List<_Card> cards = new List<_Card>(7)
            {
                new _Card("9s"),
                new _Card("Kh"),
                new _Card("9c"),
                new _Card("9d"),
                new _Card("9h"),
                new _Card("6s"),
                new _Card("As"),
            };
            byte result = _equityService.GetHandStrength(cards, false, false);
            Assert.Equal(HandStrength.FourOfAKind, result);
        }

        [Fact]
        public void IsFullHouse()
        {
            List<_Card> cards = new List<_Card>(7)
            {
                new _Card("9s"),
                new _Card("Kh"),
                new _Card("9c"),
                new _Card("9d"),
                new _Card("Ah"),
                new _Card("6s"),
                new _Card("As"),
            };
            byte result = _equityService.GetHandStrength(cards, false, false);
            Assert.Equal(HandStrength.FullHouse(false), result);

            result = _equityService.GetHandStrength(cards, true, false);
            Assert.Equal(HandStrength.FullHouse(true), result);
        }

        [Fact]
        public void IsFlush()
        {
            List<_Card> cards = new List<_Card>(7)
            {
                new _Card("9d"),
                new _Card("Kd"),
                new _Card("Th"),
                new _Card("6d"),
                new _Card("Jd"),
                new _Card("Td"),
                new _Card("Qh"),
            };
            byte result = _equityService.GetHandStrength(cards, false, false);
            Assert.Equal(HandStrength.Flush(false), result);

            result = _equityService.GetHandStrength(cards, true, false);
            Assert.Equal(HandStrength.Flush(true), result);
        }

        [Fact]
        public void IsStraight()
        {
            List<_Card> cards = new List<_Card>(7)
            {
                new _Card("9h"),
                new _Card("8c"),
                new _Card("Td"),
                new _Card("6s"),
                new _Card("Jh"),
                new _Card("Ts"),
                new _Card("Qh"),
            };

            byte result = _equityService.GetHandStrength(cards, false, false);
            Assert.Equal(HandStrength.Straight(false), result);

            result = _equityService.GetHandStrength(cards, false, true);
            Assert.Equal(HandStrength.Straight(true), result);

            cards = new List<_Card>(7)
            {
                new _Card("9h"),
                new _Card("9c"),
                new _Card("Kd"),
                new _Card("9s"),
                new _Card("Jh"),
                new _Card("Ts"),
                new _Card("Qh"),
            };

            result = _equityService.GetHandStrength(cards, false, false);
            Assert.Equal(HandStrength.Straight(false), result);

            cards = new List<_Card>(7)
            {
                new _Card("9h"),
                new _Card("8c"),
                new _Card("7d"),
                new _Card("6s"),
                new _Card("Ah"),
                new _Card("Qs"),
                new _Card("Qh"),
            };

            result = _equityService.GetHandStrength(cards, false, false);
            Assert.Equal(HandStrength.Straight(false), result);
        }

        [Fact]
        public void IsThreeOfAKind()
        {
            List<_Card> cards = new List<_Card>(7)
            {
                new _Card("8h"),
                new _Card("8c"),
                new _Card("Td"),
                new _Card("6s"),
                new _Card("8d"),
                new _Card("As"),
                new _Card("Qh"),
            };

            byte result = _equityService.GetHandStrength(cards, false, false);
            Assert.Equal(HandStrength.ThreeOfAKind(false), result);

            cards = new List<_Card>(7)
            {
                new _Card("9h"),
                new _Card("9c"),
                new _Card("Kd"),
                new _Card("9s"),
                new _Card("Jh"),
                new _Card("Ts"),
                new _Card("Qh"),
            };

            result = _equityService.GetHandStrength(cards, false, true);
            Assert.Equal(HandStrength.ThreeOfAKind(true), result);
        }

        [Fact]
        public void IsTwoPair()
        {
            List<_Card> cards = new List<_Card>(7)
            {
                new _Card("8h"),
                new _Card("8c"),
                new _Card("Td"),
                new _Card("6s"),
                new _Card("Ad"),
                new _Card("As"),
                new _Card("Qh"),
            };

            byte result = _equityService.GetHandStrength(cards, false, false);
            Assert.Equal(HandStrength.TwoPair, result);
        }

        [Fact]
        public void IsPair()
        {
            List<_Card> cards = new List<_Card>(7)
            {
                new _Card("8h"),
                new _Card("8c"),
                new _Card("Td"),
                new _Card("6s"),
                new _Card("Ad"),
                new _Card("7s"),
                new _Card("Qh"),
            };

            byte result = _equityService.GetHandStrength(cards, false, false);
            Assert.Equal(HandStrength.Pair, result);
        }

        [Fact]
        public void IsNothing()
        {
            List<_Card> cards = new List<_Card>(7)
            {
                new _Card("8h"),
                new _Card("9c"),
                new _Card("Jd"),
                new _Card("6s"),
                new _Card("Kd"),
                new _Card("7s"),
                new _Card("Qh"),
            };

            byte result = _equityService.GetHandStrength(cards, false, false);
            Assert.Equal(HandStrength.Nothing, result);
        }
    }
}
