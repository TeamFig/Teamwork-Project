using System;
using System.Linq;

namespace Poker.Core.Game_Objects
{
    using System.Windows.Forms;
    using Interfaces;
    using System.Collections.Generic;

    public class Bot : Competitor
    {
        public Bot()
            :base()
        {
            
        }

        public Bot(Panel botPanel, TextBox textBox, Label label)
            : base(botPanel, textBox, label)
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

        public override void PlayTurn()
        {
            throw new NotImplementedException();
        }
    }
}