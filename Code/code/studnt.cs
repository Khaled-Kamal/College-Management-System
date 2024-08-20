using Guna.UI.WinForms;
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

namespace College
{
    public partial class studnt : Form
    {
        public studnt()
        {
            InitializeComponent();
            display();
        }
        //data base conniction
        SqlConnection con = new SqlConnection("Data Source=NANA-085M;Initial Catalog=visualpro;Integrated Security=True");

        private void studnt_Load(object sender, EventArgs e)
        {
            filldepartment();
            display();
        }
        //استدعاء الداتا من الداتا بيز
        private void filldepartment()
        {
            con.Open();
            SqlCommand cm = new SqlCommand("select * from department", con);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            DataTable tab = new DataTable();
            adapter.Fill(tab);
            stdepart.DataSource = tab;
            stdepart.DisplayMember = "depName";
            stdepart.ValueMember = "depName";
            con.Close();

        }
        private void rest()
        {
            stname.Text = "";
            stgendr.SelectedIndex = 0;
            stbirth.Text = "";
            stphone.Text = "";
            stdepart.SelectedIndex = 0;
            stfees.Text = "";
        }
        // data base show in app
        private void display()
        {
            con.Open();
            string query = "select * from studnt";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            stDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void NoDueList()
        {
            con.Open();
            string qury = "select * from studnt where stFees >= 10000";
            SqlDataAdapter sda = new SqlDataAdapter(qury, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            stDGV.DataSource = ds.Tables[0];

            con.Close();
        }
        //******* links *******
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
            accounting acc = new accounting();
            acc.Show();
            this.Hide();

        }
        //log out
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();

        }
         // X code
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
        // **** buttons cobe ******
        private void restbtn_Click(object sender, EventArgs e)
        {
            rest();

        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            if (stname.Text == "" || stgendr.SelectedIndex == -1 || stphone.Text == "" || stdepart.SelectedIndex == -1 || stfees.Text == "")
            {
                MessageBox.Show("Missing Information");

            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cm = new SqlCommand("insert into studnt values('"+stname.Text +"','"+stgendr.SelectedItem.ToString()+"','"+stbirth.Value.Date+"','"+stphone.Text+"','"+stdepart.SelectedValue.ToString()+"','"+stfees.Text+"');", con);
                     cm.ExecuteNonQuery();
                      MessageBox.Show("Student saved ");
                    

                    con.Close();
                    
                   rest();
                    display();

                }
                catch (Exception ex){ MessageBox.Show(ex.Message); }
            }

        }
        // select the row from batabase
        int key = 0;
        private void stDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            stname.Text = stDGV.SelectedRows[0].Cells[1].Value.ToString();
            stgendr.SelectedItem= stDGV.SelectedRows[0].Cells[2].Value.ToString();
            stbirth.Text = stDGV.SelectedRows[0].Cells[3].Value.ToString();
            stphone.Text = stDGV.SelectedRows[0].Cells[4].Value.ToString();
            stdepart.SelectedValue = stDGV.SelectedRows[0].Cells[5].Value.ToString();
            stfees.Text = stDGV.SelectedRows[0].Cells[6].Value.ToString();
            
            if(stname.Text == "") { key = 0; }
            else { key = Convert.ToInt32(stDGV.SelectedRows[0].Cells[0].Value.ToString()); }



        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (key == 0) { MessageBox.Show("select the student you want delete "); }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cm = new SqlCommand("delete from studnt where stID='" + key + "';", con);
                    cm.ExecuteNonQuery();
                      MessageBox.Show("student deleted");
                    con.Close();
                    rest();
                    display();

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            if (stname.Text == "" ||stgendr.SelectedIndex==-1 || stphone.Text == ""||stdepart.SelectedIndex==-1||stfees.Text=="")
            
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("update studnt set stName='" + stname.Text + "',stGender='" + stgendr.SelectedItem.ToString() + "',stDob='" + stbirth.Value.Date + "',stDepart='" + stdepart.SelectedValue.ToString() + "',stFees='" + stfees.Text + "',stPhone='"+stphone.Text+"'where stID='"+key+"';", con);
                    com.ExecuteNonQuery();
                      MessageBox.Show("student updated");

                    con.Close();
                    rest();
                    display();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }

        }

        private void stgendr_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            NoDueList();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            display();
        }
    }
}
