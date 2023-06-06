using Microsoft.EntityFrameworkCore;
using RAZOR_LibraryManagement.Domain.Models;

namespace RAZOR_LibraryManagement.Infra.DataContext
{
    public class LM_DbContext : DbContext
    {
        public LM_DbContext(DbContextOptions<LM_DbContext> options) : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<User> Users { get;set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<BookUser> BookUsers { get; set; }

    }
}
