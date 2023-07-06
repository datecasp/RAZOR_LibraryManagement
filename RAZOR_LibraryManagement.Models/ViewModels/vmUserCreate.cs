using System.ComponentModel.DataAnnotations;

namespace RAZOR_LibraryManagement.Models.ViewModels
{
    public class vmUserCreate
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
