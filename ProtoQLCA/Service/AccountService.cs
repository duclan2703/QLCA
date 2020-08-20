using ProtoQLCA.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ProtoQLCA.Service
{
    public class AccountService
    {
        private string ConnectionString = string.Empty;

        public AccountService()
        {
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public void CreateNew(Account account)
        {
            string sqlQuery = string.Format("Insert into Account(Name, DateOfBirth, OfficeID, Username, Password) Values(N'{0}', '{1}', '{2}', '{3}', '{4}');", account.Name, account.DateOfBirth, account.OfficeID, account.Username, account.Password);
            string qr = string.Format("Insert into Account_Rol(RoleID, UserID) Values('{0}', '{1}');", account.RoleID, account.UserID);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.ExecuteScalar();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public void CreateRole(int roleid, int userid)
        {
            string qr = string.Format("Insert into Account_Rol(RoleID, UserID) Values('{0}', '{1}');", roleid, userid);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(qr, connection);
            command.ExecuteScalar();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }
        public void Delete(int ID)
        {
            string sqlQuery = string.Format("delete from Account where UserID={0}", ID);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public List<Account> GetAll()
        {
            List<Account> result = new List<Account>();
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM Account", cnn);
            cnn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Account account = new Account();
                account.UserID = Convert.ToInt32(reader["UserID"]);
                account.Name = reader["Name"].ToString();
                account.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                account.OfficeID = Convert.ToInt32(reader["OfficeID"]);
                account.Username = reader["Username"].ToString();
                account.Password = reader["Password"].ToString();
                account.RoleID = Convert.ToInt32(reader["UserID"]);
                result.Add(account);
            }
            return result;
        }


        public Account Getbykey(int id)
        {
            Account account = new Account();
            string sqlQuery = string.Format("select * from Account where UserID={0}", id);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    account.UserID = Convert.ToInt32(dataReader["UserID"]);
                    account.Name = dataReader["Name"].ToString();
                    account.DateOfBirth = Convert.ToDateTime(dataReader["DateOfBirth"]);
                    account.OfficeID = Convert.ToInt32(dataReader["OfficeID"]);
                    account.Username = dataReader["Username"].ToString();
                    account.Password = dataReader["Password"].ToString();
                    account.RoleID = Convert.ToInt32(dataReader["UserID"]);
                }
            }
            return account;
        }

        public void Update(Account account)
        {
            string UpdateQuery = string.Format("Update Account SET Name = N'{0}', DateOfBirth = '{1}', OfficeID ={2} Where UserID = {4};", account.Name, account.DateOfBirth, account.OfficeID, account.UserID);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(UpdateQuery, connection);
            command.ExecuteScalar();
            command.Dispose();
            connection.Close(); 
            connection.Dispose();
            
        }

        public void UpdatePassword(Account account)
        {
            string UpdateQuery = string.Format("Update Account SET Username ='{0}', Password = '{1}' Where UserID = {2};", account.Username, account.Password, account.UserID);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(UpdateQuery, connection);

            command.ExecuteScalar();
            command.Dispose();
            connection.Close();
            connection.Dispose();

        }
        public Account GetByUsername(string username)
        {
            Account account = new Account();
            string sqlQuery = string.Format("select * from Account where Username='{0}'", username);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    account.UserID = Convert.ToInt32(dataReader["UserID"]);
                    account.Name = dataReader["Name"].ToString();
                    account.DateOfBirth = Convert.ToDateTime(dataReader["DateOfBirth"]);
                    account.OfficeID = Convert.ToInt32(dataReader["OfficeID"]);
                    account.Username = dataReader["Username"].ToString();
                    account.Password = dataReader["Password"].ToString();
                    account.RoleID = Convert.ToInt32(dataReader["UserID"]);
                }
            }
            return account;
        }
        public int GetUserIDByUsername(string username)
        {
            Account account = new Account();
            string sqlQuery = string.Format("select UserID from Account where Username = '{0}' ", username);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            account.UserID = Convert.ToInt32(dataReader["UserID"]);
            return account.UserID;
        }
        public string GetOfficeNameByOfficeID(int OfficeID)
        {
            Office office = new Office();
            string sqlQuery = string.Format("select OfficeName from Office where OfficeID = '{0}' ", OfficeID);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                office.OfficeName = dataReader["OfficeName"].ToString();
            }
            return office.OfficeName;
        }

        public string GetNameByUserName(string username)
        {
            Account account = new Account();
            string sqlQuery = string.Format("select Name from Account Where Username='{0}'", username);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            account.Name = dataReader["Name"].ToString();
            return account.Name;
        }
        public Account GetAccountLogin(string username)
        {
            Account account = new Account();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                string qr = string.Format("select top 1 UserID,Name,OfficeID,Username,Password,RoleID from Account Where Username='{0}'", username);

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(qr, connection);
                        connection.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            account.UserID = int.Parse(row["UserID"].ToString());
                            account.Name = row["Name"].ToString();
                            account.OfficeID = int.Parse(row["OfficeID"].ToString());
                            account.Username = row["Username"].ToString();
                            account.Password = row["Password"].ToString();
                            account.RoleID = int.Parse(row["RoleID"].ToString());
                        }
                        connection.Close();
                    }
                    catch { }
                    finally { connection.Close(); }
                }
            }
            return account;
        }
        //private DataSet getAccount(string username)
        //{
        //    try
        //    {
        //        DataSet lst = new DataSet();
        //        string qr = string.Format("select top 1 UserID,Name,OfficeID,Username,Password,RoleID from Account Where Username='{0}'", username);
        //        using (SqlConnection connection = new SqlConnection(ConnectionString))
        //        {
        //            SqlCommand command = new SqlCommand(qr, connection);
        //            connection.Open();
        //            SqlDataAdapter da = new SqlDataAdapter(command);
        //            DataSet ds = new DataSet();
        //            da.Fill(ds);
        //            connection.Close();
        //            lst = ds;
        //        }
        //        return lst;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new DataSet();
        //    }
        //}
        public int GetOfficeIDNameByUsername(string username)
        {
            Account officeid = new Account();
            string sqlQuery = string.Format("select OfficeID from Account where Username = '{0}' ", username);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                officeid.OfficeID = Convert.ToInt32(dataReader["OfficeID"]);
            }
            return officeid.OfficeID;
        }

        public int GetShiftIDByShiftName(string name)
        {
            Shift shiftid = new Shift();
            string sqlQuery = string.Format("select ShiftID from Shift where ShiftName ='{0}'", name);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                shiftid.ShiftID = Convert.ToInt32(dataReader["ShiftID"]);
            }
            return shiftid.ShiftID;
        }

        public string GetShiftNameByShiftID(int shiftid)
        {
            Shift shift = new Shift();
            string sqlQuery = string.Format("select ShiftName from Shift where ShiftID = {0} ", shiftid);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                shift.ShiftName = dataReader["ShiftName"].ToString();
            }
            return shift.ShiftName;
        }

        public List<int> GetShiftID()
        {
            List<int> result = new List<int>();
            string sqlQuery = string.Format("select ShiftID from [QLAC].[dbo].[Shift]");
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                int listshiftID;
                listshiftID = Convert.ToInt32(dataReader["ShiftID"]);
                result.Add(listshiftID);
            }
            return result;
        }

        public List<Account> GetNamebyOfficeID(int office)
        {
            List<Account> result = new List<Account>();
            string sqlQuery = string.Format("select * from [QLAC].[dbo].[Account] where OfficeID={0}", office);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Account account = new Account();
                account.Name = dataReader["Name"].ToString();
                account.UserID = Convert.ToInt32(dataReader["UserID"]);
                result.Add(account);
            }
            return result;
        }

        public int GetUserIDByName(string name)
        {
            Account account = new Account();
            string sqlQuery = string.Format("select UserID from Account where Name = N'{0}' ", name);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            account.UserID = Convert.ToInt32(dataReader["UserID"]);
            return account.UserID;
        }

        public int GetOfficeIDByUserID(int id)
        {
            Account account = new Account();
            string sqlQuery = string.Format("select OfficeID from Account where UserID = {0} ", id);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            account.OfficeID = Convert.ToInt32(dataReader["OfficeID"]);
            return account.OfficeID;
        }

        public List<Account> GetListAccount()
        {
            List<Account> result = new List<Account>();
            string sqlQuery = string.Format("select Account.UserID, Account.Name, Account.DateOfBirth, Office.OfficeName from Account inner join Office on Account.OfficeID = Office.OfficeID");
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Account account = new Account();
                account.UserID = Convert.ToInt32(dataReader["UserID"]);
                account.Name = dataReader["Name"].ToString();
                account.DateOfBirth = Convert.ToDateTime(dataReader["DateOfBirth"]);
                account.OfficeName = dataReader["OfficeName"].ToString();
                result.Add(account);
            }
            return result;
        }

        public string GetAccountPassword(string username)
        {
            Account account = new Account();
            string qr = string.Format("select Password from Account where Username = '{0}'", username);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand cm = new SqlCommand(qr, connection);
            SqlDataReader dr = cm.ExecuteReader();
            dr.Read();
            account.Password = dr["Password"].ToString();
            return account.Password;
        }

        public List<string> GetName()
        {
            List<string> result = new List<string>();
            string sqlQuery = string.Format("select Name from [QLAC].[dbo].[Account]");
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                string account;
                account = dataReader["Name"].ToString();
                result.Add(account);
            }
            return result;
        }

        public string GetNameByUserID(int userid)
        {
            Account account = new Account();
            string sqlQuery = string.Format("select Name from Account Where UserID={0}", userid);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            account.Name = dataReader["Name"].ToString();
            return account.Name;
        }
    }
}