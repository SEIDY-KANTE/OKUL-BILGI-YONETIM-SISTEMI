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
    public partial class Dashboard : Form
    {
        public Dashboard()
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

        public static int totalStudent;
        public static int TotalStudent
        {
            get { return totalStudent; }
            set { totalStudent = value; }
        }

        public static int maleStudent;
        public static int TotalMale
        {
            get { return maleStudent; }
            set { maleStudent = value; }
        }

        public static int femaleStudent;
        public static int TotalFemale
        {
            get { return femaleStudent; }
            set { femaleStudent = value; }
        }


        public static int totalTeacher;
        public static int TotalTeacher
        {
            get { return totalTeacher; }
            set { totalTeacher = value; }
        }


        public static int maleTeacher;
        public static int TotalMaleTeacher
        {
            get { return maleTeacher; }
            set { maleTeacher = value; }
        }

        public static int femaleTeacher;
        public static int TotalFemaleTeacher
        {
            get { return femaleTeacher; }
            set { femaleTeacher = value; }
        }

        public static int prof;
        public static int TotalProf
        {
            get { return prof; }
            set { prof = value; }
        }


        public static int doctor;
        public static int TotalDr
        {
            get { return doctor; }
            set { doctor = value; }
        }

        public static int lect;
        public static int TotalLec
        {
            get { return lect; }
            set { lect = value; }
        }

        public static int enrolled;
        public static int TotalEnrolled
        {
            get { return enrolled; }
            set { enrolled = value; }
        }

        public static int faculty;
        public static int TotalFaculty
        {
            get { return faculty; }
            set { faculty = value; }
        }

        public static int department;
        public static int TotalDepartment
        {
            get { return department; }
            set { department = value; }
        }

        public static int course;
        public static int TotalCourse
        {
            get { return course; }
            set { course = value; }
        }

        public static int clas;
        public static int TotalClass
        {
            get { return clas; }
            set { clas = value; }
        }

        public static int building;
        public static int TotalBuilding
        {
            get { return building; }
            set { building = value; }
        }

        public static int room;
        public static int TotalRoom
        {
            get { return room; }
            set { room = value; }
        }

        MethodsClass dashboard = new MethodsClass();
        private void Dashboard_Load(object sender, EventArgs e)
        {
              
            DataTable totalStatistic = dashboard.getList(new NpgsqlCommand("select *from statistic"));
            foreach (DataRow dr in totalStatistic.Rows)
            {
                TotalStudent = Convert.ToInt32(dr["total_student"]);
                TotalMale = Convert.ToInt32(dr["male_student"]);
                TotalFemale = Convert.ToInt32(dr["female_student"]);

                TotalTeacher = Convert.ToInt32(dr["total_teacher"]);
                TotalMaleTeacher = Convert.ToInt32(dr["male_teacher"]);
                TotalFemaleTeacher = Convert.ToInt32(dr["female_teacher"]);

                TotalProf = Convert.ToInt32(dr["total_prof"]);
                TotalDr = Convert.ToInt32(dr["total_dr"]);
                TotalLec = Convert.ToInt32(dr["total_lec"]);

                TotalEnrolled = Convert.ToInt32(dr["student_enrolled"]);

                TotalFaculty = Convert.ToInt32(dr["total_faculty"]);
                TotalDepartment = Convert.ToInt32(dr["total_department"]);
                TotalCourse = Convert.ToInt32(dr["total_course"]);
                TotalClass = Convert.ToInt32(dr["total_class"]);
                TotalBuilding = Convert.ToInt32(dr["total_building"]);
                TotalRoom = Convert.ToInt32(dr["total_room"]);


            }
            StudentNum_label.Text = totalStudent.ToString()+ " students";
            MaleStudentNum_label.Text = maleStudent.ToString();
            FemaleStudentNum_label.Text = femaleStudent.ToString();

            InstructorNum_label.Text = TotalTeacher.ToString() + " instructors";
            MaleInstructorNum_label.Text = maleTeacher.ToString();
            FemaleInstructorNum_label.Text = femaleTeacher.ToString();

            ProfQualificationNum_label.Text = prof.ToString() + " professors";
            DrQualificationNum_label.Text = doctor.ToString() + " doctors";
            LectQualificationNum_label.Text = lect.ToString() + " lectors";

            StudentEnrolledNum_label.Text = enrolled.ToString() + " enrolled";

            facultyNum_label.Text = faculty.ToString() + " faculty";
            departmentNum_label.Text = department.ToString() + " department";
            courseNum_label.Text = course.ToString() + " course";
            classNum_label.Text = clas.ToString() + " class";
            buildingNum_label.Text = building.ToString() + " building";
            roomNum_label.Text = room.ToString() + " room";

        }
    }
}