using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Models.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } 
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // TODO: Add object UserAdress when created

        //Indicates if user is active for borrows
        public bool IsActive { get; set; }

        //Nav props
        public ICollection<BookUser>? BookUsers { get; set; }    
    }
}
