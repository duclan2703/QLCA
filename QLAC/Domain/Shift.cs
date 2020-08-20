using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLAC.Domain
{
    public class Shift
    {
        public virtual int ID { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string RegFrom { get; set; }
        public virtual string RegTo { get; set; }
    }
}