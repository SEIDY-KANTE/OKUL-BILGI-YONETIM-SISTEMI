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
    public partial class Department : Form
    {
        public Department()
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

        MethodsClass school = new MethodsClass();

        void updateComboxValues()
        {
            //clear_button.PerformClick();

            //School address
            DataTable countryList = school.getList(new NpgsqlCommand("select \"countryName\" from country"));
            DataTable regionList = school.getList(new NpgsqlCommand("select \"regionName\" from region"));
            DataTable townList = school.getList(new NpgsqlCommand("select \"townName\" from town"));
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

            //schoolNo for faculty and building registration
            DataTable schoolNo = school.getList(new NpgsqlCommand("select \"schoolId\", \"schoolName\" from school"));
            foreach (DataRow dr in schoolNo.Rows)
            {
                schlNo_comboBox.Items.Add(dr["schoolId"].ToString());
                schlNo_Buil_comboBox.Items.Add(dr["schoolId"].ToString()); //for building
            }
            //facultyNo for department registration
            DataTable facultyNo = school.getList(new NpgsqlCommand("select \"facultyId\",\"facultyName\" from faculty"));
            foreach (DataRow dr in facultyNo.Rows)
            {
                facNo_comboBox.Items.Add(dr["facultyId"].ToString());
            }
            //departmentNo for course registration
            DataTable departmentNo = school.getList(new NpgsqlCommand("select \"departmentId\",\"departmentName\" from department"));
            foreach (DataRow dr in departmentNo.Rows)
            {
                departNo_comboBox.Items.Add(dr["departmentId"].ToString());
            }
            //buildingNO for room registration
            DataTable buildingNo = school.getList(new NpgsqlCommand("select \"buildingId\",\"buildingName\" from building"));
            foreach (DataRow dr in buildingNo.Rows)
            {
                builNo_comboBox.Items.Add(dr["buildingId"].ToString());
            }
        }

        private void Department_Load(object sender, EventArgs e)
        {
            updateComboxValues();
        }

        bool verify(bool isDelete = false)
        {
            if (school_radioButton.Checked == true)
            {
                operationType = "school";
                if (isDelete && schoolId_textBox.Text != "")
                    return true;

                else if ((schoolId_textBox.Text == "") || (schoolName_textBox.Text == "") || phone_textBox.Text == "" || email_textBox.Text == "" ||
                    country_comboBox.Text == "" || region_comboBox.Text == "" || town_comboBox.Text == "")
                {
                    return false;
                }
                else
                    return true;
            }
            else if (faculty_radioButton.Checked == true)
            {
                operationType = "faculty";
                if (isDelete && facultyId_textBox.Text != "")
                    return true;
                else if (facultyId_textBox.Text == "" || facultyName_textBox.Text == "" || schlNo_comboBox.Text == "")
                {
                    return false;
                }
                else
                    return true;
            }
            else if (department_radioButton.Checked == true)
            {
                operationType = "department";
                if (isDelete && departmentId_textBox.Text != "")
                    return true;
                else if (departmentId_textBox.Text == "" || deprtName_textBox.Text == "" || facNo_comboBox.Text == "")
                {
                    return false;
                }
                else
                    return true;
            }
            else if (course_radioButton.Checked == true)
            {
                operationType = "course";
                if (isDelete && courseId_textBox.Text != "")
                    return true;
                else if (courseId_textBox.Text == "" || courseName_textBox.Text == "" || departNo_comboBox.Text == "")
                {
                    return false;
                }
                else
                    return true;
            }
            else if (building_radioButton.Checked == true)
            {
                operationType = "building";
                if (isDelete && buildingId_textBox.Text != "")
                    return true;
                else if (buildingId_textBox.Text == "" || buildingName_textBox.Text == "" || schlNo_Buil_comboBox.Text == "")
                {
                    return false;
                }
                else
                    return true;
            }
            else if (room_radioButton.Checked == true)
            {
                operationType = "room";
                if (isDelete && roomId_textBox.Text != "")
                    return true;
                else if (roomId_textBox.Text == "" || roomName_textBox.Text == "" || builNo_comboBox.Text == "")
                {
                    return false;
                }
                else
                    return true;
            }
            else
                return false;

        }

        public void showTable(string type = "school")
        {
            dataGridView_school.DataSource = school.search("", type); // To show values in datagridview after operation.
        }
        string operationType = string.Empty;

        void commonSaveOperation(int id, string name, int fkey, string type)
        {
            try
            {
                if (school.insertSchoolElement(id, name, fkey, operationType))
                {
                    showTable(operationType);
                    MessageBox.Show("New " + operationType + " Added", "Add " + operationType, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //get addressNo from data base
        public static int adrs;
        public static int address
        {
            get { return adrs; }
            set { adrs = value; }
        }
        private void save_button_Click(object sender, EventArgs e)
        {
            //adresse
            //To get addressNo from data base
            int adrsNo;
            DataTable adrNo = school.getList(new NpgsqlCommand("select \"townId\" from town where \"townName\"= '" + town_comboBox.Text + "'"));
            foreach (DataRow dr in adrNo.Rows)
            {
                address = Convert.ToInt32(dr["townId"]);
            }

            if (verify())
            {
                switch (operationType)
                {
                    case "school":
                        {
                            int schoolId = Convert.ToInt32(schoolId_textBox.Text);
                            string name = schoolName_textBox.Text;
                            string tel = phone_textBox.Text;
                            string email = email_textBox.Text;
                            adrsNo = adrs;
                            try
                            {
                                if (school.insertSchool(schoolId, name, tel, email, adrsNo))
                                {
                                    showTable();
                                    MessageBox.Show("New school Added", "Add School", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;
                    case "faculty":
                        {
                            int id = Convert.ToInt32(facultyId_textBox.Text);
                            string name = facultyName_textBox.Text;
                            int fkey = Convert.ToInt32(schlNo_comboBox.Text);

                            commonSaveOperation(id, name, fkey, operationType);

                        }
                        break;
                    case "department":
                        {
                            int id = Convert.ToInt32(departmentId_textBox.Text);
                            string name = deprtName_textBox.Text;
                            int fkey = Convert.ToInt32(facNo_comboBox.Text);

                            commonSaveOperation(id, name, fkey, operationType);

                        }
                        break;
                    case "course":
                        {
                            int id = Convert.ToInt32(courseId_textBox.Text);
                            string name = courseName_textBox.Text;
                            int fkey = Convert.ToInt32(departNo_comboBox.Text);

                            commonSaveOperation(id, name, fkey, operationType);

                        }
                        break;
                    case "building":
                        {
                            int id = Convert.ToInt32(buildingId_textBox.Text);
                            string name = buildingName_textBox.Text;
                            int fkey = Convert.ToInt32(schlNo_Buil_comboBox.Text);

                            commonSaveOperation(id, name, fkey, operationType);

                        }
                        break;
                    case "room":
                        {
                            int id = Convert.ToInt32(roomId_textBox.Text);
                            string name = roomName_textBox.Text;
                            int fkey = Convert.ToInt32(builNo_comboBox.Text);

                            commonSaveOperation(id, name, fkey, operationType);
                        }
                        break;

                    default:
                        break;
                }

                updateComboxValues();
            }
            else
            {
                MessageBox.Show("Empty Field", "Add " + operationType, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        void commonUpdateOperation(DataTable table, int id, string name, int fkey, string type)
        {
            if (table.Rows.Count > 0)
            {
                if (school.updateSchoolElement(id, name, fkey, operationType))
                {
                    showTable(operationType);
                    MessageBox.Show(operationType + " data updated", "Update " + operationType, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Your id doesn't exists", "Wrong Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void update_button_Click(object sender, EventArgs e)
        {
            //adresse
            //To get addressNo from data base
            int adrsNo;
            DataTable townNo = school.getList(new NpgsqlCommand("select \"townId\" from town where \"townName\"= '" + town_comboBox.Text + "'"));
            foreach (DataRow dr in townNo.Rows)
            {
                address = Convert.ToInt32(dr["townId"]);
            }

            if (verify())
            {
                switch (operationType)
                {
                    case "school":
                        {
                            int schoolId = Convert.ToInt32(schoolId_textBox.Text);
                            string name = schoolName_textBox.Text;
                            string tel = phone_textBox.Text;
                            string email = email_textBox.Text;
                            adrsNo = adrs;
                            try
                            {
                                DataTable table = school.getList(new NpgsqlCommand("SELECT * FROM school WHERE \"schoolId\"= '" + schoolId + "'"));
                                if (table.Rows.Count > 0)
                                {
                                    if (school.updateSchool(schoolId, name, tel, email, adrsNo))
                                    {
                                        showTable();
                                        MessageBox.Show(operationType + " data updated", "Update " + operationType, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        break;
                    case "faculty":
                        {
                            try
                            {
                                int id = Convert.ToInt32(facultyId_textBox.Text);
                                string name = facultyName_textBox.Text;
                                int fkey = Convert.ToInt32(schlNo_comboBox.Text);

                                DataTable table = school.getList(new NpgsqlCommand("SELECT * FROM faculty WHERE \"facultyId\"= '" + id + "'"));

                                commonUpdateOperation(table, id, name, fkey, operationType);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;
                    case "department":
                        {
                            try
                            {
                                int id = Convert.ToInt32(departmentId_textBox.Text);
                                string name = deprtName_textBox.Text;
                                int fkey = Convert.ToInt32(facNo_comboBox.Text);

                                DataTable table = school.getList(new NpgsqlCommand("SELECT * FROM department WHERE \"departmentId\"= '" + id + "'"));

                                commonUpdateOperation(table, id, name, fkey, operationType);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;
                    case "course":
                        {
                            try
                            {
                                int id = Convert.ToInt32(courseId_textBox.Text);
                                string name = courseName_textBox.Text;
                                int fkey = Convert.ToInt32(departNo_comboBox.Text);

                                DataTable table = school.getList(new NpgsqlCommand("SELECT * FROM course WHERE \"courseId\"= '" + id + "'"));

                                commonUpdateOperation(table, id, name, fkey, operationType);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        break;
                    case "building":
                        {
                            try
                            {
                                int id = Convert.ToInt32(buildingId_textBox.Text);
                                string name = buildingName_textBox.Text;
                                int fkey = Convert.ToInt32(schlNo_Buil_comboBox.Text);

                                DataTable table = school.getList(new NpgsqlCommand("SELECT * FROM building WHERE \"buildingId\"= '" + id + "'"));

                                commonUpdateOperation(table, id, name, fkey, operationType);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        break;
                    case "room":
                        {

                            try
                            {
                                int id = Convert.ToInt32(roomId_textBox.Text);
                                string name = roomName_textBox.Text;
                                int fkey = Convert.ToInt32(builNo_comboBox.Text);

                                DataTable table = school.getList(new NpgsqlCommand("SELECT * FROM room WHERE \"roomId\"= '" + id + "'"));

                                commonUpdateOperation(table, id, name, fkey, operationType);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;

                    default:
                        break;
                }
                updateComboxValues();
            }
            else
            {
                MessageBox.Show("Empty Field", "Update " + operationType, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void commonDeleteOperation(int id, DataTable table)
        {
            try
            {
                if (table.Rows.Count > 0)
                {
                    //Show a confirmation message before delete the element
                    if (MessageBox.Show("Are you sure you want to remove this " + operationType, "Remove " + operationType, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    { bool delete = school.deleteSchoolElement(id, operationType);
                        if (operationType=="school")
                            delete = school.deleteSchool(id);

                        if (delete)
                        {
                            showTable(operationType);
                            MessageBox.Show(operationType + " Removed", "Remove " + operationType, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void delete_button_Click(object sender, EventArgs e)
        {
            int id;
            bool isDelete = true;
            if (verify(isDelete))
            {
                switch (operationType)
                {
                    case "school":
                        {
                            id = Convert.ToInt32(schoolId_textBox.Text);
                            DataTable table = school.getList(new NpgsqlCommand("SELECT * FROM school WHERE \"schoolId\"= '" + id + "'"));
                            commonDeleteOperation(id,table);

                        }
                        break;
                    case "faculty":
                        {
                            id = Convert.ToInt32(facultyId_textBox.Text);
                            DataTable table = school.getList(new NpgsqlCommand("SELECT * FROM faculty WHERE \"facultyId\"= '" + id + "'"));
                            commonDeleteOperation(id, table);
                        }
                        break;
                    case "department":
                        {
                            id = Convert.ToInt32(departmentId_textBox.Text);
                            DataTable table = school.getList(new NpgsqlCommand("SELECT * FROM department WHERE \"departmentId\"= '" + id + "'"));
                            commonDeleteOperation(id, table);
                        }
                        break;
                    case "course":
                        {
                            id = Convert.ToInt32(courseId_textBox.Text);
                            DataTable table = school.getList(new NpgsqlCommand("SELECT * FROM course WHERE \"courseId\"= '" + id + "'"));
                            commonDeleteOperation(id, table);
                        }
                        break;
                    case "building":
                        {
                            id = Convert.ToInt32(buildingId_textBox.Text);
                            DataTable table = school.getList(new NpgsqlCommand("SELECT * FROM building WHERE \"buildingId\"= '" + id + "'"));
                            commonDeleteOperation(id, table);
                        }
                        break;
                    case "room":
                        {
                            id = Convert.ToInt32(roomId_textBox.Text);
                            DataTable table = school.getList(new NpgsqlCommand("SELECT * FROM room WHERE \"roomId\"= '" + id + "'"));
                            commonDeleteOperation(id, table);
                        }
                        break;

                    default:
                        break;
                }

                updateComboxValues();
            }
            else
            {
                MessageBox.Show("You must enter the " + operationType + "'s id", "Delete " + operationType, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


        private void clear_button_Click(object sender, EventArgs e)
        {
            schoolId_textBox.Text = "";
            schoolName_textBox.Text = "";
            phone_textBox.Text = "";
            email_textBox.Text = "";
            country_comboBox.Text = "";
            region_comboBox.Text = "";
            town_comboBox.Text = "";
            facultyId_textBox.Text = "";
            facultyName_textBox.Text = "";
            schlNo_comboBox.Text = "";
            departmentId_textBox.Text = "";
            deprtName_textBox.Text = "";
            facNo_comboBox.Text = "";
            courseId_textBox.Text = "";
            courseName_textBox.Text = "";
            departNo_comboBox.Text = "";
            roomId_textBox.Text = "";
            buildingId_textBox.Text = "";
            buildingName_textBox.Text = "";
            schlNo_Buil_comboBox.Text = "";
            roomId_textBox.Text = "";
            roomName_textBox.Text = "";
            builNo_comboBox.Text = "";

            school_radioButton.Checked =false;
            faculty_radioButton.Checked =false;
            department_radioButton.Checked =false;
            course_radioButton.Checked =false;
            building_radioButton.Checked = false;
            room_radioButton.Checked = false;
           
            operationType = string.Empty;

            updateComboxValues();

          
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            if(faculty_radioButton.Checked==true)
                dataGridView_school.DataSource = school.search(textBox_search.Text, "faculty");
            else if(department_radioButton.Checked==true)
                dataGridView_school.DataSource = school.search(textBox_search.Text, "department");
            else if(course_radioButton.Checked==true)
                dataGridView_school.DataSource = school.search(textBox_search.Text, "course");
            else if(building_radioButton.Checked==true)
                dataGridView_school.DataSource = school.search(textBox_search.Text, "building");
            else if(room_radioButton.Checked==true)
                dataGridView_school.DataSource = school.search(textBox_search.Text, "room");
            else
                dataGridView_school.DataSource = school.search(textBox_search.Text,"school");
        }
    }
}