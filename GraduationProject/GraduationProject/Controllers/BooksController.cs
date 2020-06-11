using ExcelDataReader;
using GraduationProject.DTO;
using GraduationProject.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GraduationProject.Controllers
{
    public class BooksController : ApiController
    {
        ApplicationDbContext context = new ApplicationDbContext();
        [Route("Api/AddExcel")]
        public IHttpActionResult PostBooks()
        {
            string message = "";
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;

            {

                if (httpRequest.Files.Count > 0)
                {
                    HttpPostedFile file = httpRequest.Files[0];
                    Stream stream = file.InputStream;

                    IExcelDataReader reader = null;

                    if (file.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (file.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        message = "This file format is not supported";
                    }

                    DataSet excelRecords = reader.AsDataSet();
                    reader.Close();

                    var finalRecords = excelRecords.Tables[0];
                    for (int i = 1; i < finalRecords.Rows.Count&&i<3; i++)
                    {
                        Book book = new Book();
                        book.Title = finalRecords.Rows[i][0].ToString();
                        book.Author = finalRecords.Rows[i][1].ToString();
                        try
                        {
                            book.PublicationYear = int.Parse(finalRecords.Rows[i][2].ToString());
                        }
                        catch
                        {
                            book.PublicationYear = 2000;
                        }
                        book.Category = finalRecords.Rows[i][3].ToString();
                       // book.BookPublisher = context.Publishers.FirstOrDefault(n => n.User.UserName == finalRecords.Rows[i][4].ToString());
                        book.PublicationCountry = finalRecords.Rows[i][5].ToString();
                        try
                        {
                            book.Price = float.Parse(finalRecords.Rows[i][6].ToString());
                        }
                        catch
                        {
                            book.Price = 0;
                        }
                        book.Currency = finalRecords.Rows[i][7].ToString();
                        book.ISBN = finalRecords.Rows[i][8].ToString();
                        book.UploadDate = DateTime.Today;
                        book.ApprovalStatus = Status.Holding;
                        book.NumberSearched = 0;
                        book.Summary = "";
                        book.Notes = "";
                        book.CoverImage = "Book.jpg";
                        context.Books.Add(book);
                    }

                    int output = context.SaveChanges();
                    if (output > 0)
                    {
                        message = "Excel file has been successfully uploaded";
                    }
                    else
                    {
                        message = "Excel file uploaded has fiald";
                    }

                }

                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }

            return Ok(message);
        }
        public IHttpActionResult PostBook()
        {

            var httpRequest = HttpContext.Current.Request;
            Book book = new Book();
            book.Title = httpRequest["Title"];
            book.Author = httpRequest["Author"];
            try
            {
                book.PublicationYear = int.Parse(httpRequest["PublicationYear"]);
            }
            catch
            {
                book.PublicationYear = 2000;
            }
            book.Category = httpRequest["Category"];
            //token
            // book.BookPublisher = context.Publishers.FirstOrDefault(n => n.User.UserName == finalRecords.Rows[i][4].ToString());
            book.PublicationCountry = httpRequest["PublicationCountry"];
            try
            {
                book.Price = float.Parse(httpRequest["Price"]);
            }
            catch
            {
                book.Price = 0;
            }
            book.Currency = httpRequest["Currency"];
            book.ISBN = httpRequest["ISBN"];
            book.Summary = httpRequest["Summary"];
            book.Notes = httpRequest["Notes"];
            book.UploadDate = DateTime.Today;
            book.ApprovalStatus = Status.Holding;
            book.NumberSearched = 0;
           
            book.CoverImage = "Book.png";
            if (httpRequest.Files.Count > 0)
            {
                HttpPostedFile Image = httpRequest.Files["Img"];
                book.CoverImage = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                Image.SaveAs(HttpContext.Current.Server.MapPath("~/Images/" + book.CoverImage));
            }
            context.Books.Add(book);
            context.SaveChanges();
            return Ok();
        }
        public IHttpActionResult GetAll(int StartIndex=0,int NumberOfBooks=3)
        {
            // var books = context.Books.Where(c => c.ApprovalStatus == Status.Approved).ToList().Skip(StartIndex).Take(NumberOfBooks).ToList();
            //books = books.Skip(StartIndex).Take(NumberOfBooks);
            List<Book> books = context.Books.Where(c=>c.ApprovalStatus==Status.Approved).OrderBy(c=>c.ID).Skip(StartIndex).Take(NumberOfBooks).ToList();
            List<BooksDTO> booksDTO = new List<BooksDTO>();
            for (int i = 0; i < books.Count; i++)
            {
                BooksDTO book = new BooksDTO();
                book.ID = books[i].ID;
                book.PublisherName = books[i].Author;
                book.Author = books[i].BookPublisher.IfNotNull(c => c.User.LName + " " + c.User.LName);
                book.Title = books[i].Title;
                book.Category = books[i].Category;
                book.Price = books[i].Price;
                book.ISBN = books[i].ISBN;
                book.Year = books[i].PublicationYear.ToString();
                booksDTO.Add(book);
            }
            return Ok<List<BooksDTO>>(booksDTO);
        }
        [Route("Api/GetOne/{id}")]
        public IHttpActionResult GetOne(int id)
        {
            var book = context.Books.FirstOrDefault(b => b.ID == id);
            BookDTO bookDTO = new BookDTO();
            bookDTO.ID = book.ID;

            bookDTO.Title = book.Title;

            bookDTO.Author = book.Author;
            bookDTO.PublicationYear = book.PublicationYear;
            bookDTO.Category = book.Category;

            bookDTO.PublicationCountry = book.PublicationCountry;
            bookDTO.Price = book.Price;
            bookDTO.Currency = book.Currency;
            bookDTO.ISBN = book.Currency;
            bookDTO.UploadDate = book.UploadDate.ToString("yyyy");
            bookDTO.ApprovalStatus = book.ApprovalStatus;
            bookDTO.Summary = book.Summary;
            bookDTO.NumberSearched = book.NumberSearched;
            bookDTO.CoverImage = book.CoverImage;
            bookDTO.Notes = book.Notes;
           // bookDTO.PublisherName = book.BookPublisher.User.FName + " " + book.BookPublisher.User.LName;
            try
            {
                bookDTO.rate = book.UserRates.Sum(c => c.RateValue) / book.UserRates.Count();
            }
            catch
            {
                bookDTO.rate = 0;
            }
            return Ok<Book>(book);
        }
    }
}
