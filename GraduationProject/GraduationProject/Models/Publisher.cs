using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class Publisher
    {
        [Key]
        public string PublisherID { get; set; }
        public string Country { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        [DefaultValue(Status.Holding)]
        public Status ApprovalStatus  { get; set; }
        [DefaultValue("")]
        public string Website { get; set; }
        [ForeignKey("PublisherID")]
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Book> PublishedBooks { get; set; }
    }
   
}