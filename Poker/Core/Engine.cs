namespace Poker.Core
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using Interfaces;
    public class Engine : IRunnable
    {
        private const int DeckCardsCount = 52;
        private const int TotalPlayersCount = 1;
        private const int TotalCompetitorsCount = 6;
        private const int PlayerTurnTimeInSeconds = 60;
        private const int DefaultCompetitorPanelWidth = 180;
        private const int DefaultCompetitorPanelHeight = 150;
        private const bool DefaultCompetitorPanelVisibility = false;
        private static readonly Color DefaultCompetitorPanelBackColor = Color.DarkBlue;
        private readonly Point DefaultDeskCardsLocation = new Point(410, 265);
        private Point[] competitorsPanelLocations;
        private IDealer dealer;
        private ICollection<ICompetitor> competitorsCollection;
        private IDeck cardsDeck;
        private IDeckCreator deckCreator;


        public Engine()
        {
            this.deckCreator = new DeckCreator(DeckCardsCount);
            this.competitorsPanelLocations = PanelsLocationsGenerator.GeneratePanelLocations();
            string [] cardImagesLocations = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly); // TODO : Make this changable
            string[] charsToRemoveToGetTag = { "Assets\\Cards\\", ".png" };
            this.cardsDeck = deckCreator.CreateDeck(cardImagesLocations, charsToRemoveToGetTag);
            this.dealer = new Dealer(cardsDeck, DeckCardsCount);
        }

        public IEnumerable<Panel> CompetitorsPanels
        {
            get
            {
                return this.CompetitorsCollection
                    .Select(competitor => competitor.CompetitorPanel);
            }
        } 

        private ICollection<ICompetitor> CompetitorsCollection
        {
            get { return this.competitorsCollection; }
             set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Competitors collection cannot be null");
                }

                this.competitorsCollection = value;
            }
        }

        public void Run()
        {
            this.dealer.ShuffleCards();
            this.dealer.ThrowCards(this.CompetitorsCollection);
        }

        public void InitializeComponents()
        {
            this.CreateCompetitors();
        }

        private void CreateCompetitors()
        {
            int playerIndex;
            for (playerIndex = 0; playerIndex < TotalPlayersCount; playerIndex++)
            {
                Panel playerPanel = new Panel()
                {
                    Width = DefaultCompetitorPanelWidth,
                    Height = DefaultCompetitorPanelHeight,
                    Visible = DefaultCompetitorPanelVisibility,
                    BackColor = DefaultCompetitorPanelBackColor,
                    Location = this.competitorsPanelLocations[playerIndex]
                };
                ICompetitor player = new Player(playerPanel);
                this.CompetitorsCollection.Add(player);
            }
        }
    }
}
