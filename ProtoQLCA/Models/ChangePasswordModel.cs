using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProtoQLCA.Models
{
    public class ChangePasswordModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}