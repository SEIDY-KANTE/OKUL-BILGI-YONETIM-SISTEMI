using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace DatabaseManagementSystem_Project
{
    public partial class newAdminUser : Form
    {
        newAdminClass newAdmin = new newAdminClass();
        MethodsClass dataUser = new MethodsClass();
        public newAdminUser()
        {
            InitializeComponent();
        }
        bool verify()
        {
            if (UserName_textBox.Text == "" || Password_textBox.Text == "")
                return false;
            else return true;
        }
        private void create_button_Click(object sender, EventArgs e)
        {
            string userName = UserName_textBox.Text;
            string password = Password_textBox.Text;

            if (verify() && (PassAgain_textBox.Text != ""))
            {
                try
                {
                    DataTable table = dataUser.getList(new NpgsqlCommand("SELECT * FROM \"adminUser\" WHERE \"username\"= '" + userName + "'"));

                    if (table.Rows.Count > 0)
                    {
                        MessageBox.Show("Username already exists", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        if (newAdmin.insertAdmin(userName, password))
                        {
                            MessageBox.Show("New user added", "New Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Login loginPage = new Login();
                            this.Hide();
                            loginPage.Show();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if(password != PassAgain_textBox.Text)
            {
                MessageBox.Show("Passwords don't match", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Missing Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void Exit_label_Click(object sender, EventArgs e)
        {
            Login loginPage = new Login();
            this.Hide();
            loginPage.Show();
        }

        private void Exit_label_MouseMove(object sender, MouseEventArgs e)
        {
            Exit_label.ForeColor = Color.Red;
          
        }

        private void Exit_label_MouseLeave(object sender, EventArgs e)
        {
            Exit_label.ForeColor = Color.White;
        }
    }
}
