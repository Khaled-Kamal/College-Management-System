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
    public partial class Tecshers : Form
    {
        public Tecshers()
        {
            InitializeComponent();
            display();
        }//data base conniction 
        SqlConnection cn = new SqlConnection("Data Source=NANA-085M;Initial Catalog=visualpro;Integrated Security=True");
        //take info from dep table
        private void filldepartment()
        {
            cn.Open();
            SqlCommand cm = new SqlCommand("select * from department", cn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            DataTable table = new DataTable();
            adapter.Fill(table);
            tDep.DataSource = table;
            tDep.DisplayMember = "depName";
            tDep.ValueMember = "depName";
            cn.Close();

        }
        // empty text boxs
        private void rest()
        {
            tName.Text = "";
            tGender.SelectedIndex = 0;
            tBirth.Text = "";
            tDep.SelectedIndex = 0;
            tAddrss.Text = "";
            tPhone.Text = "";

        }
        // data base show in app
        private void display()
        {
            cn.Open();
            string query = " select *from teacher";
            SqlDataAdapter sda = new SqlDataAdapter(query,cn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            teaDGV.DataSource = ds.Tables[0];

            cn.Close();
        }
        private void Tecshers_Load(object sender, EventArgs e)
        {
            filldepartment();
            display();


        }
        //links

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {
            Depart2 dept = new Depart2();
            dept.Show();
            this.Hide();

        }

        private void label9_Click(object sender, EventArgs e)
        {
            accounting acc = new accounting();
            acc.Show();
            this.Hide();

        }

        private void label11_Click(object sender, EventArgs e)
        {
            studnt stu = new studnt();
            stu.Show();
            this.Hide();
        }
        //logout code
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();

        }
        //close code
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //************buttons code********
        private void savebtn_Click(object sender, EventArgs e)
        {
            if (tName.Text==""||tGender.SelectedIndex==-1 || tBirth.Text == "" || tDep.SelectedIndex == -1 || tAddrss.Text == "" || tPhone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    cn.Open();
                    
                    SqlCommand cm = new SqlCommand("insert into teacher values('" + tName.Text + "','" + tGender.SelectedItem.ToString() + "','" + tBirth.Value.Date+ "','" + tPhone.Text + "','" + tDep.SelectedValue.ToString() + "','" + tAddrss.Text + "');", cn);
                    cm.ExecuteNonQuery();
                    MessageBox.Show("teacher data is saved");

                    cn.Close();
                    display();
                    rest();
                }
                catch(Exception ex) {
                    MessageBox.Show(ex.Message);
                    
                }
            }
        }

        private void restbtn_Click(object sender, EventArgs e)
        {
            rest();
        }
        int key;
        //select the row i want from db  :)
        private void teaDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tName.Text = teaDGV.SelectedRows[0].Cells[1].Value.ToString();
            tGender.SelectedItem = teaDGV.SelectedRows[0].Cells[2].Value.ToString();
            tBirth.Text = teaDGV.SelectedRows[0].Cells[3].Value.ToString();
            tPhone.Text = teaDGV.SelectedRows[0].Cells[4].Value.ToString();
            tDep.SelectedValue = teaDGV.SelectedRows[0].Cells[5].Value.ToString();
            tAddrss.Text = teaDGV.SelectedRows[0].Cells[6].Value.ToString();
          
            if (tName.Text == "") 
            {
                key = 0;
            }
            else { key = Convert.ToInt32(teaDGV.SelectedRows[0].Cells[0].Value.ToString());   }

        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("select the row which you want delete >.< ");
            }
            else
            {
                try
                {
                    cn.Open();
                    SqlCommand com = new SqlCommand("delete from teacher where TID='" + key + "';", cn);
                    com.ExecuteNonQuery();
                    MessageBox.Show("teacher data  deleted successfully   X.X");
                    cn.Close();
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
            if (tName.Text == "" || tGender.SelectedIndex == -1 || tDep.SelectedIndex == -1 || tAddrss.Text == "" || tPhone.Text == "")
            {
                MessageBox.Show("Missing Information ");
            }
            else 
            {
                try
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("update teacher set TName='" + tName.Text + "',TGender= '" + tGender.SelectedItem.ToString() + "',TDOB='" + tBirth.Value.Date + "',TDep='" + tDep.SelectedValue.ToString() + "',TAdd='" + tAddrss.Text + "',TPhone='" + tPhone.Text + "'where TID='"+key+"';", cn);
                    cm.ExecuteNonQuery();
                    MessageBox.Show("teacher data is updated ");

                    cn.Close();
                    rest();
                    display();

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }
    }
}
