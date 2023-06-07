using System.ComponentModel.DataAnnotations;

namespace RAZOR_LibraryManagement.Domain.ViewModels
{
    public class vmCategoryIndex
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
