using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Budget_Allocation
{
    public partial class form1 : Form
    {
        functions Con;

        public form1()
        {
            InitializeComponent();
            Con = new functions();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
       
        private void loginBtn_Click(object sender, EventArgs e)
        {
            if(uname.Text == "" || uPass.Text == "")
            {
                MessageBox.Show("Missing info !!");
            }
            else 
            {
                if (uname.Text == "Admin" && uPass.Text == "admin")
                {
                    home obj = new home();
                    obj.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("incorrect username & password");
                }
            }
        }
    }
}
