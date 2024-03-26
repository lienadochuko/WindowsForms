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
using Microsoft.Office.Interop.Excel;

namespace WindowsFormsApp1
{
    public partial class Registrations : Form
    {
        //connection
        string path = "Data Source=GRDS-GT-04;Initial Catalog=Registrations;Integrated Security=True";
        SqlConnection connection = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        System.Data.DataTable dt;
        int ID;

        public Registrations()
        {
            InitializeComponent();
            //connection.ConnectionString = path;
            connection = new SqlConnection(path);
            displayData();
            BtnUpdate.Enabled = false;
            BtnDelete.Enabled = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (textLastName.Text == "" || textFirstName.Text == "" || textJobGrade.Text == "" || textID.Text == "" || textEmail.Text == "" || textAddress.Text == "")
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
            ID = 0;
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
                dt = new System.Data.DataTable();
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
            BtnSave.Enabled = false;
            BtnUpdate.Enabled = true;
            BtnDelete.Enabled = true;
            ID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textLastName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textFirstName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textJobGrade.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textID.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

            radioMale.Checked = false;
            radioFemale.Checked = false;

            if (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString() == "Female")
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
            if (ID <= 0)
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
                    BtnSave.Enabled = true;
                    BtnUpdate.Enabled = false;
                    BtnDelete.Enabled = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    connection.Close();
                    clear();
                    displayData();
                    BtnSave.Enabled = true;
                    BtnUpdate.Enabled = false;
                    BtnDelete.Enabled = false;
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                cmd = new SqlCommand("DELETE fron Employee where Employee_Id ='" + ID + "' ", connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Employee Data has been Deleted");
                clear();
                displayData();
                BtnSave.Enabled = true;
                BtnUpdate.Enabled = false;
                BtnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
                clear();
                displayData();
                BtnSave.Enabled = true;
                BtnUpdate.Enabled = false;
                BtnDelete.Enabled = false;
            }

        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            BtnSave.Enabled = true;
            BtnUpdate.Enabled = false;
            BtnDelete.Enabled = false;
            clear();
        }

        private void textSearch_TextChanged_1(object sender, EventArgs e)
        {
            connection.Open();
            dataAdapter = new SqlDataAdapter("select * from Employee where Employee_LastName like '%" + textSearch.Text + "%' ", connection);
            dt = new System.Data.DataTable();
            dataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void BtnExporttoExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Workbook workbook = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet sheet = (Worksheet)excel.ActiveSheet;
                excel.Visible = true;

                for (int j = 2; j <= dataGridView1.Rows.Count; j++)
                {
                    for (int i = 1; i <= 1; i++)
                    {
                        sheet.Cells[j, i] = dataGridView1.Rows[j - 2].Cells[i - 1].Value;
                    }
                }

                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    sheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }

                for (int g = 0; g < dataGridView1.Columns.Count - 1; g++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        sheet.Cells[g + 2, j + 1] = dataGridView1.Rows[g].Cells[j].Value.ToString();
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
