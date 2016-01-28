namespace Poker.Factories
{
    using System.Drawing;
    using System.Windows.Forms;

    public static class PanelsLocationsGenerator 
    {
       private static readonly int[] HorizontalCoordinatesCollection = new int[] {500, 15, 75, 530, 980, 1020 };
        private static readonly int[] VerticalCoordinatesCollection = new int[] {530, 460, 50, 10, 25, 480};
        private static readonly AnchorStyles[] anchorsCollection =
{
                (AnchorStyles.Bottom),
                (AnchorStyles.Bottom | AnchorStyles.Left),
                (AnchorStyles.Top | AnchorStyles.Left),
                (AnchorStyles.Top),
                (AnchorStyles.Top | AnchorStyles.Right),
                (AnchorStyles.Bottom | AnchorStyles.Right)
            };
        public static AnchorStyles [] GenerateAnchorStyles()
        {
            AnchorStyles[] anchorStylesCollection = new AnchorStyles[HorizontalCoordinatesCollection.Length]; // TODO: array range variable is not good have to change
            for (int anchorIndex = 0; anchorIndex < anchorStylesCollection.Length; anchorIndex++)
            {
                AnchorStyles anchorStyle = anchorsCollection[anchorIndex];
                anchorStylesCollection[anchorIndex] = anchorStyle;
            }

            return anchorStylesCollection;
        }

        public static Point [] GeneratePanelLocations()
        {
            Point [] locations = new Point[HorizontalCoordinatesCollection.Length];
            for (int locationIndex = 0; locationIndex < locations.Length; locationIndex++)
            {
                int horizontal = HorizontalCoordinatesCollection[locationIndex]; //TODO: Magical numbers
                int vertical = VerticalCoordinatesCollection[locationIndex];
                Point location = new Point(horizontal, vertical);
                locations[locationIndex] = location;
            }

            return locations;
        }
    }
}
