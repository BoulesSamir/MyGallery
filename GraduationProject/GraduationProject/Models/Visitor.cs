using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class Visitor
    {
        
        [Key]
        public string VisitorID { get; set; }
        public string Country { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public virtual ICollection<Rate> BookRates { get; set; }
        [ForeignKey("VisitorID")]
        public virtual ApplicationUser User { get; set; }

    }
}