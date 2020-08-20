using DAO_QLAC;
using DTO_QLAC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Service_QLAC
{
    public class AccountService : DBConnect, IAccountService
    {
        public void CreateNew(Account account)
        {
            try
            {
                cnn.Open();
                string sqlQuery = string.Format("Insert into Account(Name, DateOfBirth, OfficeID, Username, Password) Values(N'{0}', '{1}', '{2}', '{3}', '{4}');", account.Name, account.DateOfBirth, account.OfficeID, account.Username, account.Password);
                SqlCommand command = new SqlCommand(sqlQuery, cnn);
                command.ExecuteScalar();
                command.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cnn.Close();
            }
        }
        public void CreateRole(int roleid, int userid)
        {
            try
            {
                cnn.Open();
                string qr = string.Format("Insert into Account_Rol(RoleID, UserID) Values('{0}', '{1}');", roleid, userid);
                SqlCommand command = new SqlCommand(qr, cnn);
                command.ExecuteScalar();
                command.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cnn.Close();
            }
        }
        public void Delete(int ID)
        {
            try
            {
                cnn.Open();
                string sqlQuery = string.Format("delete from Account where UserID={0}", ID);
                SqlCommand command = new SqlCommand(sqlQuery, cnn);
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cnn.Close();
            }
        }
        public List<Account> GetAll()
        {
            cnn.Open();
            List<Account> result = new List<Account>();
            SqlCommand command = new SqlCommand("SELECT * FROM Account", cnn);
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
            cnn.Close();
            return result;
        }
        public Account Getbykey(int id)
        {
            cnn.Open();
            Account account = new Account();
            string sqlQuery = string.Format("select * from Account where UserID={0}", id);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
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
            cnn.Close();
            return account;

        }
        public void Update(Account account)
        {
            try
            {
                cnn.Open();
                string UpdateQuery = string.Format("Update Account SET Name = N'{0}', DateOfBirth = '{1}', OfficeID ={2} Where UserID = {4};", account.Name, account.DateOfBirth, account.OfficeID, account.UserID);
                SqlCommand command = new SqlCommand(UpdateQuery, cnn);
                command.ExecuteScalar();
                command.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cnn.Close();
            }
        }
        public void UpdatePassword(Account account)
        {
            try
            {
                cnn.Open();
                string UpdateQuery = string.Format("Update Account SET Username ='{0}', Password = '{1}' Where UserID = {2};", account.Username, account.Password, account.UserID);
                SqlCommand command = new SqlCommand(UpdateQuery, cnn);
                command.ExecuteScalar();
                command.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cnn.Close();
            }
        }
        public Account GetByUsername(string username)
        {
            cnn.Open();
            Account account = new Account();
            string sqlQuery = string.Format("select * from Account where Username='{0}'", username);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
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
            cnn.Close();
            return account;
        }
        public int GetUserIDByUsername(string username)
        {
            cnn.Open();
            Account account = new Account();
            string sqlQuery = string.Format("select UserID from Account where Username = '{0}' ", username);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            account.UserID = Convert.ToInt32(dataReader["UserID"]);
            cnn.Close();
            return account.UserID;
        }
        public string GetOfficeNameByOfficeID(int OfficeID)
        {
            cnn.Open();
            Office office = new Office();
            string sqlQuery = string.Format("select OfficeName from Office where OfficeID = '{0}' ", OfficeID);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                office.OfficeName = dataReader["OfficeName"].ToString();
            }
            cnn.Close();
            return office.OfficeName;
        }
        public string GetNameByUserName(string username)
        {
            cnn.Open();
            Account account = new Account();
            string sqlQuery = string.Format("select Name from Account Where Username='{0}'", username);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            account.Name = dataReader["Name"].ToString();
            cnn.Close();
            return account.Name;
        }
        public Account GetAccountLogin(string username)
        {
            Account account = new Account();
            {
                string qr = string.Format("select top 1 UserID,Name,OfficeID,Username,Password,RoleID from Account Where Username='{0}'", username);
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(qr, cnn);
                        cnn.Open();
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
                        cnn.Close();
                    }
                    catch { }
                    finally { cnn.Close(); }
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
            cnn.Open();
            Account officeid = new Account();
            string sqlQuery = string.Format("select OfficeID from Account where Username = '{0}' ", username);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                officeid.OfficeID = Convert.ToInt32(dataReader["OfficeID"]);
            }
            cnn.Close();
            return officeid.OfficeID;
        }
        public int GetShiftIDByShiftName(string name)
        {
            cnn.Open();
            Shift shiftid = new Shift();
            string sqlQuery = string.Format("select ShiftID from Shift where ShiftName ='{0}'", name);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                shiftid.ShiftID = Convert.ToInt32(dataReader["ShiftID"]);
            }
            cnn.Close();
            return shiftid.ShiftID;
        }
        public string GetShiftNameByShiftID(int shiftid)
        {
            cnn.Open();
            Shift shift = new Shift();
            string sqlQuery = string.Format("select ShiftName from Shift where ShiftID = {0} ", shiftid);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                shift.ShiftName = dataReader["ShiftName"].ToString();
            }
            cnn.Close();
            return shift.ShiftName;
        }
        public List<int> GetShiftID()
        {
            cnn.Open();
            List<int> result = new List<int>();
            string sqlQuery = string.Format("select ShiftID from [QLAC].[dbo].[Shift]");
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                int listshiftID;
                listshiftID = Convert.ToInt32(dataReader["ShiftID"]);
                result.Add(listshiftID);
            }
            cnn.Close();
            return result;
        }
        public List<Account> GetNamebyOfficeID(int office)
        {
            cnn.Open();
            List<Account> result = new List<Account>();
            string sqlQuery = string.Format("select * from [QLAC].[dbo].[Account] where OfficeID={0}", office);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Account account = new Account();
                account.Name = dataReader["Name"].ToString();
                account.UserID = Convert.ToInt32(dataReader["UserID"]);
                result.Add(account);
            }
            cnn.Close();
            return result;
        }
        public int GetUserIDByName(string name)
        {
            cnn.Open();
            Account account = new Account();
            string sqlQuery = string.Format("select UserID from Account where Name = N'{0}' ", name);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            account.UserID = Convert.ToInt32(dataReader["UserID"]);
            cnn.Close();
            return account.UserID;
        }
        public int GetOfficeIDByUserID(int id)
        {
            cnn.Open();
            Account account = new Account();
            string sqlQuery = string.Format("select OfficeID from Account where UserID = {0} ", id);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            account.OfficeID = Convert.ToInt32(dataReader["OfficeID"]);
            cnn.Close();
            return account.OfficeID;
        }
        public List<Account> GetListAccount()
        {
            cnn.Open();
            List<Account> result = new List<Account>();
            string sqlQuery = string.Format("select Account.UserID, Account.Name, Account.DateOfBirth, Office.OfficeName from Account inner join Office on Account.OfficeID = Office.OfficeID");
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
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
            cnn.Close();
            return result;
        }
        public string GetAccountPassword(string username)
        {
            cnn.Open();
            Account account = new Account();
            string qr = string.Format("select Password from Account where Username = '{0}'", username);
            SqlCommand cm = new SqlCommand(qr, cnn);
            SqlDataReader dr = cm.ExecuteReader();
            dr.Read();
            account.Password = dr["Password"].ToString();
            cnn.Close();
            return account.Password;
        }
        public List<string> GetName()
        {
            cnn.Open();
            List<string> result = new List<string>();
            string sqlQuery = string.Format("select Name from [QLAC].[dbo].[Account]");
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                string account;
                account = dataReader["Name"].ToString();
                result.Add(account);
            }
            cnn.Close();
            return result;
        }
        public string GetNameByUserID(int userid)
        {
            cnn.Open();
            Account account = new Account();
            string sqlQuery = string.Format("select Name from Account Where UserID={0}", userid);
            SqlCommand command = new SqlCommand(sqlQuery, cnn);
            SqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            account.Name = dataReader["Name"].ToString();
            cnn.Close();
            return account.Name;
        }
    }
}