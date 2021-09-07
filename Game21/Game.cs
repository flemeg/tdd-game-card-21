using System;
using System.Collections.Generic;
using System.Linq;

namespace Game21
{
    public class Game
    {
        private List<Card> _deck;

        public void Init()
        {
            _deck = new List<Card>();

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                _deck.AddRange(PopulateCards(suit));
            }
        }

        private static IEnumerable<Card> PopulateCards(Suit suit)
        {
            return (from CardType value in Enum.GetValues(typeof(CardType)) select new Card {Suit = suit, Value = value}).ToList();
        }

        public List<Card> GetDeck()
        {
            return _deck;
        }

        public List<Card> GetShuffledDeck()
        {
            var r = new Random();
            var deck = new List<Card> { _deck[r.Next(0, _deck.Count)] };
            return deck;
        }
    }
}