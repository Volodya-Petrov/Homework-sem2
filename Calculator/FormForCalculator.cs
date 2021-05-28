using System;

using System.Windows.Forms;

namespace Calculator
{
    public partial class FormForCalculator : Form
    {
        public FormForCalculator()
        {
            InitializeComponent();
            manager = new CalculatorManager();
        }

        private CalculatorManager manager;

        private void ClickOnDigits(object sender, EventArgs e)
        {
            manager.DigitClick((sender as Button).Text);
            richTextBox1.Text = manager.CurrentValue;
        }

        private void ClickOnClear(object sender, EventArgs e)
        {
            manager.Clear();
            richTextBox1.Text = manager.CurrentValue;
        }

        private void ClickOnOperator(object sender, EventArgs e)
        {
            manager.OperatorClick((sender as Button).Text);
            richTextBox1.Text = manager.CurrentValue;
        }

        private void ClickOnEqual(object sender, EventArgs e)
        {
            manager.Equal();
            richTextBox1.Text = manager.CurrentValue;
        }
    }
}
