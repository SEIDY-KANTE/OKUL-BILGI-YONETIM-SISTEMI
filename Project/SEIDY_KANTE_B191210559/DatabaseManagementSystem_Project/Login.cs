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
    public partial class Login : Form
    {
        MethodsClass dataUser = new MethodsClass();
        public Login()
        {
            InitializeComponent();
        }
        private void login_button_Click(object sender, EventArgs e)
        {
           
            if(UserName_textBox.Text=="" || Password_textBox.Text == "")
            {
                MessageBox.Show("Missing Information","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UserName_textBox.BackColor = Color.Red;
                Password_textBox.BackColor = Color.Red;
                reset_button.Visible = true;

            }
            else
            {
                string username = UserName_textBox.Text;
                string password = Password_textBox.Text;
                DataTable table = dataUser.getList(new NpgsqlCommand("SELECT * FROM \"adminUser\" WHERE \"username\"= '" + username + "' AND \"password\"='" + password + "'"));
                if (table.Rows.Count > 0)
                {
                    Splash splashPage = new Splash();
                    this.Hide();
                    splashPage.Show();
                }
                else
                {
                    MessageBox.Show("Your username and password are not exists", "Wrong Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    incorrect_label.Visible = true;
                    reset_button.Visible = true;
                }
            }
            
        }
        private void reset_button_Click(object sender, EventArgs e)
        {
            manageAdmin managementUser = new manageAdmin();
            this.Hide();
            managementUser.Show();
        }
        private void Exit_label_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Exit_label_MouseMove(object sender, MouseEventArgs e)
        {
            Exit_label.ForeColor = Color.Red;
        }

        private void Exit_label_MouseLeave(object sender, EventArgs e)
        {
            Exit_label.ForeColor = Color.White;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            newAdminUser newAdminPage = new newAdminUser();
            this.Hide();
            newAdminPage.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            incorrect_label.ForeColor = Color.Red;
            incorrect_label.Visible = false;
            reset_button.Visible = false;

        }

       
    }
}
