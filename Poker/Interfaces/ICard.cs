using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

    }
}
