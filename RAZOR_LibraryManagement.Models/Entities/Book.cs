using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Models.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        //Holds the URL for the cover image of the book
        public string ImageUrl { get; set; }
        //Sets a human readable URL for the book
        public string UrlHandle { get; set; }
        //Indicates if book is active or inactive in library funds
        public bool IsBorrowable { get; set; }
        public int CategoryId { get; set; }

        //Nav props
        public ICollection<BookUser>? BookUsers { get; set; }
        public Category Category { get; set; }
    }
}
