using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Models.Entities
{
    public class BookUser
    {
        public int BookUserId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
        //Indicates if this user book relation is actual or past
        public bool IsActualUser { get; set; }
        //Date qhen user borrow the book
        public DateTime BorrowDate { get; set; }
    }
}
