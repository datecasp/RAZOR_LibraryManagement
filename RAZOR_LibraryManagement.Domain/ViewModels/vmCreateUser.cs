using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RAZOR_LibraryManagement.Domain.ViewModels
{
    public class vmCreateUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IdentityRole Role { get; set; }
    }
}
