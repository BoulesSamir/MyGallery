using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class Admin
    {
        [Key]
        public string AdminID { get; set; }
        [ForeignKey("AdminID")]
        public virtual ApplicationUser User { get; set; }
    }
}