﻿using System;
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

namespace Poker
{

    public partial class Form1 : Form
    {
        #region Variables
        ProgressBar asd = new ProgressBar();
        private Panel pPanel = new Panel();
        private Panel b1Panel = new Panel();
        private Panel b2Panel = new Panel();
        private Panel b3Panel = new Panel();
        private Panel b4Panel = new Panel();
        private Panel b5Panel = new Panel();
        private int call = 500;
        private int foldedPlayers = 5;
        private int chips = 10000;
        private int bot1Chips = 10000;
        private int bot2Chips = 10000;
        private int bot3Chips = 10000;
        private int bot4Chips = 10000;
        private int bot5Chips = 10000;
        private double type;
        private int rounds;
        private double b1Power;
        private double b2Power;
        private double b3Power;
        private double b4Power;
        private double b5Power;
        private double pPower;
        private double playerType = -1;
        private double raise;
        private double bot1Type = -1;
        private double bot2Type = -1;
        private double bot3Type = -1;
        private double bot4Type = -1;
        private double bot5Type = -1;
        private bool bot1Turn;
        private bool bot2Turn;
        private bool bot3Turn;
        private bool bot4Turn;
        private bool bot5Turn;
        private bool bot1FoldedTurn;
        private bool bot2FoldedTurn;
        private bool bot3FoldedTurn;
        private bool bot4FoldedTurn;
        private bool bot5FoldedTurn;
        private bool pFolded;
        private bool b1Folded;
        private bool b2Folded;
        private bool b3Folded;
        private bool b4Folded;
        private bool b5Folded;
        private bool intsadded;
        private bool changed;
        private int pCall;
        private int b1Call;
        private int b2Call;
        private int b3Call;
        private int b4Call;
        private int b5Call;
        private int pRaise;
        private int b1Raise;
        private int b2Raise;
        private int b3Raise;
        private int b4Raise;
        private int b5Raise;
        private int height;
        private int width;
        private int winners;
        private int flop = 1;
        private int turn = 2;
        private int river = 3;
        private int end = 4;
        private int maxLeft = 6;
        private int last = 123;
        private int raisedTurn = 1;
        List<bool?> bools = new List<bool?>();
        List<Type> win = new List<Type>();
        List<string> CheckWinners = new List<string>();
        List<int> ints = new List<int>();
        private bool playerFoldedTurn;
        private bool playerTurn = true;
        private bool restart;
        private bool raising;
        Poker.Type sorted;
        string[] ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
        /*string[] ImgLocation ={
                   "Assets\\Cards\\33.png","Assets\\Cards\\22.png",
                    "Assets\\Cards\\29.png","Assets\\Cards\\21.png",
                    "Assets\\Cards\\36.png","Assets\\Cards\\17.png",
                    "Assets\\Cards\\40.png","Assets\\Cards\\16.png",
                    "Assets\\Cards\\5.png","Assets\\Cards\\47.png",
                    "Assets\\Cards\\37.png","Assets\\Cards\\13.png",
                    
                    "Assets\\Cards\\12.png",
                    "Assets\\Cards\\8.png","Assets\\Cards\\18.png",
                    "Assets\\Cards\\15.png","Assets\\Cards\\27.png"};*/
        int[] Reserve = new int[17];
        Image[] Deck = new Image[52];
        private PictureBox[] holder = new PictureBox[52];
        private Timer timer = new Timer();
        private Timer updates = new Timer();
        private int t = 60;
        private int i;
        private int bb = 500;
        private int sb = 250;
        private int up = 10000000;
        private int turnCount = 0;
        #endregion
        public Form1()
        {
            //bools.Add(playerFoldedTurn); bools.Add(B1Fturn); bools.Add(bot2FoldedTurn); bools.Add(bot3FoldedTurn); bools.Add(bot4FoldedTurn); bools.Add(bot5FoldedTurn);
            this.call = this.bb;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.updates.Start();
            this.InitializeComponent();
            this.Width = this.width;
            this.Height = this.height;
            this.Shuffle();
            this.tbPot.Enabled = false;
            this.tbChips.Enabled = false;
            this.tbBotChips1.Enabled = false;
            this.tbBotChips2.Enabled = false;
            this.tbBotChips3.Enabled = false;
            this.tbBotChips4.Enabled = false;
            this.tbBotChips5.Enabled = false;
            this.tbChips.Text = "Chips : " + this.chips;
            this.tbBotChips1.Text = "Chips : " + this.bot1Chips;
            this.tbBotChips2.Text = "Chips : " + this.bot2Chips;
            this.tbBotChips3.Text = "Chips : " + this.bot3Chips;
            this.tbBotChips4.Text = "Chips : " + this.bot4Chips;
            this.tbBotChips5.Text = "Chips : " + this.bot5Chips;
            this.timer.Interval = (1 * 1 * 1000);
            this.timer.Tick += this.timer_Tick;
            this.updates.Interval = (1 * 1 * 100);
            this.updates.Tick += this.Update_Tick;
            this.tbBB.Visible = true;
            this.tbSB.Visible = true;
            this.bBB.Visible = true;
            this.bSB.Visible = true;
            this.tbBB.Visible = true;
            this.tbSB.Visible = true;
            this.bBB.Visible = true;
            this.bSB.Visible = true;
            this.tbBB.Visible = false;
            this.tbSB.Visible = false;
            this.bBB.Visible = false;
            this.bSB.Visible = false;
            this.tbRaise.Text = (this.bb * 2).ToString();
        }

        async Task Shuffle()
        {
            this.bools.Add(this.playerFoldedTurn);
            this.bools.Add(this.bot1FoldedTurn);
            this.bools.Add(this.bot2FoldedTurn);
            this.bools.Add(this.bot3FoldedTurn);
            this.bools.Add(this.bot4FoldedTurn);
            this.bools.Add(this.bot5FoldedTurn);
            this.bCall.Enabled = false;
            this.bRaise.Enabled = false;
            this.bFold.Enabled = false;
            this.bCheck.Enabled = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            bool check = false;
            Bitmap backImage = new Bitmap("Assets\\Back\\Back.png");
            int horizontal = 580, vertical = 480;
            Random r = new Random();
            for (this.i = this.ImgLocation.Length; this.i > 0; this.i--)
            {
                int j = r.Next(this.i);
                var k = this.ImgLocation[j];
                this.ImgLocation[j] = this.ImgLocation[this.i - 1];
                this.ImgLocation[this.i - 1] = k;
            }

            for (i = 0; i < 17; i++)
            {

                this.Deck[this.i] = Image.FromFile(this.ImgLocation[i]);
                var charsToRemove = new string[] { "Assets\\Cards\\", ".png" };
                foreach (string c in charsToRemove)
                {
                    this.ImgLocation[i] = this.ImgLocation[i].Replace(c, string.Empty);
                }

                this.Reserve[i] = int.Parse(this.ImgLocation[i]) - 1;// TODO: Lookhere
                this.holder[i] = new PictureBox();
                this.holder[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.holder[i].Height = 130;
                this.holder[i].Width = 80;
                this.Controls.Add(this.holder[i]);
                this.holder[i].Name = "pb" + i;
                await Task.Delay(200);
                #region Throwing Cards
                if (i < 2)
                {
                    if (this.holder[0].Tag != null)
                    {
                        this.holder[1].Tag = this.Reserve[1];
                    }

                    this.holder[0].Tag = this.Reserve[0];
                    this.holder[i].Image = this.Deck[i];
                    this.holder[i].Anchor = (AnchorStyles.Bottom);
                    //Holder[i].Dock = DockStyle.Top;
                    this.holder[i].Location = new Point(horizontal, vertical);
                    horizontal += this.holder[i].Width;
                    this.Controls.Add(this.pPanel);
                    this.pPanel.Location = new Point(this.holder[0].Left - 10, this.holder[0].Top - 10);
                    this.pPanel.BackColor = Color.DarkBlue;
                    this.pPanel.Height = 150;
                    this.pPanel.Width = 180;
                    this.pPanel.Visible = false;
                }

                if (this.bot1Chips > 0)
                {
                    this.foldedPlayers--;
                    if (i >= 2 && i < 4)
                    {
                        if (this.holder[2].Tag != null)
                        {
                            this.holder[3].Tag = this.Reserve[3];
                        }

                        this.holder[2].Tag = this.Reserve[2];
                        if (!check)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }

                        check = true;
                        this.holder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                        this.holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        this.holder[i].Location = new Point(horizontal, vertical);
                        horizontal += this.holder[i].Width;
                        this.holder[i].Visible = true;
                        this.Controls.Add(this.b1Panel);
                        this.b1Panel.Location = new Point(this.holder[2].Left - 10, this.holder[2].Top - 10);
                        this.b1Panel.BackColor = Color.DarkBlue;
                        this.b1Panel.Height = 150;
                        this.b1Panel.Width = 180;
                        this.b1Panel.Visible = false;
                        if (i == 3)
                        {
                            check = false;
                        }
                    }
                }

                if (this.bot2Chips > 0)
                {
                    this.foldedPlayers--;
                    if (i >= 4 && i < 6)
                    {
                        if (this.holder[4].Tag != null)
                        {
                            this.holder[5].Tag = this.Reserve[5];
                        }

                        this.holder[4].Tag = this.Reserve[4];
                        if (!check)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }

                        check = true;
                        this.holder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        this.holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        this.holder[i].Location = new Point(horizontal, vertical);
                        horizontal += this.holder[i].Width;
                        this.holder[i].Visible = true;
                        this.Controls.Add(this.b2Panel);
                        this.b2Panel.Location = new Point(this.holder[4].Left - 10, this.holder[4].Top - 10);
                        this.b2Panel.BackColor = Color.DarkBlue;
                        this.b2Panel.Height = 150;
                        this.b2Panel.Width = 180;
                        this.b2Panel.Visible = false;
                        if (i == 5)
                        {
                            check = false;
                        }
                    }
                }

                if (this.bot3Chips > 0)
                {
                    this.foldedPlayers--;
                    if (i >= 6 && i < 8)
                    {
                        if (this.holder[6].Tag != null)
                        {
                            this.holder[7].Tag = this.Reserve[7];
                        }

                        this.holder[6].Tag = this.Reserve[6];
                        if (!check)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }

                        check = true;
                        this.holder[i].Anchor = (AnchorStyles.Top);
                        this.holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        this.holder[i].Location = new Point(horizontal, vertical);
                        horizontal += this.holder[i].Width;
                        this.holder[i].Visible = true;
                        this.Controls.Add(this.b3Panel);
                        this.b3Panel.Location = new Point(this.holder[6].Left - 10, this.holder[6].Top - 10);
                        this.b3Panel.BackColor = Color.DarkBlue;
                        this.b3Panel.Height = 150;
                        this.b3Panel.Width = 180;
                        this.b3Panel.Visible = false;
                        if (i == 7)
                        {
                            check = false;
                        }
                    }
                }

                if (this.bot4Chips > 0)
                {
                    this.foldedPlayers--;
                    if (i >= 8 && i < 10)
                    {
                        if (this.holder[8].Tag != null)
                        {
                            this.holder[9].Tag = this.Reserve[9];
                        }

                        this.holder[8].Tag = this.Reserve[8];
                        if (!check)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }

                        check = true;
                        this.holder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        this.holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        this.holder[i].Location = new Point(horizontal, vertical);
                        horizontal += this.holder[i].Width;
                        this.holder[i].Visible = true;
                        this.Controls.Add(b4Panel);
                        this.b4Panel.Location = new Point(this.holder[8].Left - 10, this.holder[8].Top - 10);
                        this.b4Panel.BackColor = Color.DarkBlue;
                        this.b4Panel.Height = 150;
                        this.b4Panel.Width = 180;
                        this.b4Panel.Visible = false;
                        if (i == 9)
                        {
                            check = false;
                        }
                    }
                }

                if (this.bot5Chips > 0)
                {
                    this.foldedPlayers--;
                    if (i >= 10 && i < 12)
                    {
                        if (this.holder[10].Tag != null)
                        {
                            this.holder[11].Tag = Reserve[11];
                        }

                        this.holder[10].Tag = this.Reserve[10];
                        if (!check)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }

                        check = true;
                        this.holder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                        this.holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        this.holder[i].Location = new Point(horizontal, vertical);
                        horizontal += this.holder[i].Width;
                        this.holder[i].Visible = true;
                        this.Controls.Add(this.b5Panel);
                        this.b5Panel.Location = new Point(this.holder[10].Left - 10, this.holder[10].Top - 10);
                        this.b5Panel.BackColor = Color.DarkBlue;
                        this.b5Panel.Height = 150;
                        this.b5Panel.Width = 180;
                        this.b5Panel.Visible = false;
                        if (i == 11)
                        {
                            check = false;
                        }
                    }
                }

                if (i >= 12)
                {
                    this.holder[12].Tag = this.Reserve[12];
                    if (i > 12) this.holder[13].Tag = this.Reserve[13];
                    if (i > 13) this.holder[14].Tag = this.Reserve[14];
                    if (i > 14) this.holder[15].Tag = this.Reserve[15];
                    if (i > 15)
                    {
                        this.holder[16].Tag = this.Reserve[16];

                    }

                    if (!check)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }

                    check = true;
                    if (this.holder[i] != null)
                    {
                        this.holder[i].Anchor = AnchorStyles.None;
                        this.holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        this.holder[i].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }
                #endregion
                if (this.bot1Chips <= 0)
                {
                    this.bot1FoldedTurn = true;
                    this.holder[2].Visible = false;
                    this.holder[3].Visible = false;
                }
                else
                {
                    this.bot1FoldedTurn = false;
                    if (i == 3)
                    {
                        if (this.holder[3] != null)
                        {
                            this.holder[2].Visible = true;
                            this.holder[3].Visible = true;
                        }
                    }
                }

                if (this.bot2Chips <= 0)
                {
                    this.bot2FoldedTurn = true;
                    this.holder[4].Visible = false;
                    this.holder[5].Visible = false;
                }
                else
                {
                    this.bot2FoldedTurn = false;
                    if (i == 5)
                    {
                        if (this.holder[5] != null)
                        {
                            this.holder[4].Visible = true;
                            this.holder[5].Visible = true;
                        }
                    }
                }
                if (this.bot3Chips <= 0)
                {
                    this.bot3FoldedTurn = true;
                    this.holder[6].Visible = false;
                    this.holder[7].Visible = false;
                }
                else
                {
                    this.bot3FoldedTurn = false;
                    if (i == 7)
                    {
                        if (this.holder[7] != null)
                        {
                            this.holder[6].Visible = true;
                            this.holder[7].Visible = true;
                        }
                    }
                }
                if (this.bot4Chips <= 0)
                {
                    this.bot4FoldedTurn = true;
                    this.holder[8].Visible = false;
                    this.holder[9].Visible = false;
                }
                else
                {
                    this.bot4FoldedTurn = false;
                    if (i == 9)
                    {
                        if (this.holder[9] != null)
                        {
                            this.holder[8].Visible = true;
                            this.holder[9].Visible = true;
                        }
                    }
                }

                if (this.bot5Chips <= 0)
                {
                    this.bot5FoldedTurn = true;
                    this.holder[10].Visible = false;
                    this.holder[11].Visible = false;
                }
                else
                {
                    this.bot5FoldedTurn = false;
                    if (i == 11)
                    {
                        if (this.holder[11] != null)
                        {
                            this.holder[10].Visible = true;
                            this.holder[11].Visible = true;
                        }
                    }
                }
                if (i == 16)
                {
                    if (!this.restart)
                    {
                       this.MaximizeBox = true;
                       this.MinimizeBox = true;
                    }
                    this.timer.Start();
                }
            }
            if (this.foldedPlayers == 5)
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
                foldedPlayers = 5;
            }
            if (i == 17)
            {
                this.bRaise.Enabled = true;
                this.bCall.Enabled = true;
                this.bRaise.Enabled = true;
                this.bRaise.Enabled = true;
                this.bFold.Enabled = true;
            }
        }

        async Task Turns()
        {
            #region Rotating
            if (!this.playerFoldedTurn)
            {
                if (this.playerTurn)
                {
                    this.FixCall(this.playerStatus, ref this.pCall, ref this.pRaise, 1);
                    //MessageBox.Show("Player's Turn");
                    this.pbTimer.Visible = true;
                    this.pbTimer.Value = 1000;
                    this.t = 60;
                    this.up = 10000000;
                    this.timer.Start();
                    this.bRaise.Enabled = true;
                    this.bCall.Enabled = true;
                    this.bRaise.Enabled = true;
                    this.bRaise.Enabled = true;
                    this.bFold.Enabled = true;
                    this.turnCount++;
                    this.FixCall(this.playerStatus, ref this.pCall, ref this.pRaise, 2);
                }
            }

            if (this.playerFoldedTurn || !this.playerTurn)
            {
                await this.AllIn();
                if (this.playerFoldedTurn && !this.pFolded)
                {
                    if (this.bCall.Text.Contains("All in") == false || this.bRaise.Text.Contains("All in") == false)
                    {
                        this.bools.RemoveAt(0);
                        this.bools.Insert(0, null);
                        this.maxLeft--;
                        this.pFolded = true;
                    }
                }
                await this.CheckRaise(0, 0);
                this.pbTimer.Visible = false;
                this.bRaise.Enabled = false;
                this.bCall.Enabled = false;
                this.bRaise.Enabled = false;
                this.bRaise.Enabled = false;
                this.bFold.Enabled = false;
                this.timer.Stop();
                this.bot1Turn = true;
                if (!this.bot1FoldedTurn)
                {
                    if (this.bot1Turn)
                    {
                        this.FixCall(this.bot1Status, ref this.b1Call, ref this.b1Raise, 1);
                        this.FixCall(this.bot1Status, ref this.b1Call, ref this.b1Raise, 2);
                        this.Rules(2, 3, "Bot 1", ref this.bot1Type, ref this.b1Power, this.bot1FoldedTurn);
                        MessageBox.Show("Bot 1's Turn");
                        this.AI(2, 3, ref this.bot1Chips, ref this.bot1Turn, ref this.bot1FoldedTurn, this.bot1Status, 0, this.b1Power, this.bot1Type);
                        this.turnCount++;
                        this.last = 1;
                        this.bot1Turn = false;
                        this.bot2Turn = true;
                    }
                }
                if (this.bot1FoldedTurn && !this.b1Folded)
                {
                    this.bools.RemoveAt(1);
                    this.bools.Insert(1, null);
                    this.maxLeft--;
                    this.b1Folded = true;
                }
                if (this.bot1FoldedTurn || !this.bot1Turn)
                {
                    await this.CheckRaise(1, 1);
                    this.bot2Turn = true;
                }
                if (!this.bot2FoldedTurn)
                {
                    if (this.bot2Turn)
                    {
                        this.FixCall(this.bot2Status, ref this.b2Call, ref this.b2Raise, 1);
                        this.FixCall(this.bot2Status, ref this.b2Call, ref this.b2Raise, 2);
                        this.Rules(4, 5, "Bot 2", ref this.bot2Type, ref this.b2Power, this.bot2FoldedTurn);
                        MessageBox.Show("Bot 2's Turn");
                        this.AI(4, 5, ref this.bot2Chips, ref this.bot2Turn, ref this.bot2FoldedTurn, this.bot2Status, 1, this.b2Power, this.bot2Type);
                        this.turnCount++;
                        this.last = 2;
                        this.bot2Turn = false;
                        this.bot3Turn = true;
                    }
                }
                if (this.bot2FoldedTurn && !this.b2Folded)
                {
                    this.bools.RemoveAt(2);
                    this.bools.Insert(2, null);
                    this.maxLeft--;
                    this.b2Folded = true;
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
                        this.FixCall(this.bot3Status, ref this.b3Call, ref this.b3Raise, 1);
                        this.FixCall(this.bot3Status, ref this.b3Call, ref this.b3Raise, 2);
                        this.Rules(6, 7, "Bot 3", ref this.bot3Type, ref this.b3Power, this.bot3FoldedTurn);
                        MessageBox.Show("Bot 3's Turn");
                        this.AI(6, 7, ref this.bot3Chips, ref this.bot3Turn, ref this.bot3FoldedTurn, this.bot3Status, 2, this.b3Power, this.bot3Type);
                        this.turnCount++;
                        this.last = 3;
                        this.bot3Turn = false;
                        this.bot4Turn = true;
                    }
                }
                if (this.bot3FoldedTurn && !this.b3Folded)
                {
                    this.bools.RemoveAt(3);
                    this.bools.Insert(3, null);
                    this.maxLeft--;
                    this.b3Folded = true;
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
                        FixCall(bot4Status, ref b4Call, ref b4Raise, 1);
                        FixCall(bot4Status, ref b4Call, ref b4Raise, 2);
                        Rules(8, 9, "Bot 4", ref this.bot4Type, ref b4Power, this.bot4FoldedTurn);
                        MessageBox.Show("Bot 4's Turn");
                        AI(8, 9, ref bot4Chips, ref this.bot4Turn, ref this.bot4FoldedTurn, bot4Status, 3, b4Power, this.bot4Type);
                        turnCount++;
                        last = 4;
                        this.bot4Turn = false;
                        this.bot5Turn = true;
                    }
                }
                if (this.bot4FoldedTurn && !b4Folded)
                {
                    bools.RemoveAt(4);
                    bools.Insert(4, null);
                    maxLeft--;
                    b4Folded = true;
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
                        FixCall(this.bot5Status, ref b5Call, ref b5Raise, 1);
                        FixCall(this.bot5Status, ref b5Call, ref b5Raise, 2);
                        Rules(10, 11, "Bot 5", ref this.bot5Type, ref b5Power, this.bot5FoldedTurn);
                        MessageBox.Show("Bot 5's Turn");
                        AI(10, 11, ref bot5Chips, ref this.bot5Turn, ref this.bot5FoldedTurn, bot5Status, 4, b5Power, this.bot5Type);
                        turnCount++;
                        last = 5;
                        this.bot5Turn = false;
                    }
                }
                if (this.bot5FoldedTurn && !b5Folded)
                {
                    bools.RemoveAt(5);
                    bools.Insert(5, null);
                    maxLeft--;
                    b5Folded = true;
                }
                if (this.bot5FoldedTurn || !this.bot5Turn)
                {
                    await CheckRaise(5, 5);
                    this.playerTurn = true;
                }
                if (this.playerFoldedTurn && !pFolded)
                {
                    if (bCall.Text.Contains("All in") == false || bRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
                        pFolded = true;
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
                Straight[0] = Reserve[c1];
                Straight[1] = Reserve[c2];
                Straight1[0] = Straight[2] = Reserve[12];
                Straight1[1] = Straight[3] = Reserve[13];
                Straight1[2] = Straight[4] = Reserve[14];
                Straight1[3] = Straight[5] = Reserve[15];
                Straight1[4] = Straight[6] = Reserve[16];
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
                    if (Reserve[i] == int.Parse(this.holder[c1].Tag.ToString()) && Reserve[i + 1] == int.Parse(this.holder[c2].Tag.ToString()))
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
                    if (Reserve[i] % 4 == Reserve[i + 1] % 4 && Reserve[i] % 4 == f1[0] % 4)
                    {
                        if (Reserve[i] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (Reserve[i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i + 1] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (Reserve[i] / 4 < f1.Max() / 4 && Reserve[i + 1] / 4 < f1.Max() / 4)
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
                    if (Reserve[i] % 4 != Reserve[i + 1] % 4 && Reserve[i] % 4 == f1[0] % 4)
                    {
                        if (Reserve[i] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i] + current * 100;
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
                    if (Reserve[i + 1] % 4 != Reserve[i] % 4 && Reserve[i + 1] % 4 == f1[0] % 4)
                    {
                        if (Reserve[i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i + 1] + current * 100;
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
                    if (Reserve[i] % 4 == f1[0] % 4 && Reserve[i] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        Power = Reserve[i] + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (Reserve[i + 1] % 4 == f1[0] % 4 && Reserve[i + 1] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        Power = Reserve[i + 1] + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (Reserve[i] / 4 < f1.Min() / 4 && Reserve[i + 1] / 4 < f1.Min())
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
                    if (Reserve[i] % 4 == Reserve[i + 1] % 4 && Reserve[i] % 4 == f2[0] % 4)
                    {
                        if (Reserve[i] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (Reserve[i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i + 1] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (Reserve[i] / 4 < f2.Max() / 4 && Reserve[i + 1] / 4 < f2.Max() / 4)
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
                    if (Reserve[i] % 4 != Reserve[i + 1] % 4 && Reserve[i] % 4 == f2[0] % 4)
                    {
                        if (Reserve[i] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i] + current * 100;
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
                    if (Reserve[i + 1] % 4 != Reserve[i] % 4 && Reserve[i + 1] % 4 == f2[0] % 4)
                    {
                        if (Reserve[i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i + 1] + current * 100;
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
                    if (Reserve[i] % 4 == f2[0] % 4 && Reserve[i] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        Power = Reserve[i] + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (Reserve[i + 1] % 4 == f2[0] % 4 && Reserve[i + 1] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        Power = Reserve[i + 1] + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (Reserve[i] / 4 < f2.Min() / 4 && Reserve[i + 1] / 4 < f2.Min())
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
                    if (Reserve[i] % 4 == Reserve[i + 1] % 4 && Reserve[i] % 4 == f3[0] % 4)
                    {
                        if (Reserve[i] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (Reserve[i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i + 1] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (Reserve[i] / 4 < f3.Max() / 4 && Reserve[i + 1] / 4 < f3.Max() / 4)
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
                    if (Reserve[i] % 4 != Reserve[i + 1] % 4 && Reserve[i] % 4 == f3[0] % 4)
                    {
                        if (Reserve[i] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i] + current * 100;
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
                    if (Reserve[i + 1] % 4 != Reserve[i] % 4 && Reserve[i + 1] % 4 == f3[0] % 4)
                    {
                        if (Reserve[i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i + 1] + current * 100;
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
                    if (Reserve[i] % 4 == f3[0] % 4 && Reserve[i] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        Power = Reserve[i] + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (Reserve[i + 1] % 4 == f3[0] % 4 && Reserve[i + 1] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        Power = Reserve[i + 1] + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (Reserve[i] / 4 < f3.Min() / 4 && Reserve[i + 1] / 4 < f3.Min())
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
                    if (Reserve[i] % 4 == Reserve[i + 1] % 4 && Reserve[i] % 4 == f4[0] % 4)
                    {
                        if (Reserve[i] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (Reserve[i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i + 1] + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 5 });
                            sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (Reserve[i] / 4 < f4.Max() / 4 && Reserve[i + 1] / 4 < f4.Max() / 4)
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
                    if (Reserve[i] % 4 != Reserve[i + 1] % 4 && Reserve[i] % 4 == f4[0] % 4)
                    {
                        if (Reserve[i] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i] + current * 100;
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
                    if (Reserve[i + 1] % 4 != Reserve[i] % 4 && Reserve[i + 1] % 4 == f4[0] % 4)
                    {
                        if (Reserve[i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            Power = Reserve[i + 1] + current * 100;
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
                    if (Reserve[i] % 4 == f4[0] % 4 && Reserve[i] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        Power = Reserve[i] + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (Reserve[i + 1] % 4 == f4[0] % 4 && Reserve[i + 1] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        Power = Reserve[i + 1] + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (Reserve[i] / 4 < f4.Min() / 4 && Reserve[i + 1] / 4 < f4.Min())
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
                    if (Reserve[i] / 4 == 0 && Reserve[i] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Reserve[i + 1] / 4 == 0 && Reserve[i + 1] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f2.Length > 0)
                {
                    if (Reserve[i] / 4 == 0 && Reserve[i] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Reserve[i + 1] / 4 == 0 && Reserve[i + 1] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f3.Length > 0)
                {
                    if (Reserve[i] / 4 == 0 && Reserve[i] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Reserve[i + 1] / 4 == 0 && Reserve[i + 1] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f4.Length > 0)
                {
                    if (Reserve[i] / 4 == 0 && Reserve[i] % 4 == f4[0] % 4 && vf && f4.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.win.Add(new Type() { Power = Power, Current = 5.5 });
                        sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (Reserve[i + 1] / 4 == 0 && Reserve[i + 1] % 4 == f4[0] % 4 && vf)
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
                    if (Reserve[i] / 4 != Reserve[i + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (tc - k < 12)
                            {
                                max--;
                            }
                            if (tc - k >= 12)
                            {
                                if (Reserve[i] / 4 == Reserve[tc] / 4 && Reserve[i + 1] / 4 == Reserve[tc - k] / 4 ||
                                    Reserve[i + 1] / 4 == Reserve[tc] / 4 && Reserve[i] / 4 == Reserve[tc - k] / 4)
                                {
                                    if (!msgbox)
                                    {
                                        if (Reserve[i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (Reserve[i + 1] / 4) * 2 + current * 100;
                                            this.win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (Reserve[i] / 4) * 2 + current * 100;
                                            this.win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i + 1] / 4 != 0 && Reserve[i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (Reserve[i] / 4) * 2 + (Reserve[i + 1] / 4) * 2 + current * 100;
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
                            if (Reserve[tc] / 4 == Reserve[tc - k] / 4)
                            {
                                if (Reserve[tc] / 4 != Reserve[i] / 4 && Reserve[tc] / 4 != Reserve[i + 1] / 4 && current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (Reserve[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (Reserve[i] / 4) * 2 + 13 * 4 + current * 100;
                                            this.win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (Reserve[i + 1] / 4) * 2 + 13 * 4 + current * 100;
                                            this.win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i + 1] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (Reserve[tc] / 4) * 2 + (Reserve[i + 1] / 4) * 2 + current * 100;
                                            this.win.Add(new Type() { Power = Power, Current = 2 });
                                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (Reserve[i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (Reserve[tc] / 4) * 2 + (Reserve[i] / 4) * 2 + current * 100;
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
                                        if (Reserve[i] / 4 > Reserve[i + 1] / 4)
                                        {
                                            if (Reserve[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + Reserve[i] / 4 + current * 100;
                                                this.win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = Reserve[tc] / 4 + Reserve[i] / 4 + current * 100;
                                                this.win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                        else
                                        {
                                            if (Reserve[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + Reserve[i + 1] + current * 100;
                                                this.win.Add(new Type() { Power = Power, Current = 1 });
                                                sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = Reserve[tc] / 4 + Reserve[i + 1] / 4 + current * 100;
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
                if (Reserve[i] / 4 == Reserve[i + 1] / 4)
                {
                    if (!msgbox)
                    {
                        if (Reserve[i] / 4 == 0)
                        {
                            current = 1;
                            Power = 13 * 4 + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 1 });
                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 1;
                            Power = (Reserve[i + 1] / 4) * 4 + current * 100;
                            this.win.Add(new Type() { Power = Power, Current = 1 });
                            sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                    msgbox = true;
                }
                for (int tc = 16; tc >= 12; tc--)
                {
                    if (Reserve[i + 1] / 4 == Reserve[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (Reserve[i + 1] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + Reserve[i] / 4 + current * 100;
                                this.win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (Reserve[i + 1] / 4) * 4 + Reserve[i] / 4 + current * 100;
                                this.win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                    if (Reserve[i] / 4 == Reserve[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (Reserve[i] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + Reserve[i + 1] / 4 + current * 100;
                                this.win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = this.win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (Reserve[tc] / 4) * 4 + Reserve[i + 1] / 4 + current * 100;
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
                if (Reserve[i] / 4 > Reserve[i + 1] / 4)
                {
                    current = -1;
                    Power = Reserve[i] / 4;
                    this.win.Add(new Type() { Power = Power, Current = -1 });
                    sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    current = -1;
                    Power = Reserve[i + 1] / 4;
                    this.win.Add(new Type() { Power = Power, Current = -1 });
                    sorted = this.win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                if (Reserve[i] / 4 == 0 || Reserve[i + 1] / 4 == 0)
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
                if (this.holder[j].Visible)
                    this.holder[j].Image = Deck[j];
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
            if (currentText == lastly)//lastfixed
            {
                if (winners > 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        this.chips += int.Parse(tbPot.Text) / winners;
                        tbChips.Text = this.chips.ToString();
                        //pPanel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        bot1Chips += int.Parse(tbPot.Text) / winners;
                        tbBotChips1.Text = bot1Chips.ToString();
                        //b1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        bot2Chips += int.Parse(tbPot.Text) / winners;
                        tbBotChips2.Text = bot2Chips.ToString();
                        //b2Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        bot3Chips += int.Parse(tbPot.Text) / winners;
                        tbBotChips3.Text = bot3Chips.ToString();
                        //b3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        bot4Chips += int.Parse(tbPot.Text) / winners;
                        tbBotChips4.Text = bot4Chips.ToString();
                        //b4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        bot5Chips += int.Parse(tbPot.Text) / winners;
                        tbBotChips5.Text = bot5Chips.ToString();
                        //b5Panel.Visible = true;
                    }
                    //await Finish(1);
                }
                if (winners == 1)
                {
                    if (CheckWinners.Contains("Player"))
                    {
                        this.chips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //pPanel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 1"))
                    {
                        bot1Chips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //b1Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 2"))
                    {
                        bot2Chips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //b2Panel.Visible = true;

                    }
                    if (CheckWinners.Contains("Bot 3"))
                    {
                        bot3Chips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //b3Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 4"))
                    {
                        bot4Chips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //b4Panel.Visible = true;
                    }
                    if (CheckWinners.Contains("Bot 5"))
                    {
                        bot5Chips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //b5Panel.Visible = true;
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
                if (turnCount >= maxLeft - 1 || !changed && turnCount == maxLeft)
                {
                    if (currentTurn == raisedTurn - 1 || !changed && turnCount == maxLeft || raisedTurn == 0 && currentTurn == 5)
                    {
                        changed = false;
                        turnCount = 0;
                        this.raise = 0;
                        call = 0;
                        raisedTurn = 123;
                        rounds++;
                        if (!this.playerFoldedTurn)
                            this.playerStatus.Text = "";
                        if (!this.bot1FoldedTurn)
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
                    if (this.holder[j].Image != Deck[j])
                    {
                        this.holder[j].Image = Deck[j];
                        pCall = 0; pRaise = 0;
                        b1Call = 0; b1Raise = 0;
                        b2Call = 0; b2Raise = 0;
                        b3Call = 0; b3Raise = 0;
                        b4Call = 0; b4Raise = 0;
                        b5Call = 0; b5Raise = 0;
                    }
                }
            }
            if (rounds == this.turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (this.holder[j].Image != Deck[j])
                    {
                        this.holder[j].Image = Deck[j];
                        pCall = 0; pRaise = 0;
                        b1Call = 0; b1Raise = 0;
                        b2Call = 0; b2Raise = 0;
                        b3Call = 0; b3Raise = 0;
                        b4Call = 0; b4Raise = 0;
                        b5Call = 0; b5Raise = 0;
                    }
                }
            }
            if (rounds == this.river)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (this.holder[j].Image != Deck[j])
                    {
                        this.holder[j].Image = Deck[j];
                        pCall = 0; pRaise = 0;
                        b1Call = 0; b1Raise = 0;
                        b2Call = 0; b2Raise = 0;
                        b3Call = 0; b3Raise = 0;
                        b4Call = 0; b4Raise = 0;
                        b5Call = 0; b5Raise = 0;
                    }
                }
            }
            if (rounds == this.end && maxLeft == 6)
            {
                string fixedLast = "qwerty";
                if (!playerStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    Rules(0, 1, "Player", ref this.playerType, ref pPower, this.playerFoldedTurn);
                }
                if (!bot1Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    Rules(2, 3, "Bot 1", ref this.bot1Type, ref b1Power, this.bot1FoldedTurn);
                }
                if (!bot2Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    Rules(4, 5, "Bot 2", ref this.bot2Type, ref b2Power, this.bot2FoldedTurn);
                }
                if (!bot3Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    Rules(6, 7, "Bot 3", ref this.bot3Type, ref b3Power, this.bot3FoldedTurn);
                }
                if (!bot4Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    Rules(8, 9, "Bot 4", ref this.bot4Type, ref b4Power, this.bot4FoldedTurn);
                }
                if (!bot5Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    Rules(10, 11, "Bot 5", ref this.bot5Type, ref b5Power, this.bot5FoldedTurn);
                }
                Winner(this.playerType, pPower, "Player", this.chips, fixedLast);
                Winner(this.bot1Type, b1Power, "Bot 1", bot1Chips, fixedLast);
                Winner(this.bot2Type, b2Power, "Bot 2", bot2Chips, fixedLast);
                Winner(this.bot3Type, b3Power, "Bot 3", bot3Chips, fixedLast);
                Winner(this.bot4Type, b4Power, "Bot 4", bot4Chips, fixedLast);
                Winner(this.bot5Type, b5Power, "Bot 5", bot5Chips, fixedLast);
                restart = true;
                this.playerTurn = true;
                this.playerFoldedTurn = false;
                this.bot1FoldedTurn = false;
                this.bot2FoldedTurn = false;
                this.bot3FoldedTurn = false;
                this.bot4FoldedTurn = false;
                this.bot5FoldedTurn = false;
                if (this.chips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        this.chips = f2.a;
                        bot1Chips += f2.a;
                        bot2Chips += f2.a;
                        bot3Chips += f2.a;
                        bot4Chips += f2.a;
                        bot5Chips += f2.a;
                        this.playerFoldedTurn = false;
                        this.playerTurn = true;
                        bRaise.Enabled = true;
                        bFold.Enabled = true;
                        bCheck.Enabled = true;
                        bRaise.Text = "Raise";
                    }
                }
                pPanel.Visible = false; b1Panel.Visible = false; b2Panel.Visible = false; b3Panel.Visible = false; b4Panel.Visible = false; b5Panel.Visible = false;
                pCall = 0; pRaise = 0;
                b1Call = 0; b1Raise = 0;
                b2Call = 0; b2Raise = 0;
                b3Call = 0; b3Raise = 0;
                b4Call = 0; b4Raise = 0;
                b5Call = 0; b5Raise = 0;
                last = 0;
                call = bb;
                this.raise = 0;
                ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                bools.Clear();
                rounds = 0;
                pPower = 0; this.playerType = -1;
                type = 0; b1Power = 0; b2Power = 0; b3Power = 0; b4Power = 0; b5Power = 0;
                this.bot1Type = -1; this.bot2Type = -1; this.bot3Type = -1; this.bot4Type = -1; this.bot5Type = -1;
                ints.Clear();
                CheckWinners.Clear();
                winners = 0;
                this.win.Clear();
                sorted.Current = 0;
                sorted.Power = 0;
                for (int os = 0; os < 17; os++)
                {
                    this.holder[os].Image = null;
                    this.holder[os].Invalidate();
                    this.holder[os].Visible = false;
                }
                tbPot.Text = "0";
                playerStatus.Text = "";
                await Shuffle();
                await Turns();
            }
        }
        void FixCall(Label status, ref int cCall, ref int cRaise, int options)
        {
            if (rounds != 4)
            {
                if (options == 1)
                {
                    if (status.Text.Contains("Raise"))
                    {
                        var changeRaise = status.Text.Substring(6);
                        cRaise = int.Parse(changeRaise);
                    }
                    if (status.Text.Contains("Call"))
                    {
                        var changeCall = status.Text.Substring(5);
                        cCall = int.Parse(changeCall);
                    }
                    if (status.Text.Contains("Check"))
                    {
                        cRaise = 0;
                        cCall = 0;
                    }
                }
                if (options == 2)
                {
                    if (cRaise != this.raise && cRaise <= this.raise)
                    {
                        call = Convert.ToInt32(this.raise) - cRaise;
                    }
                    if (cCall != call || cCall <= call)
                    {
                        call = call - cCall;
                    }
                    if (cRaise == this.raise && this.raise > 0)
                    {
                        call = 0;
                        bCall.Enabled = false;
                        bCall.Text = "Callisfuckedup";
                    }
                }
            }
        }
        async Task AllIn()
        {
            #region All in
            if (this.chips <= 0 && !intsadded)
            {
                if (playerStatus.Text.Contains("Raise"))
                {
                    ints.Add(this.chips);
                    intsadded = true;
                }
                if (playerStatus.Text.Contains("Call"))
                {
                    ints.Add(this.chips);
                    intsadded = true;
                }
            }
            intsadded = false;
            if (bot1Chips <= 0 && !this.bot1FoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(bot1Chips);
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
            if (ints.ToArray().Length == maxLeft)
            {
                await Finish(2);
            }
            else
            {
                ints.Clear();
            }
            #endregion

            var abc = bools.Count(x => x == false);

            #region LastManStanding
            if (abc == 1)
            {
                int index = bools.IndexOf(false);
                if (index == 0)
                {
                    this.chips += int.Parse(tbPot.Text);
                    tbChips.Text = this.chips.ToString();
                    pPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }
                if (index == 1)
                {
                    bot1Chips += int.Parse(tbPot.Text);
                    tbChips.Text = bot1Chips.ToString();
                    b1Panel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }
                if (index == 2)
                {
                    bot2Chips += int.Parse(tbPot.Text);
                    tbChips.Text = bot2Chips.ToString();
                    b2Panel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }
                if (index == 3)
                {
                    bot3Chips += int.Parse(tbPot.Text);
                    tbChips.Text = bot3Chips.ToString();
                    b3Panel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }
                if (index == 4)
                {
                    bot4Chips += int.Parse(tbPot.Text);
                    tbChips.Text = bot4Chips.ToString();
                    b4Panel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }
                if (index == 5)
                {
                    bot5Chips += int.Parse(tbPot.Text);
                    tbChips.Text = bot5Chips.ToString();
                    b5Panel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }
                for (int j = 0; j <= 16; j++)
                {
                    this.holder[j].Visible = false;
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
            pPanel.Visible = false;
            b1Panel.Visible = false;
            b2Panel.Visible = false;
            b3Panel.Visible = false;
            b4Panel.Visible = false;
            b5Panel.Visible = false;
            call = bb;
            this.raise = 0;
            foldedPlayers = 5;
            type = 0;
            rounds = 0;
            b1Power = 0;
            b2Power = 0;
            b3Power = 0;
            b4Power = 0;
            b5Power = 0;
            pPower = 0;
            this.playerType = -1;
            this.raise = 0;
            this.bot1Type = -1;
            this.bot2Type = -1;
            this.bot3Type = -1;
            this.bot4Type = -1;
            this.bot5Type = -1;
            this.bot1Turn = false;
            this.bot2Turn = false;
            this.bot3Turn = false;
            this.bot4Turn = false;
            this.bot5Turn = false;
            this.bot1FoldedTurn = false;
            this.bot2FoldedTurn = false;
            this.bot3FoldedTurn = false;
            this.bot4FoldedTurn = false;
            this.bot5FoldedTurn = false;
            pFolded = false;
            b1Folded = false;
            b2Folded = false;
            b3Folded = false;
            b4Folded = false;
            b5Folded = false;
            this.playerFoldedTurn = false;
            this.playerTurn = true;
            restart = false;
            raising = false;
            pCall = 0;
            b1Call = 0;
            b2Call = 0;
            b3Call = 0;
            b4Call = 0;
            b5Call = 0;
            pRaise = 0;
            b1Raise = 0;
            b2Raise = 0;
            b3Raise = 0;
            b4Raise = 0;
            b5Raise = 0;
            height = 0;
            width = 0;
            winners = 0;
            this.flop = 1;
            this.turn = 2;
            this.river = 3;
            this.end = 4;
            maxLeft = 6;
            last = 123;
            raisedTurn = 1;
            bools.Clear();
            CheckWinners.Clear();
            ints.Clear();
            this.win.Clear();
            sorted.Current = 0;
            sorted.Power = 0;
            tbPot.Text = "0";
            t = 60;
            up = 10000000;
            turnCount = 0;
            playerStatus.Text = "";
            bot1Status.Text = "";
            bot2Status.Text = "";
            bot3Status.Text = "";
            bot4Status.Text = "";
            bot5Status.Text = "";

            if (this.chips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {
                    this.chips = f2.a;
                    bot1Chips += f2.a;
                    bot2Chips += f2.a;
                    bot3Chips += f2.a;
                    bot4Chips += f2.a;
                    bot5Chips += f2.a;
                    this.playerFoldedTurn = false;
                    this.playerTurn = true;
                    bRaise.Enabled = true;
                    bFold.Enabled = true;
                    bCheck.Enabled = true;
                    bRaise.Text = "Raise";
                }
            }
            ImgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (int os = 0; os < 17; os++)
            {
                this.holder[os].Image = null;
                this.holder[os].Invalidate();
                this.holder[os].Visible = false;
            }
            await this.Shuffle();
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
                Rules(0, 1, "Player", ref this.playerType, ref pPower, this.playerFoldedTurn);
            }
            if (!bot1Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                Rules(2, 3, "Bot 1", ref this.bot1Type, ref b1Power, this.bot1FoldedTurn);
            }
            if (!bot2Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                Rules(4, 5, "Bot 2", ref this.bot2Type, ref b2Power, this.bot2FoldedTurn);
            }
            if (!bot3Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                Rules(6, 7, "Bot 3", ref this.bot3Type, ref b3Power, this.bot3FoldedTurn);
            }
            if (!bot4Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                Rules(8, 9, "Bot 4", ref this.bot4Type, ref b4Power, this.bot4FoldedTurn);
            }
            if (!bot5Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                Rules(10, 11, "Bot 5", ref this.bot5Type, ref b5Power, this.bot5FoldedTurn);
            }
            Winner(this.playerType, pPower, "Player", this.chips, fixedLast);
            Winner(this.bot1Type, b1Power, "Bot 1", bot1Chips, fixedLast);
            Winner(this.bot2Type, b2Power, "Bot 2", bot2Chips, fixedLast);
            Winner(this.bot3Type, b3Power, "Bot 3", bot3Chips, fixedLast);
            Winner(this.bot4Type, b4Power, "Bot 4", bot4Chips, fixedLast);
            Winner(this.bot5Type, b5Power, "Bot 5", bot5Chips, fixedLast);
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
                this.holder[c1].Visible = false;
                this.holder[c2].Visible = false;
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
            tbPot.Text = (int.Parse(tbPot.Text) + call).ToString();
        }
        private void Raised(ref int sChips, ref bool sTurn, Label sStatus)
        {
            sChips -= Convert.ToInt32(this.raise);
            sStatus.Text = "Raise " + this.raise;
            tbPot.Text = (int.Parse(tbPot.Text) + Convert.ToInt32(this.raise)).ToString();
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
                        tbPot.Text = (int.Parse(tbPot.Text) + botChips).ToString();
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
            if (pbTimer.Value <= 0)
            {
                this.playerFoldedTurn = true;
                await Turns();
            }
            if (t > 0)
            {
                t--;
                pbTimer.Value = (t / 6) * 100;
            }
        }
        private void Update_Tick(object sender, object e)
        {
            if (this.chips <= 0)
            {
                tbChips.Text = "Chips : 0";
            }
            if (bot1Chips <= 0)
            {
                tbBotChips1.Text = "Chips : 0";
            }
            if (bot2Chips <= 0)
            {
                tbBotChips2.Text = "Chips : 0";
            }
            if (bot3Chips <= 0)
            {
                tbBotChips3.Text = "Chips : 0";
            }
            if (bot4Chips <= 0)
            {
                tbBotChips4.Text = "Chips : 0";
            }
            if (bot5Chips <= 0)
            {
                tbBotChips5.Text = "Chips : 0";
            }
            tbChips.Text = "Chips : " + this.chips.ToString();
            tbBotChips1.Text = "Chips : " + bot1Chips.ToString();
            tbBotChips2.Text = "Chips : " + bot2Chips.ToString();
            tbBotChips3.Text = "Chips : " + bot3Chips.ToString();
            tbBotChips4.Text = "Chips : " + bot4Chips.ToString();
            tbBotChips5.Text = "Chips : " + bot5Chips.ToString();
            if (this.chips <= 0)
            {
                this.playerTurn = false;
                this.playerFoldedTurn = true;
                bCall.Enabled = false;
                bRaise.Enabled = false;
                bFold.Enabled = false;
                bCheck.Enabled = false;
            }
            if (up > 0)
            {
                up--;
            }
            if (this.chips >= call)
            {
                bCall.Text = "Call " + call.ToString();
            }
            else
            {
                bCall.Text = "All in";
                bRaise.Enabled = false;
            }
            if (call > 0)
            {
                bCheck.Enabled = false;
            }
            if (call <= 0)
            {
                bCheck.Enabled = true;
                bCall.Text = "Call";
                bCall.Enabled = false;
            }
            if (this.chips <= 0)
            {
                bRaise.Enabled = false;
            }
            int parsedValue;

            if (tbRaise.Text != "" && int.TryParse(tbRaise.Text, out parsedValue))
            {
                if (this.chips <= int.Parse(tbRaise.Text))
                {
                    bRaise.Text = "All in";
                }
                else
                {
                    bRaise.Text = "Raise";
                }
            }
            if (this.chips < call)
            {
                bRaise.Enabled = false;
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

                bCheck.Enabled = false;
            }
            await Turns();
        }
        private async void bCall_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref this.playerType, ref pPower, this.playerFoldedTurn);
            if (this.chips >= call)
            {
                this.chips -= call;
                tbChips.Text = "Chips : " + this.chips.ToString();
                if (tbPot.Text != "")
                {
                    tbPot.Text = (int.Parse(tbPot.Text) + call).ToString();
                }
                else
                {
                    tbPot.Text = call.ToString();
                }
                this.playerTurn = false;
                playerStatus.Text = "Call " + call;
                pCall = call;
            }
            else if (this.chips <= call && call > 0)
            {
                tbPot.Text = (int.Parse(tbPot.Text) + this.chips).ToString();
                playerStatus.Text = "All in " + this.chips;
                this.chips = 0;
                tbChips.Text = "Chips : " + this.chips.ToString();
                this.playerTurn = false;
                bFold.Enabled = false;
                pCall = this.chips;
            }
            await Turns();
        }
        private async void bRaise_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref this.playerType, ref pPower, this.playerFoldedTurn);
            int parsedValue;
            if (tbRaise.Text != "" && int.TryParse(tbRaise.Text, out parsedValue))
            {
                if (this.chips > call)
                {
                    if (this.raise * 2 > int.Parse(tbRaise.Text))
                    {
                        tbRaise.Text = (this.raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (this.chips >= int.Parse(tbRaise.Text))
                        {
                            call = int.Parse(tbRaise.Text);
                            this.raise = int.Parse(tbRaise.Text);
                            playerStatus.Text = "Raise " + call.ToString();
                            tbPot.Text = (int.Parse(tbPot.Text) + call).ToString();
                            bCall.Text = "Call";
                            this.chips -= int.Parse(tbRaise.Text);
                            raising = true;
                            last = 0;
                            pRaise = Convert.ToInt32(this.raise);
                        }
                        else
                        {
                            call = this.chips;
                            this.raise = this.chips;
                            tbPot.Text = (int.Parse(tbPot.Text) + this.chips).ToString();
                            playerStatus.Text = "Raise " + call.ToString();
                            this.chips = 0;
                            raising = true;
                            last = 0;
                            pRaise = Convert.ToInt32(this.raise);
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
                this.chips += int.Parse(tbAdd.Text);
                bot1Chips += int.Parse(tbAdd.Text);
                bot2Chips += int.Parse(tbAdd.Text);
                bot3Chips += int.Parse(tbAdd.Text);
                bot4Chips += int.Parse(tbAdd.Text);
                bot5Chips += int.Parse(tbAdd.Text);
            }
            tbChips.Text = "Chips : " + this.chips.ToString();
        }
        private void bOptions_Click(object sender, EventArgs e)
        {
            tbBB.Text = bb.ToString();
            tbSB.Text = sb.ToString();
            if (tbBB.Visible == false)
            {
                tbBB.Visible = true;
                tbSB.Visible = true;
                bBB.Visible = true;
                bSB.Visible = true;
            }
            else
            {
                tbBB.Visible = false;
                tbSB.Visible = false;
                bBB.Visible = false;
                bSB.Visible = false;
            }
        }
        private void bSB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (tbSB.Text.Contains(",") || tbSB.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                tbSB.Text = sb.ToString();
                return;
            }
            if (!int.TryParse(tbSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                tbSB.Text = sb.ToString();
                return;
            }
            if (int.Parse(tbSB.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                tbSB.Text = sb.ToString();
            }
            if (int.Parse(tbSB.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }
            if (int.Parse(tbSB.Text) >= 250 && int.Parse(tbSB.Text) <= 100000)
            {
                sb = int.Parse(tbSB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }
        private void bBB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (tbBB.Text.Contains(",") || tbBB.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                tbBB.Text = bb.ToString();
                return;
            }
            if (!int.TryParse(tbSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                tbSB.Text = bb.ToString();
                return;
            }
            if (int.Parse(tbBB.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                tbBB.Text = bb.ToString();
            }
            if (int.Parse(tbBB.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }
            if (int.Parse(tbBB.Text) >= 500 && int.Parse(tbBB.Text) <= 200000)
            {
                bb = int.Parse(tbBB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }
        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            width = this.width;
            height = this.height;
        }
        #endregion
    }
}