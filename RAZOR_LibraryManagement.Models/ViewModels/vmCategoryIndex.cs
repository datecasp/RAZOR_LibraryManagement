using System.ComponentModel.DataAnnotations;

namespace RAZOR_LibraryManagement.Models.ViewModels
{
    public class vmCategoryIndex
    {
        [Required]
        public int CatId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
