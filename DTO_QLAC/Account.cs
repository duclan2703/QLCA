using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLAC
{
    public class Account
    {
        public virtual int UserID { get; set; }
        [Required]
        [DisplayName("Name")]
        public virtual string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public virtual DateTime DateOfBirth { get; set; }
        public virtual int OfficeID { get; set; }
        public virtual string OfficeName { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual int RoleID { get; set; }
    }
}
