﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RAZOR_LibraryManagement.Infra.DataContext
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var superAdminRoleId = "SuPeRaDMiN";
            var adminRoleId = "aDMiN";

            //Seed Roles (Admin and SuperAdmin)
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);

            //Seed SuperAdmin (only one)
            var superAdminId = "SuPeRaDMiNiD";
            var superAdminUser = new IdentityUser()
            {
                Id = superAdminId,
                UserName = "superadmin",
                Email = "superAdmin@mail.com",
                NormalizedUserName = "SUPERADMIN",
                NormalizedEmail = "SUPERADMIN@MAIL.COM"
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "superadmin123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add all roles to superadmin
            var superAdminRoles = new List<IdentityUserRole<string>>() 
            {
            new IdentityUserRole<string>
            {
                RoleId= superAdminRoleId,
                UserId = superAdminId
            },
            new IdentityUserRole<string>
            {
                RoleId= adminRoleId,
                UserId = superAdminId
            }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
