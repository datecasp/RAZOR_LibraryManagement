using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Models.ViewModels
{
    public class vmUserDetails
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public List<string>? ActualBooks { get; set; }
        public List<string>? HistoricBooks { get; set; }
    }
}
