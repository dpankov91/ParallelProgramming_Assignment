using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        private PrimeGenerator _primeGenerator;

        public Form1()
        {
            InitializeComponent();
            _primeGenerator = new PrimeGenerator();
        }


        private async void GeneratePrimeNumber(object sender, EventArgs e)
        {
            long first = Convert.ToInt64(textBox1.Text);
            long last = Convert.ToInt64(textBox2.Text);

            List<long> primeNumbers = await _primeGenerator.GetPrimesParallelAsync(first, last);

            listBox1.DataSource = primeNumbers;
        }

    }
}
