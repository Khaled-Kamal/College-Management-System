using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace College
{
    public partial class accounting : Form
    {
        public accounting()
        {
            InitializeComponent();
            display();
        }
        //take data from studnt db
        SqlConnection cn = new SqlConnection("Data Source=NANA-085M;Initial Catalog=visualpro;Integrated Security=True");
        private void fillstudent()
        {
            cn.Open();
            SqlCommand cm = new SqlCommand("select * from studnt",cn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            DataTable table = new DataTable();
            adapter.Fill(table);
            stid.DataSource = table;
            stid.DisplayMember = "stID";
            stid.ValueMember = "stID";

            cn.Close();
        }
        // show of database

        private void display()
        {
            cn.Open();
            string query = " select * from payment";
            SqlDataAdapter sda = new SqlDataAdapter(query, cn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            FDGV.DataSource = ds.Tables[0];

            cn.Close();

        }
        // *** links ******
        private void label11_Click(object sender, EventArgs e)
        {
            Tecshers tea = new Tecshers();
            tea.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Depart2 dep = new Depart2();
            dep.Show();
            this.Hide();

        }

        private void label9_Click(object sender, EventArgs e)
        {
            studnt stu = new studnt();
            stu.Show();
            this.Hide();
        }
        // close app
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        // logout
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();


        }

        private void accounting_Load(object sender, EventArgs e)
        {
            fillstudent();
            display();
        }
        // get student name by using id student
       
        private void GetStname()
        {
            cn.Open();
            SqlCommand cm = new SqlCommand("select * from studnt where stID='" + stid.SelectedValue.ToString() + "';", cn);
            DataTable dt = new DataTable();
            SqlDataReader reader;
            reader = cm.ExecuteReader();
            if (reader.Read())
            { stname.Text = reader[1].ToString(); }
           // reader.Read(stname.Text = reader[1].ToString());

            

            cn.Close();
        }

        private void stid_SelectedIndexChanged(object sender, EventArgs e)
        {
                  }

        private void stid_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetStname();
        }
        private void clear()
        {
            stid.SelectedIndex = -1;
            stname.Text = "";
            amount.Text = "";

        }

        // update the finencial info of student

        private void updatestudent()
        {
            try
            {
                cn.Open();
                SqlCommand com = new SqlCommand("update studnt set stFees= '" + amount.Text + "'where StId='" + stid.SelectedValue.ToString() + "';", cn);
                com.ExecuteNonQuery();
                MessageBox.Show("Student updated ! ");

                cn.Close();

            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        //  payment btn                                           
        private void paybtn_Click(object sender, EventArgs e)
        {
            if (stname.Text == "" || amount.Text == "")
            {
                MessageBox.Show("Missing Information");

            }
            else if(Convert.ToInt32(amount.Text)>100000 || Convert.ToInt32(amount.Text) < 1)
            {
                MessageBox.Show("Amount is wrong");
            }
            else
            {
                try
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("insert into payment values('" + stid.SelectedValue.ToString() + "','" + stname.Text + "','" + priod.Value.Date + "','" + amount.Text + "');", cn);
                    cm.ExecuteNonQuery();
                    MessageBox.Show("payment successfull     ;) ");

                    cn.Close();
                    display();
                    updatestudent();
                    clear();

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }

        }
        // last page of the application 
    }
}
