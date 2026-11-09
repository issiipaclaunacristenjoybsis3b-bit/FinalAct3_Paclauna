using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Paclauna_FinalAct3
{
    public partial class Register_Form : Form
    {
        public Register_Form()
        {
            InitializeComponent();
            passWord.UseSystemPasswordChar = true;
            confirmPass.UseSystemPasswordChar = true;
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            string fullName = txtfullName.Text.Trim();
            string username = txtuserName.Text.Trim();
            string password = passWord.Text;
            string confirmPassword = confirmPass.Text;

            if (fullName == "" || username == "" || password == "" || confirmPassword == "")
            {
                MessageBox.Show("Please fill all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (OleDbConnection con = Database.GetConnection())
                {
                    con.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = ?";
                    using (OleDbCommand checkCmd = new OleDbCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("?", username);
                        int userCount = (int)checkCmd.ExecuteScalar();

                        if (userCount > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    string passwordHash = PasswordHelper.HashPassword(password);
                    string insertQuery = "INSERT INTO Users (FullName, Username, [Password]) VALUES (?, ?, ?)";

                    using (OleDbCommand insertCmd = new OleDbCommand(insertQuery, con))
                    {
                        insertCmd.Parameters.AddWithValue("?", fullName);
                        insertCmd.Parameters.AddWithValue("?", username);
                        insertCmd.Parameters.AddWithValue("?", passwordHash);


                        int rowsAffected = insertCmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SignIn_Form signin = new SignIn_Form();
                            signin.Show();
                            this.Hide();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while registering the user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linksignIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignIn_Form signIn = new SignIn_Form();
            signIn.Show();
            this.Hide();

        }

        private void Register_Form_Load(object sender, EventArgs e)
        {

        }
    }
}
