using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace College
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (usertb.Text == "" || passtb.Text == "")
            {
                MessageBox.Show("enter username and password");
            }
            else if (usertb.Text == "admin" && passtb.Text == "password")
            {
                studnt stu = new studnt();
                stu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
                usertb.Text = "";
                passtb.Text = "";

            }
                 

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void rest()
        {
            usertb.Text = "";
            passtb.Text = "";
        }
        private void label6_Click(object sender, EventArgs e)
        {
            rest();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
