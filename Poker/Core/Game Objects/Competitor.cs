namespace Poker.Core.Game_Objects
{
    using Poker.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public class Competitor : ICompetitor
    {
        #region Fields

        private const int DefaultChipsCount = 10000;
        private const int DefaultPanelWidth = 180;
        private const int DefaultPanelHeight = 150;
        private const bool DefaultPanelVisibility = false;
        private static readonly Color DefaultPanelBackColor = Color.DarkBlue;

        protected ICollection<ICard> hand;
        protected Panel competitorPanel;
        protected int chipsCount;
        protected double power;
        protected double type;
        protected bool onTurn;
        protected bool foldedTurn;
        protected bool isFolded;
        protected int call;
        protected int raise;
        #endregion
        protected Competitor()
            : this(
                  new Panel()
                  {
                      Width = DefaultPanelWidth,
                      Height = DefaultPanelHeight,
                      Visible = DefaultPanelVisibility,
                      BackColor = DefaultPanelBackColor
                  },
                  DefaultChipsCount, 0D, 0D, false, false, false, 0, 0, new List<ICard>())
        {

        }

        protected Competitor(Panel competitorPanel)
            : this(
                competitorPanel,
                DefaultChipsCount,
                0D,
                0D,
                false,
                false,
                false,
                0,
                0,
                new List<ICard>())
        {
            
        }

        protected Competitor(
            Panel competitorPanel,
            int chipsCount,
            double power,
            double type,
            bool onTurn,
            bool foldedTurn,
            bool isFolded,
            int competitorCall,
            int competitorRaise,
            ICollection<ICard> hand)
        {
            this.CompetitorPanel = competitorPanel;
            this.ChipsCount = chipsCount;
            this.Power = power;
            this.Type = type;
            this.Onturn = onTurn;
            this.FoldedTurn = foldedTurn;
            this.IsFolded = isFolded;
            this.CompetitorCall = competitorCall;
            this.CompetitorRaise = competitorRaise;
            this.Hand = hand;
        }

        #region Properties
        public Panel CompetitorPanel
        {
            get
            {
                return this.competitorPanel;
            }

            protected set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Competitor cannot have Panel with null reference");
                }

                this.competitorPanel = value;
            }
        }

        public Point PanelLocation
        {
            set { this.competitorPanel.Location = value; }
        }

        public int ChipsCount
        {
            get
            {
                return this.chipsCount;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Chips count cannot be negative number");
                }

                this.chipsCount = value;
            }
        }

        public double Power
        {
            get
            {
                return this.power;
            }

            set
            {
                this.power = value;  // TODO: Need to put a validation
            }
        }

        public double Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value; // TODO: Need to put a validation
            }
        }

        public bool Onturn
        {
            get
            {
                return this.onTurn;
            }

            set
            {
                this.onTurn = value;
            }
        }

        public bool FoldedTurn
        {
            get
            {
                return this.foldedTurn;
            }

            set
            {
                this.foldedTurn = value;
            }
        }

        public bool IsFolded
        {
            get
            {
                return this.isFolded;
            }

            set
            {
                this.isFolded = value;
            }
        }

        public int CompetitorCall
        {
            get
            {
                return this.call;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Call amount cannot be a negative number");
                }

                this.call = value;
            }
        }

        public int CompetitorRaise
        {
            get
            {
                return this.raise;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Raise amount cannot be negative number");
                }

                this.raise = value;
            }
        }

        public ICollection<ICard> Hand // TODO: Must make it fixed array
        {
            get { return this.hand; }
            set { this.hand = value; }
        }

        #endregion
    }
}
