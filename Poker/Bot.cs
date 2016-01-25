using System.Collections.Generic;

namespace Poker
{
    using System;
    using System.Windows.Forms;
    using Interfaces;

    public class Bot : Competitor
    {
        #region Fields
        private Panel competitorPanel;
        private int chipsCount;
        private double power;
        private double type;
        private bool onTurn;
        private bool foldedTurn;
        private bool isFolded;
        private int call;
        private int raise;
        #endregion

        public Bot()
            :base()
        {
            
        }

        public Bot(
            Panel botPanel,
            int chipsCount,
            double power,
            double type,
            bool onTurn,
            bool foldedTurn,
            bool isFolded,
            int botCall,
            int botRaise,
            ICollection<ICard> hand)
            :base(
                 botPanel,
                 chipsCount,
                 power,
                 type,
                 onTurn,
                 foldedTurn,
                 isFolded,
                 botCall,
                 botRaise,
                 hand)
        {

        }
        #region Properties
        #endregion

    }
}