using DTO_QLAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_QLAC
{
    public interface IOrderService
    {
        void CreateNew(Order order);
        void Delete(int ID);
        List<Order> GetAll();
        Order Getbykey(int id);
        void Update(Order order);
        List<Order> GetShiftID();
        string GetShiftNameByShiftID(int ID);
        string GetOfficeNameByOfficeID(int OfficeID);
        string GetNameByOrderID(int OrderID);
        DateTime GetOrderDateByOrderID(int OrderID);
        List<Order> GetListOrder();
        List<Order> GetFilter(string Name, string Office, string FromDate, string ToDate);
    }
}
