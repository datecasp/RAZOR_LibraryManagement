using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Models.Models
{
    public class AllBooksAndUsersModel
    {
        public IEnumerable<BookModel> Books { get; set; }
        public IEnumerable<UserModel> Users { get; set; }
    }
}
