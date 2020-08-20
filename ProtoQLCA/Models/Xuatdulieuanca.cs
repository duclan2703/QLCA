using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProtoQLCA.Models
{
    public class Xuatdulieuanca
    {
        public int OrderID { get; set; }
        public string Name { get; set; }
        public string Tenca { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        
    }
}