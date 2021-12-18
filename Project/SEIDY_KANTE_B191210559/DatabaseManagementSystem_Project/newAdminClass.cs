using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DatabaseManagementSystem_Project
{
    class newAdminClass
    {
        DBconnect connect = new DBconnect();
        //create a function add user
        public bool insertAdmin(string usName, string passw)
        {
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO public.\"adminUser\"(username, password) VALUES (@usName,@passw)", connect.getconnection);
            
            command.Parameters.Add("@usName", NpgsqlTypes.NpgsqlDbType.Varchar).Value = usName;
            command.Parameters.Add("@passw", NpgsqlTypes.NpgsqlDbType.Varchar).Value = passw;
          
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
        public bool updateAdmin(string usName,string passw, string newPassw)
        {
            NpgsqlCommand command = new NpgsqlCommand("UPDATE public.\"adminUser\" SET username=@usName, password=@newpassw WHERE username=@usName AND password=@passw ", connect.getconnection);
      
            command.Parameters.Add("@usName", NpgsqlTypes.NpgsqlDbType.Varchar).Value = usName;
            command.Parameters.Add("@passw", NpgsqlTypes.NpgsqlDbType.Varchar).Value = passw;
            command.Parameters.Add("@newpassw", NpgsqlTypes.NpgsqlDbType.Varchar).Value = newPassw;

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
        public bool deleteAdmin(string usName, string passw)
        {
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM public.\"adminUser\" WHERE username=@usName AND password=@passw", connect.getconnection);
            //@stid,@cn,@sco,@desc
            command.Parameters.Add("@usName", NpgsqlTypes.NpgsqlDbType.Varchar).Value = usName;
            command.Parameters.Add("@passw", NpgsqlTypes.NpgsqlDbType.Varchar).Value = passw;

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
    }
   
}
