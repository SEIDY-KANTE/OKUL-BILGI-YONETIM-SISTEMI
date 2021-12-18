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
    public partial class countryRegistration : Form
    {
        DBconnect connect = new DBconnect(); //CONNECT TO DATA BASE
        MethodsClass dataUser = new MethodsClass();

        public bool insertCountry(int cntryId, string cntry)
        {
            NpgsqlCommand commandCountry = new NpgsqlCommand("INSERT INTO public.country(\"countryId\", \"countryName\") VALUES (@cntryid,@cntry)", connect.getconnection);

            //@cntry
            commandCountry.Parameters.Add("@cntryid", NpgsqlTypes.NpgsqlDbType.Integer).Value = cntryId;
            commandCountry.Parameters.Add("@cntry", NpgsqlTypes.NpgsqlDbType.Varchar).Value = cntry;
            connect.openConnect();
            if (commandCountry.ExecuteNonQuery() == 1)
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
        public bool updateCountry(int cntryId, string cntry)
        {
            NpgsqlCommand commandCountry = new NpgsqlCommand("UPDATE public.country SET \"countryName\"=@cntry WHERE \"countryId\"=@cntryid", connect.getconnection);

           
            //@cntry
            commandCountry.Parameters.Add("@cntryid", NpgsqlTypes.NpgsqlDbType.Integer).Value = cntryId;
            commandCountry.Parameters.Add("@cntry", NpgsqlTypes.NpgsqlDbType.Varchar).Value = cntry;
            connect.openConnect();
            if (commandCountry.ExecuteNonQuery() == 1)
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
        public bool deleteCountry(int cntryId)
        {
            NpgsqlCommand commandCountry = new NpgsqlCommand("DELETE FROM public.country WHERE \"countryId\"=@cntryid", connect.getconnection);

            
            //@cntryid
            commandCountry.Parameters.Add("@cntryid", NpgsqlTypes.NpgsqlDbType.Integer).Value = cntryId;
            connect.openConnect();
            if (commandCountry.ExecuteNonQuery() == 1)
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
        public bool insertRegion(int rgId, string rgion, int cntryId)
        {
            NpgsqlCommand commandRegion = new NpgsqlCommand("INSERT INTO public.region(\"regionId\", \"regionName\", \"countryId\") VALUES (@rgid,@rgion,@cntryid)", connect.getconnection);

            //for region
            //@rgid , @rgion,cntryid
            commandRegion.Parameters.Add("@rgid", NpgsqlTypes.NpgsqlDbType.Integer).Value = rgId;
            commandRegion.Parameters.Add("@rgion", NpgsqlTypes.NpgsqlDbType.Varchar).Value = rgion;
            commandRegion.Parameters.Add("@cntryid", NpgsqlTypes.NpgsqlDbType.Integer).Value = cntryId;

            connect.openConnect();
            if (commandRegion.ExecuteNonQuery() == 1)
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
        public bool updateRegion(int rgId, string rgion, int cntryId)
        {
            NpgsqlCommand commandRegion = new NpgsqlCommand("UPDATE public.region SET \"regionName\"=@rgion, \"countryId\"=@cntryid) WHERE  \"regionId\"=@rgid", connect.getconnection);

            //for region
            //@rgid , @rgion,cntryid
            commandRegion.Parameters.Add("@rgid", NpgsqlTypes.NpgsqlDbType.Integer).Value = rgId;
            commandRegion.Parameters.Add("@rgion", NpgsqlTypes.NpgsqlDbType.Varchar).Value = rgion;
            commandRegion.Parameters.Add("@cntryid", NpgsqlTypes.NpgsqlDbType.Integer).Value = cntryId;

            connect.openConnect();
            if (commandRegion.ExecuteNonQuery() == 1)
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
        public bool deleteRegion(int rgId)
        {
            NpgsqlCommand commandRegion = new NpgsqlCommand("DELETE FROM public.region WHERE  \"regionId\"=@rgid", connect.getconnection);

            //@rgid
            commandRegion.Parameters.Add("@rgid", NpgsqlTypes.NpgsqlDbType.Integer).Value = rgId;
       
            connect.openConnect();
            if (commandRegion.ExecuteNonQuery() == 1)
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
        public bool insertTown(int twId, string twn, int rgionId)
        {
            NpgsqlCommand commandTwon = new NpgsqlCommand("INSERT INTO public.town(\"townId\",\"townName\",\"regionId\") VALUES (@twnid,@twn,@rgnid)", connect.getconnection);

            //for town
            //@twnid, @twn ,@rgnid
            commandTwon.Parameters.Add("@twnid", NpgsqlTypes.NpgsqlDbType.Integer).Value = twId;
            commandTwon.Parameters.Add("@twn", NpgsqlTypes.NpgsqlDbType.Varchar).Value = twn;
            commandTwon.Parameters.Add("@rgnid", NpgsqlTypes.NpgsqlDbType.Integer).Value = rgionId;

            connect.openConnect();
            if (commandTwon.ExecuteNonQuery() == 1)
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
        public bool updateTown(int twId, string twn, int rgionId)
        {
            NpgsqlCommand commandTwon = new NpgsqlCommand("UPDATE public.town SET \"townName\"=@twn,\"regionId\"=@rgnid WHERE \"townId\"=@twnid", connect.getconnection);

            //for town
            //@twnid, @twn ,@rgnid
            commandTwon.Parameters.Add("@twnid", NpgsqlTypes.NpgsqlDbType.Integer).Value = twId;
            commandTwon.Parameters.Add("@twn", NpgsqlTypes.NpgsqlDbType.Varchar).Value = twn;
            commandTwon.Parameters.Add("@rgnid", NpgsqlTypes.NpgsqlDbType.Integer).Value = rgionId;

            connect.openConnect();
            if (commandTwon.ExecuteNonQuery() == 1)
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
        public bool deleteTown(int twId)
        {
            NpgsqlCommand commandTwon = new NpgsqlCommand("DELETE FROM public.town WHERE \"townId\"=@twnid", connect.getconnection);

            //@twnid
            commandTwon.Parameters.Add("@twnid", NpgsqlTypes.NpgsqlDbType.Integer).Value = twId;

            connect.openConnect();
            if (commandTwon.ExecuteNonQuery() == 1)
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

        public countryRegistration()
        {
            InitializeComponent();
        }


        private void Exit_label_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Exit_label_MouseMove(object sender, MouseEventArgs e)
        {
            Exit_label.ForeColor = Color.Red;
        }

        private void Exit_label_MouseLeave(object sender, EventArgs e)
        {
            Exit_label.ForeColor = Color.White;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Home homePage = new Home();
            this.Hide();
            homePage.Show();
        }
       
        string operationType = string.Empty;
        bool verify(bool isDelete = false)
        {
            if (country_radioButton.Checked==true)
            {
                operationType = "country";
                if (isDelete && cntryID_textBox.Text != "")
                    return true;

                else if (cntryName_textBox.Text == "" || cntryID_textBox.Text == "")
                {
                    return false;
                }
                else
                    return true;
            }
            if (region_radioButton.Checked == true)
            {
                operationType = "region";
                if (isDelete && cntryID_textBox.Text != "" && rgionID_textBox.Text!="")
                    return true;

                else if (rgionName_textBox.Text == "" || rgionID_textBox.Text=="" || cntryID_textBox.Text == "" )
                {
                    return false;
                }
                else
                    return true;
            }

            if (town_radioButton.Checked == true)
            {
                operationType = "town";
                if (isDelete && rgionID_textBox.Text != "" && twnID_textBox.Text != "")
                    return true;

                else if (twnName_textBox.Text == "" || twnID_textBox.Text=="" || cntryID_textBox.Text == "")
                {
                    return false;
                }
                else
                    return true;
            }

            else
                return false;

        }

      
        private void save_button_Click(object sender, EventArgs e)
        {
            string cntry = cntryName_textBox.Text;
            string rgion = rgionName_textBox.Text;
            string twn = twnName_textBox.Text;
            int cntryId, rgId,twId;

            if (verify())
            {
                
                switch (operationType)
                {
                    case "country":
                        {
                           
                            try
                            {
                                cntryId = Convert.ToInt32(cntryID_textBox.Text);
                                if (insertCountry(cntryId, cntry))
                                {

                                    MessageBox.Show("New Country Added", "Add country", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Country Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;
                    case "region":
                        {
                            rgId = Convert.ToInt32(rgionID_textBox.Text);
                            cntryId = Convert.ToInt32(cntryID_textBox.Text);
                            try
                            {

                                if (insertRegion(rgId, rgion, cntryId))
                                {

                                    MessageBox.Show("New Region Added", "Add Region", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Region Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;
                    case "town":
                        {
                            rgId = Convert.ToInt32(rgionID_textBox.Text);
                            twId = Convert.ToInt32(twnID_textBox.Text);
                            try
                            {
                                if (insertTown(twId, twn, rgId))
                                {

                                    MessageBox.Show("New Town Added", "Add Town", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Town Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Empty Field", "Add "+operationType, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
       
        public bool checkData(int id, string type)
        {
            string query = String.Empty;
            if (type == "country")
                query = "select *from country where \"countryId\"='" + id + "'";
            else if (type == "region")
                query = "select *from region where  \"regionId\"='" + id + "'";
            else if (type == "town")
                query = "select *from town where \"townId\"='" + id + "'";

            DataTable table = dataUser.getList(new NpgsqlCommand(query));
            if (table.Rows.Count > 0)
            { return true; }
            else
            { return false; }
        }
        private void update_button_Click(object sender, EventArgs e)
        {
            string cntry = cntryName_textBox.Text;
            string rgion = rgionName_textBox.Text;
            string twn = twnName_textBox.Text;
            int cntryId, rgId, twId;

            if (verify())
            {
                switch (operationType)
                {
                    case "country":
                        {
                            cntryId = Convert.ToInt32(cntryID_textBox.Text);
                            try
                            {
                                if (checkData(cntryId, operationType))
                                {
                                    if (updateCountry(cntryId, cntry))
                                    {

                                        MessageBox.Show(operationType + " Updated", "Update "+operationType, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                }
                                else
                                    MessageBox.Show("This "+operationType+" doesn't exists", "Wrong Id", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, operationType+" Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;
                    case "region":
                        {
                            cntryId = Convert.ToInt32(cntryID_textBox.Text);
                            rgId = Convert.ToInt32(rgionID_textBox.Text);
                            try
                            {
                                if (checkData(rgId, operationType))
                                {
                                    if (updateRegion(rgId, rgion,cntryId))
                                    {

                                        MessageBox.Show(operationType + " Updated", "Update " + operationType, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                }
                                else
                                    MessageBox.Show("This " + operationType + " doesn't exists", "Wrong Id", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, operationType + " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;
                    case "town":
                        {
                            rgId = Convert.ToInt32(rgionID_textBox.Text);
                            twId = Convert.ToInt32(twnID_textBox.Text);
                            try
                            {
                                if (checkData(twId, operationType))
                                {
                                    if (updateTown(twId, twn, rgId))
                                    {

                                        MessageBox.Show(operationType + " Updated", "Update " + operationType, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                }
                                else
                                    MessageBox.Show("This " + operationType + " doesn't exists", "Wrong Id", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, operationType + " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Empty Field", "Update " + operationType, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
           bool isDelete = true;
           int cntryId, rgId, twId;

            if (verify(isDelete))
            {
                switch (operationType)
                {
                    case "country":
                        {
                            cntryId = Convert.ToInt32(cntryID_textBox.Text);
                            try
                            {
                                if (checkData(cntryId, operationType))
                                {
                                    if (deleteCountry(cntryId))
                                    {

                                        MessageBox.Show(operationType + " Deleted", "Delete " + operationType, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                }
                                else
                                    MessageBox.Show("This " + operationType + " doesn't exists", "Wrong Id", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, operationType + " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;
                    case "region":
                        {
                            rgId = Convert.ToInt32(rgionID_textBox.Text);
                            try
                            {
                                if (checkData(rgId, operationType))
                                {
                                    if (deleteRegion(rgId))
                                    {

                                        MessageBox.Show(operationType + " Deleted", "Delete " + operationType, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                }
                                else
                                    MessageBox.Show("This " + operationType + " doesn't exists", "Wrong Id", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, operationType + " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;
                    case "town":
                        {
                            twId = Convert.ToInt32(twnID_textBox.Text);
                            try
                            {
                                if (checkData(twId, operationType))
                                {
                                    if (deleteTown(twId))
                                    {

                                        MessageBox.Show(operationType + " Deleted", "Delete " + operationType, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                }
                                else
                                    MessageBox.Show("This " + operationType + " doesn't exists", "Wrong Id", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, operationType + " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Empty Field", "Update " + operationType, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
    
}
