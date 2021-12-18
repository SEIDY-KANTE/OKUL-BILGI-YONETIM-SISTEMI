using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseManagementSystem_Project
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Home HomePage = new Home();
            this.Hide();
            HomePage.Show();
        }
        private void label2_Click(object sender, EventArgs e)
        {
            Student studentPage = new Student();
            this.Hide();
            studentPage.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Teacher teacherPage = new Teacher();
            this.Hide();
            teacherPage.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Class ClassPage = new Class();
            this.Hide();
            ClassPage.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Enroll EnrollPage = new Enroll();
            this.Hide();
            EnrollPage.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Department DepartmentPage = new Department();
            this.Hide();
            DepartmentPage.Show();
        }
        private void label7_Click(object sender, EventArgs e)
        {
            Score ScorePage = new Score();
            this.Hide();
            ScorePage.Show();
        }
        private void label8_Click(object sender, EventArgs e)
        {
            Dashboard DashboardPage = new Dashboard();
            this.Hide();
            DashboardPage.Show();
        }

        

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Login LoginPage = new Login();
            this.Hide();
            LoginPage.Show();
        }

        private void pictureBox10_MouseMove(object sender, MouseEventArgs e)
        {
            Exit_pictureBox.BackColor = Color.Red;
        }

        private void Exit_pictureBox_MouseLeave(object sender, EventArgs e)
        {
            Exit_pictureBox.BackColor = Color.Transparent;
        }

        private void CountryRegistration_label_Click(object sender, EventArgs e)
        {
            countryRegistration cntryRegistrationPage = new countryRegistration();
            this.Hide();
            cntryRegistrationPage.Show();
        }

    }
}
