using DTO_QLAC;
using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProtoQLCA.Models
{
    public class MultiOrderModel
    {
        public List<Order> Listorder { get; set; } = new List<Order>();
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public int OfficeID { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public int ShiftID { get; set; }
        public bool Ca1 { get; set; }
        public bool Ca2 { get; set; }
        public bool Ca3 { get; set; }
        [DataType(DataType.Date)]
        public DateTime EditDate { get; set; }
        public string EditBy { get; set; }
        public int Quantity { get; set; }
        public int Quantity1 { get; set; }
        public int Quantity2 { get; set; }
        public int Quantity3 { get; set; }
        public List<MultiOrderModel> Liststaff { get; set; } = new List<MultiOrderModel>();
        public List<Staff> Liststaffs { get; set; } = new List<Staff>();

    }

    public class Staff
    {
        public string Name { get; set; }
        public int ID { get; set; }
    }

    public class OrderInput
    {
        public string Name { get; set; }
        public string OrderDate { get; set; }
        public string Quantity1 { get; set; }
        public string Quantity2 { get; set; }
        public string Quantity3 { get; set; }
    }
}