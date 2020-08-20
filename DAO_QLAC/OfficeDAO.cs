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
    public class OfficeDAO : DBConnect
    {
        public void CreateNewDAO(Office office)
        {
            try
            {
                cnn.Open();
                string sqlQuery = string.Format("Insert into Office(OfficeName) Values('{0}');", office.OfficeName);
                SqlCommand command = new SqlCommand(sqlQuery, cnn);
                command.ExecuteScalar();
                command.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally { cnn.Close(); }
        }
        public void DeleteDAO(int ID)
        {
            try
            {
                cnn.Open();
                string sqlQuery = string.Format("delete from Office where OfficeID={0}", ID);
                SqlCommand command = new SqlCommand(sqlQuery, cnn);
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex) { }
            finally { cnn.Close(); }
        }
        public List<Office> GetAllDAO()
        {
            cnn.Open();
            List<Office> result = new List<Office>();
            string sqlQuery = string.Format("SELECT * FROM Office");
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Office office = new Office();
                office.OfficeID = Convert.ToInt32(reader["OfficeID"]);
                office.OfficeName = reader["OfficeName"].ToString();
                result.Add(office);
            }
            cnn.Close();
            return result;
        }
        public Office GetbykeyDAO(int id)
        {
            cnn.Open();
            Office office = new Office();
            string sqlQuery = string.Format("select * from Office where OfficeID={0}", id);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    office.OfficeID = Convert.ToInt32(dataReader["OfficeID"]);
                    office.OfficeName = dataReader["OfficeName"].ToString();
                }
            }
            cnn.Close();
            return office;
        }
        public void UpdateDAO(Office office)
        {
            try
            {
                cnn.Open();
                string UpdateQuery = string.Format("Update Office SET OfficeName=N'{0}' Where OfficeID = {1};", office.OfficeName, office.OfficeID);
                SqlCommand command = new SqlCommand(UpdateQuery, cnn);
                int SavedID = 0;
                command.ExecuteScalar();
                SavedID = office.OfficeID;
                command.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally { cnn.Close(); }
        }
        public List<Office> GetOfficeNameDAO()
        {
            cnn.Open();
            List<Office> result = new List<Office>();
            string sqlQuery = string.Format("select OfficeName from Office");
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Office office = new Office();
                office.OfficeName = dataReader["OfficeName"].ToString();
                result.Add(office);
            }
            cnn.Close();
            return result;
        }
    }
}