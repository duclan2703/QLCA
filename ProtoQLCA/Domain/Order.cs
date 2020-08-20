using ProtoQLCA.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProtoQLCA.Domain
{
    public class Order
    {
        public virtual int OrderID { get; set; }
        public virtual int UserID { get; set; }
        public virtual int ShiftID { get; set; }
        public virtual string ShiftName { get; set; }
        public virtual int OfficeID { get; set; }
        public virtual string OfficeName { get; set; }
        public virtual string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public virtual DateTime OrderDate { get; set; }
        public virtual DateTime EditDate { get; set; }
        public virtual string EditBy { get; set; }
        public virtual int Quantity { get; set; }
       
    }
}