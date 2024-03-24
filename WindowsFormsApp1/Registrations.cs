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
        public Registrations()
        {
            InitializeComponent();
            //connection.ConnectionString = path;
            connection = new SqlConnection(path);
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
                    SqlCommand cmd = new SqlCommand("INSERT INTO Employee (Employee_LastName, EMployee_FirstName, Employee_Email, Employee_Job_Grade, Employment_ID, Employee_Gender, Employee_Address) VALUES ('" + textLastName.Text + "','" + textFirstName.Text + "','" + textEmail.Text + "','" + textJobGrade.Text + "','" + textID.Text + "','" + gender + "','" + textAddress.Text + "')", connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Data saved successfully");
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
