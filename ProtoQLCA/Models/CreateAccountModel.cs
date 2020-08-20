using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProtoQLCA.Models
{
    public class CreateAccountModel
    {
        public  int UserID { get; set; }
        public  string Name { get; set; }
        public  DateTime DateOfBirth { get; set; }
        public  int OfficeID { get; set; }
        public  string Username { get; set; }
        public  string Password { get; set; }
        public  int  RoleID { get; set; }

    }
}