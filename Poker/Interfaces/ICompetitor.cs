﻿using System.Collections.Generic;
using System.Drawing;

namespace Poker.Interfaces
{
    using System.Windows.Forms;

    public interface ICompetitor
    {
        Panel CompetitorPanel { get; /*set;*/}

        ICollection<ICard> Hand { get; set; }

        Point PanelLocation { set; }

        int ChipsCount { get; set;}

        double Power { get; set;}

        double Type { get; set;}

        bool Onturn { get; set; }

        bool FoldedTurn { get; set;}

        bool IsFolded { get; set;}

        int CompetitorCall { get; set; }

        int CompetitorRaise { get; set; }


    }
}