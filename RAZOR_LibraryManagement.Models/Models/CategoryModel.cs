using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAZOR_LibraryManagement.Models.Models
{
    public class CategoryModel
{
        public int CategoryId { get; set; }
        public string Name { get; set; }
        //Indicates if category is in use or not
        public bool IsActive { get; set; }

        //Nav props
        public ICollection<BookModel>? Books { get; set; }
}
}
