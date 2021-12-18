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
    public partial class Enroll : Form
    {
        public Enroll()
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

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Login LoginPage = new Login();
            this.Hide();
            LoginPage.Show();
        }
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        MethodsClass infosEnrollment = new MethodsClass();

        private void Enroll_Load(object sender, EventArgs e)
        {
            DataTable studentList = infosEnrollment.getList(new NpgsqlCommand("select \"stdId\" from student"));
            DataTable classList = infosEnrollment.getList(new NpgsqlCommand("select \"classId\" from class"));

            foreach (DataRow dr in studentList.Rows)
            {
                studentNo_comboBox.Items.Add(dr["stdId"].ToString());
            }
            foreach (DataRow dr in classList.Rows)
            {
                classNo_comboBox.Items.Add(dr["classId"].ToString());
            }
         
        }

        bool verify()
        {
            if (enrollmentid_textBox.Text=="" || studentNo_comboBox.Text == "" || classNo_comboBox.Text == "" || semestry_comboBox.Text == "")
                return false;
            return true;
        }
        void showTable()
        {
            dataGridView_enroll.DataSource = infosEnrollment.getList(new NpgsqlCommand("select  \"enrollmentId\", \"studentId\", \"firstName\",\"lastName\",\"className\",\"semestry\",\"enrollmentDate\" from enroll right join student on \"stdId\"=\"studentId\" right join class on \"classId\"=\"classNo\" "));

        }
        private void save_button_Click(object sender, EventArgs e)
        {
            if (verify())
            {
                int enrollid = Convert.ToInt32(enrollmentid_textBox.Text);
                int studentid = Convert.ToInt32(studentNo_comboBox.Text);
                int classNo = Convert.ToInt32(classNo_comboBox.Text);
                string semestry = semestry_comboBox.Text;
                string description = description_textBox.Text;
                try
                {
                    if (infosEnrollment.insertEnroll(enrollid,studentid,classNo,semestry,description))
                    {
                        showTable();
                        MessageBox.Show("New enrollment Added", "Add Enroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Field", "Add Enroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void update_button_Click(object sender, EventArgs e)
        {
            if (verify())
            {
                int enrollid = Convert.ToInt32(enrollmentid_textBox.Text);
                int studentid = Convert.ToInt32(studentNo_comboBox.Text);
                int classNo = Convert.ToInt32(classNo_comboBox.Text);
                string semestry = semestry_comboBox.Text;
                string description = description_textBox.Text;

                try
                {
                    DataTable table = infosEnrollment.getList(new NpgsqlCommand("SELECT * FROM enroll WHERE \"enrollmentId\"= '" + enrollid + "'"));
                    if (table.Rows.Count > 0)
                    {
                        if (infosEnrollment.updateEnroll(enrollid, studentid, classNo, semestry, description))
                        {
                            showTable();
                            MessageBox.Show("Enrollment data updated", "Update Enroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Your enrollment id doesn't exists", "Wrong Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Empty Field", "Update Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            int id;
            if (enrollmentid_textBox.Text != "")
            {
                id = Convert.ToInt32(enrollmentid_textBox.Text);
                try
                {
                    DataTable table = infosEnrollment.getList(new NpgsqlCommand("SELECT * FROM enroll WHERE \"enrollmentId\"= '" + id + "'"));
                    if (table.Rows.Count > 0)
                    {
                        //Show a confirmation message before delete the enrollment
                        if (MessageBox.Show("Are you sure you want to remove this enrollment", "Remove enrollment", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (infosEnrollment.deleteEnroll(id))
                            {
                                showTable();
                                MessageBox.Show("Enrollment Removed", "Remove Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                clear_button.PerformClick();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Your enrollment id doesn't exist", "Wrong Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("You must enter the enrollment's id", "Delete Enrollment", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        private void clear_button_Click(object sender, EventArgs e)
        {
            enrollmentid_textBox.Text = "";
            studentNo_comboBox.Text = "";
            classNo_comboBox.Text = "";
            semestry_comboBox.Text = "";
            description_textBox.Text = "";
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            dataGridView_enroll.DataSource = infosEnrollment.search(search_textBox.Text, "enroll");
           
        }
    }
}