namespace Poker
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Interfaces;

    public class Player : Competitor
    {
        public Player()
            :base()
        {
            
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
    }
}
