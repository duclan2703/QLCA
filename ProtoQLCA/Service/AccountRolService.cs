using ProtoQLCA.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProtoQLCA.Service
{
    public class AccountRolService
    {
        private string ConnectionString = string.Empty;

        public AccountRolService()
        {
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public List<int> GetListRoleIDByUsername(string username)
        {
            List<int> result = new List<int>();
            string sqlQuery = string.Format("select RoleID from Account where Username='{0}'", username);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                int listroleID;                
                listroleID = Convert.ToInt32(reader["RoleID"]);
                result.Add(listroleID);
            }
            return result;
        }

        public List<int> GetListRoleIDByUserID(int ID)
        {
            List<int> result = new List<int>();
            string sqlQuery = string.Format("select RoleID from Account_Rol where UserID = {0}", ID);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int listroleID;
                listroleID = Convert.ToInt32(reader["RoleID"]);
                result.Add(listroleID);
            }
            return result;
        }
    }
}