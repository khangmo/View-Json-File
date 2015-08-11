using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SynchronousHttp.Models
{
    public class Account
    {
        public string username { get; set; }
        public string password { get; set; }
        public string enabled { get; set; }

        private bool connectionOpen;
        private MySqlConnection connection;
        private DataClassesDataContext data;

        public Account()
        {

        }

        public Account(string username)
        {
            InitConnection();

            this.username = username;

            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand();
                data = new DataClassesDataContext();

                IEnumerable<AspNetUser> users = from user in data.AspNetUsers select user;
                List<AspNetUser> lstUser = users.ToList();

                mySqlCommand.Connection = connection;
                mySqlCommand.CommandText = "select * from accounts where username= @USER";
                mySqlCommand.Parameters.AddWithValue("@USER", username);
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                try
                {
                    mySqlDataReader.Read();

                    if (mySqlDataReader.IsDBNull(0) == false)
                    {
                        this.username = mySqlDataReader.GetString(0);
                    }
                    if (mySqlDataReader.IsDBNull(1) == false)
                    {
                        this.password = mySqlDataReader.GetString(1);
                    }
                    if (mySqlDataReader.IsDBNull(2) == false)
                    {
                        this.enabled = mySqlDataReader.GetString(2);
                    }
                }
                catch (Exception ex)
                {
                    mySqlDataReader.Close();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
        }

        public void InitConnection()
        {
            connectionOpen = false;
            connection = new MySqlConnection();

            connection.ConnectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;

            if (ConnectingServer())
            {
                connectionOpen = true;
            }
            else
            {
                connectionOpen = false;
            }
        }

        public bool ConnectingServer()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}