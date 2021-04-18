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

        CalculatorManager manager;

        private void button_Click(object sender, EventArgs e)
        {
            manager.ButtonClick((sender as Button).Text);
            richTextBox1.Text = manager.CurrentValue;
        }
    }
}
