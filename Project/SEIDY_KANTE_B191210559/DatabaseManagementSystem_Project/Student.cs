using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace DatabaseManagementSystem_Project
{
    public partial class Student : Form
    {
        DBconnect db = new DBconnect();
        MethodsClass student = new MethodsClass();
        public Student()
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
        private void Student_Load(object sender, EventArgs e)
        {

            DataTable countryList = student.getList(new NpgsqlCommand("select \"countryName\" from country"));
            DataTable regionList = student.getList(new NpgsqlCommand("select \"regionName\" from region"));
            DataTable townList = student.getList(new NpgsqlCommand("select \"townName\" from town"));
            foreach (DataRow dr in countryList.Rows)
            {
                country_comboBox.Items.Add(dr["countryName"].ToString());
            }
            foreach (DataRow dr in regionList.Rows)
            {
                region_comboBox.Items.Add(dr["regionName"].ToString());
            }
            foreach (DataRow dr in townList.Rows)
            {
                town_comboBox.Items.Add(dr["townName"].ToString());
            }

        }
        bool verify()
        {
            if ((id_textBox.Text == "") || (fName_textBox.Text == "") || (lName_textBox.Text == "") ||
                (gender_comboBox.Text == "") || (level_comboBox.Text == "") || (pictureBox_student.Image == null || phone_textBox.Text=="" || email_textBox.Text==""||
                country_comboBox.Text==""|| region_comboBox.Text == ""|| town_comboBox.Text=="" ))
            {
                return false;
            }
            else
                return true;
        }
        public void showTable()
        {
            DataGridView_student.DataSource = student.search("", "student");
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[6];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }
        private void button_upload_Click(object sender, EventArgs e)
        {
            // browse photo from your computer
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
                pictureBox_student.Image = Image.FromFile(opf.FileName);
        }
        //To get addressNo from data base
        public static int adrs;
        public static int address
        {
            get { return adrs; }
            set { adrs = value; }
        }

        bool checkStudentId(int id)
        {
            DataTable table = student.getList(new NpgsqlCommand("SELECT * FROM student WHERE \"stdId\"= '" + id + "'"));
            if (table.Rows.Count > 0)
                return true;
            return false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // add new student
            string fname = fName_textBox.Text;
            string lname = lName_textBox.Text;
            DateTime bdate = dateTimePicker1.Value;
            string level = level_comboBox.Text;
            string gender = gender_comboBox.Text;

            //contact
            string tel = phone_textBox.Text;
            string em = email_textBox.Text;

            //To get addressNo from data base
            int adrsNo;
            DataTable townNo = student.getList(new NpgsqlCommand("select \"townId\" from town where \"townName\"= '" + town_comboBox.Text + "'"));
            foreach (DataRow dr in townNo.Rows)
            {
                address = Convert.ToInt32(dr["townId"]);
            }

            //we need to check student age between 10 and 100
            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year) < 10 || (this_year - born_year) > 100)
            {
                MessageBox.Show("The student age must be between 10 and 100", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {
                int stdId = Convert.ToInt32(id_textBox.Text);
                adrsNo = adrs;
                try
                {
                    if (!checkStudentId(stdId))
                    {
                        // to get photo from picture box
                        MemoryStream ms = new MemoryStream();
                        pictureBox_student.Image.Save(ms, pictureBox_student.Image.RawFormat);
                        byte[] img = ms.ToArray();
                        if (student.insertPerson(stdId, fname, lname, bdate, gender, level, img, tel, em, adrsNo))
                        {
                            showTable();
                            MessageBox.Show("New Student Added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                       
                    }
                    else
                        MessageBox.Show("Your id exists already, please change your student Id", "Exist Id", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Field", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            id_textBox.Text = "";
            fName_textBox.Text = "";
            lName_textBox.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            gender_comboBox.Text = "";
            level_comboBox.Text = "";
            pictureBox_student.Image = null;
            phone_textBox.Text = "";
            email_textBox.Text = "";
            country_comboBox.Text = "";
            region_comboBox.Text = "";
            town_comboBox.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // update student record
            string fname = fName_textBox.Text;
            string lname = lName_textBox.Text;
            DateTime bdate = dateTimePicker1.Value;
            string level = level_comboBox.Text;
            string gender = gender_comboBox.Text;

            //contact
            string tel = phone_textBox.Text;
            string em = email_textBox.Text;

            //To get addressNo from data base
            int adrsNo;
            DataTable townNo = student.getList(new NpgsqlCommand("select \"townId\" from town where \"townName\"= '" + town_comboBox.Text + "'"));
            foreach (DataRow dr in townNo.Rows)
            {
                address = Convert.ToInt32(dr["townId"]);
            }

            //we need to check student age between 10 and 100

            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year) < 10 || (this_year - born_year) > 100)
            {
                MessageBox.Show("The student age must be between 10 and 100", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {
                int stdId = Convert.ToInt32(id_textBox.Text);
                adrsNo = adrs;
                try
                {
                    
                    if (checkStudentId(stdId))
                    {
                        // to get photo from picture box
                        MemoryStream ms = new MemoryStream();
                        pictureBox_student.Image.Save(ms, pictureBox_student.Image.RawFormat);
                        byte[] img = ms.ToArray();
                        if (student.updatePerson(stdId,fname, lname, bdate, gender, level, img,tel,em, adrsNo))
                        {
                            showTable();
                            MessageBox.Show("Student data updated", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            {
                MessageBox.Show("Empty Field", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            //remove the selected Student
            int id;
            if (id_textBox.Text != "")
            {
                id = Convert.ToInt32(id_textBox.Text);
                try
                {
                    DataTable table = student.getList(new NpgsqlCommand("SELECT * FROM student WHERE \"stdId\"= '" + id + "'"));
                    if (table.Rows.Count > 0)
                    {
                        //Show a confirmation message before delete the student
                        if (MessageBox.Show("Are you sure you want to remove this student", "Remove Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (student.deletePerson(id))
                            {
                                showTable();
                                MessageBox.Show("Student Removed", "Remove student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Clear_button.PerformClick();
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
                MessageBox.Show("You must enter the student's id", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
          
        }
        //search function
        private void button5_Click(object sender, EventArgs e)
        {
            DataGridView_student.DataSource = student.search(textBox_search.Text);
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)DataGridView_student.Columns[6]; 
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

    }
}