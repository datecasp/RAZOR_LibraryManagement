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
        public virtual DbSet<AppSettingsModel> AppSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
                modelBuilder.Entity<AppSettingsModel>().HasData(new AppSettingsModel
                {
                    AppSettingsModelId = 1,
                    SettingParam = "DefaultFilled",
                    Value = 1
                });
                modelBuilder.Entity<AppSettingsModel>().HasData(new AppSettingsModel
                {
                    AppSettingsModelId = 2,
                    SettingParam = "DaysToWarningDate",
                    Value = 25
                });
                modelBuilder.Entity<AppSettingsModel>().HasData(new AppSettingsModel
                {
                    AppSettingsModelId = 3,
                    SettingParam = "DaysToReturnDate",
                    Value = 30
                });
            
        }
    }
}
