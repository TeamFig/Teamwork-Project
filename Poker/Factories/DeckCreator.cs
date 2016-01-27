namespace Poker.Factories
{
    using System;
    using System.Drawing;
    using System.Linq;
    using Interfaces;
    using Poker.Core.Game_Objects;

    public class DeckCreator : IDeckCreator
    {
        private readonly int DeckCardsCount;

        public DeckCreator(int deckCount)
        {
            if (deckCount <= 0)
            {
                throw new ArgumentException("Deck count cannot be zero or negative number");
            }

            this.DeckCardsCount = deckCount;
        }

        public IDeck CreateDeck(string[] cardImagesLocations, string [] charsToRemoveToGetTag)
        {
            Image[] deckCardsImagesCollection = FillImagesCollection(cardImagesLocations);
            int[] cardsImagesLoactionsOnlyNumbers = GetOnlyNumbersOfImgLocations(cardImagesLocations, charsToRemoveToGetTag);
            ICard[] cardsCollection = new ICard[DeckCardsCount];
            for (int card = 0; card < DeckCardsCount; card++)
            {
                Image cardFrontImage = deckCardsImagesCollection[card];
                int cardTag = cardsImagesLoactionsOnlyNumbers[card] - 1;
                cardsCollection[card] = new Card(cardFrontImage, cardTag);
            }

            IDeck createdDeck = new Deck(cardsCollection);
            return createdDeck;
        }

        private int[] GetOnlyNumbersOfImgLocations(string[] cardImagesLocations, string [] charsToRemove)
        {
            int[] cardImagesNumbersCollection = cardImagesLocations
                .Select(cardImageLocation => cardImageLocation
                .Replace(charsToRemove[0], string.Empty))
                .Select(cardImageLocation => cardImageLocation
                .Replace(charsToRemove[1], string.Empty))
                .Select(cardNumber => int.Parse(cardNumber))
                .ToArray();
            return cardImagesNumbersCollection;
        }

        private Image[] FillImagesCollection(string [] cardImagesLocations)
        {
            Image[] deckCardsImagesCollection = new Image[DeckCardsCount];
            for (int cardNumber = 0; cardNumber < DeckCardsCount; cardNumber++)
            {
                deckCardsImagesCollection[cardNumber] = Image.FromFile(cardImagesLocations[cardNumber]);
            }

            return deckCardsImagesCollection;
        }
    }
}
