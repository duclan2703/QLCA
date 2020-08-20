using ProtoQLCA.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProtoQLCA.Service
{
    public class OfficeService
    {
        private string ConnectionString = string.Empty;

        public OfficeService()
        {
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public void CreateNew(Office office)
        {
            string sqlQuery = string.Format("Insert into Office(OfficeName) Values('{0}');",office.OfficeName);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.ExecuteScalar();
            command.Dispose();
            connection.Close();
            connection.Dispose();

        }
        public void Delete(int ID)
        {
            string sqlQuery = string.Format("delete from Office where OfficeID={0}", ID);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public List<Office> GetAll()
        {
            List<Office> result = new List<Office>();
            string sqlQuery = string.Format("SELECT * FROM Office");
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Office office = new Office();
                office.OfficeID = Convert.ToInt32(reader["OfficeID"]);
                office.OfficeName = reader["OfficeName"].ToString();
                result.Add(office);
            }
            return result;
        }
        public Office Getbykey(int id)
        {
            Office office = new Office();
            string sqlQuery = string.Format("select * from Office where OfficeID={0}", id);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    office.OfficeID = Convert.ToInt32(dataReader["OfficeID"]);
                    office.OfficeName = dataReader["OfficeName"].ToString();
                }
            }
            return office;
        }
        public void Update(Office office)
        {
            string UpdateQuery = string.Format("Update Office SET OfficeName=N'{0}' Where OfficeID = {1};", office.OfficeName, office.OfficeID);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(UpdateQuery, connection);
            int SavedID = 0;
            try
            {
                command.ExecuteScalar();
                SavedID = office.OfficeID;
            }
            catch (Exception ex)
            {

            }
            command.Dispose();
            connection.Close();                          
            connection.Dispose();
        }

        public List<Office> GetOfficeName()
        {
            List<Office> result = new List<Office>();
            string sqlQuery = string.Format("select OfficeName from Office");
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Office office = new Office();
                office.OfficeName = dataReader["OfficeName"].ToString();
                result.Add(office);
            }
            return result;
        }
    }
}