using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Interfaces;

namespace Poker
{
    public interface IDeck
    {
        IList<ICard> CardsCollection { get; set; }
        void Shuffle();
        ICard GetCard();
    }
}
