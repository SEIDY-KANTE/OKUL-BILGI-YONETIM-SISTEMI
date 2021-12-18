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
    public partial class Class : Form
    {
        public Class()
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        MethodsClass infosClass = new MethodsClass();
        private void Class_Load(object sender, EventArgs e)
        {
           
                DataTable courseList = infosClass.getList(new NpgsqlCommand("select \"courseId\" from course"));
                DataTable teacherList = infosClass.getList(new NpgsqlCommand("select \"teacherId\" from teacher"));
                DataTable RoomList = infosClass.getList(new NpgsqlCommand("select \"roomId\" from room"));
                foreach (DataRow dr in courseList.Rows)
                {
                    course_comboBox.Items.Add(dr["courseId"].ToString());
                }
                foreach (DataRow dr in teacherList.Rows)
                {
                    teacher_comboBox.Items.Add(dr["teacherId"].ToString());
                }
               
                foreach (DataRow dr in RoomList.Rows)
                {
                    room_comboBox.Items.Add(dr["roomId"].ToString());
                }
        }

        bool verify()
        {
            if (id_textBox.Text=="" || clsName_textBox.Text=="" || course_comboBox.Text=="" || teacher_comboBox.Text=="" || room_comboBox.Text=="" || credit_numericUpDown.Value==0 )
            {
                return false;
            }
            else
                return true;
        }
        void showTable()
        {
            dataGridView_class.DataSource = infosClass.getList(new NpgsqlCommand("select \"classId\",\"className\",\"courseName\",credit,\"teacherNo\",\"firstName\",\"lastName\",description from class inner join teacher on \"teacherId\"=\"teacherNo\" inner join course on \"courseId\"=\"courseNo\""));

        }

        bool checkClass(int id)
        {
            DataTable table = infosClass.getList(new NpgsqlCommand("SELECT * from public.class WHERE \"classId\"= '" + id + "'"));
            if (table.Rows.Count > 0)
                return true;
            return false;
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            if (verify()) 
            { 

                int id = Convert.ToInt32(id_textBox.Text);
                string name = clsName_textBox.Text;
                int courseNo = Convert.ToInt32(course_comboBox.Text);
                int teacherNo = Convert.ToInt32(teacher_comboBox.Text);
                int roomNo = Convert.ToInt32(room_comboBox.Text);
                int credit = Convert.ToInt32(credit_numericUpDown.Value);
                string description = description_textBox.Text;
                try
                {
                    if (!checkClass(id))
                    {
                        if (infosClass.insertClass(id, name, courseNo, teacherNo, roomNo, credit, description))
                        {
                            showTable();
                            MessageBox.Show("New class Added", "Add Class", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                        MessageBox.Show("Your id exists already, please change your class Id", "Exist Id", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Field", "Add Class" , MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void update_button_Click(object sender, EventArgs e)
        {
            if (verify())
            {
                int id = Convert.ToInt32(id_textBox.Text);
                string name = clsName_textBox.Text;
                int courseNo = Convert.ToInt32(course_comboBox.Text);
                int teacherNo = Convert.ToInt32(teacher_comboBox.Text);
                int roomNo = Convert.ToInt32(room_comboBox.Text);
                int credit = Convert.ToInt32(credit_numericUpDown.Value);
                string description = description_textBox.Text;

                try
                {
                    if(checkClass(id))
                    {
                        if (infosClass.updateClass(id,name,courseNo,teacherNo,roomNo,credit,description))
                        {
                            showTable();
                            MessageBox.Show("Class data updated", "Update Class", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Your id doesn't exists", "Wrong Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Empty Field", "Update Class", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void delete_button_Click(object sender, EventArgs e)
        {

            int id;
            if (id_textBox.Text != "")
            {
                id = Convert.ToInt32(id_textBox.Text);
                try
                {
                    DataTable table = infosClass.getList(new NpgsqlCommand("SELECT * FROM class WHERE \"classId\"= '" + id + "'"));
                    if (table.Rows.Count > 0)
                    {
                        //Show a confirmation message before delete the class
                        if (MessageBox.Show("Are you sure you want to remove this class", "Remove class", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (infosClass.deleteClass(id))
                            {
                                showTable();
                                MessageBox.Show("class Removed", "Remove Class", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                clear_button.PerformClick();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Your id doesn't exist", "Wrong Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("You must enter the class's id", "Delete Class", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        private void Clear_button_Click(object sender, EventArgs e)
        {
            id_textBox.Text = "";
            clsName_textBox.Text = "";
            course_comboBox.Text = "";
            teacher_comboBox.Text = "";
            room_comboBox.Text = "";
            credit_numericUpDown.Value = 0;
            description_textBox.Text = "";
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            dataGridView_class.DataSource = infosClass.search(search_textBox.Text,"class");
        }

      
    }
}