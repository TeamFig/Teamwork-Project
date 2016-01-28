using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poker.Interfaces;

namespace Poker.Core.Game_Objects
{
    public class SmallBlind : IBlindField
    {
        private const int DefaultSmallBlindAmount = 250;
        private int currentBlindAmount;
        private Button button;
        private TextBox textBox;

        public SmallBlind(Button button, TextBox textBox, int smallBlindAmount = DefaultSmallBlindAmount)
        {
            this.Button = button;
            this.Button.Click += this.SmallBlind_OnClick;
            this.BlindAmount = smallBlindAmount;
            this.textBox = textBox;
            this.textBox.Text = smallBlindAmount.ToString();
        }

        private Button Button
        {
            get
            {
                return this.button;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Small blind button cannot be null");
                }

                this.button = value;
            }
        }

        public int BlindAmount
        {
            get
            {
                return this.currentBlindAmount;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Small blind amount must be more than 0");
                }

                this.currentBlindAmount = value;
            }
        }

        private void SmallBlind_OnClick(object sender, EventArgs e)
        {
            string newBlindAmount = this.textBox.Text;
            int blindAmountAsint;
            try
            {
                blindAmountAsint = int.Parse(newBlindAmount);
            }
            catch (FormatException)
            {
                MessageBox.Show("This field is only for integers");
                return;
            }

            double blindAmountAsDouble = double.Parse(newBlindAmount);
            if (blindAmountAsDouble != (double)blindAmountAsint)
            {
                MessageBox.Show("Blind amount must be integer number");
            }

            this.BlindAmount = blindAmountAsint;
        }
    }
}
