using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Interfaces;

namespace Poker
{
    public class Deck : IDeck
    {
        private IList<ICard> cardsCollection;
        private int deckCount;
        private int nextCardIndex;

        public Deck(IList<ICard> cardsCollection)
        {
            this.CardsCollection = cardsCollection;
            this.DeckCount = CardsCollection.Count;
            this.nextCardIndex = 0;
        }

        public int DeckCount
        {
            get
            {
                return this.deckCount;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Deck count cannot be zero or negative number");
                }

                this.deckCount = value;
            }
        }

        public IList<ICard> CardsCollection
        {
            get
            {
                return this.cardsCollection;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Cards collection cannot ne null");
                }

                this.cardsCollection = value;
            }
        }

        public ICard GetCard()
        {
            ICard cardToReturn = this.CardsCollection.ToArray()[nextCardIndex];
            this.DeckCount--;
            this.nextCardIndex++;
            return cardToReturn;
        }

        public void Shuffle()
        {
            Random randomizer = new Random();
            for (int cardIndex = this.DeckCount; cardIndex > 0; cardIndex--)
            {
                int randomNumber = randomizer.Next(cardIndex);
                ICard randomedImageLocation = this.CardsCollection[randomNumber];
                this.CardsCollection[randomNumber] = this.CardsCollection[cardIndex - 1];
                this.CardsCollection[cardIndex - 1] = randomedImageLocation;
            }
        }
    }
}
