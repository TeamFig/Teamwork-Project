using System.Drawing;
using System.Windows.Forms;

namespace Poker.Core.Game_Objects
{
    using System;
    using System.Collections.Generic;
    using Interfaces;

    public class Dealer : IDealer
    {
        private const int CardsPerCompetitor = 2;
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

        public IDeck Deck { get; private set; }

        public void ShuffleCards()
        {
            var randomizer = new Random();
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
                AnchorStyles anchor = competitor.CompetitorPanel.Anchor;
                Point cardLocation = competitor.CompetitorPanel.Location;
                for (int cardIndex = 0; cardIndex < CardsPerCompetitor; cardIndex++)
                {
                    ICard card = this.Deck.GetCard();
                    card.Anchor = anchor;
                    card.Location = cardLocation;
                    cardLocation.X += card.Width;
                    competitor.CompetitorPanel.Controls.Add(card.CardPictureBox);
                    card.CardPictureBox.BringToFront();
                    competitor.Hand.Add(card);
                }

                competitor.LookCardsInHand();
            }
        }
    }
}
