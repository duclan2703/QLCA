using ProtoQLCA.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProtoQLCA.Service
{
    public class RolService
    {
        private string ConnectionString = string.Empty;

        public RolService()
        {
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public List<string> GetListRoleNameByListRoleId(List<int> ListRoleID)
        {
            List<string> result = new List<string>(); 
            string stringListRoleID = String.Join(",", ListRoleID.ToArray());
            string sqlQuery = string.Format("select Role_Name from Rol where RoleID in ({0})", stringListRoleID);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string stringtoname = "";

                stringtoname = reader["Role_Name"].ToString();
                
                result.Add(stringtoname);
            }

            return result;
        }

        public List<Rol> GetAll()
        {
            List<Rol> listrole = new List<Rol>();
            string sqlQuery = string.Format("select * from Rol");
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while(dataReader.Read())
            {
                Rol role = new Rol();
                role.RoleID = Convert.ToInt32(dataReader["RoleID"]);
                role.Role_Name = dataReader["Role_Name"].ToString();
                role.Role_Description = dataReader["Role_Description"].ToString();
                listrole.Add(role);
            }
            return listrole;
        }

        public List<string> GetListRoleName()
        {
            List<string> result = new List<string>();
            
            string sqlQuery = string.Format("select Role_Name from Rol)");
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string stringtoname = "";

                stringtoname = reader["Role_Name"].ToString();

                result.Add(stringtoname);
            }

            return result;
        }
    }
}