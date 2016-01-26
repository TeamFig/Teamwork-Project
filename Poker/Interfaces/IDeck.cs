namespace Poker.Interfaces
{
    using System.Collections.Generic;

    public interface IDeck
    {
        IList<ICard> CardsCollection { get; set; }
        void Shuffle();
        ICard GetCard();
    }
}
