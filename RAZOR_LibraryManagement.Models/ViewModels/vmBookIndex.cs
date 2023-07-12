using System.ComponentModel.DataAnnotations;

namespace RAZOR_LibraryManagement.Models.ViewModels
{
    public class vmBookIndex
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsBorrowable { get; set; }
        //Not mapped with Automapper
        public bool IsBorrowed { get; set; }
    }
}
