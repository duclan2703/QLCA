using FX.Data;
using QLAC.Domain;
using QLAC.IServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QLAC.Services
{
    public class ShiftService : IShiftService /*: BaseService<Shift, int>, IShiftService*/
    {
        private string ConnectionString = string.Empty;

        public ShiftService()
        {
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
      
        public void CreateNew(Shift shift)
        {
            string sqlQuery = string.Format("Insert into Shift(Code, Name, RegFrom, RegTo) Values('{0}', '{1}', '{2}', '{3}');" + "Select @@ID", shift.Code, shift.Name, shift.RegFrom, shift.RegTo);
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
            
            string sqlQuery = string.Format("delete from Shift where ID={0}", ID);
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
           
            SqlConnection cnn;
            
            cnn = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM Shift", cnn);            
            cnn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Shift shift = new Shift();
                shift.ID = Convert.ToInt32(reader["ID"]);
                shift.Code = reader["Code"].ToString();
                shift.Name = reader["Name"].ToString();
                shift.RegFrom = reader["RegFrom"].ToString();
                shift.RegTo = reader["RegTo"].ToString();
                result.Add(shift);
            }
            return result;
        }

        //public ShiftService(string sessionFactoryConfigPath, string connectionString = null)
        //    : base(sessionFactoryConfigPath , connectionString)
        //{ }
        public Shift Getbykey(int id)
        {
            Shift shift = new Shift();
            string sqlQuery = string.Format("select * from Shift where ID={0}", id);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    shift.ID = Convert.ToInt32(dataReader["ID"]);
                    shift.Code = dataReader["Code"].ToString();
                    shift.Name = dataReader["Name"].ToString();
                    shift.RegFrom = dataReader["RegFrom"].ToString();
                    shift.RegTo = dataReader["RegTo"].ToString();
                }
            }
            return shift;
        }

        public void Update(Shift shift)
        {
            string UpdateQuery = string.Format("Update Shift SET Code={0}, Name = '{1}', RegFrom = '{2}', RegTo = '{3}' Where ID = {4};", shift.Code, shift.Name, shift.RegFrom, shift.RegTo, shift.ID);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(UpdateQuery, connection);
            int SavedID = 0;
            try
            {
                command.ExecuteScalar();
                SavedID = shift.ID;
            }
            catch (Exception ex) 
            {
               
            }
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }
    }
}