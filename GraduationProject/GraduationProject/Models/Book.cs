using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Management.Instrumentation;
using System.Web;

namespace GraduationProject.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }
        [DefaultValue("")]
        public string Title { get; set; }
        [DefaultValue("")]
        public string Author { get; set; }
        [DefaultValue(0)]
        public int  PublicationYear { get; set; }
        [DefaultValue("")]
        public string Category { get; set; }
        [DefaultValue("")]
        public string PublicationCountry { get; set; }
        [DefaultValue(0)]
        public float Price { get; set; }
        [DefaultValue("")]
        public string Currency { get; set; }
        [DefaultValue("")]
        public string ISBN { get; set; }
        public DateTime UploadDate { get; set; }
        [DefaultValue(Status.Holding)]
        public Status ApprovalStatus { get; set; }
        [DefaultValue("")]
        public string Summary { get; set; }
        [DefaultValue(0)]
        public int NumberSearched { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("Book.png")]
        public string CoverImage { get; set; }
        [DefaultValue("")]
        public string Notes { get; set; }
        public string PublisherID { get; set; }

        [ForeignKey("PublisherID")]
        public virtual Publisher BookPublisher { get; set; }
        public virtual ICollection<Rate> UserRates { get; set; }
        public virtual ICollection<ApplicationUser> Favourites { get; set; }
    }
}