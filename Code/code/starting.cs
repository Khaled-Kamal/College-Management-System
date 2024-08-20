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
    public partial class starting : Form
    {
        public starting()
        {
            InitializeComponent();
        }
        int startpos = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            startpos += 1;
            loading.Value = startpos;
            if (loading.Value == 100)
            {
                loading.Value = 0;
                timer1.Stop();
                Login log = new Login();
                log.Show();
                this.Hide();

            }

        }

        private void gunaCircleProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void gunaProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void starting_Load(object sender, EventArgs e)
        {
            timer1.Start();

        }

        
    }
}
