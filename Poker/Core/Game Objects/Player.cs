using System;
using System.Linq;

namespace Poker.Core.Game_Objects
{
    using System.Windows.Forms;
    using System.Collections.Generic;
    using Interfaces;

    public class Player : Competitor
    {
        private TextBox raiseTextBox;
        public Player()
            :base()
        {
            
        }

        public Player(Panel playerPanel, TextBox textBox, Label label, TextBox raiseTextBox)
            :base(playerPanel, textBox, label)
        {
            this.RaiseTextBox = raiseTextBox;
        }

        public Player(
            Panel playerPanel,
            int chipsCount,
            double power,
            double type,
            bool playerOnTurn, 
            bool playerFoldedTurn,
            bool playerIsFolded,
            int playerCall,
            int playerRaise,
            ICollection<ICard> hand)
            :base(
                 playerPanel,
                 chipsCount,
                 power,
                 type,
                 playerOnTurn,
                 playerFoldedTurn,
                 playerIsFolded,
                 playerCall,
                 playerRaise,
                 hand)
        {

        }

        public TextBox RaiseTextBox
        {
            get { return this.raiseTextBox; }
            set { this.raiseTextBox = value; }
        }

        public override int RaiseAmount
        {
            get { return int.Parse(this.RaiseTextBox.Text); }
        }

        public override void LookCardsInHand()
        {
            this.Hand.ToList().ForEach(card => card.Reveal());
        }

        public override void PlayTurn()
        {
            //foreach (var keyValuePair in this.CompetitorControls)
            //{
            //    keyValuePair.Value.Enabled = true;
            //}
        }


    }
}
