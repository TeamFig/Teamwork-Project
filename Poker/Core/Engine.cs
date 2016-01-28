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
        private int currentBet;
        private IBlindField smallBlindField;
        private IBlindField bigBlindField;
        private Pot potField;
        private ICompetitor competitorOnTurn;
        private IList<Control> competitorsStatusFields; 
        private IDictionary<string, Control> competitorControls;
        private int playingCompetitorsCount;
        private ICollection<ICompetitor> competitorsCollection;
        private IDeck cardsDeck;
        private IDeckCreator deckCreator;
        private Form1 mainWindow;


        public Engine()
        {
            this.MainWindow = new Form1();
            this.smallBlindField = new SmallBlind(
                this.MainWindow.SmallBlindButton,
                this.MainWindow.SmallBlindTextBox);
            this.bigBlindField = new BigBlind(
                this.MainWindow.BigBlindButton, 
                this.MainWindow.BigBlindTextBox);
            this.currentBet = smallBlindField.BlindAmount;
            this.potField = new Pot(this.MainWindow.PoTtTextBox);
            this.playingCompetitorsCount = TotalCompetitorsCount;
            this.leftTimeTimer = new Timer()
            {
                // tick every 1000 ms (1 sec)
                Interval = 1000
            };
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

        //public int PlayerRaiseAmount
        //{
        //    get
        //    {
        //        int raiseAmount = this.mainWindow.RaiseAmount;
        //        if (raiseAmount < 0)
        //        {
        //            throw new ArgumentException("Raise amount is not enough or incorrect"); //TODO:better checking 
        //        }

        //        return raiseAmount;
        //    }
        //}

        public IEnumerable<Panel> CompetitorsPanels
        {
            get
            {
                return this.CompetitorsCollection
                    .Select(competitor => competitor.CompetitorPanel);
            }
        }

        public Form1 MainWindow
        {
            get { return this.mainWindow; }
            set { this.mainWindow = value; }
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
            AddControls();
            ExecuteTurns();

        }

        private void ExecuteTurns()
        {
            this.mainWindow.TimeLeftBarValue = CompetitorTurnTimeInSeconds * 1000;
            this.competitorOnTurn = this.CompetitorsCollection.ToList()[0];
            this.competitorOnTurn.Onturn = true;
            string[] controlsNames = this.mainWindow.ButtonsNames;
            this.CompetitorControls[controlsNames[0]].Enabled = true;
            this.CompetitorControls[controlsNames[1]].Enabled = true;
            this.CompetitorControls[controlsNames[2]].Enabled = true;
            this.CompetitorControls[controlsNames[3]].Enabled = true;
            this.CompetitorControls[controlsNames[4]].Visible = true;
            this.leftTimeTimer.Start();
            //for (int competitorIndex = 1; competitorIndex < 0; competitorIndex++)
            //{
            //    this.mainWindow.TimeLeftBarValue = CompetitorTurnTimeInSeconds * 1000;
            //    this.competitorOnTurn = this.CompetitorsCollection.ToList()[competitorIndex];
            //    this.competitorOnTurn.Onturn = true;
            //    this.leftTimeTimer.Start();
            //}
        }

        public void InitializeComponents()
        {
            this.CreateCompetitors();
        }

        private void AddControls()
        {
            this.competitorsCollection.ToList().ForEach(competitor=> this.MainWindow.Controls.Add(competitor.CompetitorPanel.Controls[0]));
            this.competitorsCollection.ToList().ForEach(competitor => this.MainWindow.Controls.Add(competitor.CompetitorPanel.Controls[0]));
        }

        private void RaiseButton_OnClick(object sender, EventArgs e)
        {
            var player = this.CompetitorsCollection.ToList()[0];
            player.Raise(player.RaiseAmount);
            this.currentBet += player.RaiseAmount;
            this.potField.RaisePotAmount(player.RaiseAmount);
            this.DisableButtons();
            this.PlayNonPlayers();
        }

        private void CallButton_OnClick(object sender, EventArgs e)
        {
             
            ICompetitor player = this.CompetitorsCollection.ToList()[0];
            this.callAmount = this.currentBet - player.CurrentGameGivenChips;
            player.Call(callAmount);
            this.potField.RaisePotAmount(this.callAmount);
            this.DisableButtons();
            this.PlayNonPlayers();
            // TODO : Magical number
        }

        private void CheckButton_OnClick(object sender, EventArgs e)
        {
            this.CompetitorsCollection.ToList()[0].Check();
            this.DisableButtons();
            this.PlayNonPlayers();
        }

        private void DisableButtons()
        {
            string[] buttonsNames = this.MainWindow.ButtonsNames;
            for (int controlIndex = 0; controlIndex < buttonsNames.Length-1; controlIndex++)
            {
                this.CompetitorControls[buttonsNames[controlIndex]].Enabled = false;
            }

            this.CompetitorControls[buttonsNames[buttonsNames.Length - 1]].Visible = false;
        }

        private void FoldButton_OnClick(object sender, EventArgs e)
        {
            this.CompetitorsCollection.ToList()[0].Fold();
            this.playingCompetitorsCount--;
            DisableButtons();
            PlayNonPlayers();
        }

        private void CreateCompetitors()
        {
            int playerIndex;
            this.competitorsStatusFields = this.MainWindow.CompetitorsStatusFieldsCollection;
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
                TextBox playerTextBox = (TextBox)this.competitorsStatusFields[playerIndex];
                Label playerLabel = (Label) this.competitorsStatusFields[playerIndex + 1];
                TextBox playerRaiseTextBox = this.MainWindow.PlayerRaiseTextBox;
                ICompetitor player = new Player(playerPanel,playerTextBox, playerLabel, playerRaiseTextBox);
                this.CompetitorsCollection.Add(player);
            }

            for (int botIndex = playerIndex, controlIndex = playerIndex+1; botIndex < TotalCompetitorsCount; botIndex++)
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
                TextBox botTextBox =(TextBox) this.competitorsStatusFields[controlIndex];
                controlIndex++;
                Label botLabel = (Label) this.competitorsStatusFields[controlIndex];
                controlIndex++;
                ICompetitor bot = new Bot(botPanel, botTextBox, botLabel);
                this.CompetitorsCollection.Add(bot);
            }
        }

        private void TurnEnd()
        {
            this.competitorOnTurn.TimeOut();
            this.DisableButtons();
            this.PlayNonPlayers();
        }

        private void PlayNonPlayers()
        {
            for (int competitorIndex = 1; competitorIndex < this.CompetitorsCollection.Count; competitorIndex++)
            {
                MessageBox.Show("Bot " + competitorIndex + " turn.");
                this.ActNonPlayer(this.CompetitorsCollection.ToList()[competitorIndex]);
            }

            ExecuteTurns(); //TODO: Fix here bad name
        }

        private void ActNonPlayer(ICompetitor bot)
        {
            bot.Call(this.currentBet - bot.CurrentGameGivenChips);
        }

        private void On_Tick(object sender, EventArgs e)
        {
            int timeLeft = this.mainWindow.TimeLeftBarValue;
            if (timeLeft <= 0)
            {
                this.TurnEnd();
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
