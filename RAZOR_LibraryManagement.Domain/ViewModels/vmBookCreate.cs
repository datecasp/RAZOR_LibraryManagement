using System.ComponentModel.DataAnnotations;
using RAZOR_LibraryManagement.Domain.Models;

namespace RAZOR_LibraryManagement.Domain.ViewModels
{
    public class vmBookCreate
    {
       
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
        public string Category { get; set; }
    }
}
