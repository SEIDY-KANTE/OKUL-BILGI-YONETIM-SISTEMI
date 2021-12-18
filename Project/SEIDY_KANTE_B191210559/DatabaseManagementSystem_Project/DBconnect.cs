using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DatabaseManagementSystem_Project
{
    class DBconnect
    {
        //to create connection
        NpgsqlConnection connect = new NpgsqlConnection(@"Server=localhost;port=5433;username=postgres;password=6862;database=VtysProject");


        //to get connection
        public NpgsqlConnection getconnection
        {
            get
            {
                return connect;
            }
        }

        //create a function to Open conncetion
        public void openConnect()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
                connect.Open();
        }

        //Create a fuction to close connection
        public void closeConnect()
        {
            if (connect.State == System.Data.ConnectionState.Open)
                connect.Close();
        }
    }
}
