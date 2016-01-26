namespace Poker.Interfaces
{
    public interface IDeckCreator
    {
        IDeck CreateDeck(string[] cardImagesLocations, string[] charsToRemoveToGetTag);
    }
}