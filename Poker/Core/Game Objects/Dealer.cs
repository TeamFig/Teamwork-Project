namespace Poker.Core.Game_Objects
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    public class Dealer : IDealer
    {
        private const int CardsPerCompetitor = 2;
        private IDeck deck;
        private int competitorsCount;

        public Dealer(IDeck cardsDeck, int competitorsCount)
        {
            this.Deck = cardsDeck;
            this.CompetitorsCount = competitorsCount;
        }

        public int CompetitorsCount
        {
            get
            {
                return this.competitorsCount;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Competitors count cannot be zero or negative number");
                }

                this.competitorsCount = value;
            }
        }

        public IDeck Deck
        {
            get
            {
                return this.deck;
            }

            private set
            {
                this.deck = value;
            }
        }
        public void ShuffleCards()
        {
            Random randomizer = new Random();
            for (int cardIndex = this.Deck.DeckCount; cardIndex > 0; cardIndex--)
            {
                int randomNumber = randomizer.Next(cardIndex);
                ICard randomedImageLocation = this.Deck.CardsCollection[randomNumber];
                this.Deck.CardsCollection[randomNumber] = this.Deck.CardsCollection[cardIndex - 1];
                this.Deck.CardsCollection[cardIndex - 1] = randomedImageLocation;
            }
        }

        public void ThrowCards(ICollection<ICompetitor> competitorsCollection)
        {
            foreach (ICompetitor competitor in competitorsCollection)
            {
                for (int cardIndex = 0; cardIndex < CardsPerCompetitor; cardIndex++)
                {
                    ICard card = this.Deck.GetCard();
                    competitor.Hand.Add(card);
                    // TODO: Set each card picture box variables depending on Competitors panel variables
                }
            }
        }
    }
}
