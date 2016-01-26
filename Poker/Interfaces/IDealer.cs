namespace Poker.Interfaces
{
    using System.Collections.Generic;

    public interface IDealer
    {
        void ThrowCards(ICollection<ICompetitor> competitorsCollection);
        void ShuffleCards();
    }
}