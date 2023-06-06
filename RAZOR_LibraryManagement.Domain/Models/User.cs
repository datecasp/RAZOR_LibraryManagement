using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        //Indicates if user is active for loans
        public bool IsActive { get; set; }

        //Nav props
        public ICollection<BookUser>? BookUsers { get; set; }    
    }
}
