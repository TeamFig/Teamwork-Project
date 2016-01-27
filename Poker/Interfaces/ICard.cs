namespace Poker.Interfaces
{
    using System.Drawing;
    using System.Windows.Forms;

    public interface ICard
    {
        PictureBox CardPictureBox { get; }
        int Width { get; }
        AnchorStyles Anchor { set; }
        Point Location { set; }
        int CardTag { get; set; }
        void Reveal();
        void Hide();
    }
}
