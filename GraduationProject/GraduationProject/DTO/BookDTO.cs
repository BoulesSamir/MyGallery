using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.DTO
{
    public class BookDTO
    {
        
        public int ID { get; set; }
      
        public string Title { get; set; }
       
        public string Author { get; set; }
      
        public int PublicationYear { get; set; }
    
        public string Category { get; set; }
      
        public string PublicationCountry { get; set; }
        public float Price { get; set; }
        
        public string Currency { get; set; }
      
        public string ISBN { get; set; }
        public string UploadDate { get; set; }
       
        public Status ApprovalStatus { get; set; }
        
        public string Summary { get; set; }
        public int NumberSearched { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CoverImage { get; set; }
        public string Notes { get; set; }
   
        public  string PublisherName { get; set; }
        public float rate { get; set; }

    }
}