using ProtoQLCA.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProtoQLCA.Service
{
    public class OrderService
    {
        private string ConnectionString = string.Empty;

        public OrderService()
        {
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public void CreateNew(Order order)
        {
            string sqlQuery = string.Format("Insert into Phieuan(UserID, ShiftID, OfficeID, Name, OrderDate, EditDate, EditBy, Quantity) Values('{0}', '{1}', '{2}', N'{3}', '{4}', '{5}', '{6}', '{7}');", order.UserID, order.ShiftID, order.OfficeID, order.Name, order.OrderDate, order.EditDate, order.EditBy, order.Quantity);
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
            string sqlQuery = string.Format("delete from Phieuan where OrderID={0}", ID);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public List<Order> GetAll()
        {
            List<Order> result = new List<Order>();
            SqlConnection cnn;
            cnn = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM Phieuan", cnn);
            cnn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Order order = new Order();
                order.OrderID = Convert.ToInt32(reader["OrderID"]);
                order.UserID = Convert.ToInt32(reader["UserID"]);
                order.ShiftID = Convert.ToInt32(reader["ShiftID"]);
                order.OfficeID = Convert.ToInt32(reader["OfficeID"]);
                order.Name = reader["Name"].ToString();
                order.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                order.EditDate = Convert.ToDateTime(reader["EditDate"]);
                order.EditBy = reader["EditBy"].ToString();
                order.Quantity = Convert.ToInt32(reader["Quantity"]);
                result.Add(order);
            }
            return result;
        }


        public Order Getbykey(int id)
        {
            Order order = new Order();
            string sqlQuery = string.Format("select * from Phieuan where OrderID={0}", id);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    order.OrderID = Convert.ToInt32(dataReader["OrderID"]);
                    order.UserID = Convert.ToInt32(dataReader["UserID"]);
                    order.ShiftID = Convert.ToInt32(dataReader["ShiftID"]);
                    order.OfficeID = Convert.ToInt32(dataReader["OfficeID"]);
                    order.Name = dataReader["Name"].ToString();
                    order.OrderDate = Convert.ToDateTime(dataReader["OrderDate"]);
                    order.EditDate = Convert.ToDateTime(dataReader["EditDate"]);
                    order.EditBy = dataReader["EditBy"].ToString();
                    order.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                }
            }
            return order;
        }

        public void Update(Order order)
        {
            string UpdateQuery = string.Format("Update Phieuan SET EditDate={0}, EditBy = '{1}' Where OrderID = {2};", order.EditDate, order.EditBy, order.OrderID);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(UpdateQuery, connection);
            command.ExecuteScalar();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        public List<Order> GetShiftID()
        {
            List<Order> result = new List<Order>();
            string sqlQuery = string.Format("select ShiftID from [QLAC].[dbo].[Shift]");
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Order order = new Order();
                order.ShiftID = Convert.ToInt32(dataReader["ShiftID"]);
                result.Add(order);
            }
            return result;
        }

        public string GetShiftNameByShiftID(int ID)
        {
            Shift shift = new Shift();
            string sqlQuery = string.Format("select ShiftName from [QLAC].[dbo].[Shift] where ShiftID ={0}", ID);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            dataReader.Read();
            shift.ShiftName = dataReader["ShiftName"].ToString();
            return shift.ShiftName;
        }

        public string GetOfficeNameByOfficeID(int OfficeID)
        {
            Office office = new Office();
            string sqlQuery = string.Format("select OfficeName from Office where OfficeID = {0} ", OfficeID);
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

        public string GetNameByOrderID(int OrderID)
        {
            Order order = new Order();
            string sqlQuery = string.Format("select Name from Phieuan where OrderID = {0} ", OrderID);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                order.Name = dataReader["Name"].ToString();
            }
            return order.Name;
        }

        public DateTime GetOrderDateByOrderID(int OrderID)
        {
            Order order = new Order();
            string sqlQuery = string.Format("select OrderDate from Phieuan where OrderID = {0} ", OrderID);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                order.OrderDate = Convert.ToDateTime(dataReader["OrderDate"]);
            }
            return order.OrderDate;
        }

        public List<Order> GetListOrder()
        {
            List<Order> result = new List<Order>();
            string sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID");
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Order order = new Order();
                order.OrderID = Convert.ToInt32(dataReader["OrderID"]);
                order.UserID = Convert.ToInt32(dataReader["UserID"]);
                order.ShiftName = dataReader["ShiftName"].ToString();
                order.OfficeName = dataReader["OfficeName"].ToString();
                order.Name = dataReader["Name"].ToString();
                order.OrderDate = Convert.ToDateTime(dataReader["OrderDate"]);
                order.EditDate = Convert.ToDateTime(dataReader["EditDate"]);
                order.EditBy = dataReader["EditBy"].ToString();
                order.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                result.Add(order);
            }
            return result;
        }

        public List<Order> GetListOrderbyName(string Name)
        {
            List<Order> result = new List<Order>();
            string sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID where Name='{0}'", Name);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Order order = new Order();
                order.OrderID = Convert.ToInt32(dataReader["OrderID"]);
                order.UserID = Convert.ToInt32(dataReader["UserID"]);
                order.ShiftName = dataReader["ShiftName"].ToString();
                order.OfficeName = dataReader["OfficeName"].ToString();
                order.Name = dataReader["Name"].ToString();
                order.OrderDate = Convert.ToDateTime(dataReader["OrderDate"]);
                order.EditDate = Convert.ToDateTime(dataReader["EditDate"]);
                order.EditBy = dataReader["EditBy"].ToString();
                order.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                result.Add(order);
            }
            return result;
        }

        public List<Order> GetListOrderbyDateRange(string FromDate, string ToDate)
        {
            List<Order> result = new List<Order>();
            string sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID where OrderDate >= '{0}' and OrderDate <= '{1} 23:59:59.597'", FromDate, ToDate);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Order order = new Order();
                order.OrderID = Convert.ToInt32(dataReader["OrderID"]);
                order.UserID = Convert.ToInt32(dataReader["UserID"]);
                order.ShiftName = dataReader["ShiftName"].ToString();
                order.OfficeName = dataReader["OfficeName"].ToString();
                order.Name = dataReader["Name"].ToString();
                order.OrderDate = Convert.ToDateTime(dataReader["OrderDate"]);
                order.EditDate = Convert.ToDateTime(dataReader["EditDate"]);
                order.EditBy = dataReader["EditBy"].ToString();
                order.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                result.Add(order);
            }
            return result;
        }

        public List<Order> GetListOrderbyOfficeName(string Office)
        {
            List<Order> result = new List<Order>();
            string sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID where Office='{0}'", Office);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Order order = new Order();
                order.OrderID = Convert.ToInt32(dataReader["OrderID"]);
                order.UserID = Convert.ToInt32(dataReader["UserID"]);
                order.ShiftName = dataReader["ShiftName"].ToString();
                order.OfficeName = dataReader["OfficeName"].ToString();
                order.Name = dataReader["Name"].ToString();
                order.OrderDate = Convert.ToDateTime(dataReader["OrderDate"]);
                order.EditDate = Convert.ToDateTime(dataReader["EditDate"]);
                order.EditBy = dataReader["EditBy"].ToString();
                order.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                result.Add(order);
            }
            return result;
        }

        public List<Order> GetListOrderbyNameandDate(string Name, string FromDate, string ToDate)
        {
            List<Order> result = new List<Order>();
            string sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID where Name='{0}' and OrderDate >= '{1}' and OrderDate <= '{2} 23:59:59.597'", Name, FromDate, ToDate);
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Order order = new Order();
                order.OrderID = Convert.ToInt32(dataReader["OrderID"]);
                order.UserID = Convert.ToInt32(dataReader["UserID"]);
                order.ShiftName = dataReader["ShiftName"].ToString();
                order.OfficeName = dataReader["OfficeName"].ToString();
                order.Name = dataReader["Name"].ToString();
                order.OrderDate = Convert.ToDateTime(dataReader["OrderDate"]);
                order.EditDate = Convert.ToDateTime(dataReader["EditDate"]);
                order.EditBy = dataReader["EditBy"].ToString();
                order.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                result.Add(order);
            }
            return result;
        }

        public List<Order> GetFilter(string Name, string Office, string FromDate, string ToDate)
        {
            List<Order> result = new List<Order>();
            string sqlQuery = string.Empty;
            if (Name != null)
            {
                sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID where Name=N'{0}' order by convert(date,OrderDate,103) desc", Name);
            }
            if (Office != null)
            {
                sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID where OfficeName=N'{0}' order by convert(date,OrderDate,103) desc", Office);
            }
            if (Name != null && Office != null)
            {
                sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID where Name=N'{0}'and OfficeName=N'{1}' order by convert(date,OrderDate,103) desc", Name, Office);
            }
            if (FromDate != null && ToDate != null)
            {
                var fromdate = DateTime.ParseExact(FromDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd");
                var todate = DateTime.ParseExact(ToDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd");
                sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID where OrderDate >= '{0}' and OrderDate <= '{1} 23:59:59.597' order by convert(date,OrderDate,103) desc", fromdate, todate);
            }
            if (FromDate == null && ToDate != null)
            {
                var todate = DateTime.ParseExact(ToDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd");
                sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID where OrderDate <= '{0} 23:59:59.597' order by convert(date,OrderDate,103) desc", todate);
            }
            if (FromDate != null && ToDate == null)
            {
                var fromdate = DateTime.ParseExact(FromDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd");
                sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID where OrderDate >= '{0} 23:59:59.597' order by convert(date,OrderDate,103) desc", fromdate);
            }
            if (Name != null && FromDate != null && ToDate != null)
            {
                var fromdate = DateTime.ParseExact(FromDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd");
                var todate = DateTime.ParseExact(ToDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd");
                sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID where Name=N'{0}' and OrderDate >= '{1}' and OrderDate <= '{2} 23:59:59.597' order by convert(date,OrderDate,103) desc", Name, fromdate, todate);
            }
            if (Name != null && FromDate == null && ToDate != null)
            {
                var todate = DateTime.ParseExact(ToDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd");
                sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID where Name=N'{0}' and OrderDate <= '{1} 23:59:59.597' order by convert(date,OrderDate,103) desc", Name, todate);
            }
            if (Name != null && FromDate != null && ToDate == null)
            {
                var fromdate = DateTime.ParseExact(FromDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd");
                sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID where Name=N'{0}' and OrderDate >= '{1} 23:59:59.597' order by convert(date,OrderDate,103) desc", Name, fromdate);
            }
            if (Office != null && FromDate != null && ToDate != null)
            {
                var fromdate = DateTime.ParseExact(FromDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd");
                var todate = DateTime.ParseExact(ToDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd");
                sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID where OfficeName=N'{0}' and OrderDate >= '{1}' and OrderDate <= '{2} 23:59:59.597' order by convert(date,OrderDate,103) desc", Office, fromdate, todate);
            }
            if (Office != null && FromDate == null && ToDate != null)
            {
                var todate = DateTime.ParseExact(ToDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd");
                sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID where OfficeName=N'{0}' and OrderDate <= '{1} 23:59:59.597' order by convert(date,OrderDate,103) desc", Office, todate);
            }
            if (Office != null && FromDate != null && ToDate == null)
            {
                var fromdate = DateTime.ParseExact(FromDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd");
                sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID where OfficeName=N'{0}' and OrderDate >= '{1} 23:59:59.597' order by convert(date,OrderDate,103) desc", Office, fromdate);
            }
            if (Name != null && Office != null && FromDate != null && ToDate != null)
            {
                var fromdate = DateTime.ParseExact(FromDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd");
                var todate = DateTime.ParseExact(ToDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd");
                sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID where Name=N'{0}'and OfficeName=N'{1}' and OrderDate >= '{2}' and OrderDate <= '{3} 23:59:59.597' order by convert(date,OrderDate,103) desc", Name, Office, fromdate, todate);
            }
            if (Name == null && Office == null && FromDate == null && ToDate == null)
            {
                sqlQuery = string.Format("select PhieuAn.OrderID, PhieuAn.UserID, Shift.ShiftName, Office.OfficeName, PhieuAn.Name, PhieuAn.OrderDate, PhieuAn.EditDate, PhieuAn.EditBy, PhieuAn.Quantity from Phieuan inner join Office on Phieuan.OfficeID = Office.OfficeID inner join Shift on PhieuAn.ShiftID = Shift.ShiftID order by convert(date,OrderDate,103) desc");
            }
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Order order = new Order();
                order.OrderID = Convert.ToInt32(dataReader["OrderID"]);
                order.UserID = Convert.ToInt32(dataReader["UserID"]);
                order.ShiftName = dataReader["ShiftName"].ToString();
                order.OfficeName = dataReader["OfficeName"].ToString();
                order.Name = dataReader["Name"].ToString();
                order.OrderDate = Convert.ToDateTime(dataReader["OrderDate"]);
                order.EditDate = Convert.ToDateTime(dataReader["EditDate"]);
                order.EditBy = dataReader["EditBy"].ToString();
                order.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                result.Add(order);
            }
            return result;
        }
    }
}