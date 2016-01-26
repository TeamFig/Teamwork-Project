namespace Poker.Interfaces
{
    using System.Collections.Generic;

    public interface IDeck
    {
        IList<ICard> CardsCollection { get; set; }
        int DeckCount { get; }
        void Shuffle();
        ICard GetCard();
    }
}
