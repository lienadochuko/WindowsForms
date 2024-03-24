using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //DataTable dt = new DataTable();


        public Form1()
        {
            InitializeComponent();
        }

        string optionSelected;


        private void Form1_Load(object sender, EventArgs e)
        {
            //cb.Items.Add("Software Engineer");
            //cb.Items.Add("Cloud Engineer");
            //cb.Items.Add("Data Analyst");
            //cb.Items.Add("Database Administrator");
        }

        //private void cb_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    optionSelected = cb.SelectedItem.ToString();
        //}

        Registrations Reg = new Registrations();

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You selected: " + optionSelected);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reg.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
