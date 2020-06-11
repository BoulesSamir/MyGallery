using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class Rate
    {
        
        public float RateValue { get; set; }
        [Key, Column(Order = 0)]
        public string VisitorID { get; set; }
        [ForeignKey("VisitorID")]
        public virtual Visitor User { get; set; }
        [Key, Column(Order = 1)]
        public int BookID { get; set; }
        [ForeignKey("BookID")]
        public virtual Book Book { get; set; }
    }
}