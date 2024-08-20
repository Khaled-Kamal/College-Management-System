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
    public partial class Depart2 : Form
    {
        
        public Depart2()
        {
            InitializeComponent();
            View();
        }
       
        SqlConnection cn = new SqlConnection("Data Source=NANA-085M;Initial Catalog=visualpro;Integrated Security=True");
        //text boxsتفريغ ال
        private void Rest()
        {
            depName.Text = "";
            depDesp.Text = "";
            depDur.Text = "";
        } 
        //اظهار الداتا بيز في البرنامج
        private void View()
        {
            cn.Open();
            string query = "select * from department";
            SqlDataAdapter sda = new SqlDataAdapter(query,cn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            depDGV.DataSource = ds.Tables[0];

            cn.Close();
        }
        // close application
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //save button
        private void savebtn1_Click(object sender, EventArgs e)
        {
            if (depName.Text== "" || depDesp.Text == "" || depDur.Text == "")
            {
                MessageBox.Show("Missing Information ");

            }
            else
            {
                try
                {
                    cn.Open();
                    
                     
                    SqlCommand cm = new SqlCommand("insert into department values('" + depName.Text + "','" + depDesp.Text + "','"+depDur.Text+"');", cn);
                     cm.ExecuteNonQuery();
                    
                      MessageBox.Show("department saved ");

                            
                    cn.Close();
                    Rest();
                    View();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        // selecte rows in comb box
        int key ;
        private void depDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            depName.Text = depDGV.SelectedRows[0].Cells[1].Value.ToString();
            depDesp.Text = depDGV.SelectedRows[0].Cells[2].Value.ToString();
            depDur.Text = depDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (depName.Text == "") { key = 0; }
            else { key = Convert.ToInt32(depDGV.SelectedRows[0].Cells[0].Value.ToString()); }
        }
        //delete button
        private void deletebtn1_Click(object sender, EventArgs e)
        {
            if (key == 0) { MessageBox.Show("select the department you want delete"); }
            else
            {
                try 
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("delete from department where depID = '" + key + "';", cn);
                     cm.ExecuteNonQuery();
                     MessageBox.Show("department deleted");


                    cn.Close();
                    Rest();
                    View();

                } catch(Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        //rest buttton
        private void resetbtn1_Click(object sender, EventArgs e)
        {
            Rest();
        }
        // ******links*******
        private void label11_Click(object sender, EventArgs e)
        {
            Tecshers tea = new Tecshers();
            tea.Show();
            this.Hide();

        }

        private void label13_Click(object sender, EventArgs e)
        {
            studnt stu = new studnt();
            stu.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            accounting acc = new accounting();
            acc.Show();
            this.Hide();

        }
        // logout
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();

        }
        // edit button
        private void editbtn1_Click(object sender, EventArgs e)
        {
            if (depName.Text == "" || depDesp.Text == "" || depDur.Text == "")
            {
                MessageBox.Show("selecte the department you want update ");
            }
            else
            {
                try
                {
                    cn.Open();
                    SqlCommand cm = new SqlCommand("update department set depName='"+depName.Text+"',depDesc='"+depDesp.Text+"',depDur= '"+depDur.Text+"'where depID='"+key+"';",cn);
                     cm.ExecuteNonQuery();
                         MessageBox.Show("department updated ");
                    cn.Close();
                    Rest();
                    View();

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void Depart2_Load(object sender, EventArgs e)
        {

        }
    }
}
