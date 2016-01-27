namespace Poker.Core
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using Interfaces;
    using Poker.Core.Game_Objects;
    using Poker.Factories;

    public class Engine : IRunnable
    {
        private const int DeckCardsCount = 52;
        private const int TotalPlayersCount = 1;
        private const int TotalCompetitorsCount = 6;
        private const int CompetitorTurnTimeInSeconds = 60;
        private const int DefaultCompetitorPanelWidth = 180;
        private const int DefaultCompetitorPanelHeight = 180;
        private const bool DefaultCompetitorPanelVisibility = true;
        private static readonly Color DefaultCompetitorPanelBackColor = Color.DarkBlue;
        private readonly Point DefaultDeskCardsLocation = new Point(410, 265);
        private readonly Point[] competitorsPanelLocations;
        private readonly AnchorStyles[] competitorsPanelAnchors;
        private readonly IDealer dealer;
        private readonly Timer leftTimeTimer;
        private int callAmount;
        private ICompetitor competitorOnTurn;
        private IDictionary<string, Control> competitorControls;
        private int playingCompetitorsCount;
        private ICollection<ICompetitor> competitorsCollection;
        private IDeck cardsDeck;
        private IDeckCreator deckCreator;
        private Form1 mainWindow;


        public Engine()
        {
            this.mainWindow = new Form1();
            this.playingCompetitorsCount = TotalCompetitorsCount;
            this.leftTimeTimer = new Timer()
            {
                // tick every 1000 ms (1 sec)
                Interval = 1000
            };
            this.mainWindow.
            this.CompetitorControls = this.mainWindow.PlayerControls;
            string[] controlsNames = this.mainWindow.ButtonsNames;
            this.CompetitorControls[controlsNames[0]].Click += this.CallButton_OnClick;
            this.CompetitorControls[controlsNames[1]].Click += this.RaiseButton_OnClick;
            this.CompetitorControls[controlsNames[2]].Click += this.CheckButton_OnClick;
            this.CompetitorControls[controlsNames[3]].Click += this.FoldButton_OnClick;
            this.leftTimeTimer.Tick += this.On_Tick;
            this.CompetitorsCollection = new List<ICompetitor>();
            this.deckCreator = new DeckCreator(DeckCardsCount);
            this.competitorsPanelLocations = PanelsLocationsGenerator.GeneratePanelLocations();
            this.competitorsPanelAnchors = PanelsLocationsGenerator.GenerateAnchorStyles();
            string [] cardImagesLocations = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly); // TODO : Make this changable
            string[] charsToRemoveToGetTag = { "Assets\\Cards\\", ".png" };
            this.cardsDeck = deckCreator.CreateDeck(cardImagesLocations, charsToRemoveToGetTag);
            this.dealer = new Dealer(cardsDeck, DeckCardsCount);
        }

        public IDictionary<string, Control> CompetitorControls
        {
            get
            {
                return this.competitorControls;
            }

            set
            {
                this.competitorControls = value;
            }
        }

        public int PlayerRaiseAmount
        {
            get
            {
                int raiseAmount = this.mainWindow.RaiseAmount;
                if (raiseAmount < 0)
                {
                    throw new ArgumentException("Raise amount is not enough or incorrect"); //TODO:better checking 
                }

                return raiseAmount;
            }
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
            ExecuteTurns();

        }

        private void ExecuteTurns()
        {

            foreach (var competitor in CompetitorsCollection)
            {
                this.mainWindow.TimeLeftBarValue = CompetitorTurnTimeInSeconds*1000;
                this.competitorOnTurn = competitor;
                competitor.Onturn = true;
                competitor.
                this.leftTimeTimer.Start();

            }

        }

        public void InitializeComponents()
        {
            this.CreateCompetitors();
        }

        private void RaiseButton_OnClick(object sender, EventArgs e)
        {
            this.CompetitorsCollection.ToList()[0].Raise(this.PlayerRaiseAmount);
        }

        private void CallButton_OnClick(object sender, EventArgs e)
        {
            this.CompetitorsCollection.ToList()[0].Call();
        }

        private void CheckButton_OnClick(object sender, EventArgs e)
        {
            this.CompetitorsCollection.ToList()[0].Check();
        }

        private void FoldButton_OnClick(object sender, EventArgs e)
        {
            this.CompetitorsCollection.ToList()[0].Fold();
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
                    Location = this.competitorsPanelLocations[playerIndex],
                    Anchor = this.competitorsPanelAnchors[playerIndex]
                };
                IDictionary<string, Control> playerControls = this.mainWindow.PlayerControls;
                ICompetitor player = new Player(playerPanel, playerControls);
                this.CompetitorsCollection.Add(player);
            }

            for (int botIndex = playerIndex; botIndex < TotalCompetitorsCount; botIndex++)
            {
                Panel botPanel = new Panel()
                {
                    Width = DefaultCompetitorPanelWidth,
                    Height = DefaultCompetitorPanelHeight,
                    Visible = DefaultCompetitorPanelVisibility,
                    BackColor = DefaultCompetitorPanelBackColor,
                    Location = this.competitorsPanelLocations[botIndex],
                    Anchor = this.competitorsPanelAnchors[botIndex]
                };
                ICompetitor bot = new Bot(botPanel);
                this.CompetitorsCollection.Add(bot);
            }
        }

        private void On_Tick(object sender, EventArgs e)
        {
            int timeLeft = this.mainWindow.TimeLeftBarValue;
            if (timeLeft <= 0)
            {
                this.competitorOnTurn.TimeOut();
                //await Turns(); TODO:Fix here
            }
            else
            {
                // minus one second
                this.mainWindow.TimeLeftBarValue = timeLeft - 1000;
            }
        }
    }
}
