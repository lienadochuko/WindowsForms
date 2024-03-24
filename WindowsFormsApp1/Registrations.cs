using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public partial class Registrations : Form
    {
        //connection
        string path = "Data Source=GRDS-GT-04;Initial Catalog=Registrations;Integrated Security=True";
        SqlConnection connection = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        DataTable dt = new DataTable();
        int ID;

        public Registrations()
        {
            InitializeComponent();
            //connection.ConnectionString = path;
            connection = new SqlConnection(path);
            displayData();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if(textLastName.Text == "" || textFirstName.Text == "" || textJobGrade.Text == "" || textID.Text == "" || textEmail.Text == "" || textAddress.Text == "")
            {
                MessageBox.Show("Please fill in all fields");

            }
            else
            {
                string gender;
                try
                {
                    if (radioMale.Checked)
                    {
                        gender = "Male";
                    }
                    else if (radioFemale.Checked)
                    {
                        gender = "Female";
                    }
                    else
                    {
                        throw new Exception("Please select your gender");
                    }

                    connection.Open();
                    cmd = new SqlCommand("INSERT INTO Employee (Employee_LastName, EMployee_FirstName, Employee_Email, Employee_Job_Grade, Employment_ID, Employee_Gender, Employee_Address) VALUES ('" + textLastName.Text + "','" + textFirstName.Text + "','" + textEmail.Text + "','" + textJobGrade.Text + "','" + textID.Text + "','" + gender + "','" + textAddress.Text + "')", connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Data saved successfully");
                    clear();
                    displayData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
            }
        }
    
        public void clear()
        {
            textLastName.Text = "";
            textFirstName.Text = "";
            textJobGrade.Text = "";
            textID.Text = "";
            textEmail.Text = "";
            textAddress.Text = "";
            radioMale.Checked = false;
            radioFemale.Checked = false;
        }

        public void displayData()
        {
            try
            {
                dt = new DataTable();
                connection.Open();
                dataAdapter = new SqlDataAdapter("SELECT * FROM Employee", connection);
                dataAdapter.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textLastName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textFirstName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textJobGrade.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textID.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

            radioMale.Checked = false;
            radioFemale.Checked = false;

            if(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString() == "Female")
            {
                radioFemale.Checked = true;
            }
            else
            {
                radioMale.Checked = true;
            }

            textAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string gender;
            if(ID <= 0)
            {
                MessageBox.Show("Select an Employee to Update from the Table");
            }
            else
            {
                try
                {
                    if (radioMale.Checked)
                    {
                        gender = "Male";
                    }
                    else if (radioFemale.Checked)
                    {
                        gender = "Female";
                    }
                    else
                    {
                        throw new Exception("Please select your gender");
                    }
                    connection.Open();
                    cmd = new SqlCommand("UPDATE Employee SET Employee_LastName = '" + textLastName.Text + "', EMployee_FirstName = '" + textFirstName.Text +
                        "', Employee_Email = '" + textEmail.Text + "', Employee_Job_Grade = '" + textJobGrade.Text + "', Employment_ID = '" + textID.Text
                        + "', Employee_Gender = '" + gender + "', Employee_Address = '" + textAddress.Text + "'where Employee_Id ='" + ID + "' ", connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Employee Data has been Updated");
                    clear();
                    displayData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                }
            }
        }
    }
}
