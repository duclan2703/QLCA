using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProtoQLCA.Domain
{
    public class Rol
    {
        public virtual int RoleID { get; set; }
        public virtual string Role_Name { get; set; }
        public virtual string Role_Description { get; set; }

    }
}