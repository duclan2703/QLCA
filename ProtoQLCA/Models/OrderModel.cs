using DTO_QLAC;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProtoQLCA.Models
{
    public class OrderModel
    {
        public List<Order> Listorder { get; set; } = new List<Order>();
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public int OfficeID { get; set; }
        public string OfficeName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }
        public int ShiftID { get; set; }
        public bool Ca1 { get; set; }
        public bool Ca2 { get; set; }
        public bool Ca3 { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EditDate { get; set; }
        public string EditBy { get; set; }
        public int Quantity { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<FilterModel> Listfilter { get; set; } = new List<FilterModel>();
    }

    public class OrderShiftModel
    {
        public int ShiftID { get; set; }
        public string ShiftName { get; set; }
        public int Quantity { get; set; }

    }

    public class FilterModel
    {
        public string Name { get; set; }
        public string OfficeName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}