 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QLAC.Domain
{
    public class Staff
    {
        public virtual int ID { get; set; }
        [Required]
        [DisplayName("Name")]
        public virtual string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public virtual System.DateTime DateOfBirth { get; set; }
        public virtual string Position { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string Office { get; set; }

    }
}