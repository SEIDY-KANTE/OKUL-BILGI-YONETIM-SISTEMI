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
    public partial class Teacher : Form
    {
        public Teacher()
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
        MethodsClass teacher= new MethodsClass();

        private void Teacher_Load(object sender, EventArgs e)
        {
            DataTable countryList = teacher.getList(new NpgsqlCommand("select \"countryName\" from country"));
            DataTable regionList = teacher.getList(new NpgsqlCommand("select \"regionName\" from region"));
            DataTable townList = teacher.getList(new NpgsqlCommand("select \"townName\" from town"));
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
            if ((id_textBox.Text == "") || (firstName_textBox.Text == "") || (lastName_textBox.Text == "") ||
                (gender_comboBox.Text == "") || (qualification_comboBox.Text == "") || (pictureBox_teacher.Image == null || phone_textBox.Text == "" || email_textBox.Text == "" ||
                country_comboBox.Text == "" || region_comboBox.Text == "" || town_comboBox.Text == ""))
            {
                return false;
            }
            else
                return true;
        }
        public void showTable()
        {
            dataGridView_teacher.DataSource = teacher.search("", "teacher");
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)dataGridView_teacher.Columns[6];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Login LoginPage = new Login();
            this.Hide();
            LoginPage.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // browse photo from your computer
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Photo(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
                pictureBox_teacher.Image = Image.FromFile(opf.FileName);
        }
        //get addressNo from data base
        public static int adrs;
        public static int address
        {
            get { return adrs; }
            set { adrs = value; }
        }

        bool checkTeacherId(int id)
        {
            DataTable table = teacher.getList(new NpgsqlCommand("SELECT * FROM teacher WHERE \"teacherId\"= '" + id + "'"));
            if (table.Rows.Count > 0)
                return true;
            return false;
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            // add new teacher
            string fname = firstName_textBox.Text;
            string lname = lastName_textBox.Text;
            DateTime bdate = dateTimePicker1.Value;
            string qualification = qualification_comboBox.Text;
            string gender = gender_comboBox.Text;

            //contact
            string tel = phone_textBox.Text;
            string em = email_textBox.Text;

            //adresse
            //To get addressNo from data base
            int adrsNo;
            DataTable townNo = teacher.getList(new NpgsqlCommand("select \"townId\" from town where \"townName\"= '" + town_comboBox.Text + "'"));
            foreach (DataRow dr in townNo.Rows)
            {
                address = Convert.ToInt32(dr["townId"]);
            }
            //we need to check teacher age between 25 and 100

            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year) < 25 || (this_year - born_year) > 100)
            {
                MessageBox.Show("The teacher age must be between 25 and 100", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {
                int id = Convert.ToInt32(id_textBox.Text);
                adrsNo = adrs;

                try
                {
                    if (!checkTeacherId(id))
                    {
                        // to get photo from picture box
                        MemoryStream ms = new MemoryStream();
                        pictureBox_teacher.Image.Save(ms, pictureBox_teacher.Image.RawFormat);
                        byte[] img = ms.ToArray();
                        if (teacher.insertPerson(id, fname, lname, bdate, gender, qualification, img, tel, em, adrsNo, "teacher"))
                        {
                            showTable();
                            MessageBox.Show("New Teacher Added", "Add Teacher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                        MessageBox.Show("Your id exists already, please change your teacher Id", "Exist Id", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Field", "Add Teacher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Update_button_Click(object sender, EventArgs e)
        {
            // update teacher record
            string fname = firstName_textBox.Text;
            string lname = lastName_textBox.Text;
            DateTime bdate = dateTimePicker1.Value;
            string qualification = qualification_comboBox.Text;
            string gender = gender_comboBox.Text;

            //contact
            string tel = phone_textBox.Text;
            string em = email_textBox.Text;

            //adresse
            //To get addressNo from data base
            int adrsNo;
            DataTable townNo = teacher.getList(new NpgsqlCommand("select \"townId\" from town where \"townName\"= '" + town_comboBox.Text + "'"));
            foreach (DataRow dr in townNo.Rows)
            {
                address = Convert.ToInt32(dr["townId"]);
            }

            //we need to check teacher age between 25 and 100

            int born_year = dateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            if ((this_year - born_year) < 25 || (this_year - born_year) > 100)
            {
                MessageBox.Show("The teacher age must be between 25 and 100", "Invalid Birthdate", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verify())
            {
                int teacherId = Convert.ToInt32(id_textBox.Text);
                adrsNo = adrs;
                try
                {
                   
                    if (checkTeacherId(teacherId))
                    {
                        // to get photo from picture box
                        MemoryStream ms = new MemoryStream();
                        pictureBox_teacher.Image.Save(ms, pictureBox_teacher.Image.RawFormat);
                        byte[] img = ms.ToArray();
                        if (teacher.updatePerson(teacherId, fname, lname, bdate, gender, qualification, img, tel, em, adrsNo, "teacher"))
                        {
                            showTable();
                            MessageBox.Show("Teacher data updated", "Update Teacher", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Empty Field", "Update Teacher", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void Delete_button_Click(object sender, EventArgs e)
        {
            //remove the selected teacher
            int id;
            if (id_textBox.Text != "")
            {
                id = Convert.ToInt32(id_textBox.Text);
                try
                {
                    DataTable table = teacher.getList(new NpgsqlCommand("SELECT * FROM teacher WHERE \"teacherId\"= '" + id + "'"));
                    if (table.Rows.Count > 0)
                    {
                        //Show a confirmation message before delete the teacher
                        if (MessageBox.Show("Are you sure you want to remove this teacher", "Remove teacher", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (teacher.deletePerson(id, "teacher"))
                            {
                                showTable();
                                MessageBox.Show("Teacher Removed", "Remove teacher", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("You must enter the teacher's id", "Delete teacher", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        //clear function
        private void Clear_button_Click(object sender, EventArgs e)
        {
            id_textBox.Text = "";
            firstName_textBox.Text = "";
            lastName_textBox.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            gender_comboBox.Text = "";
            qualification_comboBox.Text = "";
            pictureBox_teacher.Image = null;
            phone_textBox.Text = "";
            email_textBox.Text = "";
            country_comboBox.Text = "";
            region_comboBox.Text = "";
            town_comboBox.Text = "";
        }

        //search function
        private void search_button_Click(object sender, EventArgs e)
        {
            dataGridView_teacher.DataSource = teacher.search(textBox_search.Text,"teacher");
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)dataGridView_teacher.Columns[6];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

    }
}