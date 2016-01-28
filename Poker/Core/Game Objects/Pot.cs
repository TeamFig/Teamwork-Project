using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker.Core.Game_Objects
{
    public class Pot
    {
        private const int PotStartAmount = 0;
        private TextBox textBox;
        private int currentPotAmount;

        public Pot(TextBox textBox, int potAmount = PotStartAmount)
        {
            this.textBox = textBox;
            this.currentPotAmount = potAmount;
        }

        private int CurrentPotAmount
        {
            get
            {
                return this.currentPotAmount;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Pot amount cannot be negative number");
                }

                this.currentPotAmount = value;
            }
        }

        public void RaisePotAmount(int amountToAdd)
        {
            if (amountToAdd <= 0)
            {
                throw new InvalidOperationException("Cannot Raise pot with negative or zero amount");
            }

            this.CurrentPotAmount += amountToAdd;
            this.textBox.Text = this.CurrentPotAmount.ToString();
        }

        public void ClearPotAmount()
        {
            this.CurrentPotAmount = PotStartAmount;
            this.textBox.Text = this.CurrentPotAmount.ToString();
        }
    }
}
