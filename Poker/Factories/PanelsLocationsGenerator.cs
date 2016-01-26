namespace Poker.Factories
{
    using System.Drawing;

    public static class PanelsLocationsGenerator 
    {
       private static readonly int[] HorizontalCoordinatesCollection = new int[] { 580, 15, 75, 590, 1115, 1160 };
       private static readonly int[] VerticalCoordinatesCollection = new int[] { 480, 420, 65, 25, 65, 420 };

        public static Point [] GeneratePanelLocations()
        {
            Point [] locations = new Point[HorizontalCoordinatesCollection.Length];
            for (int locationIndex = 0; locationIndex < locations.Length; locationIndex++)
            {
                int horizontal = HorizontalCoordinatesCollection[locationIndex];
                int vertical = VerticalCoordinatesCollection[locationIndex];
                Point location = new Point(horizontal, vertical);
                locations[locationIndex] = location;
            }

            return locations;
        }
    }
}
