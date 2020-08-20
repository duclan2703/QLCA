using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTO_QLAC
{
    public class Shift
    {
        public virtual int ShiftID { get; set; }
        public virtual string ShiftName { get; set; }
        public virtual DateTime EditTime { get; set; }
    }
}