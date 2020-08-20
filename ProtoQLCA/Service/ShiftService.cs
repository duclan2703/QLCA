using ProtoQLCA.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProtoQLCA.Service
{
    public class ShiftService
    {
        private string ConnectionString = string.Empty;

        public ShiftService()
        {
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public void CreateNew(Shift shift)
        {
            string sqlQuery = string.Format("Insert into Shift(ShiftName) Values('{0}');", shift.ShiftName);
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
            string sqlQuery = string.Format("delete from Shift where ShiftID={0}", ID);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public List<Shift> GetAll()
        {
            List<Shift> result = new List<Shift>();
            string sqlQuery = string.Format("SELECT * FROM Shift");
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Shift shift = new Shift();
                shift.ShiftID = Convert.ToInt32(reader["ShiftID"]);
                shift.ShiftName = reader["ShiftName"].ToString();
                result.Add(shift);
            }
            return result;
        }
        public Shift Getbykey(int id)
        {
            Shift shift = new Shift();
            string sqlQuery = string.Format("select * from Shift where ShiftID={0}", id);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    shift.ShiftID = Convert.ToInt32(dataReader["ShiftID"]);
                    shift.ShiftName = dataReader["ShiftName"].ToString();
                }
            }
            return shift;
        }
        public void Update(Shift shift)
        {
            string UpdateQuery = string.Format("Update Office SET ShiftName={0} Where ShiftID = {1};", shift.ShiftName, shift.ShiftID);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(UpdateQuery, connection);
            int SavedID = 0;
            try
            {
                command.ExecuteScalar();
                SavedID = shift.ShiftID;
            }
            catch (Exception ex)
            {

            }
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public string GetShiftNamebyShiftID(int id)
        {
            Shift shift = new Shift();
            string sqlQuery = string.Format("select ShiftName from Shift where ShiftID={0}", id);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {

                    shift.ShiftName = dataReader["ShiftName"].ToString();
                }
            }
            return shift.ShiftName;
        }
    }
}