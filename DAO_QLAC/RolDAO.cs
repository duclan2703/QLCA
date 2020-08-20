using DAO_QLAC;
using DTO_QLAC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAO_QLAC
{
    public class RolDAO : DBConnect
    {
        public List<string> GetListRoleNameByListRoleIdDAO(List<int> ListRoleID)
        {
            cnn.Open();
            List<string> result = new List<string>(); 
            string stringListRoleID = String.Join(",", ListRoleID.ToArray());
            string sqlQuery = string.Format("select Role_Name from Rol where RoleID in ({0})", stringListRoleID);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string stringtoname = "";

                stringtoname = reader["Role_Name"].ToString();
                
                result.Add(stringtoname);
            }
            cnn.Close();
            return result;
        }
        public List<Rol> GetAllDAO()
        {
            cnn.Open();
            List<Rol> listrole = new List<Rol>();
            string sqlQuery = string.Format("select * from Rol");
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            while(dataReader.Read())
            {
                Rol role = new Rol();
                role.RoleID = Convert.ToInt32(dataReader["RoleID"]);
                role.Role_Name = dataReader["Role_Name"].ToString();
                role.Role_Description = dataReader["Role_Description"].ToString();
                listrole.Add(role);
            }
            cnn.Close();
            return listrole;
        }
        public List<string> GetListRoleNameDAO()
        {
            cnn.Open();
            List<string> result = new List<string>();
            string sqlQuery = string.Format("select Role_Name from Rol)");
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string stringtoname = "";
                stringtoname = reader["Role_Name"].ToString();
                result.Add(stringtoname);
            }
            cnn.Close();
            return result;
        }
    }
}