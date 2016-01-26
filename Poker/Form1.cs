using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Poker.Core.Game_Objects;
using Poker.Factories;
using Poker.Interfaces;

namespace Poker
{

    public partial class Form1 : Form
    {
        private const int DeckCardsCount = 52;
        private const int CardsPerCompetitor = 2;
        private const int TotalCompetitorsCount = 6;
        private const int PlayerTurnTimeInSeconds = 60;
        private readonly Point DefaultDeskCardsLocation = new Point(410, 265);
        #region Variables
        ProgressBar asd = new ProgressBar();
        private ICompetitor bot1;
        private Panel playerPanel = new Panel();
        private Panel bot2Panel = new Panel();
        private Panel bot3Panel = new Panel();
        private Panel bot4Panel = new Panel();
        private Panel bot5Panel = new Panel();
        private int call = 500;
        private int foldedPlayers = 5;
        private int playerChips = 10000;
        private int bot2Chips = 10000;
        private int bot3Chips = 10000;
        private int bot4Chips = 10000;
        private int bot5Chips = 10000;
        private double type;
        private int rounds;
        private double bot2Power;
        private double bot3Power;
        private double bot4Power;
        private double bot5Power;
        private double playerPower;
        private double playerType = -1;
        private double raise;
        private double bot2Type = -1;
        private double bot3Type = -1;
        private double bot4Type = -1;
        private double bot5Type = -1;
        private bool playerTurn = true;
        private bool bot2Turn;
        private bool bot3Turn;
        private bool bot4Turn;
        private bool bot5Turn;
        private bool playerFoldedTurn;
        private bool bot2FoldedTurn;
        private bool bot3FoldedTurn;
        private bool bot4FoldedTurn;
        private bool bot5FoldedTurn;
        private bool playerFolded;
        private bool bot2Folded;
        private bool bot3Folded;
        private bool bot4Folded;
        private bool bot5Folded;
        private bool intsadded;
        private bool changed;
        private int playerCall;
        private int bot2Call;
        private int bot3Call;
        private int bot4Call;
        private int bot5Call;
        private int playerRaise;
        private int bot2Raise;
        private int bot3Raise;
        private int bot4Raise;
        private int bot5Raise;
        private int height = 800;
        private int width = 1200;
        private int winners;
        private int flop = 1;
        private int turn = 2;
        private int river = 3;
        private int end = 4;
        private int leftCompetitorsCount = 6;
        private int raisedTurn = 1;
        List<bool?> bools = new List<bool?>();
        List<Type> win = new List<Type>();
        List<string> CheckWinners = new List<string>();
        List<int> ints = new List<int>();
        private bool restart;
        private bool raising;
        Poker.Type sorted;
        private string[] cardImagesLocations;
        /*string[] cardImagesLocations ={
                   "Assets\\Cards\\33.png","Assets\\Cards\\22.png",
                    "Assets\\Cards\\29.png","Assets\\Cards\\21.png",
                    "Assets\\Cards\\36.png","Assets\\Cards\\17.png",
                    "Assets\\Cards\\40.png","Assets\\Cards\\16.png",
                    "Assets\\Cards\\5.png","Assets\\Cards\\47.png",
                    "Assets\\Cards\\37.png","Assets\\Cards\\13.png",
                    
                    "Assets\\Cards\\12.png",
                    "Assets\\Cards\\8.png","Assets\\Cards\\18.png",
                    "Assets\\Cards\\15.png","Assets\\Cards\\27.png"};*/
        private readonly int[]  throwenCardsTagsCollection = new int[17];
        private readonly Image[] cardImagesCollection = new Image[52];
        private readonly PictureBox[] cardsPictureBoxes = new PictureBox[52];
        private readonly Timer timer = new Timer();
        private readonly Timer updates = new Timer();
        private int i;
        private int bigBlind = 500;
        private int smallBlind = 250;
        private int turnCount = 0;
        private IDeck cardsDeck;
        private IDeckCreator deckCreator;
        private ICompetitor[] competitorsCollection;
        #endregion
        public Form1()
        {
            this.competitorsCollection = new ICompetitor[TotalCompetitorsCount];
            Panel bot1Panel = new Panel();
            int bot1Chips = 10000;
            double bot1Power = 0;
            double bot1Type = -1;
            bool bot1Turn = false;
            bool bot1FoldedTurn = false;
            bool bot1Folded = false;
            int bot1Call = 0;
            int bot1Raise = 0;
            this.bot1 = new Bot(bot1Panel, bot1Chips, bot1Power, bot1Type, bot1Turn, bot1FoldedTurn, bot1Folded, bot1Call, bot1Raise, new List<ICard>());        
            string[] cardImagesLocations = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            string [] charsToRemove = { "Assets\\Cards\\", ".png" };
            this.deckCreator = new DeckCreator(DeckCardsCount);
            this.cardsDeck = this.deckCreator.CreateDeck(cardImagesLocations, charsToRemove);
            this.call = this.bigBlind;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
  
            this.InitializeComponent();
            this.PrepareForGame();
            this.updates.Start();
            this.potTextBox.Enabled = false;
            this.playerChipsTextBox.Enabled = false;
            this.bot1ChipsTextBox.Enabled = false;
            this.bot2ChipsTextBox.Enabled = false;
            this.bot3ChipsTextBox.Enabled = false;
            this.bot4ChipsTextBox.Enabled = false;
            this.bot5ChipsTextBox.Enabled = false;
            this.playerChipsTextBox.Text = "Chips : " + this.playerChips;
            this.bot1ChipsTextBox.Text = "Chips : " + this.bot1.ChipsCount;
            this.bot2ChipsTextBox.Text = "Chips : " + this.bot2Chips;
            this.bot3ChipsTextBox.Text = "Chips : " + this.bot3Chips;
            this.bot4ChipsTextBox.Text = "Chips : " + this.bot4Chips;
            this.bot5ChipsTextBox.Text = "Chips : " + this.bot5Chips;
            this.timer.Interval = 1000;
            this.timerProgressBar.Maximum = PlayerTurnTimeInSeconds * 1000;
            this.timerProgressBar.Value = PlayerTurnTimeInSeconds * 1000;
            this.timer.Tick += this.timer_Tick;
            this.updates.Interval = 100;
            this.updates.Tick += this.Update_Tick;
            this.bigBlindTextBox.Visible = false;
            this.smallBlindTextBox.Visible = false;
            this.bigBlindButton.Visible = false;
            this.smallBlindButton.Visible = false;
            this.raiseTextBox.Text = (this.bigBlind * 2).ToString();
        }

        private async Task PrepareForGame()
        { 
            this.bools.Add(this.playerFoldedTurn);
            this.bools.Add(this.bot1.FoldedTurn);
            this.bools.Add(this.bot2FoldedTurn);
            this.bools.Add(this.bot3FoldedTurn);
            this.bools.Add(this.bot4FoldedTurn);
            this.bools.Add(this.bot5FoldedTurn);
            this.callButton.Enabled = false;
            this.raiseButton.Enabled = false;
            this.foldButton.Enabled = false;
            this.checkButton.Enabled = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //bool check = false;

            //Bitmap cardBackImage = new Bitmap("Assets\\Back\\Back.png");

            await ThrowCards();

            #region Commented
            //                await Task.Delay(200);

            //    #region Player
            //                if (cardIndex < 2)
            //                {

            //                    //if (this.cardsPictureBoxes[0].Tag != null)
            //                    //{
            //                    //    this.cardsPictureBoxes[1].Tag = this.throwenCardsTagsCollection[1];
            //                    //}

            //                    //this.cardsPictureBoxes[0].Tag = this.throwenCardsTagsCollection[0];





            //                    //this.playerPanel.BackColor = Color.DarkBlue;
            //                    //this.playerPanel.Height = 150;
            //                    //this.playerPanel.Width = 180;
            //                    //this.playerPanel.Visible = false;
            //                }
            //#endregion
            //                if (this.bot1.ChipsCount > 0)
            //                {

            //                    if (cardIndex >= 2 && cardIndex < 4)
            //                    {
            //#region if there are 2 cards on the table and less than 4
            //                        if (this.cardsPictureBoxes[2].Tag != null)
            //                        {
            //                            this.cardsPictureBoxes[3].Tag = this.throwenCardsTagsCollection[3];
            //                        }

            //                        this.cardsPictureBoxes[2].Tag = this.throwenCardsTagsCollection[2];
            //                        if (!check)
            //                        {

            //                        }

            //                        check = true;
            //                        card.Anchor = ();
            //                        //this.cardsPictureBoxes[cardIndex].Image = cardBackImage; NO need by default card image is its Back
            //                        card.Location = new Point(horizontal, vertical);
            //                        horizontal += card.Width;
            //                        this.cardsPictureBoxes[cardIndex].Visible = true;
            //                        this.Controls.Add(this.bot1.CompetitorPanel);
            //                        this.bot1.CompetitorPanel.Location = new Point(card.CardPictureBox.Left - 10, card.CardPictureBox.Top - 10);
            //                        this.bot1.CompetitorPanel.BackColor = Color.DarkBlue;
            //                        this.bot1.CompetitorPanel.Height = 150;
            //                        this.bot1.CompetitorPanel.Width = 180;
            //                        this.bot1.CompetitorPanel.Visible = false;
            //                        if (cardIndex == 3)
            //                        {
            //                            check = false;
            //                        }
            //                    }
            //                    #endregion
            //                }

            //                if (this.bot2Chips > 0)
            //                {
            //                    this.foldedPlayers--;
            //                    if (cardIndex >= 4 && cardIndex < 6)
            //                    {
            //#region if there are 4 cards ont the table and less than 6
            //                        if (this.cardsPictureBoxes[4].Tag != null)
            //                        {
            //                            this.cardsPictureBoxes[5].Tag = this.throwenCardsTagsCollection[5];
            //                        }

            //                        this.cardsPictureBoxes[4].Tag = this.throwenCardsTagsCollection[4];
            //                        if (!check)
            //                        {
            //                            horizontal = 75;
            //                            vertical = 65;
            //                        }

            //                        check = true;
            //                        this.cardsPictureBoxes[cardIndex].Anchor = ();
            //                        this.cardsPictureBoxes[cardIndex].Image = cardBackImage;
            //                        //Holder[i].Image = cardImagesCollection[i];
            //                        this.cardsPictureBoxes[cardIndex].Location = new Point(horizontal, vertical);
            //                        horizontal += this.cardsPictureBoxes[cardIndex].Width;
            //                        this.cardsPictureBoxes[cardIndex].Visible = true;
            //                        this.Controls.Add(this.bot2Panel);
            //                        this.bot2Panel.Location = new Point(this.cardsPictureBoxes[4].Left - 10, this.cardsPictureBoxes[4].Top - 10);
            //                        this.bot2Panel.BackColor = Color.DarkBlue;
            //                        this.bot2Panel.Height = 150;
            //                        this.bot2Panel.Width = 180;
            //                        this.bot2Panel.Visible = false;
            //                        if (cardIndex == 5)
            //                        {
            //                            check = false;
            //                        }
            //                    }
            //                        #endregion
            //                }

            //                if (this.bot3Chips > 0)
            //                {
            //                    this.foldedPlayers--;
            //                    if (cardIndex >= 6 && cardIndex < 8)
            //                    {
            //#region if there are 6 cards on the table and less than 8
            //                        if (this.cardsPictureBoxes[6].Tag != null)
            //                        {
            //                            this.cardsPictureBoxes[7].Tag = this.throwenCardsTagsCollection[7];
            //                        }

            //                        this.cardsPictureBoxes[6].Tag = this.throwenCardsTagsCollection[6];
            //                        if (!check)
            //                        {
            //                            horizontal = 590;
            //                            vertical = 25;
            //                        }

            //                        check = true;
            //                        this.cardsPictureBoxes[cardIndex].Anchor = ;
            //                        this.cardsPictureBoxes[cardIndex].Image = cardBackImage;
            //                        //Holder[i].Image = cardImagesCollection[i];
            //                        this.cardsPictureBoxes[cardIndex].Location = new Point(horizontal, vertical);
            //                        horizontal += this.cardsPictureBoxes[cardIndex].Width;
            //                        this.cardsPictureBoxes[cardIndex].Visible = true;
            //                        this.Controls.Add(this.bot3Panel);
            //                        this.bot3Panel.Location = new Point(this.cardsPictureBoxes[6].Left - 10, this.cardsPictureBoxes[6].Top - 10);
            //                        this.bot3Panel.BackColor = Color.DarkBlue;
            //                        this.bot3Panel.Height = 150;
            //                        this.bot3Panel.Width = 180;
            //                        this.bot3Panel.Visible = false;
            //                        if (cardIndex == 7)
            //                        {
            //                            check = false;
            //                        }
            //                    }
            //#endregion
            //                }

            //                if (this.bot4Chips > 0)
            //                {
            //                    this.foldedPlayers--;
            //                    if (cardIndex >= 8 && cardIndex < 10)
            //                    {
            //#region if there are 8 cards on the table and less than 10
            //                        if (this.cardsPictureBoxes[8].Tag != null)
            //                        {
            //                            this.cardsPictureBoxes[9].Tag = this.throwenCardsTagsCollection[9];
            //                        }

            //                        this.cardsPictureBoxes[8].Tag = this.throwenCardsTagsCollection[8];
            //                        if (!check)
            //                        {
            //                            horizontal = 1115;
            //                            vertical = 65;
            //                        }

            //                        check = true;
            //                        this.cardsPictureBoxes[cardIndex].Anchor = ;
            //                        this.cardsPictureBoxes[cardIndex].Image = cardBackImage;
            //                        this.cardsPictureBoxes[cardIndex].Location = new Point(horizontal, vertical);
            //                        horizontal += this.cardsPictureBoxes[cardIndex].Width;
            //                        this.cardsPictureBoxes[cardIndex].Visible = true;
            //                        this.Controls.Add(this.bot4Panel);
            //                        this.bot4Panel.Location = new Point(this.cardsPictureBoxes[8].Left - 10, this.cardsPictureBoxes[8].Top - 10);
            //                        this.bot4Panel.BackColor = Color.DarkBlue;
            //                        this.bot4Panel.Height = 150;
            //                        this.bot4Panel.Width = 180;
            //                        this.bot4Panel.Visible = false;
            //                        if (cardIndex == 9)
            //                        {
            //                            check = false;
            //                        }
            //                    }
            //                        #endregion
            //                }

            //                if (this.bot5Chips > 0)
            //                {
            //                    this.foldedPlayers--;
            //                    if (cardIndex >= 10 && cardIndex < 12)
            //                    {
            //#region if there are 10 cards on the table and less than 12
            //                        if (this.cardsPictureBoxes[10].Tag != null)
            //                        {
            //                            this.cardsPictureBoxes[11].Tag = this.throwenCardsTagsCollection[11];
            //                        }

            //                        this.cardsPictureBoxes[10].Tag = this.throwenCardsTagsCollection[10];
            //                        if (!check)
            //                        {
            //                            horizontal = 1160;
            //                            vertical = 420;
            //                        }

            //                        check = true;
            //                        this.cardsPictureBoxes[cardIndex].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            //                        this.cardsPictureBoxes[cardIndex].Image = cardBackImage;
            //                        //Holder[i].Image = cardImagesCollection[i];
            //                        this.cardsPictureBoxes[cardIndex].Location = new Point(horizontal, vertical);
            //                        horizontal += this.cardsPictureBoxes[cardIndex].Width;
            //                        this.cardsPictureBoxes[cardIndex].Visible = true;
            //                        this.Controls.Add(this.bot5Panel);
            //                        this.bot5Panel.Location = new Point(this.cardsPictureBoxes[10].Left - 10, this.cardsPictureBoxes[10].Top - 10);
            //                        this.bot5Panel.BackColor = Color.DarkBlue;
            //                        this.bot5Panel.Height = 150;
            //                        this.bot5Panel.Width = 180;
            //                        this.bot5Panel.Visible = false;
            //                        if (cardIndex == 11)
            //                        {
            //                            check = false;
            //                        }
            //                    }
            //#endregion
            //                }

            //                if (cardIndex >= 12)
            //                {
            //#region if there are 12 cards on the table

            //                    if (!check)
            //                    {

            //                    }

            //                    check = true;
            //                    if (this.cardsPictureBoxes[cardIndex] != null)
            //                    {
            //                        this.cardsPictureBoxes[cardIndex].Anchor = AnchorStyles.None;
            //                        this.cardsPictureBoxes[cardIndex].Image = cardBackImage;
            //                        //Holder[i].Image = cardImagesCollection[i];
            //                        this.cardsPictureBoxes[cardIndex].Location = new Point(horizontal, vertical);
            //                        horizontal += 110;
            //                    }
            //                }
            //#endregion
            //#endregion
            //#region check whether bot 1 has no chips left
            //if (this.bot1.ChipsCount <= 0)
            //{
            //    this.bot1.FoldedTurn = true;
            //    this.cardsPictureBoxes[2].Visible = false;
            //    this.cardsPictureBoxes[3].Visible = false;
            //}
            //else
            //{
            //    this.bot1.FoldedTurn = false;
            //    if (cardIndex == 3)
            //    {
            //        if (this.cardsPictureBoxes[3] != null)
            //        {
            //            this.cardsPictureBoxes[2].Visible = true;
            //            this.cardsPictureBoxes[3].Visible = true;
            //        }
            //    }
            //}
            //#endregion
            //#region check whether bot 2 has no chips left
            //if (this.bot2Chips <= 0)
            //{
            //    this.bot2FoldedTurn = true;
            //    this.cardsPictureBoxes[4].Visible = false;
            //    this.cardsPictureBoxes[5].Visible = false;
            //}
            //else
            //{
            //    this.bot2FoldedTurn = false;
            //    if (cardIndex == 5)
            //    {
            //        if (this.cardsPictureBoxes[5] != null)
            //        {
            //            this.cardsPictureBoxes[4].Visible = true;
            //            this.cardsPictureBoxes[5].Visible = true;
            //        }
            //    }
            //}
            //#endregion
            //#region check whether bot 3 has no chips left
            //if (this.bot3Chips <= 0)
            //{
            //    this.bot3FoldedTurn = true;
            //    this.cardsPictureBoxes[6].Visible = false;
            //    this.cardsPictureBoxes[7].Visible = false;
            //}
            //else
            //{
            //    this.bot3FoldedTurn = false;
            //    if (cardIndex == 7)
            //    {
            //        if (this.cardsPictureBoxes[7] != null)
            //        {
            //            this.cardsPictureBoxes[6].Visible = true;
            //            this.cardsPictureBoxes[7].Visible = true;
            //        }
            //    }
            //}
            //#endregion
            //#region check whether bot 4 has no chips left
            //if (this.bot4Chips <= 0)
            //{
            //    this.bot4FoldedTurn = true;
            //    this.cardsPictureBoxes[8].Visible = false;
            //    this.cardsPictureBoxes[9].Visible = false;
            //}
            //else
            //{
            //    this.bot4FoldedTurn = false;
            //    if (cardIndex == 9)
            //    {
            //        if (this.cardsPictureBoxes[9] != null)
            //        {
            //            this.cardsPictureBoxes[8].Visible = true;
            //            this.cardsPictureBoxes[9].Visible = true;
            //        }
            //    }
            //}
            //#endregion
            //#region check whether bot 5 has no chips left
            //if (this.bot5Chips <= 0)
            //{
            //    this.bot5FoldedTurn = true;
            //    this.cardsPictureBoxes[10].Visible = false;
            //    this.cardsPictureBoxes[11].Visible = false;
            //}
            //else
            //{
            //    this.bot5FoldedTurn = false;
            //    if (cardIndex == 11)
            //    {
            //        if (this.cardsPictureBoxes[11] != null)
            //        {
            //            this.cardsPictureBoxes[10].Visible = true;
            //            this.cardsPictureBoxes[11].Visible = true;
            //        }
            //    }
            //}
            #endregion

            //    if (cardIndex == 16)
            //    {
            //        if (!this.restart)
            //        {
            //          
            //          
            //        }


            //    }
            //}
            this.timer.Start();
            if (this.foldedPlayers == 0)
            {
                DialogResult dialogResult = MessageBox.Show("Would You Like To Play Again ?", "You Won , Congratulations ! ", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                this.foldedPlayers = 5;
            }
                this.MaximizeBox = true;
                this.MinimizeBox = true;
                this.raiseButton.Enabled = true;
                this.callButton.Enabled = true;
                this.raiseButton.Enabled = true;
                this.raiseButton.Enabled = true;
                this.foldButton.Enabled = true;
        }
        
         private async Task ThrowCards()
        {
            int[] horizontalCoordinatesCollection = new int[] {580, 15, 75, 590, 1115, 1160};
            int[] verticalCoordinatesCollection = new int[] {480, 420, 65, 25, 65, 420};
            AnchorStyles[] anchorsCollection =
            {
                (AnchorStyles.Bottom),
                (AnchorStyles.Bottom | AnchorStyles.Left),
                (AnchorStyles.Top | AnchorStyles.Left),
                (AnchorStyles.Top),
                (AnchorStyles.Top | AnchorStyles.Right),
                (AnchorStyles.Bottom | AnchorStyles.Right)
            };
             await Task.Delay(10);
            this.cardsDeck.Shuffle();
            ICompetitor player = new Player();
             int playerCardHorizontalCord = horizontalCoordinatesCollection[0];
             int playerCardVerticalCord = verticalCoordinatesCollection[0];
             for (int j = 0; j < 2; j++)
             {
                ICard playerCard = this.cardsDeck.GetCard();
                playerCard.Anchor = anchorsCollection[0];
                playerCard.Location = new Point(playerCardHorizontalCord, playerCardVerticalCord);
                playerCardHorizontalCord += playerCard.Width;
                this.Controls.Add(playerCard.CardPictureBox);
                playerCard.Reveal();
                player.Hand.Add(playerCard);
                player.PanelLocation = new Point(playerCard.CardPictureBox.Left - 10, playerCard.CardPictureBox.Top - 10);
                this.Controls.Add(player.CompetitorPanel);
                this.foldedPlayers--;
                competitorsCollection[0] = player;
            }
            

            for (int competitor = 1; competitor < this.competitorsCollection.Length; competitor++)
            {
                ICompetitor bot = new Bot();

                int horizontal = horizontalCoordinatesCollection[competitor];
                int vertical = verticalCoordinatesCollection[competitor];
                for (int i = 0; i < CardsPerCompetitor; i++)
                {
                    ICard card = this.cardsDeck.GetCard();
                    card.Anchor = anchorsCollection[competitor];
                    card.Location = new Point(horizontal, vertical);
                    horizontal += card.Width;
                    this.Controls.Add(card.CardPictureBox);
                    bot.Hand.Add(card);
                    bot.PanelLocation = new Point(card.CardPictureBox.Left - 10, card.CardPictureBox.Top - 10);
                }

                this.Controls.Add(bot.CompetitorPanel);
                this.foldedPlayers--;
                competitorsCollection[competitor] = bot;
            }
        }

        async Task Turns()
        {
            #region Rotating
            if (!this.playerFoldedTurn)
            {
                if (this.playerTurn)
                {
                    this.FixCall(this.playerStatus, ref this.playerCall, ref this.playerRaise);
                    //MessageBox.Show("Player's Turn");
                    this.timerProgressBar.Visible = true;
                    this.timerProgressBar.Value = PlayerTurnTimeInSeconds * 1000;
                    this.timer.Start();
                    this.raiseButton.Enabled = true;
                    this.callButton.Enabled = true;
                    this.raiseButton.Enabled = true;
                    this.raiseButton.Enabled = true;
                    this.foldButton.Enabled = true;
                    this.turnCount++;
                }
            }

            if (this.playerFoldedTurn || !this.playerTurn)
            {
                await this.AllIn();
                if (this.playerFoldedTurn && !this.playerFolded)
                {
                    if (!this.callButton.Text.Contains("All in") || !this.raiseButton.Text.Contains("All in"))
                    {
                        this.bools.RemoveAt(0);
                        this.bools.Insert(0, null);
                        this.leftCompetitorsCount--;
                        this.playerFolded = true;
                    }
                }

                await this.CheckRaise(0, 0);
                this.timerProgressBar.Visible = false;
                this.raiseButton.Enabled = false;
                this.callButton.Enabled = false;
                this.raiseButton.Enabled = false;
                this.raiseButton.Enabled = false;
                this.foldButton.Enabled = false;
                this.timer.Stop();
                this.bot1.Onturn = true;
                if (!this.bot1.FoldedTurn)
                {
                    if (this.bot1.Onturn)
                    {
                      //  this.FixCall(this.bot1Status, ref this.bot1.CompetitorCall, ref this.bot1.CompetitorRaise);
                       // this.Rules(2, 3, "Bot 1", ref this.bot1.Type, ref this.bot1.Power, this.bot1.FoldedTurn);
                        MessageBox.Show("Bot 1's Turn");
                       // this.AI(2, 3, ref this.bot1Chips, ref this.bot1Turn, ref this.bot1FoldedTurn, this.bot1Status, 0, this.bot1Power, this.bot1Type);
                        this.turnCount++;
                        this.bot1.Onturn = false;
                        this.bot2Turn = true;
                    }
                }

                if (this.bot1.FoldedTurn && !this.bot1.IsFolded)
                {
                    this.bools.RemoveAt(1);
                    this.bools.Insert(1, null);
                    this.leftCompetitorsCount--;
                    this.bot1.IsFolded = true;
                }

                if (this.bot1.FoldedTurn || !this.bot1.Onturn)
                {
                    await this.CheckRaise(1, 1);
                    this.bot2Turn = true;
                }

                if (!this.bot2FoldedTurn)
                {
                    if (this.bot2Turn)
                    {
                        this.FixCall(this.bot2Status, ref this.bot2Call, ref this.bot2Raise);
                        this.Rules(4, 5, "Bot 2", ref this.bot2Type, ref this.bot2Power, this.bot2FoldedTurn);
                        MessageBox.Show("Bot 2's Turn");
                        this.AI(4, 5, ref this.bot2Chips, ref this.bot2Turn, ref this.bot2FoldedTurn, this.bot2Status, 1, this.bot2Power, this.bot2Type);
                        this.turnCount++;
                        this.bot2Turn = false;
                        this.bot3Turn = true;
                    }
                }

                if (this.bot2FoldedTurn && !this.bot2Folded)
                {
                    this.bools.RemoveAt(2);
                    this.bools.Insert(2, null);
                    this.leftCompetitorsCount--;
                    this.bot2Folded = true;
                }

                if (this.bot2FoldedTurn || !this.bot2Turn)
                {
                    await this.CheckRaise(2, 2);
                    this.bot3Turn = true;
                }

                if (!this.bot3FoldedTurn)
                {
                    if (this.bot3Turn)
                    {
                        this.FixCall(this.bot3Status, ref this.bot3Call, ref this.bot3Raise);
                        this.Rules(6, 7, "Bot 3", ref this.bot3Type, ref this.bot3Power, this.bot3FoldedTurn);
                        MessageBox.Show("Bot 3's Turn");
                        this.AI(6, 7, ref this.bot3Chips, ref this.bot3Turn, ref this.bot3FoldedTurn, this.bot3Status, 2, this.bot3Power, this.bot3Type);
                        this.turnCount++;
                        this.bot3Turn = false;
                        this.bot4Turn = true;
                    }
                }

                if (this.bot3FoldedTurn && !this.bot3Folded)
                {
                    this.bools.RemoveAt(3);
                    this.bools.Insert(3, null);
                    this.leftCompetitorsCount--;
                    this.bot3Folded = true;
                }

                if (this.bot3FoldedTurn || !this.bot3Turn)
                {
                    await this.CheckRaise(3, 3);
                    this.bot4Turn = true;
                }

                if (!this.bot4FoldedTurn)
                {
                    if (this.bot4Turn)
                    {
                        FixCall(bot4Status, ref this.bot4Call, ref this.bot4Raise);
                        Rules(8, 9, "Bot 4", ref this.bot4Type, ref this.bot4Power, this.bot4FoldedTurn);
                        MessageBox.Show("Bot 4's Turn");
                        AI(8, 9, ref bot4Chips, ref this.bot4Turn, ref this.bot4FoldedTurn, bot4Status, 3, this.bot4Power, this.bot4Type);
                        turnCount++;
                        this.bot4Turn = false;
                        this.bot5Turn = true;
                    }
                }

                if (this.bot4FoldedTurn && !this.bot4Folded)
                {
                    bools.RemoveAt(4);
                    bools.Insert(4, null);
                    this.leftCompetitorsCount--;
                    this.bot4Folded = true;
                }

                if (this.bot4FoldedTurn || !this.bot4Turn)
                {
                    await CheckRaise(4, 4);
                    this.bot5Turn = true;
                }

                if (!this.bot5FoldedTurn)
                {
                    if (this.bot5Turn)
                    {
                        FixCall(this.bot5Status, ref this.bot5Call, ref this.bot5Raise);
                        Rules(10, 11, "Bot 5", ref this.bot5Type, ref this.bot5Power, this.bot5FoldedTurn);
                        MessageBox.Show("Bot 5's Turn");
                        AI(10, 11, ref bot5Chips, ref this.bot5Turn, ref this.bot5FoldedTurn, bot5Status, 4, this.bot5Power, this.bot5Type);
                        turnCount++;
                        this.bot5Turn = false;
                    }
                }

                if (this.bot5FoldedTurn && !this.bot5Folded)
                {
                    bools.RemoveAt(5);
                    bools.Insert(5, null);
                    this.leftCompetitorsCount--;
                    this.bot5Folded = true;
                }

                if (this.bot5FoldedTurn || !this.bot5Turn)
                {
                    await CheckRaise(5, 5);
                    this.playerTurn = true;
                }

                if (this.playerFoldedTurn && !this.playerFolded)
                {
                    if (this.callButton.Text.Contains("All in") == false || this.raiseButton.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        this.leftCompetitorsCount--;
                        this.playerFolded = true;
                    }
                }
                #endregion
                await AllIn();
                if (!restart)
                {
                    await Turns();
                }

                restart = false;
            }
        }

        void Rules(int c1, int c2, string currentText, ref double current, ref double Power, bool foldedTurn)
        {
            if (c1 == 0 && c2 == 1)
            {
            }
            if (!foldedTurn || c1 == 0 && c2 == 1 && !this.playerStatus.Text.Contains("Fold"))
            {
                #region Variables
                bool done = false, vf = false;
                int[] Straight1 = new int[5];
                int[] Straight = new int[7];
                Straight[0] = this.throwenCardsTagsCollection[c1];
                Straight[1] = this.throwenCardsTagsCollection[c2];
                Straight1[0] = Straight[2] = this.throwenCardsTagsCollection[12];
                Straight1[1] = Straight[3] = this.throwenCardsTagsCollection[13];
                Straight1[2] = Straight[4] = this.throwenCardsTagsCollection[14];
                Straight1[3] = Straight[5] = this.throwenCardsTagsCollection[15];
                Straight1[4] = Straight[6] = this.throwenCardsTagsCollection[16];
                var a = Straight.Where(o => o % 4 == 0).ToArray();
                var b = Straight.Where(o => o % 4 == 1).ToArray();
                var c = Straight.Where(o => o % 4 == 2).ToArray();
                var d = Straight.Where(o => o % 4 == 3).ToArray();
                var st1 = a.Select(o => o / 4).Distinct().ToArray();
                var st2 = b.Select(o => o / 4).Distinct().ToArray();
                var st3 = c.Select(o => o / 4).Distinct().ToArray();
                var st4 = d.Select(o => o / 4).Distinct().ToArray();
                Array.Sort(Straight); Array.Sort(st1); Array.Sort(st2); Array.Sort(st3); Array.Sort(st4);
                #endregion
                for (i = 0; i < 16; i++)
                {
                    if (this.throwenCardsTagsCollection[i] == int.Parse(this.cardsPictureBoxes[c1].Tag.ToString()) && this.throwenCardsTagsCollection[i + 1] == int.Parse(this.cardsPictureBoxes[c2].Tag.ToString()))
                    {
                        //Pair from Hand current = 1

                        rPairFromHand(ref current, ref Power);

                        #region Pair or Two Pair from Table current = 2 || 0
                        rPairTwoPair(ref current, ref Power);
                        #endregion

                        #region Two Pair current = 2
                        rTwoPair(ref current, ref Power);
                        #endregion

                        #region Three of a kind current = 3
                        rThreeOfAKind(ref current, ref Power, Straight);
                        #endregion

                        #region Straight current = 4
                        rStraight(ref current, ref Power, Straight);
                        #endregion

                        #region Flush current = 5 || 5.5
                        rFlush(ref current, ref Power, ref vf, Straight1);
                        #endregion

                        #region Full House current = 6
                        rFullHouse(ref current, ref Power, ref done, Straight);
                        #endregion

                        #region Four of a Kind current = 7
                        rFourOfAKind(ref current, ref Power, Straight);
                        #endregion

                        #region Straight Flush current = 8 || 9
                        rStraightFlush(ref current, ref Power, st1, st2, st3, st4);
                        #endregion

                        #region High Card current = -1
                        rHighCard(ref current, ref Power);
                        #endregion
                    }
                }
            }
        }
        private void rStraightFlush(ref double current, ref double Power, int[] st1, int[] st2, int[] st3, int[] st4)
        {
            if (current >= -1)
            {
                if (st1.Length >= 5)
                {
                    if (st1[0] + 4 == st1[4])
                    {
                        current = 8;
                        Power = (st1.Max()) / 4 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 8 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st1[0] == 0 && st1[1] == 9 && st1[2] == 10 && st1[3] == 11 && st1[0] + 12 == st1[4])
                    {
                        current = 9;
                        Power = (st1.Max()) / 4 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 9 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st2.Length >= 5)
                {
                    if (st2[0] + 4 == st2[4])
                    {
                        current = 8;
                        Power = (st2.Max()) / 4 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 8 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st2[0] == 0 && st2[1] == 9 && st2[2] == 10 && st2[3] == 11 && st2[0] + 12 == st2[4])
                    {
                        current = 9;
                        Power = (st2.Max()) / 4 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 9 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st3.Length >= 5)
                {
                    if (st3[0] + 4 == st3[4])
                    {
                        current = 8;
                        Power = (st3.Max()) / 4 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 8 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st3[0] == 0 && st3[1] == 9 && st3[2] == 10 && st3[3] == 11 && st3[0] + 12 == st3[4])
                    {
                        current = 9;
                        Power = (st3.Max()) / 4 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 9 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st4.Length >= 5)
                {
                    if (st4[0] + 4 == st4[4])
                    {
                        current = 8;
                        Power = (st4.Max()) / 4 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 8 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st4[0] == 0 && st4[1] == 9 && st4[2] == 10 && st4[3] == 11 && st4[0] + 12 == st4[4])
                    {
                        current = 9;
                        Power = (st4.Max()) / 4 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 9 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }
        private void rFourOfAKind(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (Straight[j] / 4 == Straight[j + 1] / 4 && Straight[j] / 4 == Straight[j + 2] / 4 &&
                        Straight[j] / 4 == Straight[j + 3] / 4)
                    {
                        current = 7;
                        Power = (Straight[j] / 4) * 4 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 7 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0 && Straight[j + 3] / 4 == 0)
                    {
                        current = 7;
                        Power = 13 * 4 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 7 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }
        private void rFullHouse(ref double current, ref double Power, ref bool done, int[] Straight)
        {
            if (current >= -1)
            {
                type = Power;
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                current = 6;
                                Power = 13 * 2 + current * 100;
                                this.win.Add(new Type() { Power = Power, Current = 6 });
                                sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                            if (fh.Max() / 4 > 0)
                            {
                                current = 6;
                                Power = fh.Max() / 4 * 2 + current * 100;
                                this.win.Add(new Type() { Power = Power, Current = 6 });
                                sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                        }
                        if (!done)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                Power = 13;
                                done = true;
                                j = -1;
                            }
                            else
                            {
                                Power = fh.Max() / 4;
                                done = true;
                                j = -1;
                            }
                        }
                    }
                }
                if (current != 6)
                {
                    Power = type;
                }
            }
        }
        private void rFlush(ref double current, ref double Power, ref bool vf, int[] Straight1)
        {
            if (current >= -1)
            {
                var f1 = Straight1.Where(o => o % 4 == 0).ToArray();
                var f2 = Straight1.Where(o => o % 4 == 1).ToArray();
                var f3 = Straight1.Where(o => o % 4 == 2).ToArray();
                var f4 = Straight1.Where(o => o % 4 == 3).ToArray();
                if (f1.Length == 3 || f1.Length == 4)
                {
                    if (this.throwenCardsTagsCollection[i] % 4 == this.throwenCardsTagsCollection[i + 1] % 4 && this.throwenCardsTagsCollection[i] % 4 == f1[0] % 4)
                    {
                        if (this.throwenCardsTagsCollection[i] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = this.throwenCardsTagsCollection[i] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (this.throwenCardsTagsCollection[i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = this.throwenCardsTagsCollection[i + 1] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (this.throwenCardsTagsCollection[i] / 4 < f1.Max() / 4 && this.throwenCardsTagsCollection[i + 1] / 4 < f1.Max() / 4)
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 4)//different cards in hand
                {
                    if (this.throwenCardsTagsCollection[i] % 4 != this.throwenCardsTagsCollection[i + 1] % 4 && this.throwenCardsTagsCollection[i] % 4 == f1[0] % 4)
                    {
                        if (this.throwenCardsTagsCollection[i] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = this.throwenCardsTagsCollection[i] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (this.throwenCardsTagsCollection[i + 1] % 4 != this.throwenCardsTagsCollection[i] % 4 && this.throwenCardsTagsCollection[i + 1] % 4 == f1[0] % 4)
                    {
                        if (this.throwenCardsTagsCollection[i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = this.throwenCardsTagsCollection[i + 1] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f1.Max() + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 5)
                {
                    if (this.throwenCardsTagsCollection[i] % 4 == f1[0] % 4 && this.throwenCardsTagsCollection[i] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        Power = this.throwenCardsTagsCollection[i] + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (this.throwenCardsTagsCollection[i + 1] % 4 == f1[0] % 4 && this.throwenCardsTagsCollection[i + 1] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        Power = this.throwenCardsTagsCollection[i + 1] + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.throwenCardsTagsCollection[i] / 4 < f1.Min() / 4 && this.throwenCardsTagsCollection[i + 1] / 4 < f1.Min())
                    {
                        current = 5;
                        Power = f1.Max() + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f2.Length == 3 || f2.Length == 4)
                {
                    if (this.throwenCardsTagsCollection[i] % 4 == this.throwenCardsTagsCollection[i + 1] % 4 && this.throwenCardsTagsCollection[i] % 4 == f2[0] % 4)
                    {
                        if (this.throwenCardsTagsCollection[i] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = this.throwenCardsTagsCollection[i] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (this.throwenCardsTagsCollection[i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = this.throwenCardsTagsCollection[i + 1] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (this.throwenCardsTagsCollection[i] / 4 < f2.Max() / 4 && this.throwenCardsTagsCollection[i + 1] / 4 < f2.Max() / 4)
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 4)//different cards in hand
                {
                    if (this.throwenCardsTagsCollection[i] % 4 != this.throwenCardsTagsCollection[i + 1] % 4 && this.throwenCardsTagsCollection[i] % 4 == f2[0] % 4)
                    {
                        if (this.throwenCardsTagsCollection[i] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = this.throwenCardsTagsCollection[i] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (this.throwenCardsTagsCollection[i + 1] % 4 != this.throwenCardsTagsCollection[i] % 4 && this.throwenCardsTagsCollection[i + 1] % 4 == f2[0] % 4)
                    {
                        if (this.throwenCardsTagsCollection[i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = this.throwenCardsTagsCollection[i + 1] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f2.Max() + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 5)
                {
                    if (this.throwenCardsTagsCollection[i] % 4 == f2[0] % 4 && this.throwenCardsTagsCollection[i] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        Power = this.throwenCardsTagsCollection[i] + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (this.throwenCardsTagsCollection[i + 1] % 4 == f2[0] % 4 && this.throwenCardsTagsCollection[i + 1] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        Power = this.throwenCardsTagsCollection[i + 1] + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.throwenCardsTagsCollection[i] / 4 < f2.Min() / 4 && this.throwenCardsTagsCollection[i + 1] / 4 < f2.Min())
                    {
                        current = 5;
                        Power = f2.Max() + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f3.Length == 3 || f3.Length == 4)
                {
                    if (this.throwenCardsTagsCollection[i] % 4 == this.throwenCardsTagsCollection[i + 1] % 4 && this.throwenCardsTagsCollection[i] % 4 == f3[0] % 4)
                    {
                        if (this.throwenCardsTagsCollection[i] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = this.throwenCardsTagsCollection[i] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (this.throwenCardsTagsCollection[i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = this.throwenCardsTagsCollection[i + 1] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (this.throwenCardsTagsCollection[i] / 4 < f3.Max() / 4 && this.throwenCardsTagsCollection[i + 1] / 4 < f3.Max() / 4)
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 4)//different cards in hand
                {
                    if (this.throwenCardsTagsCollection[i] % 4 != this.throwenCardsTagsCollection[i + 1] % 4 && this.throwenCardsTagsCollection[i] % 4 == f3[0] % 4)
                    {
                        if (this.throwenCardsTagsCollection[i] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = this.throwenCardsTagsCollection[i] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (this.throwenCardsTagsCollection[i + 1] % 4 != this.throwenCardsTagsCollection[i] % 4 && this.throwenCardsTagsCollection[i + 1] % 4 == f3[0] % 4)
                    {
                        if (this.throwenCardsTagsCollection[i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = this.throwenCardsTagsCollection[i + 1] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f3.Max() + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 5)
                {
                    if (this.throwenCardsTagsCollection[i] % 4 == f3[0] % 4 && this.throwenCardsTagsCollection[i] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        Power = this.throwenCardsTagsCollection[i] + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (this.throwenCardsTagsCollection[i + 1] % 4 == f3[0] % 4 && this.throwenCardsTagsCollection[i + 1] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        Power = this.throwenCardsTagsCollection[i + 1] + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.throwenCardsTagsCollection[i] / 4 < f3.Min() / 4 && this.throwenCardsTagsCollection[i + 1] / 4 < f3.Min())
                    {
                        current = 5;
                        Power = f3.Max() + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f4.Length == 3 || f4.Length == 4)
                {
                    if (this.throwenCardsTagsCollection[i] % 4 == this.throwenCardsTagsCollection[i + 1] % 4 && this.throwenCardsTagsCollection[i] % 4 == f4[0] % 4)
                    {
                        if (this.throwenCardsTagsCollection[i] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = this.throwenCardsTagsCollection[i] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (this.throwenCardsTagsCollection[i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = this.throwenCardsTagsCollection[i + 1] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (this.throwenCardsTagsCollection[i] / 4 < f4.Max() / 4 && this.throwenCardsTagsCollection[i + 1] / 4 < f4.Max() / 4)
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 4)//different cards in hand
                {
                    if (this.throwenCardsTagsCollection[i] % 4 != this.throwenCardsTagsCollection[i + 1] % 4 && this.throwenCardsTagsCollection[i] % 4 == f4[0] % 4)
                    {
                        if (this.throwenCardsTagsCollection[i] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = this.throwenCardsTagsCollection[i] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (this.throwenCardsTagsCollection[i + 1] % 4 != this.throwenCardsTagsCollection[i] % 4 && this.throwenCardsTagsCollection[i + 1] % 4 == f4[0] % 4)
                    {
                        if (this.throwenCardsTagsCollection[i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = this.throwenCardsTagsCollection[i + 1] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            Power = f4.Max() + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 5)
                {
                    if (this.throwenCardsTagsCollection[i] % 4 == f4[0] % 4 && this.throwenCardsTagsCollection[i] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        Power = this.throwenCardsTagsCollection[i] + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (this.throwenCardsTagsCollection[i + 1] % 4 == f4[0] % 4 && this.throwenCardsTagsCollection[i + 1] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        Power = this.throwenCardsTagsCollection[i + 1] + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.throwenCardsTagsCollection[i] / 4 < f4.Min() / 4 && this.throwenCardsTagsCollection[i + 1] / 4 < f4.Min())
                    {
                        current = 5;
                        Power = f4.Max() + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }
                //ace
                if (f1.Length > 0)
                {
                    if (this.throwenCardsTagsCollection[i] / 4 == 0 && this.throwenCardsTagsCollection[i] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (this.throwenCardsTagsCollection[i + 1] / 4 == 0 && this.throwenCardsTagsCollection[i + 1] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f2.Length > 0)
                {
                    if (this.throwenCardsTagsCollection[i] / 4 == 0 && this.throwenCardsTagsCollection[i] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (this.throwenCardsTagsCollection[i + 1] / 4 == 0 && this.throwenCardsTagsCollection[i + 1] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f3.Length > 0)
                {
                    if (this.throwenCardsTagsCollection[i] / 4 == 0 && this.throwenCardsTagsCollection[i] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (this.throwenCardsTagsCollection[i + 1] / 4 == 0 && this.throwenCardsTagsCollection[i + 1] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f4.Length > 0)
                {
                    if (this.throwenCardsTagsCollection[i] / 4 == 0 && this.throwenCardsTagsCollection[i] % 4 == f4[0] % 4 && vf && f4.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (this.throwenCardsTagsCollection[i + 1] / 4 == 0 && this.throwenCardsTagsCollection[i + 1] % 4 == f4[0] % 4 && vf)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }
        private void rStraight(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                var op = Straight.Select(o => o / 4).Distinct().ToArray();
                for (int j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        if (op.Max() - 4 == op[j])
                        {
                            current = 4;
                            Power = op.Max() + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 4 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                        else
                        {
                            current = 4;
                            Power = op[j + 4] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 4 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                    }
                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        current = 4;
                        Power = 13 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 4 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }
        private void rThreeOfAKind(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            current = 3;
                            Power = 13 * 3 + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 3 });
                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 3;
                            Power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 3 });
                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                }
            }
        }
        private void rTwoPair(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    if (this.throwenCardsTagsCollection[i] / 4 != this.throwenCardsTagsCollection[i + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (tc - k < 12)
                            {
                                max--;
                            }
                            if (tc - k >= 12)
                            {
                                if (this.throwenCardsTagsCollection[i] / 4 == this.throwenCardsTagsCollection[tc] / 4 && this.throwenCardsTagsCollection[i + 1] / 4 == this.throwenCardsTagsCollection[tc - k] / 4 ||
                                    this.throwenCardsTagsCollection[i + 1] / 4 == this.throwenCardsTagsCollection[tc] / 4 && this.throwenCardsTagsCollection[i] / 4 == this.throwenCardsTagsCollection[tc - k] / 4)
                                {
                                    if (!msgbox)
                                    {
                                        if (this.throwenCardsTagsCollection[i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (this.throwenCardsTagsCollection[i + 1] / 4) * 2 + current * 100;
                                            this.win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (this.throwenCardsTagsCollection[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (this.throwenCardsTagsCollection[i] / 4) * 2 + current * 100;
                                            this.win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (this.throwenCardsTagsCollection[i + 1] / 4 != 0 && this.throwenCardsTagsCollection[i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (this.throwenCardsTagsCollection[i] / 4) * 2 + (this.throwenCardsTagsCollection[i + 1] / 4) * 2 + current * 100;
                                            this.win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }
                                    msgbox = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void rPairTwoPair(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                bool msgbox1 = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    for (int k = 1; k <= max; k++)
                    {
                        if (tc - k < 12)
                        {
                            max--;
                        }
                        if (tc - k >= 12)
                        {
                            if (this.throwenCardsTagsCollection[tc] / 4 == this.throwenCardsTagsCollection[tc - k] / 4)
                            {
                                if (this.throwenCardsTagsCollection[tc] / 4 != this.throwenCardsTagsCollection[i] / 4 && this.throwenCardsTagsCollection[tc] / 4 != this.throwenCardsTagsCollection[i + 1] / 4 && current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (this.throwenCardsTagsCollection[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (this.throwenCardsTagsCollection[i] / 4) * 2 + 13 * 4 + current * 100;
                                            this.win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (this.throwenCardsTagsCollection[i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (this.throwenCardsTagsCollection[i + 1] / 4) * 2 + 13 * 4 + current * 100;
                                            this.win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (this.throwenCardsTagsCollection[i + 1] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (this.throwenCardsTagsCollection[tc] / 4) * 2 + (this.throwenCardsTagsCollection[i + 1] / 4) * 2 + current * 100;
                                            this.win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (this.throwenCardsTagsCollection[i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (this.throwenCardsTagsCollection[tc] / 4) * 2 + (this.throwenCardsTagsCollection[i] / 4) * 2 + current * 100;
                                            this.win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }
                                    msgbox = true;
                                }
                                if (current == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        if (this.throwenCardsTagsCollection[i] / 4 > this.throwenCardsTagsCollection[i + 1] / 4)
                                        {
                                            if (this.throwenCardsTagsCollection[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + this.throwenCardsTagsCollection[i] / 4 + current * 100;
                                                this.win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = this.throwenCardsTagsCollection[tc] / 4 + this.throwenCardsTagsCollection[i] / 4 + current * 100;
                                                this.win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                        else
                                        {
                                            if (this.throwenCardsTagsCollection[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + this.throwenCardsTagsCollection[i + 1] + current * 100;
                                                this.win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = this.throwenCardsTagsCollection[tc] / 4 + this.throwenCardsTagsCollection[i + 1] / 4 + current * 100;
                                                this.win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                    }
                                    msgbox1 = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void rPairFromHand(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                bool cardsAreWithSameSymbol = this.throwenCardsTagsCollection[i]/4 ==
                                              this.throwenCardsTagsCollection[i + 1]/4;
                if (cardsAreWithSameSymbol)
                {
                    if (!msgbox)
                    {
                        bool cardSymbolIsAce = this.throwenCardsTagsCollection[i] < 4;
                        if (cardSymbolIsAce)
                        {
                            current = 1;
                            Power = 13 * 4 + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 1 });
                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 1;
                            Power = (this.throwenCardsTagsCollection[i + 1] / 4) * 4 + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 1 });
                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                    msgbox = true;
                }
                for (int tc = 16; tc >= 12; tc--)
                {
                     cardsAreWithSameSymbol = this.throwenCardsTagsCollection[i + 1] / 4 == this.throwenCardsTagsCollection[tc] / 4;
                    if (cardsAreWithSameSymbol)
                    {
                        if (!msgbox)
                        {
                            bool cardSymbolIsAce = this.throwenCardsTagsCollection[i + 1] < 4;
                            if (cardSymbolIsAce)
                            {
                                current = 1;
                                Power = 13 * 4 + this.throwenCardsTagsCollection[i] / 4 + current * 100;
                                this.win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (this.throwenCardsTagsCollection[i + 1] / 4) * 4 + this.throwenCardsTagsCollection[i] / 4 + current * 100;
                                this.win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                    cardsAreWithSameSymbol = this.throwenCardsTagsCollection[i]/4 ==
                                             this.throwenCardsTagsCollection[tc]/4;
                    if (cardsAreWithSameSymbol)
                    {
                        if (!msgbox)
                        {
                            bool cardIsAce = this.throwenCardsTagsCollection[i] < 4;
                            if (cardIsAce)
                            {
                                current = 1;
                                Power = 13 * 4 + this.throwenCardsTagsCollection[i + 1] / 4 + current * 100;
                                this.win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (this.throwenCardsTagsCollection[tc] / 4) * 4 + this.throwenCardsTagsCollection[i + 1] / 4 + current * 100;
                                this.win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                }
            }
        }
        private void rHighCard(ref double current, ref double Power)
        {
            if (current == -1)
            {
                if (this.throwenCardsTagsCollection[i] / 4 > this.throwenCardsTagsCollection[i + 1] / 4)
                {
                    current = -1;
                    Power = this.throwenCardsTagsCollection[i] / 4;
                    this.win.Add(new Type() { Power = Power, Current = -1 });
                    sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    current = -1;
                    Power = this.throwenCardsTagsCollection[i + 1] / 4;
                    this.win.Add(new Type() { Power = Power, Current = -1 });
                    sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                if (this.throwenCardsTagsCollection[i] / 4 == 0 || this.throwenCardsTagsCollection[i + 1] / 4 == 0)
                {
                    current = -1;
                    Power = 13;
                    this.win.Add(new Type() { Power = Power, Current = -1 });
                    sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
            }
        }

        void Winner(double current, double Power, string currentText, int chips, string lastly)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }
            for (int j = 0; j <= 16; j++)
            {
                //await Task.Delay(5);
                if (this.cardsPictureBoxes[j].Visible)
                    this.cardsPictureBoxes[j].Image = this.cardImagesCollection[j];
            }
            if (current == sorted.Current)
            {
                if (Power == sorted.Power)
                {
                    winners++;
                    CheckWinners.Add(currentText);
                    if (current == -1)
                    {
                        MessageBox.Show(currentText + " High Card ");
                    }
                    if (current == 1 || current == 0)
                    {
                        MessageBox.Show(currentText + " Pair ");
                    }
                    if (current == 2)
                    {
                        MessageBox.Show(currentText + " Two Pair ");
                    }
                    if (current == 3)
                    {
                        MessageBox.Show(currentText + " Three of a Kind ");
                    }
                    if (current == 4)
                    {
                        MessageBox.Show(currentText + " Straight ");
                    }
                    if (current == 5 || current == 5.5)
                    {
                        MessageBox.Show(currentText + " Flush ");
                    }
                    if (current == 6)
                    {
                        MessageBox.Show(currentText + " Full House ");
                    }
                    if (current == 7)
                    {
                        MessageBox.Show(currentText + " Four of a Kind ");
                    }
                    if (current == 8)
                    {
                        MessageBox.Show(currentText + " Straight Flush ");
                    }
                    if (current == 9)
                    {
                        MessageBox.Show(currentText + " Royal Flush ! ");
                    }
                }
            }
            if (currentText == lastly)// lastfixed
            {
                if (winners > 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        this.playerChips += int.Parse(this.potTextBox.Text) / winners;
                        this.playerChipsTextBox.Text = this.playerChips.ToString();
                        //playerPanel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        bot1.ChipsCount += int.Parse(this.potTextBox.Text) / winners;
                        this.bot1ChipsTextBox.Text = bot1.ChipsCount.ToString();
                        //bot1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        bot2Chips += int.Parse(this.potTextBox.Text) / winners;
                        this.bot2ChipsTextBox.Text = bot2Chips.ToString();
                        //bot2Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        bot3Chips += int.Parse(this.potTextBox.Text) / winners;
                        this.bot3ChipsTextBox.Text = bot3Chips.ToString();
                        //bot3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        bot4Chips += int.Parse(this.potTextBox.Text) / winners;
                        this.bot4ChipsTextBox.Text = bot4Chips.ToString();
                        //bot4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        bot5Chips += int.Parse(this.potTextBox.Text) / winners;
                        this.bot5ChipsTextBox.Text = bot5Chips.ToString();
                        //bot5Panel.Visible = true;
                    }
                    //await Finish(1);
                }
                if (winners == 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        this.playerChips += int.Parse(this.potTextBox.Text);
                        //await Finish(1);
                        //playerPanel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        bot1.ChipsCount += int.Parse(this.potTextBox.Text);
                        //await Finish(1);
                        //bot1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        bot2Chips += int.Parse(this.potTextBox.Text);
                        //await Finish(1);
                        //bot2Panel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        bot3Chips += int.Parse(this.potTextBox.Text);
                        //await Finish(1);
                        //bot3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        bot4Chips += int.Parse(this.potTextBox.Text);
                        //await Finish(1);
                        //bot4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        bot5Chips += int.Parse(this.potTextBox.Text);
                        //await Finish(1);
                        //bot5Panel.Visible = true;
                    }
                }
            }
        }
        async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (raising)
            {
                turnCount = 0;
                raising = false;
                raisedTurn = currentTurn;
                changed = true;
            }
            else
            {
                if (turnCount >= this.leftCompetitorsCount - 1 || !changed && turnCount == this.leftCompetitorsCount)
                {
                    if (currentTurn == raisedTurn - 1 || !changed && turnCount == this.leftCompetitorsCount || raisedTurn == 0 && currentTurn == 5)
                    {
                        changed = false;
                        turnCount = 0;
                        this.raise = 0;
                        call = 0;
                        raisedTurn = 123;
                        rounds++;
                        if (!this.playerFoldedTurn)
                            this.playerStatus.Text = "";
                        if (!this.bot1.FoldedTurn)
                            this.bot1Status.Text = "";
                        if (!this.bot2FoldedTurn)
                            this.bot2Status.Text = "";
                        if (!this.bot3FoldedTurn)
                            this.bot3Status.Text = "";
                        if (!this.bot4FoldedTurn)
                            this.bot4Status.Text = "";
                        if (!this.bot5FoldedTurn)
                            this.bot5Status.Text = "";
                    }
                }
            }
            if (this.rounds == this.flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    if (this.cardsPictureBoxes[j].Image != this.cardImagesCollection[j])
                    {
                        this.cardsPictureBoxes[j].Image = this.cardImagesCollection[j];
                        this.playerCall = 0;
                        this.playerRaise = 0;
                        this.bot1.CompetitorCall = 0;
                        this.bot1.CompetitorRaise = 0;
                        this.bot2Call = 0;
                        this.bot2Raise = 0;
                        this.bot3Call = 0;
                        this.bot3Raise = 0;
                        this.bot4Call = 0;
                        this.bot4Raise = 0;
                        this.bot5Call = 0;
                        this.bot5Raise = 0;
                    }
                }
            }
            if (rounds == this.turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (this.cardsPictureBoxes[j].Image != this.cardImagesCollection[j])
                    {
                        this.cardsPictureBoxes[j].Image = this.cardImagesCollection[j];
                        this.playerCall = 0;
                        this.playerRaise = 0;
                        this.bot1.CompetitorCall = 0;
                        this.bot1.CompetitorRaise = 0;
                        this.bot2Call = 0;
                        this.bot2Raise = 0;
                        this.bot3Call = 0;
                        this.bot3Raise = 0;
                        this.bot4Call = 0;
                        this.bot4Raise = 0;
                        this.bot5Call = 0;
                        this.bot5Raise = 0;
                    }
                }
            }
            if (rounds == this.river)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (this.cardsPictureBoxes[j].Image != this.cardImagesCollection[j])
                    {
                        this.cardsPictureBoxes[j].Image = this.cardImagesCollection[j];
                        this.playerCall = 0;
                        this.playerRaise = 0;
                        this.bot1.CompetitorCall = 0;
                        this.bot1.CompetitorRaise = 0;
                        this.bot2Call = 0;
                        this.bot2Raise = 0;
                        this.bot3Call = 0;
                        this.bot3Raise = 0;
                        this.bot4Call = 0;
                        this.bot4Raise = 0;
                        this.bot5Call = 0;
                        this.bot5Raise = 0;
                    }
                }
            }
            if (rounds == this.end && this.leftCompetitorsCount == 6)
            {
                string fixedLast = "qwerty";
                if (!playerStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, this.playerFoldedTurn);
                }
                if (!bot1Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                   // Rules(2, 3, "Bot 1", ref this.bot1Type, ref this.bot1Power, this.bot1FoldedTurn);
                }
                if (!bot2Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    Rules(4, 5, "Bot 2", ref this.bot2Type, ref this.bot2Power, this.bot2FoldedTurn);
                }
                if (!bot3Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    Rules(6, 7, "Bot 3", ref this.bot3Type, ref this.bot3Power, this.bot3FoldedTurn);
                }
                if (!bot4Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    Rules(8, 9, "Bot 4", ref this.bot4Type, ref this.bot4Power, this.bot4FoldedTurn);
                }
                if (!bot5Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    Rules(10, 11, "Bot 5", ref this.bot5Type, ref this.bot5Power, this.bot5FoldedTurn);
                }
                Winner(this.playerType, this.playerPower, "Player", this.playerChips, fixedLast);
                Winner(this.bot1.Type, this.bot1.Power, "Bot 1", bot1.ChipsCount, fixedLast);
                Winner(this.bot2Type, this.bot2Power, "Bot 2", bot2Chips, fixedLast);
                Winner(this.bot3Type, this.bot3Power, "Bot 3", bot3Chips, fixedLast);
                Winner(this.bot4Type, this.bot4Power, "Bot 4", bot4Chips, fixedLast);
                Winner(this.bot5Type, this.bot5Power, "Bot 5", bot5Chips, fixedLast);
                restart = true;
                this.playerTurn = true;
                this.playerFoldedTurn = false;
                this.bot1.FoldedTurn = false;
                this.bot2FoldedTurn = false;
                this.bot3FoldedTurn = false;
                this.bot4FoldedTurn = false;
                this.bot5FoldedTurn = false;
                if (this.playerChips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        this.playerChips = f2.a;
                        this.bot1.ChipsCount += f2.a;
                        this.bot2Chips += f2.a;
                        this.bot3Chips += f2.a;
                        this.bot4Chips += f2.a;
                        this.bot5Chips += f2.a;
                        this.playerFoldedTurn = false;
                        this.playerTurn = true;
                        this.raiseButton.Enabled = true;
                        this.foldButton.Enabled = true;
                        this.checkButton.Enabled = true;
                        this.raiseButton.Text = "Raise";
                    }
                }
                this.playerPanel.Visible = false;
                this.bot1.CompetitorPanel.Visible = false;
                this.bot2Panel.Visible = false;
                this.bot3Panel.Visible = false;
                this.bot4Panel.Visible = false;
                this.bot5Panel.Visible = false;
                this.playerCall = 0;
                this.playerRaise = 0;
                this.bot1.CompetitorCall = 0;
                this.bot1.CompetitorRaise = 0;
                this.bot2Call = 0;
                this.bot2Raise = 0;
                this.bot3Call = 0;
                this.bot3Raise = 0;
                this.bot4Call = 0;
                this.bot4Raise = 0;
                this.bot5Call = 0;
                this.bot5Raise = 0;
                call = this.bigBlind;
                this.raise = 0;
                bools.Clear();
                rounds = 0;
                this.playerPower = 0;
                this.playerType = -1;
                type = 0;
                this.bot1.Power = 0;
                this.bot2Power = 0;
                this.bot3Power = 0;
                this.bot4Power = 0;
                this.bot5Power = 0;
                this.bot1.Type = -1;
                this.bot2Type = -1;
                this.bot3Type = -1;
                this.bot4Type = -1;
                this.bot5Type = -1;
                ints.Clear();
                CheckWinners.Clear();
                winners = 0;
                this.win.Clear();
                sorted.Current = 0;
                sorted.Power = 0;
                for (int os = 0; os < 17; os++)
                {
                    this.cardsPictureBoxes[os].Image = null;
                    this.cardsPictureBoxes[os].Invalidate();
                    this.cardsPictureBoxes[os].Visible = false;
                }
                this.potTextBox.Text = "0";
                playerStatus.Text = "";
                await PrepareForGame();
                await Turns();
            }
        }
        void FixCall(Label status, ref int competitorCall, ref int competitorRaise)
        {
            if (this.rounds != 4)
            {
                if (status.Text.Contains("Raise"))
                {
                    string changeRaise = status.Text.Substring(6);
                    competitorRaise = int.Parse(changeRaise);
                }

                if (status.Text.Contains("Call"))
                {
                    string changeCall = status.Text.Substring(5);
                    competitorCall = int.Parse(changeCall);
                }

                if (status.Text.Contains("Check"))
                {
                    competitorRaise = 0;
                    competitorCall = 0;
                }

                if (competitorRaise != this.raise && competitorRaise <= this.raise)
                {
                    call = Convert.ToInt32(this.raise) - competitorRaise;
                }

                if (competitorCall != this.call || competitorCall <= call)
                {
                    this.call -= competitorCall;
                }

                if (competitorRaise == this.raise && this.raise > 0)
                {
                    call = 0;
                    this.callButton.Enabled = false;
                    this.callButton.Text = "Callisfuckedup";
                }
            }
        }

        async Task AllIn()
        {
            #region All in
            if (this.playerChips <= 0 && !intsadded)
            {
                if (playerStatus.Text.Contains("Raise"))
                {
                    ints.Add(this.playerChips);
                    intsadded = true;
                }
                if (playerStatus.Text.Contains("Call"))
                {
                    ints.Add(this.playerChips);
                    intsadded = true;
                }
            }
            intsadded = false;
            if (bot1.ChipsCount <= 0 && !this.bot1.FoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(bot1.ChipsCount);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (bot2Chips <= 0 && !this.bot2FoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(bot2Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (bot3Chips <= 0 && !this.bot3FoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(bot3Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (bot4Chips <= 0 && !this.bot4FoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(bot4Chips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (bot5Chips <= 0 && !this.bot5FoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(bot5Chips);
                    intsadded = true;
                }
            }
            if (ints.ToArray().Length == this.leftCompetitorsCount)
            {
                await Finish(2);
            }
            else
            {
                ints.Clear();
            }
            #endregion

            int abc = bools.Count(x => x == false);

            #region LastManStanding
            if (abc == 1)
            {
                int index = bools.IndexOf(false);
                if (index == 0)
                {
                    this.playerChips += int.Parse(this.potTextBox.Text);
                    this.playerChipsTextBox.Text = this.playerChips.ToString();
                    this.playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }
                if (index == 1)
                {
                    bot1.ChipsCount += int.Parse(this.potTextBox.Text);
                    this.playerChipsTextBox.Text = bot1.ChipsCount.ToString();
                    this.bot1.CompetitorPanel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }
                if (index == 2)
                {
                    bot2Chips += int.Parse(this.potTextBox.Text);
                    this.playerChipsTextBox.Text = bot2Chips.ToString();
                    this.bot2Panel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }
                if (index == 3)
                {
                    bot3Chips += int.Parse(this.potTextBox.Text);
                    this.playerChipsTextBox.Text = bot3Chips.ToString();
                    this.bot3Panel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }
                if (index == 4)
                {
                    bot4Chips += int.Parse(this.potTextBox.Text);
                    this.playerChipsTextBox.Text = bot4Chips.ToString();
                    this.bot4Panel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }
                if (index == 5)
                {
                    bot5Chips += int.Parse(this.potTextBox.Text);
                    this.playerChipsTextBox.Text = bot5Chips.ToString();
                    this.bot5Panel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }
                for (int j = 0; j <= 16; j++)
                {
                    this.cardsPictureBoxes[j].Visible = false;
                }
                await Finish(1);
            }
            intsadded = false;
            #endregion

            #region FiveOrLessLeft
            if (abc < 6 && abc > 1 && rounds >= this.end)
            {
                await Finish(2);
            }
            #endregion


        }
        async Task Finish(int n)
        {
            if (n == 2)
            {
                FixWinners();
            }
            this.playerPanel.Visible = false;
            this.bot1.CompetitorPanel.Visible = false;
            this.bot2Panel.Visible = false;
            this.bot3Panel.Visible = false;
            this.bot4Panel.Visible = false;
            this.bot5Panel.Visible = false;
            call = this.bigBlind;
            this.raise = 0;
            foldedPlayers = 5;
            type = 0;
            rounds = 0;
            this.bot1.Power = 0;
            this.bot2Power = 0;
            this.bot3Power = 0;
            this.bot4Power = 0;
            this.bot5Power = 0;
            this.playerPower = 0;
            this.playerType = -1;
            this.raise = 0;
            this.bot1.Type = -1;
            this.bot2Type = -1;
            this.bot3Type = -1;
            this.bot4Type = -1;
            this.bot5Type = -1;
            this.bot1.Onturn = false;
            this.bot2Turn = false;
            this.bot3Turn = false;
            this.bot4Turn = false;
            this.bot5Turn = false;
            this.bot1.FoldedTurn = false;
            this.bot2FoldedTurn = false;
            this.bot3FoldedTurn = false;
            this.bot4FoldedTurn = false;
            this.bot5FoldedTurn = false;
            this.playerFolded = false;
            this.bot1.IsFolded = false;
            this.bot2Folded = false;
            this.bot3Folded = false;
            this.bot4Folded = false;
            this.bot5Folded = false;
            this.playerFoldedTurn = false;
            this.playerTurn = true;
            restart = false;
            raising = false;
            this.playerCall = 0;
            this.bot1.CompetitorCall = 0;
            this.bot2Call = 0;
            this.bot3Call = 0;
            this.bot4Call = 0;
            this.bot5Call = 0;
            this.playerRaise = 0;
            this.bot1.CompetitorRaise = 0;
            this.bot2Raise = 0;
            this.bot3Raise = 0;
            this.bot4Raise = 0;
            this.bot5Raise = 0;
            height = 0;
            width = 0;
            winners = 0;
            this.flop = 1;
            this.turn = 2;
            this.river = 3;
            this.end = 4;
            this.leftCompetitorsCount = 6;
            raisedTurn = 1;
            bools.Clear();
            CheckWinners.Clear();
            ints.Clear();
            this.win.Clear();
            this.sorted.Current = 0;
            this.sorted.Power = 0;
            this.potTextBox.Text = "0";
            this.turnCount = 0;
            this.playerStatus.Text = string.Empty;
            this.bot1Status.Text = string.Empty;
            this.bot2Status.Text = string.Empty;
            this.bot3Status.Text = string.Empty;
            this.bot4Status.Text = string.Empty;
            this.bot5Status.Text = string.Empty;

            if (this.playerChips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {
                    this.playerChips = f2.a;
                    bot1.ChipsCount += f2.a;
                    bot2Chips += f2.a;
                    bot3Chips += f2.a;
                    bot4Chips += f2.a;
                    bot5Chips += f2.a;
                    this.playerFoldedTurn = false;
                    this.playerTurn = true;
                    this.raiseButton.Enabled = true;
                    this.foldButton.Enabled = true;
                    this.checkButton.Enabled = true;
                    this.raiseButton.Text = "Raise";
                }
            }

            for (int os = 0; os < 17; os++)
            {
                this.cardsPictureBoxes[os].Image = null;
                this.cardsPictureBoxes[os].Invalidate();
                this.cardsPictureBoxes[os].Visible = false;
            }
            await this.PrepareForGame();
            //await Turns();
        }
        void FixWinners()
        {
            this.win.Clear();
            sorted.Current = 0;
            sorted.Power = 0;
            string fixedLast = "qwerty";
            if (!playerStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, this.playerFoldedTurn);
            }
            if (!bot1Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
               // Rules(2, 3, "Bot 1", ref this.bot1Type, ref this.bot1Power, this.bot1FoldedTurn);
            }
            if (!bot2Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                Rules(4, 5, "Bot 2", ref this.bot2Type, ref this.bot2Power, this.bot2FoldedTurn);
            }
            if (!bot3Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                Rules(6, 7, "Bot 3", ref this.bot3Type, ref this.bot3Power, this.bot3FoldedTurn);
            }
            if (!bot4Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                Rules(8, 9, "Bot 4", ref this.bot4Type, ref this.bot4Power, this.bot4FoldedTurn);
            }
            if (!bot5Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                Rules(10, 11, "Bot 5", ref this.bot5Type, ref this.bot5Power, this.bot5FoldedTurn);
            }
            Winner(this.playerType, this.playerPower, "Player", this.playerChips, fixedLast);
            Winner(this.bot1.Type, this.bot1.Power, "Bot 1", bot1.ChipsCount, fixedLast);
            Winner(this.bot2Type, this.bot2Power, "Bot 2", bot2Chips, fixedLast);
            Winner(this.bot3Type, this.bot3Power, "Bot 3", bot3Chips, fixedLast);
            Winner(this.bot4Type, this.bot4Power, "Bot 4", bot4Chips, fixedLast);
            Winner(this.bot5Type, this.bot5Power, "Bot 5", bot5Chips, fixedLast);
        }
        void AI(int c1, int c2, ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, double botCurrent)
        {
            if (!sFTurn)
            {
                if (botCurrent == -1)
                {
                    HighCard(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 0)
                {
                    PairTable(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 1)
                {
                    PairHand(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 2)
                {
                    TwoPair(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 3)
                {
                    ThreeOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 4)
                {
                    Straight(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 5 || botCurrent == 5.5)
                {
                    Flush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 6)
                {
                    FullHouse(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 7)
                {
                    FourOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 8 || botCurrent == 9)
                {
                    StraightFlush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
            }
            if (sFTurn)
            {
                this.cardsPictureBoxes[c1].Visible = false;
                this.cardsPictureBoxes[c2].Visible = false;
            }
        }
        private void HighCard(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            HP(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 20, 25);
        }
        private void PairTable(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            HP(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 16, 25);
        }
        private void PairHand(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);
            if (botPower <= 199 && botPower >= 140)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 6, rRaise);
            }
            if (botPower <= 139 && botPower >= 128)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 7, rRaise);
            }
            if (botPower < 128 && botPower >= 101)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 9, rRaise);
            }
        }
        private void TwoPair(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);
            if (botPower <= 290 && botPower >= 246)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 3, rRaise);
            }
            if (botPower <= 244 && botPower >= 234)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 4, rRaise);
            }
            if (botPower < 234 && botPower >= 201)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 4, rRaise);
            }
        }
        private void ThreeOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);
            if (botPower <= 390 && botPower >= 330)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }
            if (botPower <= 327 && botPower >= 321)//10  8
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }
            if (botPower < 321 && botPower >= 303)//7 2
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }
        }
        private void Straight(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);
            if (botPower <= 480 && botPower >= 410)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }
            if (botPower <= 409 && botPower >= 407)//10  8
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }
            if (botPower < 407 && botPower >= 404)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }
        }
        private void Flush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fCall, fRaise);
        }
        private void FullHouse(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);
            if (botPower <= 626 && botPower >= 620)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise);
            }
            if (botPower < 620 && botPower >= 602)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise);
            }
        }
        private void FourOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);
            if (botPower <= 752 && botPower >= 704)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fkCall, fkRaise);
            }
        }
        private void StraightFlush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sfCall, sfRaise);
            }
        }

        private void Fold(ref bool sTurn, ref bool sFTurn, Label sStatus)
        {
            raising = false;
            sStatus.Text = "Fold";
            sTurn = false;
            sFTurn = true;
        }
        private void Check(ref bool cTurn, Label cStatus)
        {
            cStatus.Text = "Check";
            cTurn = false;
            raising = false;
        }
        private void Call(ref int sChips, ref bool sTurn, Label sStatus)
        {
            raising = false;
            sTurn = false;
            sChips -= call;
            sStatus.Text = "Call " + call;
            this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + call).ToString();
        }
        private void Raised(ref int sChips, ref bool sTurn, Label sStatus)
        {
            sChips -= Convert.ToInt32(this.raise);
            sStatus.Text = "Raise " + this.raise;
            this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + Convert.ToInt32(this.raise)).ToString();
            call = Convert.ToInt32(this.raise);
            raising = true;
            sTurn = false;
        }
        private static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }
        private void HP(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, int n, int n1)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
            if (call <= 0)
            {
                Check(ref sTurn, sStatus);
            }
            if (call > 0)
            {
                if (rnd == 1)
                {
                    if (call <= RoundN(sChips, n))
                    {
                        Call(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
                if (rnd == 2)
                {
                    if (call <= RoundN(sChips, n1))
                    {
                        Call(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
            }
            if (rnd == 3)
            {
                if (this.raise == 0)
                {
                    this.raise = call * 2;
                    Raised(ref sChips, ref sTurn, sStatus);
                }
                else
                {
                    if (this.raise <= RoundN(sChips, n))
                    {
                        this.raise = call * 2;
                        Raised(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
            }
            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }
        private void PH(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int n, int n1, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (rounds < 2)
            {
                if (call <= 0)
                {
                    Check(ref sTurn, sStatus);
                }
                if (call > 0)
                {
                    if (call >= RoundN(sChips, n1))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (this.raise > RoundN(sChips, n))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (!sFTurn)
                    {
                        if (call >= RoundN(sChips, n) && call <= RoundN(sChips, n1))
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (this.raise <= RoundN(sChips, n) && this.raise >= (RoundN(sChips, n)) / 2)
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (this.raise <= (RoundN(sChips, n)) / 2)
                        {
                            if (this.raise > 0)
                            {
                                this.raise = RoundN(sChips, n);
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                this.raise = call * 2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }

                    }
                }
            }
            if (rounds >= 2)
            {
                if (call > 0)
                {
                    if (call >= RoundN(sChips, n1 - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (this.raise > RoundN(sChips, n - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (!sFTurn)
                    {
                        if (call >= RoundN(sChips, n - rnd) && call <= RoundN(sChips, n1 - rnd))
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (this.raise <= RoundN(sChips, n - rnd) && this.raise >= (RoundN(sChips, n - rnd)) / 2)
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (this.raise <= (RoundN(sChips, n - rnd)) / 2)
                        {
                            if (this.raise > 0)
                            {
                                this.raise = RoundN(sChips, n - rnd);
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                this.raise = call * 2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }
                    }
                }
                if (call <= 0)
                {
                    this.raise = RoundN(sChips, r - rnd);
                    Raised(ref sChips, ref sTurn, sStatus);
                }
            }
            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }
        void Smooth(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, int name, int n, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (call <= 0)
            {
                Check(ref botTurn, botStatus);
            }
            else
            {
                if (call >= RoundN(botChips, n))
                {
                    if (botChips > call)
                    {
                        Call(ref botChips, ref botTurn, botStatus);
                    }
                    else if (botChips <= call)
                    {
                        raising = false;
                        botTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (this.raise > 0)
                    {
                        if (botChips >= this.raise * 2)
                        {
                            this.raise *= 2;
                            Raised(ref botChips, ref botTurn, botStatus);
                        }
                        else
                        {
                            Call(ref botChips, ref botTurn, botStatus);
                        }
                    }
                    else
                    {
                        this.raise = call * 2;
                        Raised(ref botChips, ref botTurn, botStatus);
                    }
                }
            }
            if (botChips <= 0)
            {
                botFTurn = true;
            }
        }

        #region UI
        private async void timer_Tick(object sender, object e)
        {
            int timeLeft = this.timerProgressBar.Value;
            if (timeLeft <= 0)
            {
                this.playerFoldedTurn = true;
                await Turns();
            }
            else
            {
                // minus one second
                this.timerProgressBar.Value = timeLeft - 1000;
            }
        }
        private void Update_Tick(object sender, object e)
        {
            if (this.playerChips <= 0)
            {
                this.playerChipsTextBox.Text = "Chips : 0";
            }
            if (bot1.ChipsCount <= 0)
            {
                this.bot1ChipsTextBox.Text = "Chips : 0";
            }
            if (bot2Chips <= 0)
            {
                this.bot2ChipsTextBox.Text = "Chips : 0";
            }
            if (bot3Chips <= 0)
            {
                this.bot3ChipsTextBox.Text = "Chips : 0";
            }
            if (bot4Chips <= 0)
            {
                this.bot4ChipsTextBox.Text = "Chips : 0";
            }
            if (bot5Chips <= 0)
            {
                this.bot5ChipsTextBox.Text = "Chips : 0";
            }
            this.playerChipsTextBox.Text = "Chips : " + this.playerChips.ToString();
            this.bot1ChipsTextBox.Text = "Chips : " + bot1.ChipsCount.ToString();
            this.bot2ChipsTextBox.Text = "Chips : " + bot2Chips.ToString();
            this.bot3ChipsTextBox.Text = "Chips : " + bot3Chips.ToString();
            this.bot4ChipsTextBox.Text = "Chips : " + bot4Chips.ToString();
            this.bot5ChipsTextBox.Text = "Chips : " + bot5Chips.ToString();
            if (this.playerChips <= 0)
            {
                this.playerTurn = false;
                this.playerFoldedTurn = true;
                this.callButton.Enabled = false;
                this.raiseButton.Enabled = false;
                this.foldButton.Enabled = false;
                this.checkButton.Enabled = false;
            }

            if (this.playerChips >= call)
            {
                this.callButton.Text = "Call " + call.ToString();
            }
            else
            {
                this.callButton.Text = "All in";
                this.raiseButton.Enabled = false;
            }

            if (call > 0)
            {
                this.checkButton.Enabled = false;
            }

            if (call <= 0)
            {
                this.checkButton.Enabled = true;
                this.callButton.Text = "Call";
                this.callButton.Enabled = false;
            }

            if (this.playerChips <= 0)
            {
                this.raiseButton.Enabled = false;
            }

            int parsedValue;

            if (this.raiseTextBox.Text != "" && int.TryParse(this.raiseTextBox.Text, out parsedValue))
            {
                if (this.playerChips <= int.Parse(this.raiseTextBox.Text))
                {
                    this.raiseButton.Text = "All in";
                }
                else
                {
                    this.raiseButton.Text = "Raise";
                }
            }
            if (this.playerChips < call)
            {
                this.raiseButton.Enabled = false;
            }
        }
        private async void bFold_Click(object sender, EventArgs e)
        {
            playerStatus.Text = "Fold";
            this.playerTurn = false;
            this.playerFoldedTurn = true;
            await Turns();
        }
        private async void bCheck_Click(object sender, EventArgs e)
        {
            if (call <= 0)
            {
                this.playerTurn = false;
                playerStatus.Text = "Check";
            }
            else
            {
                //pStatus.Text = "All in " + Chips;

                this.checkButton.Enabled = false;
            }
            await Turns();
        }
        private async void bCall_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, this.playerFoldedTurn);
            if (this.playerChips >= call)
            {
                this.playerChips -= call;
                this.playerChipsTextBox.Text = "Chips : " + this.playerChips;
                if (this.potTextBox.Text != "")
                {
                    this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + call).ToString();
                }
                else
                {
                    this.potTextBox.Text = call.ToString();
                }
                this.playerTurn = false;
                playerStatus.Text = "Call " + call;
                this.playerCall = call;
            }
            else if (this.playerChips <= call && call > 0)
            {
                this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + this.playerChips).ToString();
                playerStatus.Text = "All in " + this.playerChips;
                this.playerChips = 0;
                this.playerChipsTextBox.Text = "Chips : " + this.playerChips.ToString();
                this.playerTurn = false;
                this.foldButton.Enabled = false;
                this.playerCall = this.playerChips;
            }
            await Turns();
        }
        private async void bRaise_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref this.playerType, ref this.playerPower, this.playerFoldedTurn);
            int parsedValue;
            if (this.raiseTextBox.Text != "" && int.TryParse(this.raiseTextBox.Text, out parsedValue))
            {
                if (this.playerChips > call)
                {
                    if (this.raise * 2 > int.Parse(this.raiseTextBox.Text))
                    {
                        this.raiseTextBox.Text = (this.raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (this.playerChips >= int.Parse(this.raiseTextBox.Text))
                        {
                            call = int.Parse(this.raiseTextBox.Text);
                            this.raise = int.Parse(this.raiseTextBox.Text);
                            playerStatus.Text = "Raise " + call.ToString();
                            this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + call).ToString();
                            this.callButton.Text = "Call";
                            this.playerChips -= int.Parse(this.raiseTextBox.Text);
                            raising = true;
                            this.playerRaise = Convert.ToInt32(this.raise);
                        }
                        else
                        {
                            call = this.playerChips;
                            this.raise = this.playerChips;
                            this.potTextBox.Text = (int.Parse(this.potTextBox.Text) + this.playerChips).ToString();
                            playerStatus.Text = "Raise " + call.ToString();
                            this.playerChips = 0;
                            raising = true;
                            this.playerRaise = Convert.ToInt32(this.raise);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            this.playerTurn = false;
            await Turns();
        }
        private void bAdd_Click(object sender, EventArgs e)
        {
            if (tbAdd.Text == "") { }
            else
            {
                this.playerChips += int.Parse(tbAdd.Text);
                bot1.ChipsCount += int.Parse(tbAdd.Text);
                bot2Chips += int.Parse(tbAdd.Text);
                bot3Chips += int.Parse(tbAdd.Text);
                bot4Chips += int.Parse(tbAdd.Text);
                bot5Chips += int.Parse(tbAdd.Text);
            }
            this.playerChipsTextBox.Text = "Chips : " + this.playerChips.ToString();
        }
        private void bOptions_Click(object sender, EventArgs e)
        {
            this.bigBlindTextBox.Text = this.bigBlind.ToString();
            this.smallBlindTextBox.Text = this.smallBlind.ToString();
            if (this.bigBlindTextBox.Visible == false)
            {
                this.bigBlindTextBox.Visible = true;
                this.smallBlindTextBox.Visible = true;
                this.bigBlindButton.Visible = true;
                this.smallBlindButton.Visible = true;
            }
            else
            {
                this.bigBlindTextBox.Visible = false;
                this.smallBlindTextBox.Visible = false;
                this.bigBlindButton.Visible = false;
                this.smallBlindButton.Visible = false;
            }
        }
        private void bSB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.smallBlindTextBox.Text.Contains(",") || this.smallBlindTextBox.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                this.smallBlindTextBox.Text = this.smallBlind.ToString();
                return;
            }
            if (!int.TryParse(this.smallBlindTextBox.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.smallBlindTextBox.Text = this.smallBlind.ToString();
                return;
            }
            if (int.Parse(this.smallBlindTextBox.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                this.smallBlindTextBox.Text = this.smallBlind.ToString();
            }
            if (int.Parse(this.smallBlindTextBox.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }
            if (int.Parse(this.smallBlindTextBox.Text) >= 250 && int.Parse(this.smallBlindTextBox.Text) <= 100000)
            {
                this.smallBlind = int.Parse(this.smallBlindTextBox.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }
        private void bBB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.bigBlindTextBox.Text.Contains(",") || this.bigBlindTextBox.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                this.bigBlindTextBox.Text = this.bigBlind.ToString();
                return;
            }
            if (!int.TryParse(this.smallBlindTextBox.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.smallBlindTextBox.Text = this.bigBlind.ToString();
                return;
            }
            if (int.Parse(this.bigBlindTextBox.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                this.bigBlindTextBox.Text = this.bigBlind.ToString();
            }
            if (int.Parse(this.bigBlindTextBox.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }
            if (int.Parse(this.bigBlindTextBox.Text) >= 500 && int.Parse(this.bigBlindTextBox.Text) <= 200000)
            {
                this.bigBlind = int.Parse(this.bigBlindTextBox.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }
        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            Width = this.width;
            Height = this.height;
        }
        #endregion
    }
}