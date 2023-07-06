using System.ComponentModel.DataAnnotations;

namespace RAZOR_LibraryManagement.Models.Entities
{
    public class AppSettingsEntity
    {
        [Key]
        public int Id { get; set; }
        public string SettingParam { get; set; }
        public int Value { get; set; }
    }
}
