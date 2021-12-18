using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using Npgsql;

namespace DatabaseManagementSystem_Project
{
    public partial class Score : Form
    {
        public Score()
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

        MethodsClass infosScore = new MethodsClass();
        private void Score_Load(object sender, EventArgs e)
        {
            DataTable classList = infosScore.getList(new NpgsqlCommand("select \"classId\" from class"));
            DataTable studentLis = infosScore.getList(new NpgsqlCommand("select \"stdId\" from student"));

            foreach (DataRow dr in classList.Rows)
            {
                classNo_comboBox.Items.Add(dr["classId"].ToString());
            }
            foreach (DataRow dr in studentLis.Rows)
            {
                studentNo_comboBox.Items.Add(dr["stdId"].ToString());
            }
           
        }
        private void show()
        {
            dataGridView_score.DataSource = infosScore.getList(new NpgsqlCommand("SELECT \"studentNo\",\"firstName\", \"lastName\",\"classNo\",\"studentScore\",description FROM student INNER JOIN score ON \"studentNo\"=\"stdId\" "));
        }
        private void save_button_Click(object sender, EventArgs e)
        {
            if (studentNo_comboBox.Text == "" || classNo_comboBox.Text == "")
            {
                MessageBox.Show("Need score data", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int stdNo = Convert.ToInt32(studentNo_comboBox.Text);
                int clsNo = Convert.ToInt32(classNo_comboBox.Text);
                double scor = Convert.ToDouble(Score_numericUpDown.Value);
                string desc = Description_textBox.Text;

                if (infosScore.studentIsEnrolled(stdNo,clsNo))
                {
                    if (!infosScore.checkScore(stdNo, clsNo))
                    {
                        try
                        {
                            if (infosScore.insertScore(stdNo, clsNo, scor, desc))
                            {
                                show();
                                clear_button.PerformClick();
                                MessageBox.Show("New score added", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else
                            {
                                MessageBox.Show("Score not added", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("The score for this class are alerady exists", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("this student is not yet enrolled.", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }
        private void update_button_Click(object sender, EventArgs e)
        {
            if (studentNo_comboBox.Text == "" || classNo_comboBox.Text == "")
            {
                MessageBox.Show("Need score data", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int stdNo = Convert.ToInt32(studentNo_comboBox.Text);
                int clsNo = Convert.ToInt32(classNo_comboBox.Text);
                double scor = Convert.ToDouble(Score_numericUpDown.Value);
                string desc = Description_textBox.Text;
                try
                {
                    if (infosScore.checkScore(stdNo, clsNo))
                    {
                        if (infosScore.updateScore(stdNo, clsNo, scor, desc))
                        {
                            show();
                            clear_button.PerformClick();
                            MessageBox.Show("Score data updated", "Update Score", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("Score not added", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Your id or/and class doesn't/arent't exists", "Wrong Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void delete_button_Click(object sender, EventArgs e)
        {
            if (studentNo_comboBox.Text == "" || classNo_comboBox.Text == "")
            {
                MessageBox.Show("Field Error- We need Student No and Class No", "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int stdNo = Convert.ToInt32(studentNo_comboBox.Text);
                int clsNo = Convert.ToInt32(classNo_comboBox.Text);

                try
                {
                    if (infosScore.checkScore(stdNo, clsNo))
                    {
                        if (MessageBox.Show("Are you sure you want to remove this score", "Delete Score", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (infosScore.deleteScore(stdNo, clsNo))
                            {
                                show();
                                MessageBox.Show("Score Removed", "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                clear_button.PerformClick();
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Your id or/and class doesn't/arent't exists", "Wrong Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void search_button_Click(object sender, EventArgs e)
        {
            dataGridView_score.DataSource = infosScore.search(search_textBox.Text, "score");
        }

        private void clear_Click(object sender, EventArgs e)
        {
            studentNo_comboBox.Text = "";
            classNo_comboBox.Text = "";
            Score_numericUpDown.Value = 0;
            Description_textBox.Text = "";
        }
    }
}