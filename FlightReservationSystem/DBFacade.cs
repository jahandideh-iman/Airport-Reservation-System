﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace FlightReservationSystem
{
    static public class DBFacade
    {
        static SqlConnection connection;

        static DBFacade()
        {
            connection = new SqlConnection(@"Data Source=ARMAN\ARMANSQLSERVER;Initial Catalog=FlightReservationDataBase;Integrated Security=True");
        }
        static public bool IsUserValid(String username, String password)
        {
            return GetUser(username, password).Rows.Count != 0;
        }

        static public Salesman GetStaffByUsername(String username)
        {
            SqlCommand cmd = new SqlCommand("GetStaffInfo", connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.CommandType = CommandType.StoredProcedure;

            DataTable dt = Select(cmd);

            return new Salesman(uint.Parse(dt.Rows.Find("ID").ToString()), dt.Rows.Find("FirstName").ToString(), dt.Rows.Find("LastName").ToString());
        }


        static private DataTable GetUser(String username, String password)
        {
            SqlCommand cmd = new SqlCommand("UserSelection", connection);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            cmd.CommandType = CommandType.StoredProcedure;
            return Select(cmd);
        }

        static private DataTable Select(SqlCommand cmd)
        {
            try
            {
                connection.Open();
                cmd.Connection = connection;
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

    }
}