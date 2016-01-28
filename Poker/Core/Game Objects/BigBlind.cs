using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poker.Interfaces;

namespace Poker.Core.Game_Objects
{
    public class BigBlind : IBlindField
    {
        private const int DefaultBigBlindAmount = 500;
        private int currentBlindAmount;
        private Button button;
        private TextBox textBox;

        public BigBlind(Button button, TextBox textBox, int bigBlindAmount = DefaultBigBlindAmount)
        {
            this.Button = button;
            this.Button.Click += this.BigBlind_OnClick;
            this.BlindAmount = bigBlindAmount;
            this.textBox = textBox;
            this.textBox.Text = bigBlindAmount.ToString();
        }

        private Button Button
        {
            get { return this.button; }

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
            get { return this.currentBlindAmount; }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Small blind amount must be more than 0");
                }

                this.currentBlindAmount = value;
            }
        }

        private void BigBlind_OnClick(object sender, EventArgs e)
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
            if (blindAmountAsDouble != (double) blindAmountAsint)
            {
                MessageBox.Show("Enter only integers!");
                return;
            }

            this.BlindAmount = blindAmountAsint;
        }
    }
}
