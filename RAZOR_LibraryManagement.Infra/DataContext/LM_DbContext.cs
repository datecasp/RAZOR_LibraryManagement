using Microsoft.EntityFrameworkCore;
using RAZOR_LibraryManagement.Models.Entities;

namespace RAZOR_LibraryManagement.Infra.DataContext
{
    public class LM_DbContext : DbContext
    {
        public LM_DbContext(DbContextOptions<LM_DbContext> options) : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<BookUser> BookUsers { get; set; }
        public virtual DbSet<AppSettingsEntity> AppSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AppSettingsEntity>().HasData(new AppSettingsEntity
            {
                Id = 1,
                SettingParam = "DefaultFilled",
                Value = 1
            });
            modelBuilder.Entity<AppSettingsEntity>().HasData(new AppSettingsEntity
            {
                Id = 2,
                SettingParam = "DaysToWarningDate",
                Value = 25
            });
            modelBuilder.Entity<AppSettingsEntity>().HasData(new AppSettingsEntity
            {
                Id = 3,
                SettingParam = "DaysToReturnDate",
                Value = 30
            });
            modelBuilder.Entity<AppSettingsEntity>().HasData(new AppSettingsEntity
            {
                Id = 4,
                SettingParam = "MaxNumOfBooks",
                Value = 2
            });

        }
    }
}
