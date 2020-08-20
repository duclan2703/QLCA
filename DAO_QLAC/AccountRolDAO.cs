using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAO_QLAC
{
    public class AccountRolDAO : DBConnect
    {
        public List<int> GetListRoleIDByUsernameDAO(string username)
        {
            cnn.Open();
            List<int> result = new List<int>();
            string sqlQuery = string.Format("select RoleID from Account where Username='{0}'", username);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                int listroleID;
                listroleID = Convert.ToInt32(reader["RoleID"]);
                result.Add(listroleID);
            }
            cnn.Close();
            return result;
        }

        public List<int> GetListRoleIDByUserIDDAO(int ID)
        {
            cnn.Open();
            List<int> result = new List<int>();
            string sqlQuery = string.Format("select RoleID from Account_Rol where UserID = {0}", ID);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int listroleID;
                listroleID = Convert.ToInt32(reader["RoleID"]);
                result.Add(listroleID);
            }
            cnn.Close();
            return result;
        }
    }
}