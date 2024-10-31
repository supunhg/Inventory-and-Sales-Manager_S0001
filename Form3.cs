using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fridayGUI_2
{
    public partial class Form3 : Form
    {
        public static int flag = 0;

        public Form3()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void newOrderButton_Click(object sender, EventArgs e)
        {
            
        }

        private void newOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void productReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = 1;

            Form4 form4 = new Form4(flag);
            form4.Show();
            this.Hide();
        }

        private void salesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flag = 2;

            Form4 form4 = new Form4(flag);
            form4.Show();
            this.Hide();
        }
    }
}
