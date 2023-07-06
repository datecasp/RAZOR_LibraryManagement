using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Models.Entities
{
    public class Category
{
        public int CategoryId { get; set; }
        public string Name { get; set; }
        //Indicates if category is in use or not
        public bool IsActive { get; set; }

        //Nav props
        public ICollection<Book>? Books { get; set; }
}
}
