﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProtoQLCA.Domain
{
    public class Per
    {
        public virtual int PermissionID { get; set; }
        public virtual string PermissionName { get; set; }
        public virtual string Description { get; set; }
    }
}