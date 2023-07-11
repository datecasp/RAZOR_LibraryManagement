using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RAZOR_LibraryManagement.Domain.Interfaces;
using RAZOR_LibraryManagement.Models.ViewModels;

namespace RAZOR_LibraryManagement.Domain.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public AdminService(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<vmAdminUserList>> GetAdminsListService()
        {
            var adminsList = (await _userManager.GetUsersInRoleAsync("admin")).ToList();
            var vmAdminsList = new List<vmAdminUserList>();
            foreach(var admin in adminsList)
            {
                vmAdminsList.Add(new vmAdminUserList
                {
                    Id = admin.Id,
                    UserName = admin.UserName,
                    Email = admin.Email,
                    Role = _userManager.GetRolesAsync(admin).Result.Max()
                });
            }
            return vmAdminsList;
        }

        public async Task<vmNotification> CreateAdminService(vmAdminUserCreate admin)
        {
            //Seed admin
            var adminIU = new IdentityUser
            {
                UserName = admin.UserName,
                NormalizedUserName = admin.UserName.ToUpper(),
                Email = admin.Email,                NormalizedEmail = admin.Email.ToLower()
            };

            var identityResult = await _userManager.CreateAsync(adminIU, admin.Password);

            if (identityResult.Succeeded)
            {
                //Add Admin Role to just created admin
                var addRolesResult = await _userManager.AddToRoleAsync(adminIU, "Admin");

                if (addRolesResult.Succeeded)
                {
                    var notification = new vmNotification
                    {
                        Type = Lang.Notification.NotificationType.Success,
                        Message = "Admin created successfully"
                    };
                    return notification;
                }
            }
            var errorNotification = new vmNotification
            {
                Type = Lang.Notification.NotificationType.Error,
                Message = "Something went wrong"
            };
                
            return errorNotification;
        }
    }
}
