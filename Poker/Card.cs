namespace Poker
{
    using System;
    using System.Windows.Forms;
    using Poker.Interfaces;
    using System.Drawing;

    public class Card : ICard
    {
        private readonly Bitmap CardBackImage = new Bitmap("Assets\\Back\\Back.png");
        private PictureBox cardPictureBox;
        private int cardTag;
        private Image cardFrontImage;

        public Card(Image cardFrontImage, int cardTag)
        {
            this.CardFrontImage = cardFrontImage;
            this.CardTag = cardTag; 
            this.CardPictureBox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Height = 130,
                Width = 80,
                Image = CardBackImage,
                Visible = true
            };
        }
        
        public int Width
        {
            get { return this.cardPictureBox.Width; }
        }

        public AnchorStyles Anchor
        {
            private get;
            set;
        }

        public Point Location
        {
            private get { return this.CardPictureBox.Location; }
            set { this.CardPictureBox.Location = value; }
        }

        public PictureBox CardPictureBox
        {
            get
            {
                return this.cardPictureBox;
            }

            private set { this.cardPictureBox = value; }
        }

        public int CardTag
        {
            get
            {
                return this.cardTag;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Card tag cannot be negative number");
                }

                this.cardTag = value;
            }
        }

        private Image CardFrontImage
        {
            get
            {
                return this.cardFrontImage;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Card image cannot be null");
                }

                this.cardFrontImage = value;
            }
        }

        public void Reveal()
        {
            this.CardPictureBox.Image = this.CardFrontImage;
        }
    }
}
