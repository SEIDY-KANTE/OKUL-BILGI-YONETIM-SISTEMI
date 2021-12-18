using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DatabaseManagementSystem_Project
{
    class MethodsClass
    {
        DBconnect connect = new DBconnect();
         
        //create a function to add a new persons to the database
        public bool insertPerson(int id, string fname, string lname, DateTime bdate, string gender, string level, byte[] img,string tel,string em, int adrsNo,string person="student")
        {

            string query = string.Empty;
            string contact_query = string.Empty;
            if (person == "student")
            {
                query = "INSERT INTO public.student(\"stdId\", \"firstName\", \"lastName\", \"dateOfBirth\", gender, level, photo, \"addressNo\")VALUES(@id, @fn, @ln, @bd, @gd, @lv, @img,@adrs)";
                contact_query = "INSERT INTO public.contact(telefon, email, \"personId\")  VALUES (@tel, @em, @stid)";
            }

            else if (person == "teacher")
            {
                query = "INSERT INTO public.teacher(\"teacherId\", \"firstName\", \"lastName\", \"dateOfBirth\", gender, qualification, photo, \"addressNo\")VALUES(@id, @fn, @ln, @bd, @gd, @lv, @img,@adrs)";
                contact_query = "INSERT INTO public.personelcontact(telefon, email, \"personId\")  VALUES (@tel, @em, @stid)";
            }


            NpgsqlCommand command = new NpgsqlCommand(query, connect.getconnection);
            NpgsqlCommand commandContact = new NpgsqlCommand(contact_query, connect.getconnection);
           
            //@id , @fn, @ln, @bd, @gd, @lv, @img,@adrs
            command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            command.Parameters.Add("@fn", NpgsqlTypes.NpgsqlDbType.Varchar).Value = fname;
            command.Parameters.Add("@ln", NpgsqlTypes.NpgsqlDbType.Varchar).Value = lname;
            command.Parameters.Add("@bd", NpgsqlTypes.NpgsqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gd", NpgsqlTypes.NpgsqlDbType.Varchar).Value = gender;
            command.Parameters.Add("@lv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = level;
            command.Parameters.Add("@img", NpgsqlTypes.NpgsqlDbType.Bytea).Value = img;
            command.Parameters.Add("@adrs", NpgsqlTypes.NpgsqlDbType.Integer).Value = adrsNo;

            //for contact adress (contact Id, telefon number, email and Student Id)
            // @cnid, @tel, @em, @id
            commandContact.Parameters.Add("@stid", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            commandContact.Parameters.Add("@tel", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tel;
            commandContact.Parameters.Add("@em", NpgsqlTypes.NpgsqlDbType.Varchar).Value = em;
          
            connect.openConnect();
            if (command.ExecuteNonQuery() == 1 && commandContact.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }
       
        //create a function edit for Person
        public bool updatePerson(int id, string fname, string lname, DateTime bdate, string gender, string phone, byte[] img,string tel, string em,int adrsNo,string person="student")
        {

            string query = string.Empty;
            string contact_query = string.Empty;

           
            DataTable table = getList(new NpgsqlCommand("SELECT * FROM contact WHERE \"personId\"= '" + id + "'")); //check if the contact information exits
           
            if (person == "student")
            {
                query = "UPDATE public.student SET \"firstName\"=@fn, \"lastName\"=@ln, \"dateOfBirth\"=@bd, gender =@gd, level =@lv, photo =@img ,\"addressNo\"=@adrs WHERE \"stdId\"=@id";

                if (table.Rows.Count > 0)
                    contact_query = "UPDATE  public.contact SET telefon=@tel, email=@em, \"personId\"=@stid WHERE \"personId\" =@stid"; //update
                else
                    contact_query = "INSERT INTO public.contact(telefon, email, \"personId\")  VALUES (@tel, @em, @stid)";              //insert


            }

            else if (person == "teacher")
            {
                query = "UPDATE public.teacher SET \"firstName\"=@fn, \"lastName\"=@ln, \"dateOfBirth\"=@bd, gender =@gd, qualification =@lv, photo =@img ,\"addressNo\"=@adrs WHERE \"teacherId\"=@id";

                if (table.Rows.Count > 0)
                    contact_query = "UPDATE  public.personelcontact SET telefon=@tel, email=@em, \"personId\"=@stid WHERE \"personId\" =@stid"; //update 
                else
                    contact_query = "INSERT INTO public.personelcontact(telefon, email, \"personId\")  VALUES (@tel, @em, @stid)";              //insert

            }

            NpgsqlCommand command = new NpgsqlCommand(query, connect.getconnection);

            //@id,@fn, @ln, @bd, @gd, @lv, @adr, @img,@adrs
            command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            command.Parameters.Add("@fn", NpgsqlTypes.NpgsqlDbType.Varchar).Value = fname;
            command.Parameters.Add("@ln", NpgsqlTypes.NpgsqlDbType.Varchar).Value = lname;
            command.Parameters.Add("@bd", NpgsqlTypes.NpgsqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gd", NpgsqlTypes.NpgsqlDbType.Varchar).Value = gender;
            command.Parameters.Add("@lv", NpgsqlTypes.NpgsqlDbType.Varchar).Value = phone;
            command.Parameters.Add("@img", NpgsqlTypes.NpgsqlDbType.Bytea).Value = img;
            command.Parameters.Add("@adrs", NpgsqlTypes.NpgsqlDbType.Integer).Value = adrsNo;


            //for contact adress (contact Id, telefon number, email and Student Id)
            // @cnid, @tel, @em, @id
            NpgsqlCommand commandContact = new NpgsqlCommand(contact_query, connect.getconnection);

            commandContact.Parameters.Add("@stid", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            commandContact.Parameters.Add("@tel", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tel;
            commandContact.Parameters.Add("@em", NpgsqlTypes.NpgsqlDbType.Varchar).Value = em;

          
            connect.openConnect();
            if (commandContact.ExecuteNonQuery() == 1 && command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }
        //Create a function to delete data
        //we need only id 
        public bool deletePerson(int id, string person="student")
        {
            string query = string.Empty;
            string contact_query = string.Empty;
            string person_fkey_query = string.Empty;

           

            if (person == "student")
            {
                query = "DELETE FROM student WHERE \"stdId\"=@id";
                contact_query = "DELETE FROM public.contact WHERE \"personId\" =@stdid"; //delete student's when student deleted

                person_fkey_query ="SELECT * FROM enroll WHERE \"enrollmentId\"= '" + id + "'";

            }
                
            else if (person == "teacher")
            {
                query = "DELETE FROM teacher WHERE \"teacherId\"=@id";
                contact_query = "DELETE FROM public.personelcontact WHERE \"personId\" =@stdid"; //delete teacher contact when teacher deleted
                person_fkey_query = "SELECT * FROM class WHERE \"teacherNo\"= '" + id + "'";
                
            }
            

            NpgsqlCommand commandContact = new NpgsqlCommand(contact_query, connect.getconnection);
            //@id
            commandContact.Parameters.Add("@stdid", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;


            NpgsqlCommand command = new NpgsqlCommand(query, connect.getconnection);
            //@id
            command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;



            connect.openConnect();

            bool isEnrolled = false;
            DataTable table = getList(new NpgsqlCommand(person_fkey_query));
            if (table.Rows.Count > 0)
            {
                isEnrolled = true;
            }
            if (!isEnrolled)
            {
                commandContact.ExecuteNonQuery(); //delete studen's contact if he isn't enrolled
                                                    //or delete teacher's contact if he doesn't in class table
            }
            
            if (command.ExecuteNonQuery()==1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }

        //create a function to add school's infos
        public bool insertSchool(int id, string name, string tel, string em, int adrsNo)
        {

            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO public.school(\"schoolId\",\"schoolName\",\"addressNo\") VALUES(@id, @name,@adrs)", connect.getconnection);
            NpgsqlCommand commandContact = new NpgsqlCommand("INSERT INTO public.schoolcontact(telefon, email, \"schoolId\")  VALUES (@tel, @em, @schid)", connect.getconnection);

            //@id, @name
            command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            command.Parameters.Add("@name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = name;
            command.Parameters.Add("@adrs", NpgsqlTypes.NpgsqlDbType.Integer).Value = adrsNo;


            //for contact adress (contact Id, telefon number, email and school Id)
            // @schid, @tel, @em, @id
            commandContact.Parameters.Add("@schid", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            commandContact.Parameters.Add("@tel", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tel;
            commandContact.Parameters.Add("@em", NpgsqlTypes.NpgsqlDbType.Varchar).Value = em;
            connect.openConnect();
            if (command.ExecuteNonQuery() == 1 && commandContact.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }
        //create a function to add school's elements
        public bool insertSchoolElement(int id, string name, int fkey, string type)
        {
            string query = string.Empty;

            if (type == "faculty")
                query = "INSERT INTO public.faculty(\"facultyId\", \"facultyName\", \"schoolNo\") VALUES (@id, @name, @fkey)";
            else if (type == "department")
                query = "INSERT INTO public.department(\"departmentId\", \"departmentName\", \"facultyNo\") VALUES (@id, @name, @fkey)";
            else if (type == "course")
                query = "INSERT INTO public.course(\"courseId\", \"courseName\", \"departmentNo\") VALUES (@id, @name, @fkey)";
            else if (type == "building")
                query = "INSERT INTO public.building(\"buildingId\", \"buildingName\", \"schoolNo\") VALUES (@id, @name, @fkey)";
            else if (type == "room")
                query = "INSERT INTO public.room(\"roomId\", \"roomName\", \"buildingNo\") VALUES (@id, @name, @fkey)";
            else
                return false;

            NpgsqlCommand command = new NpgsqlCommand(query, connect.getconnection);

            command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            command.Parameters.Add("@name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = name;
            command.Parameters.Add("@fkey", NpgsqlTypes.NpgsqlDbType.Integer).Value = fkey;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }
        //create a function edit for School
        public bool updateSchool(int id, string name, string tel, string em, int adrsNo)
        {

            NpgsqlCommand command = new NpgsqlCommand("UPDATE public.school SET \"schoolId\"=@id,\"schoolName\"=@name,\"addressNo\"=@adrs WHERE \"schoolId\"=@id ", connect.getconnection);

            string contactquery = string.Empty;

            DataTable table = getList(new NpgsqlCommand("SELECT * FROM schoolcontact WHERE \"schoolId\"= '" + id + "'")); //check if the contact information exits
            if (table.Rows.Count > 0)
                contactquery = "UPDATE public.schoolcontact SET telefon=@tel, email=@em WHERE schoolcontact.\"schoolId\"=@schid"; //update
            else                                                                                                                 
                contactquery = "INSERT INTO public.schoolcontact(telefon, email, \"schoolId\")  VALUES (@tel, @em, @schid)";     //insert

            NpgsqlCommand commandContact = new NpgsqlCommand(contactquery, connect.getconnection);

            //@id, @name
            command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            command.Parameters.Add("@name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = name;
            command.Parameters.Add("@adrs", NpgsqlTypes.NpgsqlDbType.Integer).Value = adrsNo;


            //for contact adress (contact Id, telefon number, email and School Id)
            // @schid, @tel, @em, @id
            commandContact.Parameters.Add("@schid", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            commandContact.Parameters.Add("@tel", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tel;
            commandContact.Parameters.Add("@em", NpgsqlTypes.NpgsqlDbType.Varchar).Value = em;
            connect.openConnect();
            if (command.ExecuteNonQuery() == 1 && commandContact.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }
        //create a function edit for School's elements
        public bool updateSchoolElement(int id, string name, int fkey, string type)
        {
            string query = string.Empty;

            if (type == "faculty")
                query = "UPDATE public.faculty SET \"facultyName\"=@name, \"schoolNo\"=@fkey WHERE \"facultyId\"=@id";
            else if (type == "department")
                query = "UPDATE public.department SET \"departmentName\"=@name, \"facultyNo\"=@fkey WHERE \"departmentId\"=@id";
            else if (type == "course")
                query = "UPDATE public.course SET \"courseName\"=@name, \"departmentNo\"=@fkey WHERE \"courseId\"=@id";
            else if (type == "building")
                query = "UPDATE public.building SET \"buildingName\"=@name, \"schoolNo\"=@fkey WHERE \"buildingId\"=@id";
            else if (type == "room")
                query = "UPDATE public.room SET \"roomName\"=@name, \"buildingNo\"=@fkey WHERE \"roomId\"=@id";
            else
                return false;

            NpgsqlCommand command = new NpgsqlCommand(query, connect.getconnection);

            command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            command.Parameters.Add("@name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = name;
            command.Parameters.Add("@fkey", NpgsqlTypes.NpgsqlDbType.Integer).Value = fkey;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }

        //Create a function to delete data (for school)
        //we need only id 
        public bool deleteSchool(int id)
        {
           
            NpgsqlCommand commandContact = new NpgsqlCommand("DELETE FROM public.schoolcontact WHERE \"schoolId\" =@schid", connect.getconnection);
            //@schid
            commandContact.Parameters.Add("@schid", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;

            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM public.school WHERE \"schoolId\"=@id", connect.getconnection);

            //@id
            command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;

            
            connect.openConnect();

            bool hasFaculty = false;
            DataTable table = getList(new NpgsqlCommand("SELECT * FROM faculty WHERE \"schoolNo\"= '" + id + "'"));
            if (table.Rows.Count > 0)
            {
                hasFaculty = true;
            }
            if (!hasFaculty)
            {
                commandContact.ExecuteNonQuery(); //delete school's contact if he hasn't faculty
            }

            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }
        //Create a function to delete data for (faculty, department, course, building and room)
        //we need only id 
        public bool deleteSchoolElement(int id,string type)
        {
            string query = string.Empty;

            if (type == "faculty")
                query = "DELETE FROM public.faculty WHERE \"facultyId\"=@id";
            else if (type == "department")
                query = "DELETE FROM public.department WHERE \"departmentId\"=@id";
            else if (type == "course")
                query = "DELETE FROM public.course WHERE \"courseId\"=@id";
            else if (type == "building")
                query = "DELETE FROM public.building WHERE \"buildingId\"=@id";
            else if (type == "room")
                query = "DELETE FROM public.room WHERE \"roomId\"=@id";
            else
                return false;

            NpgsqlCommand command = new NpgsqlCommand(query, connect.getconnection);

            //@id
            command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }
       
        //create a function to insert class
        public bool insertClass(int id, string name, int courseNo, int teacherNo,int roomNo, int credit, string description)
        {
           
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO public.class(\"classId\", \"className\", credit, \"teacherNo\", \"courseNo\", \"roomNo\", description) VALUES (@id, @name,  @crd, @teacno,@crsno, @rmno, @desc)", connect.getconnection);

            // @id, @name, @crsno, @teacno, @rmno, @crd, @desc
            command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            command.Parameters.Add("@name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = name;
            command.Parameters.Add("@crsno", NpgsqlTypes.NpgsqlDbType.Integer).Value = courseNo;
            command.Parameters.Add("@teacno", NpgsqlTypes.NpgsqlDbType.Integer).Value = teacherNo;
            command.Parameters.Add("@rmno", NpgsqlTypes.NpgsqlDbType.Integer).Value = roomNo;
            command.Parameters.Add("@crd", NpgsqlTypes.NpgsqlDbType.Integer).Value = credit;
            command.Parameters.Add("@desc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = description;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }
        //create a function to edit class
        public bool updateClass(int id, string name, int courseNo, int teacherNo, int roomNo, int credit, string description)
        {

            NpgsqlCommand command = new NpgsqlCommand("UPDATE public.class SET \"className\"=@name, credit=@crd, \"roomNo\"=@rmno, \"courseNo\"=@crsno, \"teacherNo\"=@teacno, description=@desc  WHERE \"classId\"=@id", connect.getconnection);

            // @id, @name, @crsno, @teacno, @rmno, @crd, @desc

            command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            command.Parameters.Add("@name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = name;
            command.Parameters.Add("@crsno", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            command.Parameters.Add("@teacno", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            command.Parameters.Add("@rmno", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            command.Parameters.Add("@crd", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            command.Parameters.Add("@desc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = description;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }
        //Create a function to delete data (for class)
        //we need only id 
        public bool deleteClass(int id)
        {

            NpgsqlCommand command= new NpgsqlCommand("DELETE FROM public.schoolcontactDELETE FROM public.class WHERE \"schoolId\" =@id", connect.getconnection);

            //@id
            command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }

        //create a function to enroll a student
        public bool insertEnroll(int enrollid,int studentNo, int classNo, string semestry,string description)
        {
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO public.enroll( \"enrollmentId\", \"studentId\",\"classNo\", semestry,description, \"enrollmentDate\") VALUES (@enrollid,@stdid, @clsid, @smst, @desc,CURRENT_TIMESTAMP::TIMESTAMP)", connect.getconnection);

            //enrollid,@stdid, @clsid, @smst, @desc
            command.Parameters.Add("@enrollid", NpgsqlTypes.NpgsqlDbType.Integer).Value = enrollid;
            command.Parameters.Add("@stdid", NpgsqlTypes.NpgsqlDbType.Integer).Value = studentNo;
            command.Parameters.Add("@clsid", NpgsqlTypes.NpgsqlDbType.Integer).Value = classNo;
            command.Parameters.Add("@smst", NpgsqlTypes.NpgsqlDbType.Varchar).Value = semestry;
            command.Parameters.Add("@desc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = description;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }
        public bool updateEnroll(int enrollmentId,int studentNo, int classNo, string semestry, string description)
        {
            NpgsqlCommand command = new NpgsqlCommand("UPDATE public.enroll SET \"studentId\"=@stdid, \"classNo\"=@clsid, semestry=@smst, description=@desc , \"enrollmentDate\"= CURRENT_TIMESTAMP::TIMESTAMP WHERE \"enrollmentId\"=@enrollid", connect.getconnection);

            //@enrollid, @stdid, @clsid, @smst, @desc  enrollmentId
            command.Parameters.Add("@enrollid", NpgsqlTypes.NpgsqlDbType.Integer).Value = enrollmentId;
            command.Parameters.Add("@stdid", NpgsqlTypes.NpgsqlDbType.Integer).Value = studentNo;
            command.Parameters.Add("@clsid", NpgsqlTypes.NpgsqlDbType.Integer).Value = classNo;
            command.Parameters.Add("@smst", NpgsqlTypes.NpgsqlDbType.Varchar).Value = semestry;
            command.Parameters.Add("@desc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = description;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }
        public bool deleteEnroll(int enrollmentId)
        {
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM public.enroll WHERE \"enrollmentId\"=@enrollid", connect.getconnection);

            //@enrollid
            command.Parameters.Add("@enrollid", NpgsqlTypes.NpgsqlDbType.Integer).Value = enrollmentId;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }

        }
        //create a function add score
        public bool insertScore(int stdid, int classid, double scor, string desc)
        {
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO public.score(\"studentNo\", \"classNo\", \"studentScore\", description) VALUES (@stid,@cn,@sco,@desc)", connect.getconnection);
            //@stid,@cn,@sco,@desc
            command.Parameters.Add("@stid", NpgsqlTypes.NpgsqlDbType.Integer).Value = stdid;
            command.Parameters.Add("@cn", NpgsqlTypes.NpgsqlDbType.Integer).Value = classid;
            command.Parameters.Add("@sco", NpgsqlTypes.NpgsqlDbType.Double).Value = scor;
            command.Parameters.Add("@desc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = desc;
            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }
        //create a function to check if student enrolled or not
        public bool studentIsEnrolled(int stdId, int cNo)
        {
            DataTable table = getList(new NpgsqlCommand("SELECT * FROM enroll WHERE \"studentId\"= '" + stdId + "' AND \"classNo\"= '" + cNo + "'"));
            if (table.Rows.Count > 0)
            { return true; }
            else
            { return false; }
        }
        // create a function to check already class score
        public bool checkScore(int stdId, int cNo)
        {
            DataTable table = getList(new NpgsqlCommand("SELECT * FROM score WHERE \"studentNo\"= '" + stdId + "' AND \"classNo\"= '" + cNo + "'"));
            if (table.Rows.Count > 0)
            { return true; }
            else
            { return false; }
        }
        // Create A function to edit score data
        public bool updateScore(int stdid, int cn, double scor, string desc)
        {
            NpgsqlCommand command = new NpgsqlCommand("UPDATE score SET \"studentScore\"=@sco,description=@desc WHERE \"studentNo\"=@stid AND \"classNo\"=@cn", connect.getconnection);
            //@stid,@sco,@desc
            command.Parameters.Add("@cn", NpgsqlTypes.NpgsqlDbType.Integer).Value = cn;
            command.Parameters.Add("@stid", NpgsqlTypes.NpgsqlDbType.Integer).Value = stdid;
            command.Parameters.Add("@sco", NpgsqlTypes.NpgsqlDbType.Double).Value = scor;
            command.Parameters.Add("@desc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = desc;
            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }
        //Create a function to delete a score data
        public bool deleteScore(int studentNo, int classNo)
        {
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM score WHERE \"studentNo\"=@id AND \"classNo\"=@cn", connect.getconnection);

            //@id, @cn
            command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = studentNo;
            command.Parameters.Add("@cn", NpgsqlTypes.NpgsqlDbType.Integer).Value = classNo;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }


        //create a function search
        public DataTable search(string searchdata, string type = "student")
        {

            string query = string.Empty;

            if (type == "student") 
                query = "select *from searchStudent('"+searchdata+"')"; //function to search student
           
            else if (type == "teacher") 
                query = "select *from searchTeacher('" + searchdata + "')"; //search teacher

            else if (type == "school") 
                query = "select *from searchSchool('" + searchdata + "')"; //search school

            else if (type == "class") 
                query = "select *from searchClass('" + searchdata + "')"; //search class

            else if (type == "enroll") 
                query = "select *from searchEnroll('" + searchdata + "')"; //search enroll

            else if (type == "score") 
                query = "select *from searchScore('" + searchdata + "')"; //search score

            else if (type == "faculty")
                query = "select *from searchFaculty('" + searchdata + "')";

            else if (type == "department")
                query = "select *from searchDepartment('" + searchdata + "')";

            else if (type == "course")
                query = "select *from searchCourse('" + searchdata + "')";

            else if (type == "building")
                query = "select *from searchBuilding('" + searchdata + "')";

            else if (type == "room")
                query = "select *from searchRoom('" + searchdata + "')";



            NpgsqlCommand command = new NpgsqlCommand(query, connect.getconnection);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        
        public DataTable getList(NpgsqlCommand command)
        {
            command.Connection = connect.getconnection;
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
