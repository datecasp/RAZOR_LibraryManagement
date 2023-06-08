using System.ComponentModel.DataAnnotations;

namespace RAZOR_LibraryManagement.Domain.ViewModels
{
    public class vmBookIndex
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public bool IsBorrowable { get; set; }
    }
}
