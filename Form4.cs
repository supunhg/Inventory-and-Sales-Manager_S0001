using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace fridayGUI_2
{
    public partial class Form4 : Form
    {
        public static int calledFlag;

        public Form4(int flag)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            calledFlag = flag;

            if (calledFlag == 1)
            {
                LoadProductDetailsReport();
            }
            else if(calledFlag == 2)
            {
                LoadSalesDetailsReport();
            }
            else
            {
                return;
            }
        }

        private void LoadProductDetailsReport()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.connStr))
            {
                conn.Open();

                string query = "SELECT * FROM ProductTable";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);

                        if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                        {
                            CrystalReport2 rpt2 = new CrystalReport2();
                            rpt2.Load("C:/Users/maste/source/repos/fridayGUI_2/CrystalReport2.rpt");
                            rpt2.SetDataSource(dataSet.Tables[0]);
                            this.crystalReportViewer1.ReportSource = rpt2;
                        }
                        else
                        {
                            MessageBox.Show("No data found!");
                        }
                        conn.Close();
                    }
                }
            }
        }

        private void LoadSalesDetailsReport()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.connStr))
            {
                conn.Open();

                string query = "SELECT * FROM OrderTable";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);

                        if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                        {
                            CrystalReport1 rpt1 = new CrystalReport1();
                            rpt1.Load("C:/Users/maste/source/repos/fridayGUI_2/CrystalReport1.rpt");
                            rpt1.SetDataSource(dataSet.Tables[0]);
                            this.crystalReportViewer1.ReportSource = rpt1;
                        }
                        else
                        {
                            MessageBox.Show("No data found!");
                        }
                        conn.Close();
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void CrystalReport11_InitReport(object sender, EventArgs e)
        {

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void productDetailsButton_Click(object sender, EventArgs e)
        {
            LoadProductDetailsReport();
        }

        private void salesDetailsButton_Click(object sender, EventArgs e)
        {
            LoadSalesDetailsReport();
        }
    }
}
