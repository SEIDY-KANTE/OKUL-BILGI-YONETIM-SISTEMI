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
    public partial class manageAdmin : Form
    {
        newAdminClass infosAdmin = new newAdminClass();
        MethodsClass dataUser = new MethodsClass();
        public manageAdmin()
        {
            InitializeComponent();
        }
        bool verify()
        {
            if (userName_textBox.Text == "" || password_textBox.Text == "")
                return false;
            else return true;
        }
        private void update_button_Click(object sender, EventArgs e)
        {
            if (verify() && newPassword_textBox.Text != "")
            {
                string userName = userName_textBox.Text;
                string password = password_textBox.Text;
                string newPassword = newPassword_textBox.Text;
               
                try
                {
                    DataTable table = dataUser.getList(new NpgsqlCommand("SELECT * FROM \"adminUser\" WHERE \"username\"= '" + userName + "' AND \"password\"= '" + password + "'"));
                    if (table.Rows.Count > 0)
                    {
                        if (infosAdmin.updateAdmin(userName,password,newPassword))
                        {
                            MessageBox.Show("User data updated", "Update User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Login loginPage = new Login();
                            this.Hide();
                            loginPage.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("This user doesn't exists", "Wrong User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Empty Field", "Update User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void delete_button_Click(object sender, EventArgs e)
        {
            if (verify())
            {
                string userName = userName_textBox.Text;
                string password = password_textBox.Text;
               
                try
                {
                    DataTable table = dataUser.getList(new NpgsqlCommand("SELECT * FROM \"adminUser\" WHERE \"username\"= '" + userName + "' AND \"password\"= '" + password + "'"));
                    if (table.Rows.Count > 0)
                    {
                        if (infosAdmin.deleteAdmin(userName, password))
                        {
                            MessageBox.Show("User data deleted", "Delete User", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Login loginPage = new Login();
                            this.Hide();
                            loginPage.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("This user doesn't exists", "Wrong User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Empty Field", "Delete User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void Exit_label_MouseMove(object sender, MouseEventArgs e)
        {
            Exit_label.ForeColor = Color.Red;

        }
        private void Exit_label_MouseLeave(object sender, EventArgs e)
        {
            Exit_label.ForeColor = Color.White;
        }
        private void Exit_label_Click(object sender, EventArgs e)
        {
            Login loginPage = new Login();
            this.Hide();
            loginPage.Show();
        }

       
    }
}
