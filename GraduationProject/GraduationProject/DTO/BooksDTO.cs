using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.DTO
{
    public class BooksDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string PublisherName { get; set; }
        public string Category { get; set; }
        public string Year { get; set; }
        public string ISBN { get; set; }
        public float Price { get; set; }
    }
}