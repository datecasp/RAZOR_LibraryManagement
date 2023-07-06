using System.ComponentModel.DataAnnotations;

namespace RAZOR_LibraryManagement.Models.ViewModels
{
    public class vmBookCreate
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public bool IsBorrowable { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string UrlHandle { get; set; }
    }
}
