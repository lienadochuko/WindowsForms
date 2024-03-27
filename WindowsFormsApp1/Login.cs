using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        //connection
        string path = "Data Source=GRDS-GT-04;Initial Catalog=LoginUsers;Integrated Security=True";
        SqlConnection connection = new SqlConnection("Data Source=GRDS-GT-04;Initial Catalog=Registrations;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter dataAdapter = new SqlDataAdapter();
        //System.Data.DataTable dt;

        public Login()
        {
            InitializeComponent();
            //connection
            //connection = new SqlConnection(path);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (textUserName.Text == "" && textUserPassword.Text == "")
                {
                    MessageBox.Show("Please Enter UserName and Password");
                }
                else
                {
                    connection.Open();
                    cmd = new SqlCommand("select * from LoginUsers where U_Name=@Name and U_Pass=@Pass", connection);
                    cmd.Parameters.AddWithValue("@Name", textUserName.Text);
                    cmd.Parameters.AddWithValue("@Pass", textUserPassword.Text);
                    dataAdapter = new SqlDataAdapter(cmd);
                    cmd.ExecuteNonQuery();
                    DataSet ds = new DataSet();
                    dataAdapter.Fill(ds);
                    connection.Close();

                    int count = ds.Tables[0].Rows.Count;
                    if (count == 1)
                    {
                        MessageBox.Show("Login Successful");
                        this.Hide();
                        Form1 f1 = new Form1();
                        f1.Show();
                    }
                    else
                    {
                        connection.Close();
                        MessageBox.Show("Login Failed");
                    }
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
