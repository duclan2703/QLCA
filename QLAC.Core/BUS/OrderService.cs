using DTO_QLAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_QLAC.BUS
{
    public class OrderService : IOrderService
    {
        OrderDAO dao = new OrderDAO();
        public void CreateNew(Order order)
        {
            dao.CreateNewDAO(order);
        }

        public void Delete(int ID)
        {
            dao.DeleteDAO(ID);
        }

        public List<Order> GetAll()
        {
            return dao.GetAllDAO();
        }

        public Order Getbykey(int id)
        {
            return dao.GetbykeyDAO(id);
        }

        public List<Order> GetFilter(string Name, string Office, string FromDate, string ToDate)
        {
            return dao.GetFilterDAO(Name, Office, FromDate, ToDate);
        }

        public List<Order> GetListOrder()
        {
            return dao.GetListOrderDAO();
        }

        public string GetNameByOrderID(int OrderID)
        {
            return dao.GetNameByOrderIDDAO(OrderID);
        }

        public string GetOfficeNameByOfficeID(int OfficeID)
        {
            return dao.GetOfficeNameByOfficeIDDAO(OfficeID);
        }

        public DateTime GetOrderDateByOrderID(int OrderID)
        {
            return dao.GetOrderDateByOrderIDDAO(OrderID);
        }

        public List<Order> GetShiftID()
        {
            return dao.GetShiftIDDAO();
        }

        public string GetShiftNameByShiftID(int ID)
        {
            return dao.GetShiftNameByShiftIDDAO(ID);
        }

        public void Update(Order order)
        {
            dao.UpdateDAO(order);
        }
    }
}
