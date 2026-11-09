using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paclauna_FinalAct3
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SignIn_Form signIn = new SignIn_Form();
                signIn.Show();
                this.Hide();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
        }

        private void button9_Click(object sender, EventArgs e) { }

    private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }
    } }
