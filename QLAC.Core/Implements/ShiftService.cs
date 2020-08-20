using DAO_QLAC;
using DTO_QLAC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Service_QLAC
{
    public class ShiftService : DBConnect
    {
        public void CreateNew(Shift shift)
        {
            try
            {
                cnn.Open();
                string sqlQuery = string.Format("Insert into Shift(ShiftName) Values('{0}');", shift.ShiftName);
                SqlCommand command = new SqlCommand(sqlQuery, cnn);
                command.ExecuteScalar();
                command.Dispose();
            }
            catch (Exception ex) { }
            finally { cnn.Close(); }
        }
        public void Delete(int ID)
        {
            try
            {
                cnn.Open();
                string sqlQuery = string.Format("delete from Shift where ShiftID={0}", ID);
                SqlCommand command = new SqlCommand(sqlQuery, cnn);
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex) { }
            finally { cnn.Close(); }
        }
        public List<Shift> GetAll()
        {
            cnn.Open();
            List<Shift> result = new List<Shift>();
            string sqlQuery = string.Format("SELECT * FROM Shift");
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Shift shift = new Shift();
                shift.ShiftID = Convert.ToInt32(reader["ShiftID"]);
                shift.ShiftName = reader["ShiftName"].ToString();
                result.Add(shift);
            }
            cnn.Close();
            return result;
        }
        public Shift Getbykey(int id)
        {
            cnn.Open();
            Shift shift = new Shift();
            string sqlQuery = string.Format("select * from Shift where ShiftID={0}", id);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    shift.ShiftID = Convert.ToInt32(dataReader["ShiftID"]);
                    shift.ShiftName = dataReader["ShiftName"].ToString();
                }
            }
            cnn.Close();
            return shift;
        }
        public void Update(Shift shift)
        {
            try
            {
                cnn.Open();
                string UpdateQuery = string.Format("Update Office SET ShiftName={0} Where ShiftID = {1};", shift.ShiftName, shift.ShiftID);
                SqlCommand command = new SqlCommand(UpdateQuery, cnn);
                int SavedID = 0;
                command.ExecuteScalar();
                SavedID = shift.ShiftID;
                command.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally { cnn.Close(); }
        }
        public string GetShiftNamebyShiftID(int id)
        {
            cnn.Open();
            Shift shift = new Shift();
            string sqlQuery = string.Format("select ShiftName from Shift where ShiftID={0}", id);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {

                    shift.ShiftName = dataReader["ShiftName"].ToString();
                }
            }
            cnn.Close();
            return shift.ShiftName;
        }
    }
}