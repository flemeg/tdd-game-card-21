using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Game21.Tests
{
    [TestFixture]
    public class GameTests
    {

        [Test]
        public void CreateOneCard_CardShouldHaveValueAndSuit()
        {
            var card = new Card
            {
                Value = CardType.Card_7,
                Suit = Suit.Diamonds
            };

            Assert.NotNull(card.Value);
            Assert.NotNull(card.Suit);
        }

        [Test]
        public void CreateCard_CardShouldHaveValueSuitFace()
        {
            var card = new Card
            {
                Value = CardType.Card_Queen,
                Suit = Suit.Diamonds
            };

            Assert.NotNull(card.Value);
            Assert.NotNull(card.Suit);
            int cardValue = (int)card.Value;
            Assert.AreEqual(11, cardValue);
        }

        [Test]
        public void StartGame_GameShouldHaveCorrectCardsCount()
        {
            var game = new Game();
            game.Init();

            IEnumerable<Card> cards = game.GetDeck();

            var diamondCards = cards.Where(c => c.Suit == Suit.Diamonds);
            var clubsCards = cards.Where(c => c.Suit == Suit.Clubs);
            var heartsCards = cards.Where(c => c.Suit == Suit.Hearts);
            var spadesCards = cards.Where(c => c.Suit == Suit.Spades);

            Assert.AreEqual(13, diamondCards.Count());
            Assert.AreEqual(13, heartsCards.Count());
            Assert.AreEqual(13, clubsCards.Count());
            Assert.AreEqual(13, spadesCards.Count());
            Assert.AreEqual(52, game.GetDeck().Count);
        }

        [Test]
        public void StartGame_CheckIfDeckIsShuffled()
        {
            var game = new Game();
            game.Init();

            var cards = game.GetShuffledDeck();

            var shuffledCards = 0;

            for (var i = 0; i < cards.Count(); i++)
            {
                var card = cards[i];

                if ((int)card.Value != i + 1 && i < 11)
                {
                    shuffledCards++;
                }

                if (i == 11)
                {
                    if ((int)card.Value != 11)
                    {
                        shuffledCards++;
                    }
                }
            }

            Assert.AreNotEqual(0, shuffledCards);
        }

        [Test]
        public void StartGame_EachCardShouldNotBeDuplicate()
        {
            var game = new Game();
            game.Init();

            var deck = game.GetDeck();

            var numberOfDuplicates = from c in deck
                                     group c by c
                                     into g
                                     let count = g.Count()
                                     orderby count descending
                                     select new { Value = g.Key, Count = count };

            Assert.AreEqual(0, numberOfDuplicates.Count(entry => entry.Count > 1));
        }
    }
}
